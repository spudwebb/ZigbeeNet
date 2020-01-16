//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:3.0.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZigBeeNet.Hardware.Ember.Ezsp.Command
{
    using ZigBeeNet.Hardware.Ember.Internal.Serializer;
    
    
    /// <summary>
    /// Class to implement the Ember EZSP command " incomingRouteRecordHandler ".
    /// Reports the arrival of a route record command frame.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspIncomingRouteRecordHandler : EzspFrameResponse
    {
        
        public const int FRAME_ID = 89;
        
        /// <summary>
        ///  The source of the route record.
        /// </summary>
        private int _source;
        
        /// <summary>
        ///  The EUI64 of the source.
        /// </summary>
        private IeeeAddress _sourceEui;
        
        /// <summary>
        ///  The link quality from the node that last relayed the route record.
        /// </summary>
        private int _lastHopLqi;
        
        /// <summary>
        ///  The energy level (in units of dBm) observed during the reception.
        /// </summary>
        private int _lastHopRssi;
        
        /// <summary>
        ///  The route record. Each relay in the list is an uint16_t node ID.
        /// </summary>
        private int[] _relayList;
        
        public EzspIncomingRouteRecordHandler(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _source = deserializer.DeserializeUInt16();
            _sourceEui = deserializer.DeserializeEmberEui64();
            _lastHopLqi = deserializer.DeserializeUInt8();
            _lastHopRssi = deserializer.DeserializeInt8S();
            int relayCount = deserializer.DeserializeUInt8();
            _relayList = deserializer.DeserializeUInt16Array(relayCount);
        }
        
        /// <summary>
        /// The source to set as <see cref="EmberNodeId"/> </summary>
        public void SetSource(int source)
        {
            _source = source;
        }
        
        /// <summary>
        /// The sourceEui to set as <see cref="EmberEUI64"/> </summary>
        public void SetSourceEui(IeeeAddress sourceEui)
        {
            _sourceEui = sourceEui;
        }
        
        /// <summary>
        /// The lastHopLqi to set as <see cref="uint8_t"/> </summary>
        public void SetLastHopLqi(int lastHopLqi)
        {
            _lastHopLqi = lastHopLqi;
        }
        
        /// <summary>
        /// The lastHopRssi to set as <see cref="int8s"/> </summary>
        public void SetLastHopRssi(int lastHopRssi)
        {
            _lastHopRssi = lastHopRssi;
        }
        
        /// <summary>
        /// The relayList to set as <see cref="uint16_t[]"/> </summary>
        public void SetRelayList(int[] relayList)
        {
            _relayList = relayList;
        }
        
        /// <summary>
        ///  The source of the route record.
        /// Return the source as <see cref="System.Int32"/>
        /// </summary>
        public int GetSource()
        {
            return _source;
        }
        
        /// <summary>
        ///  The EUI64 of the source.
        /// Return the sourceEui as <see cref="IeeeAddress"/>
        /// </summary>
        public IeeeAddress GetSourceEui()
        {
            return _sourceEui;
        }
        
        /// <summary>
        ///  The link quality from the node that last relayed the route record.
        /// Return the lastHopLqi as <see cref="System.Int32"/>
        /// </summary>
        public int GetLastHopLqi()
        {
            return _lastHopLqi;
        }
        
        /// <summary>
        ///  The energy level (in units of dBm) observed during the reception.
        /// Return the lastHopRssi as <see cref="System.Int32"/>
        /// </summary>
        public int GetLastHopRssi()
        {
            return _lastHopRssi;
        }
        
        /// <summary>
        ///  The route record. Each relay in the list is an uint16_t node ID.
        /// Return the relayList as <see cref="System.Int32"/>
        /// </summary>
        public int[] GetRelayList()
        {
            return _relayList;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspIncomingRouteRecordHandler [source=");
            builder.Append(string.Format("0x{0:X04}", _source));
            builder.Append(", sourceEui=");
            builder.Append(_sourceEui);
            builder.Append(", lastHopLqi=");
            builder.Append(_lastHopLqi);
            builder.Append(", lastHopRssi=");
            builder.Append(_lastHopRssi);
            builder.Append(", relayList=");
            if (_relayList == null)
            {
                builder.Append("null");
            }
            else
            {
                for (int cnt = 0
                ; cnt < _relayList.Length; cnt++
                )
                {
                    if (cnt > 0)
                    {
                        builder.Append(' ');
                    }
                    builder.Append(string.Format("0x{0:X04}", _relayList[cnt]));
                }
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}
