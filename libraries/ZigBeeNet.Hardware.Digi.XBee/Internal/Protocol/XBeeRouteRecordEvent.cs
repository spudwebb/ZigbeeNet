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
    /// Class to implement the XBee command " Route Record ".
    /// The route record indicator is received whenever a device sends a ZigBee route record
    /// command. The device uses the route record indicator with many-to-one routing to create
    /// source routes for devices in a network. 
    /// This class provides methods for processing XBee API commands.
    /// </summary>
    public class XBeeRouteRecordEvent : XBeeFrame, IXBeeEvent
    {
        
        /// <summary>
        /// Response field
        /// MSB first, LSB last. The 64-bit address of the device that initiated the route record. 
        /// </summary>
        private IeeeAddress _ieeeAddress;
        
        /// <summary>
        /// Response field
        /// The sender's 16-bit address.
        /// </summary>
        private int _networkAddress;
        
        /// <summary>
        /// Response field
        /// </summary>
        private ReceiveOptions _receiveOptions;
        
        /// <summary>
        /// Response field
        /// The number of addresses in the source route (excluding source and destination). 
        /// </summary>
        private int[] _addressList;
        
        /// <summary>
        ///  MSB first, LSB last. The 64-bit address of the device that initiated the route record. 
        /// Return the ieeeAddress as <see cref="IeeeAddress"/>
        /// </summary>
        public IeeeAddress GetIeeeAddress()
        {
            return _ieeeAddress;
        }
        
        /// <summary>
        ///  The sender's 16-bit address.
        /// Return the networkAddress as <see cref="System.Int32"/>
        /// </summary>
        public int GetNetworkAddress()
        {
            return _networkAddress;
        }
        
        /// <summary>
        ///  Return the receiveOptions as <see cref="ReceiveOptions"/>
        /// </summary>
        public ReceiveOptions GetReceiveOptions()
        {
            return _receiveOptions;
        }
        
        /// <summary>
        ///  The number of addresses in the source route (excluding source and destination). 
        /// Return the addressList as <see cref="System.Int32"/>
        /// </summary>
        public int[] GetAddressList()
        {
            return _addressList;
        }
        
        /// <summary>
        /// Method for deserializing the fields for the response </summary>
        public void Deserialize(int[] incomingData)
        {
            InitializeDeserializer(incomingData);
            _ieeeAddress = DeserializeIeeeAddress();
            _networkAddress = DeserializeInt16();
            _receiveOptions = DeserializeReceiveOptions();
            int numberOfAddresses = DeserializeInt8();
            _addressList = DeserializeInt16Array(numberOfAddresses);
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder(560);
            builder.Append("XBeeRouteRecordEvent [ieeeAddress=");
            builder.Append(_ieeeAddress);
            builder.Append(", networkAddress=");
            builder.Append(_networkAddress);
            builder.Append(", receiveOptions=");
            builder.Append(_receiveOptions);
            builder.Append(", addressList=");
            if (_addressList == null)
            {
                builder.Append("null");
            }
            else
            {
                for (int cnt = 0
                ; cnt < _addressList.Length; cnt++
                )
                {
                    if (cnt > 0)
                    {
                        builder.Append(' ');
                    }
                    builder.Append(string.Format("0x{0:X4}", _addressList[cnt]));
                }
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}
