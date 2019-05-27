using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator.Zcl
{
    public class ZigBeeZclClusterGenerator : ZigBeeBaseClassGenerator
    {

        ZigBeeZclClusterGenerator(List<ZigBeeXmlCluster> clusters, Dictionary<string, string> dependencies)
        {
            //this._generatedDate = _generatedDate;
            this._dependencies = dependencies;

            foreach (ZigBeeXmlCluster cluster in clusters)
            {
                // Suppress GENERAL cluster as it's not really a cluster!
                if (cluster.Name.Equals("GENERAL", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                try
                {
                    GenerateZclClusterClasses(cluster, packageRoot, new FileStream(_sourceRootPath, FileMode.Open));
                }
                catch (IOException e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e);
                }
            }
        }

        private void GenerateZclClusterClasses(ZigBeeXmlCluster cluster, string packageRootPrefix, FileStream sourceRootPath)
        {

            string packageRoot = packageRootPrefix;
            string packagePath = GetPackagePath(sourceRootPath, packageRoot);
            FileStream packageFile = GetPackageFile(packagePath + (packageZclCluster).Replace('.', '/'));

            String className = "Zcl" + StringToUpperCamelCase(cluster.Name) + "Cluster";
            TextWriter @out = GetClassOut(packageFile, className);

            OutputLicense(@out);

            @out.WriteLine("package " + packageRoot + packageZclCluster + ";");
            @out.WriteLine();

            ImportsClear();

            int commandsServer = 0;
            int commandsClient = 0;
            foreach (ZigBeeXmlCommand command in cluster.Commands)
            {
                ImportsAdd(packageRoot + packageZclCluster + "." + StringToLowerCamelCase(cluster.Name).ToLower() + "."
                        + StringToUpperCamelCase(command.Name));

                if (command.Source.Equals("server"))
                {
                    commandsServer++;
                }
                if (command.Source.Equals("client"))
                {
                    commandsClient++;
                }

                foreach (ZigBeeXmlField field in command.Fields)
                {
                    ImportsAddClass(field);
                }
            }

            bool addAttributeTypes = false;
            bool readAttributes = false;
            bool writeAttributes = false;
            List<ZigBeeXmlAttribute> attributesClient = new List<ZigBeeXmlAttribute>();
            List<ZigBeeXmlAttribute> attributesServer = new List<ZigBeeXmlAttribute>();
            foreach (ZigBeeXmlAttribute attribute in cluster.Attributes)
            {
                if (attribute.Writable)
                {
                    addAttributeTypes = true;
                    writeAttributes = true;
                }
                readAttributes = true;
                if (attribute.Side.Equals("server"))
                {
                    attributesServer.Add(attribute);
                }
                if (attribute.Side.Equals("client"))
                {
                    attributesClient.Add(attribute);
                }

                ImportsAddClass(attribute);
            }

            if (addAttributeTypes)
            {
                ImportsAdd("com.zsmartsystems.zigbee.zcl.protocol.ZclDataType");
            }

            ImportsAdd(packageRoot + packageZcl + ".ZclCluster");
            if (cluster.Attributes.Count > 0)
            {
                ImportsAdd(packageRoot + packageZclProtocol + ".ZclDataType");
                // importsAdd(packageRoot + packageZclProtocol + ".ZclClusterType");
            }

            if (cluster.Commands.Count != 0)
            {
                ImportsAdd(packageRoot + packageZcl + ".ZclCommand");
            }
            // imports.add(packageRoot + packageZcl + ".ZclCommandMessage");
            ImportsAdd("javax.annotation.Generated");

            // imports.add(packageRoot + ".ZigBeeDestination");
            ImportsAdd(packageRoot + ".ZigBeeEndpoint");
            if (cluster.Attributes.Count != 0 || cluster.Commands.Count != 0)
            {
                ImportsAdd(packageRoot + ".CommandResult");
                ImportsAdd("java.util.concurrent.Future");
            }
            // imports.add(packageRoot + ".ZigBeeEndpoint");
            ImportsAdd(packageRoot + packageZcl + ".ZclAttribute");
            ImportsAdd("java.util.Map");
            ImportsAdd("java.util.concurrent.ConcurrentHashMap");

            OutputImports(@out);

            @out.WriteLine();
            @out.WriteLine("/**");
            @out.WriteLine(" * <b>" + cluster.Name + "</b> cluster implementation (<i>Cluster ID " + "0x" + cluster.Code.ToString("X4") + "</i>).");
            if (cluster.Description.Count != 0)
            {
                @out.WriteLine(" * <p>");
                OutputWithLinebreak(@out, "", cluster.Description);
            }

            @out.WriteLine(" * <p>");
            @out.WriteLine(" * Code is auto-generated. Modifications may be overwritten!");

            @out.WriteLine(" */");
            // outputClassJavaDoc(out);
            OutputClassGenerated(@out);
            @out.WriteLine("public class " + className + " extends ZclCluster {");

            @out.WriteLine("    /**");
            @out.WriteLine("     * The ZigBee Cluster Library Cluster ID");
            @out.WriteLine("     */");
            @out.WriteLine("    public static final int CLUSTER_ID = 0x" + cluster.Code.ToString("X4"));
            @out.WriteLine();
            @out.WriteLine("    /**");
            @out.WriteLine("     * The ZigBee Cluster Library Cluster Name");
            @out.WriteLine("     */");
            @out.WriteLine("    public static final String CLUSTER_NAME = \"" + cluster.Name + "\";");
            @out.WriteLine();

            if (cluster.Attributes.Count != 0)
            {
                @out.WriteLine("    // Attribute constants");
                foreach (ZigBeeXmlAttribute attribute in cluster.Attributes)
                {
                    if (attribute.ArrayStart != null && attribute.ArrayCount != null && attribute.ArrayCount > 0)
                    {
                        int? arrayCount = attribute.ArrayStart;
                        int? arrayStep = attribute.ArrayStep == null ? 1 : attribute.ArrayStep;
                        for (int count = 0; count < attribute.ArrayCount; count++)
                        {
                            if (attribute.Description.Count != 0)
                            {
                                @out.WriteLine("    /**");
                                OutputWithLinebreak(@out, "    ", attribute.Description);
                                @out.WriteLine("     */");
                            }

                            String name = Regex.Replace(attribute.Name, "\\{\\{count\\}\\}", arrayCount.ToString()); //attribute.Name.replaceAll("\\{\\{count\\}\\}", arrayCount));
                            @out.WriteLine("    public static final int " + GetEnum(name) + " = 0x" + (attribute.Code + arrayCount).Value.ToString("X4") + ";");
                            arrayCount += arrayStep;
                        }
                    }
                    else
                    {
                        if (attribute.Description.Count != 0)
                        {
                            @out.WriteLine("    /**");
                            OutputWithLinebreak(@out, "    ", attribute.Description);
                            @out.WriteLine("     */");
                        }
                        @out.WriteLine("    public static int " + GetEnum(attribute.Name) + " = 0x" + attribute.Code.ToString("X4") + ";");
                    }
                }
                @out.WriteLine();
            }

            @out.WriteLine("    @Override");
            @out.WriteLine("    protected Map<Integer, ZclAttribute> initializeClientAttributes() {");
            CreateInitializeAttributes(@out, cluster.Name, attributesClient);
            @out.WriteLine();

            @out.WriteLine("    @Override");
            @out.WriteLine("    protected Map<Integer, ZclAttribute> initializeServerAttributes() {");
            CreateInitializeAttributes(@out, cluster.Name, attributesServer);
            @out.WriteLine();

            // TODO: Add client attributes

            if (commandsServer != 0)
            {
                @out.WriteLine("    @Override");
                @out.WriteLine("    protected Map<Integer, Class<? extends ZclCommand>> initializeServerCommands() {");
                @out.WriteLine("        Map<Integer, Class<? extends ZclCommand>> commandMap = new ConcurrentHashMap<>("
                        + commandsServer + ");");
                @out.WriteLine();
                foreach (ZigBeeXmlCommand command in cluster.Commands)
                {
                    if (command.Source.Equals("server", StringComparison.InvariantCultureIgnoreCase))
                    {
                        @out.WriteLine("        commandMap.put(0x" + command.Code.ToString("X4") + ", " + StringToUpperCamelCase(command.Name) + ".class);");
                    }
                }
                @out.WriteLine();

                @out.WriteLine("        return commandMap;");
                @out.WriteLine("    }");
                @out.WriteLine();
            }

            if (commandsClient != 0)
            {
                @out.WriteLine("    @Override");
                @out.WriteLine("    protected Map<Integer, Class<? extends ZclCommand>> initializeClientCommands() {");
                @out.WriteLine("        Map<Integer, Class<? extends ZclCommand>> commandMap = new ConcurrentHashMap<>(" + commandsClient + ");");
                @out.WriteLine();
                foreach (ZigBeeXmlCommand command in cluster.Commands)
                {
                    if (command.Source.Equals("client", StringComparison.InvariantCultureIgnoreCase))
                    {
                        @out.WriteLine("        commandMap.put(0x" + command.Code.ToString("X4") + ", " + StringToUpperCamelCase(command.Name) + ".class);");
                    }
                }
                @out.WriteLine();

                @out.WriteLine("        return commandMap;");
                @out.WriteLine("    }");
                @out.WriteLine();
            }

            @out.WriteLine("    /**");
            @out.WriteLine("     * Default constructor to create a " + cluster.Name + " cluster.");
            @out.WriteLine("     *");
            @out.WriteLine("     * @param zigbeeEndpoint the {@link ZigBeeEndpoint} this cluster is contained within");
            @out.WriteLine("     */");
            @out.WriteLine("    public " + className + "(final ZigBeeEndpoint zigbeeEndpoint) {");
            @out.WriteLine("        super(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME);");
            @out.WriteLine("    }");

            foreach (ZigBeeXmlAttribute attribute in cluster.Attributes)
            {
                if (attribute.Side.Equals("client"))
                {
                    continue;
                }

                if (!ZclDataType.Mapping.ContainsKey(attribute.Type)) //(zclDataType == null)
                {
                    throw new ArgumentException(
                            "Unknown ZCL Type \"" + attribute.Type + "\" for attribute \"" + attribute.Name + "\".");
                }

                DataTypeMap zclDataType = ZclDataType.Mapping[attribute.Type];

                if (attribute.Writable)
                {
                    OutputAttributeJavaDoc(@out, "Set", attribute, zclDataType);
                    if (attribute.ArrayStart != null && attribute.ArrayCount != null && attribute.ArrayCount > 0)
                    {
                        string name = Regex.Replace(attribute.Name, "\\{\\{count\\}\\}", ""); //attribute.name.replaceAll("\\{\\{count\\}\\}", "");
                        @out.WriteLine("    @Deprecated");
                        @out.WriteLine("    public Future<CommandResult> set" + StringToUpperCamelCase(name).Replace("_", "")
                                + "(final int arrayOffset, final " + GetDataTypeClass(attribute) + " value) {");

                        //name = attribute.Name.replaceAll("\\{\\{count\\}\\}", Integer.toString(attribute.ArrayStart));
                        name = Regex.Replace(attribute.Name, "\\{\\{count\\}\\}", attribute.ArrayStart.Value.ToString());

                        @out.WriteLine(
                                "        return write(serverAttributes.get(" + GetEnum(name) + " + arrayOffset), value);");
                    }
                    else
                    {
                        @out.WriteLine("    @Deprecated");
                        @out.WriteLine("    public Future<CommandResult> set"
                                + StringToUpperCamelCase(attribute.Name).Replace("_", "") + "(final "
                                + GetDataTypeClass(attribute) + " value) {");
                        @out.WriteLine("        return write(serverAttributes.get(" + GetEnum(attribute.Name) + "), value);");
                    }
                    @out.WriteLine("    }");
                }

                // if (attribute.attributeAccess.toLowerCase().contains("read")) {
                OutputAttributeJavaDoc(@out, "Get", attribute, zclDataType);
                if (attribute.ArrayStart != null && attribute.ArrayCount != null && attribute.ArrayCount > 0)
                {
                    //String name = attribute.Name.replaceAll("\\{\\{count\\}\\}", "");
                    string name = Regex.Replace(attribute.Name, "\\{\\{count\\}\\}", "");

                    @out.WriteLine("    @Deprecated");
                    @out.WriteLine("    public Future<CommandResult> get" + StringToUpperCamelCase(name).Replace("_", "")
                            + "Async(final int arrayOffset) {");
                    @out.WriteLine("        if (arrayOffset < " + attribute.ArrayStart + " || arrayOffset > "
                            + (attribute.ArrayStart + attribute.ArrayCount - 1) + ") {");
                    @out.WriteLine("            throw new IllegalArgumentException(\"arrayOffset out of bounds\");");
                    @out.WriteLine("        }");
                    @out.WriteLine();
                    name = Regex.Replace(attribute.Name, "\\{\\{count\\}\\}", attribute.ArrayStart.Value.ToString());
                    //name = attribute.name.replaceAll("\\{\\{count\\}\\}", Integer.toString(attribute.arrayStart));
                    @out.WriteLine("        return read(serverAttributes.get(" + GetEnum(name) + " + arrayOffset));");
                }
                else
                {
                    @out.WriteLine("    @Deprecated");
                    @out.WriteLine("    public Future<CommandResult> get"
                            + StringToUpperCamelCase(attribute.Name).Replace("_", "") + "Async() {");
                    @out.WriteLine("        return read(serverAttributes.get(" + GetEnum(attribute.Name) + "));");
                }
                @out.WriteLine("    }");

                // TODO: Needs to document the counter
                OutputAttributeJavaDoc(@out, "Synchronously get", attribute, zclDataType);
                if (attribute.ArrayStart != null && attribute.ArrayCount != null && attribute.ArrayCount > 0)
                {
                    //String name = attribute.Name.replaceAll("\\{\\{count\\}\\}", "");
                    string name = Regex.Replace(attribute.Name, "\\{\\{count\\}\\}", "");
                    @out.WriteLine("    @Deprecated");
                    @out.WriteLine("    public " + GetDataTypeClass(attribute) + " get"
                            + StringToUpperCamelCase(name).Replace("_", "")
                            + "(final int arrayOffset, final long refreshPeriod) {");

                    name = Regex.Replace(attribute.Name, "\\{\\{count\\}\\}", attribute.ArrayStart.Value.ToString());
                    //name = attribute.Name.replaceAll("\\{\\{count\\}\\}", Integer.toString(attribute.arrayStart));
                    @out.WriteLine("        if (serverAttributes.get(" + GetEnum(name) + " + arrayOffset"
                            + ").isLastValueCurrent(refreshPeriod)) {");
                    @out.WriteLine("            return (" + GetDataTypeClass(attribute) + ") serverAttributes.get("
                            + GetEnum(name) + " + arrayOffset).getLastValue();");
                    @out.WriteLine("        }");
                    @out.WriteLine();
                    @out.WriteLine("        return (" + GetDataTypeClass(attribute) + ") readSync(serverAttributes.get("
                            + GetEnum(name) + " + arrayOffset));");
                }
                else
                {
                    @out.WriteLine("    @Deprecated");
                    @out.WriteLine("    public " + GetDataTypeClass(attribute) + " get"
                            + StringToUpperCamelCase(attribute.Name).Replace("_", "") + "(final long refreshPeriod) {");
                    @out.WriteLine("        if (serverAttributes.get(" + GetEnum(attribute.Name)
                            + ").isLastValueCurrent(refreshPeriod)) {");
                    @out.WriteLine("            return (" + GetDataTypeClass(attribute) + ") serverAttributes.get("
                            + GetEnum(attribute.Name) + ").getLastValue();");
                    @out.WriteLine("        }");
                    @out.WriteLine();
                    @out.WriteLine("        return (" + GetDataTypeClass(attribute) + ") readSync(serverAttributes.get("
                            + GetEnum(attribute.Name) + "));");
                }
                @out.WriteLine("    }");
                // }

                if (!attribute.Optional)
                {
                    OutputAttributeJavaDoc(@out, "Set reporting for", attribute, zclDataType);
                    if (attribute.ArrayStart != null && attribute.ArrayCount != null && attribute.ArrayCount > 0)
                    {
                        string name = Regex.Replace(attribute.Name, "\\{\\{count\\}\\}", attribute.ArrayStart.Value.ToString());
                        // String name = attribute.Name.replaceAll("\\{\\{count\\}\\}", Integer.toString(attribute.arrayStart));
                        string offset;
                        if (attribute.ArrayStep == null)
                        {
                            offset = "arrayOffset - 1";
                        }
                        else
                        {
                            offset = "(arrayOffset - 1) * " + attribute.ArrayStep;
                        }

                        if (zclDataType.Analogue)
                        {
                            @out.WriteLine("    @Deprecated");
                            @out.WriteLine("    public Future<CommandResult> set" + StringToUpperCamelCase(name)
                                    + "Reporting(final int arrayOffset, final int minInterval, final int maxInterval, final Object reportableChange) {");
                            @out.WriteLine("        return setReporting(serverAttributes.get(" + GetEnum(name) + " + " + offset
                                    + "), minInterval, maxInterval, reportableChange);");
                        }
                        else
                        {
                            @out.WriteLine("    @Deprecated");
                            @out.WriteLine("    public Future<CommandResult> set" + StringToUpperCamelCase(name)
                                    + "Reporting(final int arrayOffset, final int minInterval, final int maxInterval) {");
                            @out.WriteLine("        return setReporting(serverAttributes.get(" + GetEnum(name) + " + " + offset
                                    + "), minInterval, maxInterval);");
                        }
                    }
                    else
                    {
                        if (zclDataType.Analogue)
                        {
                            @out.WriteLine("    @Deprecated");
                            @out.WriteLine("    public Future<CommandResult> set" + StringToUpperCamelCase(attribute.Name)
                                    + "Reporting(final int minInterval, final int maxInterval, final Object reportableChange) {");
                            @out.WriteLine("        return setReporting(serverAttributes.get(" + GetEnum(attribute.Name)
                                    + "), minInterval, maxInterval, reportableChange);");
                        }
                        else
                        {
                            @out.WriteLine("    @Deprecated");
                            @out.WriteLine("    public Future<CommandResult> set" + StringToUpperCamelCase(attribute.Name)
                                    + "Reporting(final int minInterval, final int maxInterval) {");
                            @out.WriteLine("        return setReporting(serverAttributes.get(" + GetEnum(attribute.Name)
                                    + "), minInterval, maxInterval);");
                        }
                    }
                    @out.WriteLine("    }");
                }
            }

            foreach (ZigBeeXmlCommand command in cluster.Commands)
            {
                @out.WriteLine();
                @out.WriteLine("    /**");
                @out.WriteLine("     * The " + command.Name);
                if (command.Description.Count != 0)
                {
                    @out.WriteLine("     * <p>");
                    OutputWithLinebreak(@out, "    ", command.Description);
                }
                @out.WriteLine("     *");

                LinkedList<ZigBeeXmlField> fields = new LinkedList<ZigBeeXmlField>(command.Fields);
                foreach (ZigBeeXmlField field in fields)
                {
                    @out.WriteLine("     * @param " + StringToLowerCamelCase(field.Name) + " {@link " + GetDataTypeClass(field) + "} " + field.Name);
                }

                @out.WriteLine("     * @return the {@link Future<CommandResult>} command result future");
                @out.WriteLine("     */");
                @out.Write("    public Future<CommandResult> " + StringToLowerCamelCase(command.Name) + "(");

                bool first = true;
                foreach (ZigBeeXmlField field in fields)
                {
                    if (first == false)
                    {
                        @out.Write(", ");
                    }
                    @out.Write(GetDataTypeClass(field) + " " + StringToLowerCamelCase(field.Name));
                    first = false;
                }

                @out.WriteLine(") {");
                if (fields.Count == 0)
                {
                    @out.WriteLine("        return send(new " + StringToUpperCamelCase(command.Name) + "());");
                }
                else
                {
                    @out.WriteLine("        " + StringToUpperCamelCase(command.Name) + " command = new " + StringToUpperCamelCase(command.Name) + "();");
                    @out.WriteLine();
                    @out.WriteLine("        // Set the fields");

                    foreach (ZigBeeXmlField field in fields)
                    {
                        @out.WriteLine("        command.set" + StringToUpperCamelCase(field.Name) + "(" + StringToLowerCamelCase(field.Name) + ");");
                    }
                    @out.WriteLine();
                    @out.WriteLine("        return send(command);");
                }
                @out.WriteLine("    }");
            }

            @out.WriteLine("}");

            @out.Flush();
            @out.Close();
        }

        private void CreateInitializeAttributes(TextWriter @out, string clusterName, List<ZigBeeXmlAttribute> attributes)
        {
            @out.WriteLine("        Map<Integer, ZclAttribute> attributeMap = new ConcurrentHashMap<>(" + attributes.Count + ");");

            if (attributes.Count != 0)
            {
                @out.WriteLine();
                foreach (ZigBeeXmlAttribute attribute in attributes)
                {
                    if (attribute.ArrayStart != null && attribute.ArrayCount != null && attribute.ArrayCount > 0)
                    {
                        int? ArrayCount = attribute.ArrayStart;
                        int? arrayStep = attribute.ArrayStep == null ? 1 : attribute.ArrayStep;
                        for (int count = 0; count < attribute.ArrayCount; count++)
                        {
                            string name = Regex.Replace(attribute.Name, "\\{\\{count\\}\\}", ArrayCount.ToString());
                            //String name = attribute.Name,"\\{\\{count\\}\\}", Integer.toString(ArrayCount));
                            @out.WriteLine("        attributeMap.put(" + GetEnum(name) + ", "
                                    + DefineAttribute(attribute, clusterName, name, 0) + ");");
                            ArrayCount += arrayStep;
                        }
                    }
                    else
                    {
                        @out.WriteLine("        attributeMap.put(" + GetEnum(attribute.Name) + ", "
                                + DefineAttribute(attribute, clusterName, attribute.Name, 0) + ");");
                    }
                }
            }
            @out.WriteLine();
            @out.WriteLine("        return attributeMap;");
            @out.WriteLine("    }");
        }

        private string DefineAttribute(ZigBeeXmlAttribute attribute, string clusterName, string attributeName, int count)
        {
            return "new ZclAttribute(this, " + GetEnum(attributeName) + ", \"" + attributeName + "\", " + "ZclDataType."
                    + attribute.Type + ", " + !attribute.Optional + ", " + true + ", " + attribute.Writable + ", "
                    + attribute.Reportable + ")";
        }

        private string GetEnum(string name)
        {
            return "ATTR_" + StringToConstantEnum(name);
        }
    }
}