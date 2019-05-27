using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator.Zcl
{
    public abstract class ZigBeeBaseClassGenerator
    {
        // TODO: check if fields can be private
        protected string _generatedDate;
        protected Dictionary<string, string> _dependencies;

        protected int _lineLen = 80;
        protected string _sourceRootPath = "target/src/main/java/";
        protected List<string> _importList = new List<string>();

        protected static string packageRoot = "com.zsmartsystems.zigbee";
        protected static string packageZcl = ".zcl";
        protected static string packageZclField = packageZcl + ".field";
        protected string packageZclCluster = packageZcl + ".clusters";
        protected string packageZclProtocol = packageZcl + ".protocol";
        protected string packageZclProtocolCommand;

        protected static string packageZdp = ".zdo";
        protected static string packageZdpField = packageZdp + ".field";
        protected string packageZdpCommand = packageZdp + ".command";
        protected string packageZdpTransaction = packageZdp + ".transaction";
        protected string packageZdpDescriptors = packageZdpField;

        protected static List<string> standardTypes = new List<string>();
        protected static Dictionary<string, string> customTypes = new Dictionary<string, string>();
        protected static List<string> fixedCaseAcronyms = new List<string>();

        public ZigBeeBaseClassGenerator()
        {
            packageZclProtocolCommand = packageZclCluster;
        }

        static ZigBeeBaseClassGenerator()
        {
            fixedCaseAcronyms.Add("AA");
            fixedCaseAcronyms.Add("AC");
            fixedCaseAcronyms.Add("AAA");
            fixedCaseAcronyms.Add("ACE");
            fixedCaseAcronyms.Add("APS");
            fixedCaseAcronyms.Add("CIE");
            fixedCaseAcronyms.Add("CR");
            fixedCaseAcronyms.Add("CO");
            fixedCaseAcronyms.Add("CO2");
            fixedCaseAcronyms.Add("DC");
            fixedCaseAcronyms.Add("DRLC");
            fixedCaseAcronyms.Add("DST");
            fixedCaseAcronyms.Add("ECC");
            fixedCaseAcronyms.Add("ECDSA");
            fixedCaseAcronyms.Add("EUI");
            fixedCaseAcronyms.Add("FC");
            fixedCaseAcronyms.Add("HAN");
            fixedCaseAcronyms.Add("HW");
            fixedCaseAcronyms.Add("ID");
            fixedCaseAcronyms.Add("IAS");
            fixedCaseAcronyms.Add("IEEE");
            fixedCaseAcronyms.Add("LQI");
            fixedCaseAcronyms.Add("MAC");
            fixedCaseAcronyms.Add("MMO");
            fixedCaseAcronyms.Add("NWK");
            fixedCaseAcronyms.Add("PIN");
            fixedCaseAcronyms.Add("PIR");
            fixedCaseAcronyms.Add("RMS");
            fixedCaseAcronyms.Add("RSSI");
            fixedCaseAcronyms.Add("SMAC");
            fixedCaseAcronyms.Add("SW");
            fixedCaseAcronyms.Add("UTC");
            fixedCaseAcronyms.Add("WAN");
            fixedCaseAcronyms.Add("WD");
            fixedCaseAcronyms.Add("XY");
            fixedCaseAcronyms.Add("ZCL");

            fixedCaseAcronyms.Add("may");
            fixedCaseAcronyms.Add("shall");
            fixedCaseAcronyms.Add("should");

            fixedCaseAcronyms.Add("ZigBee");

            standardTypes.Add("Integer");
            standardTypes.Add("Boolean");
            standardTypes.Add("Object");
            standardTypes.Add("Long");
            standardTypes.Add("Double");
            standardTypes.Add("String");
            standardTypes.Add("int[]");

            customTypes.Add("IeeeAddress", packageRoot + ".IeeeAddress");
            customTypes.Add("ByteArray", packageRoot + packageZclField + ".ByteArray");
            customTypes.Add("ZclStatus", packageRoot + packageZcl + ".ZclStatus");
            customTypes.Add("ZdoStatus", packageRoot + packageZdp + ".ZdoStatus");
            customTypes.Add("BindingTable", packageRoot + packageZdpField + ".BindingTable");
            customTypes.Add("NeighborTable", packageRoot + packageZdpField + ".NeighborTable");
            customTypes.Add("RoutingTable", packageRoot + packageZdpField + ".RoutingTable");
            customTypes.Add("Calendar", "java.util.Calendar");
            customTypes.Add("ImageUpgradeStatus", packageRoot + packageZclField + ".ImageUpgradeStatus");
        }

        protected string StringToConstantEnum(string value)
        {
            return StringToConstant(value).Replace("_", "");
        }

        protected string StringToConstant(string value)
        {
            // value = value.replaceAll("\\(.*?\\) ?", "");
            value = value.Trim();
            value = value.Replace("+", "_Plus");
            value = value.Replace("(", "_");
            value = value.Replace(")", "_");
            value = value.Replace(" ", "_");
            value = value.Replace("-", "_");
            value = value.Replace(".", "_");
            value = value.Replace("/", "_");
            value = value.Replace("#", "_");
            value = value.Replace("_+", "_");
            if (value.EndsWith("_"))
            {
                value = value.Substring(0, value.Length - 1);
            }
            return value.ToUpper();
        }

        private string ToProperCase(string s)
        {
            return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
        }

        private string ToCamelCase(string value)
        {
            // value = value.replaceAll("\\(.*?\\) ?", "");
            value = value.Replace("(", "_");
            value = value.Replace(")", "_");
            value = value.Replace("+", "_Plus");
            value = value.Replace(" ", "_");
            value = value.Replace("-", "_");
            value = value.Replace(".", "_");
            value = value.Replace("/", "_");
            value = value.Replace("#", "_");
            value = value.Replace("_+", "_");
            string[] parts = value.Split("_"); //ReplaceAll(Regex) ??
            string camelCaseString = "";

            foreach (string part in parts)
            {
                camelCaseString = camelCaseString + ToProperCase(part);
            }

            return camelCaseString;
        }

        protected string StringToUpperCamelCase(string value)
        {
            return ToCamelCase(value);
        }

        protected string StringToLowerCamelCase(string value)
        {
            string cc = ToCamelCase(value);

            return cc.Substring(0, 1).ToLower() + cc.Substring(1);
        }

        protected string UpperCaseFirstCharacter(string val)
        {
            return val.Substring(0, 1).ToUpper() + val.Substring(1);
        }

        protected string LowerCaseFirstCharacter(string val)
        {
            return val.Substring(0, 1).ToLower() + val.Substring(1);
        }

        protected TextWriter GetClassOut(FileStream packageFile, string className)
        {
            //Directory.CreateDirectory(packageFile.)
            //packageFile.mkdirs();
            FileStream classFile = File.Create(packageFile.Name + Path.DirectorySeparatorChar.ToString() + className + ".cs");
            Console.WriteLine("Generating: " + classFile.Name);
            //FileOutputStream fileOutputStream = new FileOutputStream(classFile, false);
            return new StreamWriter(classFile);
        }

        protected void ImportsClear()
        {
            _importList.Clear();
        }

        protected void ImportsAdd(string importClass)
        {
            if (_importList.Contains(importClass))
            {
                return;
            }

            _importList.Add(importClass);
        }

        protected void OutputImports(TextWriter @out)
        {
            _importList.Sort();

            bool found = false;

            foreach (string importClass in _importList)
            {
                if (!importClass.StartsWith("java."))
                {
                    continue;
                }

                found = true;

                @out.WriteLine("import " + importClass + ";");
            }

            if (found)
            {
                @out.WriteLine();
                found = false;
            }

            foreach (string importClass in _importList)
            {
                if (!importClass.StartsWith("javax."))
                {
                    continue;
                }
                found = true;
                @out.WriteLine("import " + importClass + ";");
            }

            if (found)
            {
                @out.WriteLine();
                found = false;
            }

            foreach (string importClass in _importList)
            {
                if (importClass.StartsWith("java.") || importClass.StartsWith("javax."))
                {
                    continue;
                }

                @out.WriteLine("import " + importClass + ";");
            }
        }

        protected void OutputWithLinebreak(TextWriter @out, string indent, List<ZigBeeXmlDescription> descriptions)
        {
            bool firstDescription = true;
            foreach (ZigBeeXmlDescription description in descriptions)
            {
                if (description.Description == null)
                {
                    continue;
                }
                string[] words = Regex.Split(description.Description, "\\s+");

                if (words.Length == 0)
                {
                    continue;
                }

                if (!firstDescription)
                {
                    @out.WriteLine(indent + " * <p>");
                }

                firstDescription = false;

                @out.Write(indent + " *");

                int len = 2 + indent.Length;

                foreach (string word in words)
                {
                    if (word.Equals("note:", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (len > 2)
                        {
                            @out.WriteLine();
                        }

                        @out.WriteLine(indent + " * <p>");
                        @out.Write(indent + " * <b>Note:</b>");

                        continue;
                    }

                    if (len + word.Length > _lineLen)
                    {
                        @out.WriteLine();
                        @out.Write(indent + " *");
                        len = 2 + indent.Length;
                    }

                    @out.Write(" ");

                    var wordToWrite = word;
                    foreach (string acronym in fixedCaseAcronyms)
                    {
                        if (acronym.Equals(word, StringComparison.InvariantCultureIgnoreCase))
                        {
                            wordToWrite = acronym;
                        }
                    }

                    @out.Write(wordToWrite);
                    len += word.Length;
                }

                if (len != 2 + indent.Length)
                {
                    @out.WriteLine();
                }
            }
        }

        public string ReplaceFirst(string text, string search, string replace)
        {
            var regex = new Regex(Regex.Escape(search));
            var newText = regex.Replace(text, replace, 1);

            return newText;
        }

        protected void OutputLicense(TextWriter @out)
        {
            string year = "XXXX";

            StreamReader br;
            try
            {
                br = new StreamReader(new FileStream("../pom.xml", FileMode.Open));
                string line = br.ReadLine();

                while (line != null)
                {
                    if (line.Contains("<license.year>") && line.Contains("</license.year>"))
                    {
                        year = line.Substring(line.IndexOf("<license.year>") + 14, line.IndexOf("</license.year>"));
                        break;
                    }
                    line = br.ReadLine();
                }

                br.Close();

                br = new StreamReader(new FileStream("../src/etc/header.txt", FileMode.Open));
                line = br.ReadLine();

                @out.WriteLine("/**");

                while (line != null)
                {
                    var newLine = ReplaceFirst(line, "\\$\\{year\\}", year);
                    @out.WriteLine(" * " + newLine);
                    line = br.ReadLine();
                }

                @out.WriteLine(" */");
                br.Close();
            }
            catch (FileNotFoundException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e);
            }
            catch (IOException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e);
            }
        }

        protected FileStream GetPackageFile(string packagePath)
        {
            if (!File.Exists(packagePath))
            {
                Directory.CreateDirectory(packagePath);
            }

            return new FileStream(packagePath, FileMode.Open);
        }

        protected string GetPackagePath(FileStream sourceRootPath, string packageRoot)
        {
            return sourceRootPath.Name + Path.DirectorySeparatorChar.ToString() + packageRoot.Replace(".", Path.DirectorySeparatorChar.ToString());
        }

        protected void OutputClassGenerated(TextWriter @out)
        {
            //@out.WriteLine("@Generated(value = \"" + ZigBeeCodeGenerator.class.getName() + "\", date = \"" + generatedDate+ "\")");
        }

        protected void OutputAttributeJavaDoc(TextWriter @out, string type, ZigBeeXmlAttribute attribute, DataTypeMap zclDataType)
        {
            @out.WriteLine();
            @out.WriteLine("    /**");
            @out.WriteLine("     * " + type + " the <i>" + attribute.Name + "</i> attribute [attribute ID <b>0x" + attribute.Code.ToString("X4") + "</b>].");
            if (attribute.Description.Count != 0)
            {
                @out.WriteLine("     * <p>");
                OutputWithLinebreak(@out, "    ", attribute.Description);
            }
            if ("Synchronously get".Equals(type))
            {
                @out.WriteLine("     * <p>");
                @out.WriteLine("     * This method can return cached data if the attribute has already been received.");
                @out.WriteLine(
                        "     * The parameter <i>refreshPeriod</i> is used to control this. If the attribute has been received");
                @out.WriteLine(
                        "     * within <i>refreshPeriod</i> milliseconds, then the method will immediately return the last value");
                @out.WriteLine(
                        "     * received. If <i>refreshPeriod</i> is set to 0, then the attribute will always be updated.");
                @out.WriteLine("     * <p>");
                @out.WriteLine(
                        "     * This method will block until the response is received or a timeout occurs unless the current value is returned.");
            }
            @out.WriteLine("     * <p>");
            @out.WriteLine("     * The attribute is of type {@link " + GetDataTypeClass(attribute) + "}.");
            @out.WriteLine("     * <p>");
            @out.WriteLine("     * The implementation of this attribute by a device is "
                    + (attribute.Optional ? "OPTIONAL" : "MANDATORY"));
            @out.WriteLine("     *");
            if (attribute.ArrayCount != null && attribute.ArrayStart != null)
            {
                @out.WriteLine("     * @param arrayOffset attribute array offset (" + attribute.ArrayStart
                        + " < arrayOffset < " + (attribute.ArrayStart.Value + attribute.ArrayCount.Value - 1) + ")");
            }
            if ("Set reporting for".Equals(type))
            {
                @out.WriteLine("     * @param minInterval minimum reporting period");
                @out.WriteLine("     * @param maxInterval maximum reporting period");
                if (zclDataType.Analogue)
                {
                    @out.WriteLine("     * @param reportableChange {@link Object} delta required to trigger report");
                }
            }
            else if ("Set".Equals(type))
            {
                @out.WriteLine("     * @param " + StringToLowerCamelCase(attribute.Name) + " the {@link "
                        + GetDataTypeClass(attribute) + "} attribute value to be set");
            }

            if ("Synchronously get".Equals(type))
            {
                @out.WriteLine(
                        "     * @param refreshPeriod the maximum age of the data (in milliseconds) before an update is needed");
                @out.WriteLine(
                        "     * @return the {@link " + GetDataTypeClass(attribute) + "} attribute value, or null on error");
            }
            else
            {
                @out.WriteLine("     * @return the {@link Future<CommandResult>} command result future");
            }

            string replacedBy;
            if ("Set reporting for".Equals(type))
            {
                replacedBy = "setReporting(int attributeId, int minInterval, int maxInterval";
                if (zclDataType.Analogue)
                {
                    replacedBy += ", Object reportableChange";
                }
            }
            else if (type.Contains("Set"))
            {
                replacedBy = "writeAttribute(int attributeId, Object value";
            }
            else if ("Synchronously get".Equals(type))
            {
                replacedBy = "ZclAttribute#readValue(long refreshPeriod";
            }
            else
            {
                replacedBy = "readAttribute(int attributeId";
            }
            replacedBy += ")";

            @out.WriteLine("     * @deprecated As of release 1.2.0, replaced by {@link #" + replacedBy + "}");

            @out.WriteLine("     */");
        }

        protected string GetDataTypeClass(ZigBeeXmlAttribute attribute)
        {
            // if (attribute.implementationClass.isEmpty()) {
            if (ZclDataType.Mapping.ContainsKey(attribute.Type))
            {
                return ZclDataType.Mapping[attribute.Type].DataClass;
            }

            if (_dependencies.ContainsKey(attribute.ImplementationClass))
            {
                // importsAdd(dependencies.get(type));
                return attribute.ImplementationClass;
            }

            Console.WriteLine("Unknown data type " + attribute.Type);
            return "(UNKNOWN::" + attribute.Type + ")";
            // }
            // return attribute.implementationClass;
        }

        protected string GetDataTypeClass(ZigBeeXmlField field)
        {
            string dataType = "";

            // if (field.implementationClass.isEmpty()) {
            if (ZclDataType.Mapping.ContainsKey(field.Type))
            {
                dataType = ZclDataType.Mapping[field.Type].DataClass;
            }
            else if (_dependencies.ContainsKey(field.ImplementationClass))
            {
                // importsAdd(dependencies.get(type));
                dataType = field.ImplementationClass;
            }

            if (string.IsNullOrEmpty(dataType))
            {
                Console.WriteLine("Unknown data type " + field.Type);
                return "(UNKNOWN::" + field.Type + ")";
            }

            if (field.Sizer == null)
            {
                return dataType;
            }
            else
            {
                return "List<" + dataType + ">";
            }

            // }
            // return field.implementationClass;
        }

        protected void ImportsAddClass(ZigBeeXmlField field)
        {
            ImportsAddClassInternal(GetDataTypeClass(field));
        }

        protected void ImportsAddClass(ZigBeeXmlAttribute attribute)
        {
            ImportsAddClassInternal(GetDataTypeClass(attribute));
        }

        protected void ImportsAddClassInternal(string type)
        {
            string typeName = type;
            if (type.StartsWith("List"))
            {
                ImportsAdd("java.util.List");
                typeName = typeName.Substring(typeName.IndexOf("<") + 1, typeName.IndexOf(">"));
            }

            if (standardTypes.Contains(typeName))
            {
                return;
            }

            if (customTypes.ContainsKey(typeName))
            {
                ImportsAdd(customTypes[typeName]);
                return;
            }

            if (_dependencies.ContainsKey(type))
            {
                ImportsAdd(_dependencies[type]);
                return;
            }

            string packageName;
            if (type.Contains("Descriptor"))
            {
                packageName = packageZdpDescriptors;
            }
            else
            {
                packageName = packageZclField;
            }
            ImportsAdd(packageRoot + packageName + "." + typeName);
        }

        protected string GetZclClusterCommandPackage(ZigBeeXmlCluster cluster)
        {
            if (cluster.Name.StartsWith("ZDO"))
            {
                return packageRoot + packageZdpCommand;
            }
            else
            {
                return packageRoot + packageZclProtocolCommand + "."
                        + StringToLowerCamelCase(cluster.Name).Replace("_", "").ToLower();
            }
        }
    }
}