//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol
{
    
    
    /// <summary>
    /// Class to implement the XBee command " AT ".
    /// A device sends this frame in response to an AT Command (0x08 or 0x09) frame. Some commands send
    /// back multiple frames; for example, the ND command. 
    /// This class provides methods for processing XBee API commands.
    /// </summary>
    public class XBeeAtResponse : XBeeFrame, IXBeeResponse 
    {
        
        /// <summary>
        /// Response field
        /// The frame Id 
        /// </summary>
        private int _frameId;
        
        /// <summary>
        /// Response field
        /// Command name: two ASCII characters that identify the command. 
        /// </summary>
        private string _atCommand;
        
        /// <summary>
        /// Response field
        /// </summary>
        private CommandStatus _commandStatus;
        
        /// <summary>
        /// Response field
        /// The register data in binary format. If the host sets the register, the device does not return
        /// this field. 
        /// </summary>
        private int[] _commandData;
        
        /// <summary>
        ///  The frame Id 
        /// Return the frameId as <see cref="System.Int32"/>
        /// </summary>
        public int GetFrameId()
        {
            return _frameId;
        }
        
        /// <summary>
        ///  Command name: two ASCII characters that identify the command. 
        /// Return the atCommand as <see cref="System.String"/>
        /// </summary>
        public string GetAtCommand()
        {
            return _atCommand;
        }
        
        /// <summary>
        ///  Return the commandStatus as <see cref="CommandStatus"/>
        /// </summary>
        public CommandStatus GetCommandStatus()
        {
            return _commandStatus;
        }
        
        /// <summary>
        ///  The register data in binary format. If the host sets the register, the device does not return
        /// this field. 
        /// Return the commandData as <see cref="System.Int32"/>
        /// </summary>
        public int[] GetCommandData()
        {
            return _commandData;
        }
        
        /// <summary>
        /// Method for deserializing the fields for the response </summary>
        public void Deserialize(int[] incomingData)
        {
            InitializeDeserializer(incomingData);
            _frameId = DeserializeInt8();
            _atCommand = DeserializeAtCommand();
            _commandStatus = DeserializeCommandStatus();
            if (_commandStatus != CommandStatus.OK || IsComplete())
            {
                    return;
            }
            _commandData = DeserializeData();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder(464);
            builder.Append("XBeeAtResponse [frameId=");
            builder.Append(_frameId);
            builder.Append(", atCommand=");
            builder.Append(_atCommand);
            builder.Append(", commandStatus=");
            builder.Append(_commandStatus);
            if (_commandStatus == CommandStatus.OK)
            {
                builder.Append(", commandData=");
                if (_commandData == null)
                {
                builder.Append("null");
                }
                else
                {
                    for (int cnt = 0
                    ; cnt < _commandData.Length; cnt++
                    )
                    {
                        if (cnt > 0)
                        {
                        builder.Append(' ');
                        }
                        builder.Append(string.Format("0x{0:X2}", _commandData[cnt]));
                    }
                }
            }
            builder.Append(_commandStatus);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
