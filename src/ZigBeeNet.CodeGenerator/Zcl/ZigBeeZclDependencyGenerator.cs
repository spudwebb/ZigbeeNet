using System;
using System.Collections.Generic;
using System.IO;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator.Zcl
{
    public class ZigBeeZclDependencyGenerator : ZigBeeBaseClassGenerator
    {
        //private Dictionary<string, string> _dependencies = new Dictionary<string, string>();
        private HashSet<string> _zclTypes = new HashSet<string>();

        public ZigBeeZclDependencyGenerator(List<ZigBeeXmlCluster> clusters)
        {
            _dependencies = new Dictionary<string, string>();

            foreach (ZigBeeXmlCluster cluster in clusters)
            {
                try
                {
                    GenerateZclClusterDependencies(cluster, packageRoot);
                }
                catch (IOException e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e);
                }
            }
        }

        private void GenerateZclClusterDependencies(ZigBeeXmlCluster cluster, string packageRootPrefix)
        {
            if (cluster.Constants != null)
            {
                foreach (ZigBeeXmlConstant constant in cluster.Constants)
                {
                    string packageRoot = packageRootPrefix + packageZclProtocolCommand + "."
                            + StringToLowerCamelCase(cluster.Name).Replace("_", "").ToLower();

                    string className = constant.ClassName;

                    if (_dependencies.ContainsKey(className))//(lastClass != null)
                    {
                        throw new ArgumentException(
                                "Duplicate class definition: " + packageRoot + "." + className + " with " + packageRoot + "." + className);
                    }
                }
            }

            if (cluster.Structures != null)
            {
                foreach (ZigBeeXmlStructure structure in cluster.Structures)
                {
                    string packageRoot = packageRootPrefix + packageZclProtocolCommand + "."
                            + StringToLowerCamelCase(cluster.Name).Replace("_", "").ToLower();

                    string className = structure.ClassName;

                    //String lastClass = dependencies.put(className, packageRoot + "." + className);
                    if (_dependencies.ContainsKey(className)) //(lastClass != null)
                    {
                        throw new ArgumentException(
                                "Duplicate class definition: " + packageRoot + "." + className + " with " + packageRoot + "." + className);
                    }

                    foreach (ZigBeeXmlField field in structure.Fields)
                    {
                        _zclTypes.Add(field.Type);
                    }
                }
            }

            if (cluster.Commands != null)
            {
                foreach (ZigBeeXmlCommand command in cluster.Commands)
                {
                    foreach (ZigBeeXmlField field in command.Fields)
                    {
                        _zclTypes.Add(field.Type);
                    }
                }
            }

            if (cluster.Attributes != null)
            {
                foreach (ZigBeeXmlAttribute attribute in cluster.Attributes)
                {
                    _zclTypes.Add(attribute.Type);
                }
            }

        }

        public Dictionary<string, string> GetDependencyMap()
        {
            return _dependencies;
        }

        public HashSet<string> GetZclTypeMap()
        {
            return _zclTypes;
        }
    }
}