using System;
using System.Collections.Generic;
using System.IO;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator.Zcl
{

    public class ZigBeeBaseFieldGenerator : ZigBeeBaseClassGenerator
    {
        private const string OPERATOR_LOGIC_AND = "LOGIC_AND";
        private const string OPERATOR_EQUAL = "EQUAL";
        private const string OPERATOR_NOT_EQUAL = "NOT_EQUAL";
        private const string OPERATOR_GREATER_THAN = "GREATER_THAN";
        private const string OPERATOR_GREATER_THAN_OR_EQUAL = "GREATER_THAN_OR_EQUAL";
        private const string OPERATOR_LESS_THAN = "LESS_THAN";
        private const string OPERATOR_LESS_THAN_OR_EQUAL = "LESS_THAN_OR_EQUAL";

        protected void GenerateFields(TextWriter @out, string parentClass, string className, List<ZigBeeXmlField> fields, List<string> reservedFields)
        {
            foreach (ZigBeeXmlField field in fields)
            {
                if (reservedFields.Contains(StringToLowerCamelCase(field.Name)))
                {
                    continue;
                }

                if (GetAutoSized(fields, StringToLowerCamelCase(field.Name)) != null)
                {
                    continue;
                }

                @out.WriteLine();
                @out.WriteLine("    /**");
                @out.WriteLine("     * Gets " + field.Name + ".");
                if (field.Description.Count != 0)
                {
                    @out.WriteLine("     * ");
                    OutputWithLinebreak(@out, "    ", field.Description);
                }
                @out.WriteLine("     *");
                @out.WriteLine("     * @return the " + field.Name);
                @out.WriteLine("     */");
                @out.WriteLine("    public " + GetDataTypeClass(field) + " Get" + StringToUpperCamelCase(field.Name) + "() {");
                @out.WriteLine("        return " + StringToLowerCamelCase(field.Name) + ";");
                @out.WriteLine("    }");
                @out.WriteLine();
                @out.WriteLine("    /**");
                @out.WriteLine("     * Sets " + field.Name + ".");
                if (field.Description.Count != 0)
                {
                    @out.WriteLine("     * ");
                    OutputWithLinebreak(@out, "    ", field.Description);
                }
                @out.WriteLine("     *");
                @out.WriteLine("     * @param " + StringToLowerCamelCase(field.Name) + " the " + field.Name);
                @out.WriteLine("     */");
                @out.WriteLine("    public void set" + StringToUpperCamelCase(field.Name) + "( " + GetDataTypeClass(field)
                        + " " + StringToLowerCamelCase(field.Name) + ") {");
                @out.WriteLine("        this." + StringToLowerCamelCase(field.Name) + " = "
                        + StringToLowerCamelCase(field.Name) + ";");
                @out.WriteLine("    }");

            }

            if (fields.Count > 0)
            {
                @out.WriteLine();
                //@out.WriteLine("    @Override");
                @out.WriteLine("    public override void Serialize(ZclFieldSerializer serializer) {");
                if (parentClass.StartsWith("Zdo"))
                {
                    @out.WriteLine("        base.Serialize(serializer);");
                    @out.WriteLine();
                }

                foreach (ZigBeeXmlField field in fields)
                {
                    // if (reservedFields.contains(StringToLowerCamelCase(field.Name))) {
                    // continue;
                    // }

                    // Rules...
                    // if listSizer == null, then just output the field
                    // if listSizer != null and contains && then check the param bit
                    if (GetAutoSized(fields, StringToLowerCamelCase(field.Name)) != null)
                    {
                        ZigBeeXmlField sizedField = GetAutoSized(fields, StringToLowerCamelCase(field.Name));
                        @out.WriteLine("        serializer.serialize(" + StringToLowerCamelCase(sizedField.Name)
                                + ".Count(), ZclDataType." + field.Type + ");");

                        continue;
                    }

                    if (field.Sizer != null)
                    {
                        @out.WriteLine("        for (int cnt = 0; cnt < " + StringToLowerCamelCase(field.Name)
                                + ".Count(); cnt++) {");
                        @out.WriteLine("            serializer.serialize(" + StringToLowerCamelCase(field.Name)
                                + "[cnt], ZclDataType." + field.Type + ");");
                        @out.WriteLine("        }");
                    }
                    else if (field.Condition != null)
                    {
                        if (field.Condition.Value.Equals("statusResponse"))
                        {
                            // Special case where a ZclStatus may be sent, or, a list of results.
                            // This checks for a single response
                            @out.WriteLine("        if (status == ZclStatus.SUCCESS) {");
                            @out.WriteLine("            serializer.Serialize(status, ZclDataType.ZCL_STATUS);");
                            @out.WriteLine("            return;");
                            @out.WriteLine("        }");
                            continue;
                        }
                        else if (field.Condition.Operator.Equals(OPERATOR_LOGIC_AND))
                        {
                            @out.WriteLine(
                                    "        if ((" + field.Condition.Field + " & " + field.Condition.Value + ") != 0) {");
                        }
                        else
                        {
                            @out.WriteLine("        if (" + field.Condition.Field + " " + GetOperator(field.Condition.Operator)
                                    + " " + field.Condition.Value + ") {");
                        }
                        @out.WriteLine("            serializer.Serialize(" + StringToLowerCamelCase(field.Name)
                                + ", ZclDataType." + field.Type + ");");
                        @out.WriteLine("        }");
                    }
                    else
                    {
                        if (field.Type != null && !string.IsNullOrEmpty(field.Type))
                        {
                            @out.WriteLine("        serializer.Serialize(" + StringToLowerCamelCase(field.Name)
                                    + ", ZclDataType." + field.Type + ");");
                        }
                        else
                        {
                            @out.WriteLine("        " + StringToLowerCamelCase(field.Name) + ".Serialize(serializer);");
                        }
                    }
                }
                @out.WriteLine("    }");

                @out.WriteLine();
                //@out.WriteLine("    @Override");
                @out.WriteLine("    public overrid void Deserialize(ZclFieldDeserializer deserializer) {");
                if (parentClass.StartsWith("Zdo"))
                {
                    @out.WriteLine("        base.Deserialize(deserializer);");
                    @out.WriteLine();
                }
                bool first = true;
                foreach (ZigBeeXmlField field in fields)
                {
                    if (field.Sizer != null)
                    {
                        if (first)
                        {
                            @out.WriteLine("        // Create lists");
                            first = false;
                        }
                        @out.WriteLine("        " + StringToLowerCamelCase(field.Name) + " = new Array"+ GetDataTypeClass(field) + "();");
                    }
                }
                if (first == false)
                {
                    @out.WriteLine();
                }
                foreach (ZigBeeXmlField field in fields)
                {
                    // if (reservedFields.contains(StringToLowerCamelCase(field.Name))) {
                    // continue;
                    // }

                    if (field.CompleteOnZero)
                    {
                        @out.WriteLine("        if (deserializer.IsEndOfStream()) {");
                        @out.WriteLine("            return;");
                        @out.WriteLine("        }");
                    }
                    if (GetAutoSized(fields, StringToLowerCamelCase(field.Name)) != null)
                    {
                        @out.WriteLine(
                                "        ushort " + StringToLowerCamelCase(field.Name) + " = (" + GetDataTypeClass(field)
                                        + ") deserializer.Deserialize(ZclDataType." + field.Type + ");");
                        continue;
                    }

                    if (field.Sizer != null)
                    {
                        var startIndex = GetDataTypeClass(field).IndexOf('<') + 1;
                        var length = GetDataTypeClass(field).IndexOf('>') - startIndex;

                        string dataType = GetDataTypeClass(field).Substring(startIndex, length);

                        @out.WriteLine("        if (" + field.Sizer + " != null) {");
                        @out.WriteLine("            for (int cnt = 0; cnt < " + field.Sizer + "; cnt++) {");
                        @out.WriteLine("                " + StringToLowerCamelCase(field.Name) + ".Add((" + dataType
                                + ") deserializer.Deserialize(" + "ZclDataType." + field.Type + "));");
                        @out.WriteLine("            }");
                        @out.WriteLine("        }");
                    }
                    else if (field.Condition != null)
                    {
                        if (field.Condition.Value.Equals("statusResponse"))
                        {
                            // Special case where a ZclStatus may be sent, or, a list of results.
                            // This checks for a single response
                            @out.WriteLine("        if (deserializer.GetRemainingLength() == 1) {");
                            @out.WriteLine(
                                    "            status = (ZclStatus) deserializer.Deserialize(ZclDataType.ZCL_STATUS);");
                            @out.WriteLine("            return;");
                            @out.WriteLine("        }");
                            continue;
                        }
                        else if (field.Condition.Operator.Equals(OPERATOR_LOGIC_AND))
                        {
                            @out.WriteLine(
                                    "        if ((" + field.Condition.Field + " & " + field.Condition.Value + ") != 0) {");
                        }
                        else
                        {
                            @out.WriteLine("        if (" + field.Condition.Field + " " + GetOperator(field.Condition.Operator)
                                    + " " + field.Condition.Value + ") {");
                        }
                        @out.WriteLine("            " + StringToLowerCamelCase(field.Name) + " = (" + GetDataTypeClass(field)
                                + ") deserializer.Deserialize(" + "ZclDataType." + field.Type + ");");
                        @out.WriteLine("        }");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(field.Type))
                        {
                            @out.WriteLine("        " + StringToLowerCamelCase(field.Name) + " = (" + GetDataTypeClass(field)
                                    + ") deserializer.Deserialize(" + "ZclDataType." + field.Type + ");");
                        }
                        else
                        {
                            @out.WriteLine("        " + StringToLowerCamelCase(field.Name) + " = new "
                                    + GetDataTypeClass(field) + "();");
                            @out.WriteLine("        " + StringToLowerCamelCase(field.Name) + ".Deserialize(deserializer);");
                        }
                    }

                    if (field.Name.ToLower().Equals("status") && field.Type.Equals("ZDO_STATUS"))
                    {
                        @out.WriteLine("        if (status != ZdoStatus.SUCCESS) {");
                        @out.WriteLine("            // Don't read the full response if we have an error");
                        @out.WriteLine("            return;");
                        @out.WriteLine("        }");
                    }
                }
                @out.WriteLine("    }");
            }
        }

        protected void GenerateToString(TextWriter @out, string className, List<ZigBeeXmlField> fields, List<string> reservedFields)
        {
            int fieldLen = 0;
            foreach (ZigBeeXmlField field in fields)
            {
                fieldLen += StringToLowerCamelCase(field.Name).Length + 20;
            }

            @out.WriteLine();
            //@out.WriteLine("    @Override");
            @out.WriteLine("    public override string ToString() {");
            @out.WriteLine("        StringBuilder builder = new StringBuilder(" + (className.Length + 3 + fieldLen)
                    + ");");

            @out.WriteLine("        builder.Append(\"" + className + " [\");");
            @out.WriteLine("        builder.Append(base.ToString());");
            foreach (ZigBeeXmlField field in fields)
            {
                // if (reservedFields.contains(stringToLowerCamelCase(field.name))) {
                // continue;
                // }
                if (GetAutoSized(fields, StringToLowerCamelCase(field.Name)) != null)
                {
                    continue;
                }
                @out.WriteLine("        builder.Append(\", " + StringToLowerCamelCase(field.Name) + "=\");");
                @out.WriteLine("        builder.Append(" + StringToLowerCamelCase(field.Name) + ");");
            }
            @out.WriteLine("        builder.Append(\']\');");
            @out.WriteLine("        return builder.ToString();");
            @out.WriteLine("    }");
        }

        private string GetOperator(string @operator)
        {
            switch (@operator)
            {
                case OPERATOR_LOGIC_AND:
                    return "&&";
                case OPERATOR_EQUAL:
                    return "==";
                case OPERATOR_NOT_EQUAL:
                    return "!=";
                case OPERATOR_GREATER_THAN:
                    return ">";
                case OPERATOR_GREATER_THAN_OR_EQUAL:
                    return ">=";
                case OPERATOR_LESS_THAN:
                    return "<";
                case OPERATOR_LESS_THAN_OR_EQUAL:
                    return "<";
                default:
                    return "<<Unknown " + @operator +">>";
            }
        }

        protected ZigBeeXmlField GetAutoSized(List<ZigBeeXmlField> fields, string name)
        {
            foreach (ZigBeeXmlField field in fields)
            {
                if (field.Sizer != null)
                {
                    Console.WriteLine();
                }
                if (name.Equals(field.Sizer))
                {
                    return field;
                }
            }
            return null;
        }
    }
}