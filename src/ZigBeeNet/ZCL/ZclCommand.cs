using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    public abstract class ZclCommand : ZigBeeCommand
    {

        /// <summary>
        /// True if this is a generic command
        /// </summary>
        public bool GenericCommand { get; set; }

        /// <summary>
        /// The command ID
        /// </summary>
        public byte CommandId { get; set; }

        /// <summary>
        /// The manufacturer code; this value needs to be set for manufacturer-specific commands. If this value is null, then
        /// the command is assumed to be not manufacturer-specific.
        /// 
        /// Examples for manufacturer-specific commands are:
        /// 
        /// Commands from a manufacturer-specific cluster
        /// Manufacturer-specific commands added to a non-manufacturer-specific cluster
        /// Generic commands that target manufacturer-specific attributes
        /// </summary>
        public int? ManufacturerCode { get; set; } = null;

        /// <summary>
        /// The command direction for this command.
        /// 
        /// If this command is to be sent to the server, this will return true.
        /// If this command is to be sent from the server, this will return false.
        /// </summary>
        public ZclCommandDirection CommandDirection { get; set; }


        /// <summary>
        /// Gets whether the manufacturer-specific bit needs to be set for this command.
        ///
        /// <returns>whether the manufacturer-specific bit needs to be set for this command</returns>
        /// </summary>
        public bool IsManufacturerSpecific()
        {
            return ManufacturerCode != null;
        }



        public override string ToString()
        {
            ushort resolvedClusterId = ClusterId;
            ZclClusterType clusterType = ZclClusterType.GetValueById(resolvedClusterId);

            StringBuilder builder = new StringBuilder()
                .Append(ZclClusterType.GetValueById(ClusterId).Label)
                .Append(clusterType != null ? clusterType.Label : resolvedClusterId.ToString())
                .Append(": ")
                .Append(base.ToString());

            return builder.ToString();
        }
    }
}
