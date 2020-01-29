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
    /// Class to implement the Ember EZSP command " setSourceRoute ".
    /// Supply a source route for the next outgoing message.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspSetSourceRouteRequest : EzspFrameRequest
    {
        
        public const int FRAME_ID = 90;
        
        /// <summary>
        ///  The destination of the source route.
        /// </summary>
        private int _destination;
        
        /// <summary>
        ///  The route record. Each relay in the list is an uint16_t node ID.
        /// </summary>
        private int[] _relayList;
        
        private EzspSerializer _serializer;
        
        public EzspSetSourceRouteRequest()
        {
            _frameId = FRAME_ID;
            _serializer = new EzspSerializer();
        }
        
        /// <summary>
        /// The destination to set as <see cref="EmberNodeId"/> </summary>
        public void SetDestination(int destination)
        {
            _destination = destination;
        }
        
        /// <summary>
        /// The relayList to set as <see cref="uint16_t[]"/> </summary>
        public void SetRelayList(int[] relayList)
        {
            _relayList = relayList;
        }
        
        /// <summary>
        ///  The destination of the source route.
        /// Return the destination as <see cref="System.Int32"/>
        /// </summary>
        public int GetDestination()
        {
            return _destination;
        }
        
        /// <summary>
        ///  The route record. Each relay in the list is an uint16_t node ID.
        /// Return the relayList as <see cref="System.Int32"/>
        /// </summary>
        public int[] GetRelayList()
        {
            return _relayList;
        }
        
        /// <summary>
        /// Method for serializing the command fields </summary>
        public override int[] Serialize()
        {
            SerializeHeader(_serializer);
            _serializer.SerializeUInt16(_destination);
            _serializer.SerializeUInt8(_relayList.Length);
            _serializer.SerializeUInt16Array(_relayList);
            return _serializer.GetPayload();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspSetSourceRouteRequest [destination=");
            builder.Append(string.Format("0x{0:X04}", _destination));
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
