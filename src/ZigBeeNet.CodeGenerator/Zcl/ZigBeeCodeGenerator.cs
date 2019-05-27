using System;
using System.Collections.Generic;
using System.IO;
using ZigBeeNet.CodeGenerator.Xml;

namespace ZigBeeNet.CodeGenerator.Zcl
{
    public class ZigBeeCodeGenerator
    {
        public static void Generate(string[] args)
        {
            //string sourceRootPath = "target/src/main/java/";
            //string outRootPath = "C:/temp/java";

            //DeleteRecursive(new File(sourceRootPath));

            //DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'");
            //dateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
            //String generatedDate = dateFormat.format(new Date());

            ZigBeeXmlParser zclParser = new ZigBeeXmlParser();
            zclParser.AddFile("./Resources/XXXX_General.xml");

            zclParser.AddFile("./Resources/0000_Basic.xml");
            zclParser.AddFile("./Resources/0001_PowerConfiguration.xml");
            zclParser.AddFile("./Resources/0003_Identify.xml");
            zclParser.AddFile("./Resources/0004_Groups.xml");
            zclParser.AddFile("./Resources/0005_Scenes.xml");
            zclParser.AddFile("./Resources/0006_OnOff.xml");
            zclParser.AddFile("./Resources/0007_OnOffSwitchConfiguration.xml");
            zclParser.AddFile("./Resources/0008_LevelControl.xml");
            zclParser.AddFile("./Resources/0009_Alarms.xml");
            zclParser.AddFile("./Resources/000A_Time.xml");
            zclParser.AddFile("./Resources/000B_RssiLocation.xml");
            zclParser.AddFile("./Resources/000C_AnalogInputBasic.xml");
            zclParser.AddFile("./Resources/000F_BinaryInputBasic.xml");
            zclParser.AddFile("./Resources/0012_MultistateInputBasic.xml");
            zclParser.AddFile("./Resources/0013_MultistateOutputBasic.xml");
            zclParser.AddFile("./Resources/0014_MultistateValueBasic.xml");
            zclParser.AddFile("./Resources/0019_OtaUpgrade.xml");
            zclParser.AddFile("./Resources/0020_PollControl.xml");
                  
            zclParser.AddFile("./Resources/0101_DoorLock.xml");
            zclParser.AddFile("./Resources/0102_WindowCovering.xml");
         
            zclParser.AddFile("./Resources/0201_Thermostat.xml");
            zclParser.AddFile("./Resources/0202_FanControl.xml");
            zclParser.AddFile("./Resources/0203_DehumidificationControl.xml");
            zclParser.AddFile("./Resources/0204_ThermostatUserInterfaceConfiguration.xml");
                    
            zclParser.AddFile("./Resources/0300_ColorControl.xml");
                  
            zclParser.AddFile("./Resources/0400_IlluminanceMeasurement.xml");
            zclParser.AddFile("./Resources/0401_IlluminanceLevelSensing.xml");
            zclParser.AddFile("./Resources/0402_TemperatureMeasurement.xml");
            zclParser.AddFile("./Resources/0403_PressureMeasurement.xml");
            zclParser.AddFile("./Resources/0404_FlowMeasurement.xml");
            zclParser.AddFile("./Resources/0405_RelativeHumidityMeasurement.xml");
            zclParser.AddFile("./Resources/0406_OccupancySensing.xml");
 
            zclParser.AddFile("./Resources/0500_IasZone.xml");
            zclParser.AddFile("./Resources/0501_IasAce.xml");
            zclParser.AddFile("./Resources/0502_IasWd.xml");
            
            zclParser.AddFile("./Resources/0700_Price.xml");
            zclParser.AddFile("./Resources/0701_DemandResponseAndLoadControl.xml");
            zclParser.AddFile("./Resources/0702_Metering.xml");
            zclParser.AddFile("./Resources/0703_Messaging.xml");
            zclParser.AddFile("./Resources/0704_SmartEnergyTunneling.xml");
            zclParser.AddFile("./Resources/0705_Prepayment.xml");
            zclParser.AddFile("./Resources/0800_KeyEstablishment.xml");
                            
            zclParser.AddFile("./Resources/0B04_ElectricalMeasurement.xml");
            zclParser.AddFile("./Resources/0B05_Diagnostics.xml");

            List<ZigBeeXmlCluster> zclClusters = zclParser.ParseClusterConfiguration();

            ZigBeeXmlParser zdoParser = new ZigBeeXmlParser();
            zdoParser.AddFile("./Resources/XXXX_ZigBeeDeviceObject.xml");

            List<ZigBeeXmlCluster> zdoClusters = zdoParser.ParseClusterConfiguration();

            // Process all enums, bitmaps and structures first so we have a consolidated list.
            // We use this later when generating the imports in the cluster and command classes.
            List<ZigBeeXmlCluster> allClusters = new List<ZigBeeXmlCluster>();
            allClusters.AddRange(zclClusters);
            allClusters.AddRange(zdoClusters);
            ZigBeeZclDependencyGenerator typeGenerator = new ZigBeeZclDependencyGenerator(allClusters);
            Dictionary<string, string> zclTypes = typeGenerator.GetDependencyMap();

            var zclClusterGenerator = new ZigBeeZclClusterGenerator(zclClusters, zclTypes);
            var commandGenerator = new ZigBeeZclCommandGenerator(zclClusters, zclTypes);
            //new ZigBeeZclConstantGenerator(zclClusters, generatedDate, zclTypes);
            //new ZigBeeZclStructureGenerator(zclClusters, generatedDate, zclTypes);
            //new ZigBeeZclClusterTypeGenerator(zclClusters, generatedDate, zclTypes);

            var zdoClusterGenerator = new ZigBeeZclCommandGenerator(zdoClusters, zclTypes);

            zclParser = new ZigBeeXmlParser();
            zclParser.AddFile("./Resources/zigbee_constants.xml");
            ZigBeeXmlGlobal globals = zclParser.ParseGlobalConfiguration();

            //foreach (ZigBeeXmlConstant constant in globals.Constants)
            //{
            //    new ZigBeeZclConstantGenerator(constant);
            //}

            //string inRootPath = sourceRootPath.Substring(0, sourceRootPath.Length - 1);
            //compareFiles(inRootPath, outRootPath, "");

            //new ZigBeeZclReadmeGenerator(zclClusters);
        }

//        private static boolean fileCompare(String file1, String file2) throws IOException
//        {
//            File f = new File(file1);
//        if (!f.exists()) {
//            return false;
//        }
//        f = new File(file2);
//        if (!f.exists()) {
//            return false;
//        }

//        BufferedReader reader1 = new BufferedReader(new FileReader(file1));
//        BufferedReader reader2 = new BufferedReader(new FileReader(file2));

//        String line1 = reader1.readLine();
//        String line2 = reader2.readLine();

//        boolean areEqual = true;

//        int lineNum = 1;

//        while (line1 != null || line2 != null) {
//            if (line1 == null || line2 == null) {
//                areEqual = false;

//                break;
//            } else if (!line1.startsWith("@Generated") && !line1.equalsIgnoreCase(line2)) {
//                areEqual = false;

//                break;
//            }

//line1 = reader1.readLine();
//            line2 = reader2.readLine();

//            lineNum++;
//        }

//        if (areEqual) {
//            System.out.println("Two files have same content.");
//        } else {
//            System.out.println("Two files have different content. They differ at line " + lineNum);
//System.out.println("File1 has " + line1 + " and File2 has " + line2 + " at line " + lineNum);
//        }

//        reader1.close();
//        reader2.close();

//        return areEqual;
//    }

//    private static void copyFile(String source, String dest) throws IOException
//{
//    File target = new File(dest);

//File parent = target.getParentFile();
//        if (!parent.exists() && !parent.mkdirs()) {
//            throw new IllegalStateException("Couldn't create dir: " + parent);
//        }

//        if (target.exists()) {
//            Files.delete(new File(dest).toPath());
//        }

//        Files.copy(new File(source).toPath(), new File(dest).toPath());
//    }

//    private static void compareFiles(String inFolder, String outFolder, String folder)
//{
//    File[] files = new File(inFolder + folder).listFiles();
//    for (File file : files)
//    {
//        if (file.isDirectory())
//        {
//            compareFiles(inFolder, outFolder, folder + "/" + file.getName());
//        }
//        else
//        {
//            System.out.println("File: " + folder + "/" + file.getName());
//            try
//            {
//                if (!fileCompare(inFolder + folder + "/" + file.getName(),
//                        outFolder + folder + "/" + file.getName()))
//                {
//                    copyFile(inFolder + folder + "/" + file.getName(), outFolder + folder + "/" + file.getName());
//                    System.out.println("File: " + folder + "/" + file.getName() + " updated");
//                }
//            }
//            catch (IOException e)
//            {
//                e.printStackTrace();
//            }
//        }
//    }
//}

//private static boolean deleteRecursive(File path)
//{
//    boolean ret = true;
//    if (path.isDirectory())
//    {
//        for (File f : path.listFiles())
//        {
//            ret = ret && deleteRecursive(f);
//        }
//    }
//    return ret && path.delete();
//}

}
}