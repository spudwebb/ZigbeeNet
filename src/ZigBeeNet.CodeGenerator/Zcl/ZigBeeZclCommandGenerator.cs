using System;
using System.Collections.Generic;
using System.IO;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator.Zcl
{
    public class ZigBeeZclCommandGenerator : ZigBeeBaseFieldGenerator
    {

        public ZigBeeZclCommandGenerator(List<ZigBeeXmlCluster> clusters, Dictionary<string, string> dependencies)
        {
            //this._generatedDate = generatedDate;
            this._dependencies = dependencies;

            foreach (ZigBeeXmlCluster cluster in clusters)
            {
                try
                {
                    GenerateZclClusterCommands(cluster, packageRoot, new FileStream(_sourceRootPath, FileMode.Open));
                }
                catch (IOException e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e);
                }
            }
        }

        private void GenerateZclClusterCommands(ZigBeeXmlCluster cluster, string packageRootPrefix, FileStream sourceRootPath)
        {

            foreach (ZigBeeXmlCommand command in cluster.Commands)
            {
                string packageRoot = GetZclClusterCommandPackage(cluster);
                string packagePath = GetPackagePath(sourceRootPath, packageRoot);
                FileStream packageFile = GetPackageFile(packagePath);

                string className = StringToUpperCamelCase(command.Name);
                TextWriter @out = GetClassOut(packageFile, className);

                // List of fields that are handled internally by super class
                List<string> reservedFields = new List<string>();

                ImportsClear();

                foreach (ZigBeeXmlField field in command.Fields)
                {
                    if (GetDataTypeClass(field).StartsWith("List"))
                    {
                        ImportsAdd("java.util.List");
                    }

                    if (field.Sizer != null)
                    {
                        ImportsAdd("java.util.ArrayList");
                    }
                }
                OutputLicense(@out);

                @out.WriteLine("package " + packageRoot + ";");
                @out.WriteLine();
                ImportsAdd("javax.annotation.Generated");

                if (command.Response != null)
                {
                    ImportsAdd(packageRootPrefix + ".transaction.ZigBeeTransactionMatcher");
                    ImportsAdd(packageRootPrefix + ".ZigBeeCommand");

                    ImportsAdd(packageRoot + "." + command.Response.Command);
                }

                string commandExtends = "";
                if (packageRoot.Contains(".zcl."))
                {
                    ImportsAdd(packageRootPrefix + packageZcl + ".ZclCommand");
                    ImportsAdd(packageRootPrefix + packageZclProtocol + ".ZclCommandDirection");
                    commandExtends = "ZclCommand";
                }
                else
                {
                    if (command.Name.Contains("Response"))
                    {
                        commandExtends = "ZdoResponse";
                        reservedFields.Add("status");
                    }
                    else
                    {
                        commandExtends = "ZdoRequest";
                    }
                    ImportsAdd(packageRootPrefix + packageZdp + "." + commandExtends);
                }

                if (command.Fields.Count > 0)
                {
                    ImportsAdd(packageRootPrefix + packageZcl + ".ZclFieldSerializer");
                    ImportsAdd(packageRootPrefix + packageZcl + ".ZclFieldDeserializer");
                    ImportsAdd(packageRootPrefix + packageZclProtocol + ".ZclDataType");
                }

                foreach (ZigBeeXmlField field in command.Fields)
                {
                    ImportsAddClass(field);
                }

                OutputImports(@out);

                @out.WriteLine();
                @out.WriteLine("/**");
                @out.WriteLine(" * " + command.Name + " value object class.");

                @out.WriteLine(" * <p>");
                if (packageRoot.Contains(".zcl."))
                {
                    @out.WriteLine(" * Cluster: <b>" + cluster.Name + "</b>. Command ID 0x"
                            + command.Code.ToString("X2") + " is sent <b>"
                            + (command.Source.Equals("client") ? "TO" : "FROM") + "</b> the server.");
                    @out.WriteLine(" * This command is " + ((cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                            ? "a <b>generic</b> command used across the profile."
                            : "a <b>specific</b> command used for the " + cluster.Name + " cluster."));
                }

                if (command.Description.Count > 0)
                {
                    @out.WriteLine(" * <p>");
                    OutputWithLinebreak(@out, "", command.Description);
                }

                @out.WriteLine(" * <p>");
                @out.WriteLine(" * Code is auto-generated. Modifications may be overwritten!");
                @out.WriteLine(" */");
                OutputClassGenerated(@out);
                @out.Write("public class " + className + " extends " + commandExtends);
                if (command.Response != null)
                {
                    @out.Write(" implements ZigBeeTransactionMatcher");
                }
                @out.WriteLine(" {");

                if (commandExtends.Equals("ZclCommand"))
                {
                    if (!cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                    {
                        @out.WriteLine("    /**");
                        @out.WriteLine("     * The cluster ID to which this command belongs.");
                        @out.WriteLine("     */");
                        @out.WriteLine("    public static int CLUSTER_ID = 0x" + cluster.Code.ToString("X4") + ";");
                        @out.WriteLine();
                    }
                    @out.WriteLine("    /**");
                    @out.WriteLine("     * The command ID.");
                    @out.WriteLine("     */");
                    @out.WriteLine("    public static int COMMAND_ID = 0x" + command.Code.ToString("X2") + ";");
                    @out.WriteLine();
                }
                else
                {
                    @out.WriteLine("    /**");
                    @out.WriteLine("     * The ZDO cluster ID.");
                    @out.WriteLine("     */");
                    @out.WriteLine("    public static int CLUSTER_ID = 0x" + command.Code.ToString("X4") + ";");
                    @out.WriteLine();
                }

                foreach (ZigBeeXmlField field in command.Fields)
                {
                    if (reservedFields.Contains(StringToLowerCamelCase(field.Name)))
                    {
                        continue;
                    }
                    if (GetAutoSized(command.Fields, StringToLowerCamelCase(field.Name)) != null)
                    {
                        continue;
                    }

                    @out.WriteLine("    /**");
                    @out.WriteLine("     * " + field.Name + " command message field.");
                    if (field.Description.Count != 0)
                    {
                        @out.WriteLine("     * <p>");
                        OutputWithLinebreak(@out, "    ", field.Description);
                    }
                    @out.WriteLine("     */");
                    @out.WriteLine("    private " + GetDataTypeClass(field) + " " + StringToLowerCamelCase(field.Name) + ";");
                    @out.WriteLine();
                }

                @out.WriteLine("    /**");
                @out.WriteLine("     * Default constructor.");
                @out.WriteLine("     */");
                @out.WriteLine("    public " + className + "() {");
                if (!cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                {
                    @out.WriteLine("        clusterId = CLUSTER_ID;");
                }
                if (commandExtends.Equals("ZclCommand"))
                {
                    @out.WriteLine("        commandId = COMMAND_ID;");
                    @out.WriteLine("        genericCommand = "
                            + (cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase) ? "true" : "false") + ";");
                    @out.WriteLine("        commandDirection = ZclCommandDirection."
                            + (command.Source.Equals("client") ? "CLIENT_TO_SERVER" : "SERVER_TO_CLIENT") + ";");
                }
                @out.WriteLine("    }");

                if (cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                {
                    @out.WriteLine();
                    @out.WriteLine("    /**");
                    @out.WriteLine("     * Sets the cluster ID for <i>generic</i> commands. {@link " + className
                            + "} is a <i>generic</i> command.");
                    @out.WriteLine("     * <p>");
                    @out.WriteLine(
                            "     * For commands that are not <i>generic</i>, this method will do nothing as the cluster ID is fixed.");
                    @out.WriteLine("     * To test if a command is <i>generic</i>, use the {@link #isGenericCommand} method.");
                    @out.WriteLine("     *");
                    @out.WriteLine(
                            "     * @param clusterId the cluster ID used for <i>generic</i> commands as an {@link Integer}");

                    @out.WriteLine("     */");
                    @out.WriteLine("    @Override");
                    @out.WriteLine("    public void setClusterId(Integer clusterId) {");
                    @out.WriteLine("        this.clusterId = clusterId;");
                    @out.WriteLine("    }");
                }

                GenerateFields(@out, commandExtends, className, command.Fields, reservedFields);

                if (command.Response != null)
                {
                    @out.WriteLine();
                    @out.WriteLine("    @Override");
                    @out.WriteLine("    public boolean isTransactionMatch(ZigBeeCommand request, ZigBeeCommand response) {");
                    if (command.Response.Matchers.Count == 0)
                    {
                        @out.WriteLine("        return (response instanceof " + command.Response.Command + ")");
                        @out.WriteLine("                && ((ZdoRequest) request).getDestinationAddress().equals((("
                                + command.Response.Command + ") response).getSourceAddress());");
                    }
                    else
                    {
                        @out.WriteLine("        if (!(response instanceof " + command.Response.Command + ")) {");
                        @out.WriteLine("            return false;");
                        @out.WriteLine("        }");
                        @out.WriteLine();
                        @out.Write("        return ");

                        bool first = true;
                        foreach (ZigBeeXmlMatcher matcher in command.Response.Matchers)
                        {
                            if (first == false)
                            {
                                @out.WriteLine();
                                @out.Write("                && ");
                            }
                            first = false;
                            @out.WriteLine("(((" + StringToUpperCamelCase(command.Name) + ") request).get"
                                    + matcher.CommandField + "()");
                            @out.Write("                .equals(((" + command.Response.Command + ") response).get"
                                    + matcher.ResponseField + "()))");
                        }
                        @out.WriteLine(";");
                    }
                    @out.WriteLine("    }");
                }

                GenerateToString(@out, className, command.Fields, reservedFields);

                @out.WriteLine();
                @out.WriteLine("}");

                @out.Flush();
                @out.Close();
            }
        }

    }
}