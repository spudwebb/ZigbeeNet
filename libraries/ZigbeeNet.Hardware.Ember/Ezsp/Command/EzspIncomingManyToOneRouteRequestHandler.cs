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
    /// Class to implement the Ember EZSP command " incomingManyToOneRouteRequestHandler ".
    /// A callback indicating that a many-to-one route to the concentrator with the given short and
    /// long id is available for use.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspIncomingManyToOneRouteRequestHandler : EzspFrameResponse
    {
        
        public const int FRAME_ID = 125;
        
        /// <summary>
        ///  The short id of the concentrator.
        /// </summary>
        private int _source;
        
        /// <summary>
        ///  The EUI64 of the concentrator.
        /// </summary>
        private IeeeAddress _longId;
        
        /// <summary>
        ///  The path cost to the concentrator. The cost may decrease as additional route request packets
        /// for this discovery arrive, but the callback is made only once.
        /// </summary>
        private int _cost;
        
        public EzspIncomingManyToOneRouteRequestHandler(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _source = deserializer.DeserializeUInt16();
            _longId = deserializer.DeserializeEmberEui64();
            _cost = deserializer.DeserializeUInt8();
        }
        
        /// <summary>
        /// The source to set as <see cref="EmberNodeId"/> </summary>
        public void SetSource(int source)
        {
            _source = source;
        }
        
        /// <summary>
        /// The longId to set as <see cref="EmberEUI64"/> </summary>
        public void SetLongId(IeeeAddress longId)
        {
            _longId = longId;
        }
        
        /// <summary>
        /// The cost to set as <see cref="uint8_t"/> </summary>
        public void SetCost(int cost)
        {
            _cost = cost;
        }
        
        /// <summary>
        ///  The short id of the concentrator.
        /// Return the source as <see cref="System.Int32"/>
        /// </summary>
        public int GetSource()
        {
            return _source;
        }
        
        /// <summary>
        ///  The EUI64 of the concentrator.
        /// Return the longId as <see cref="IeeeAddress"/>
        /// </summary>
        public IeeeAddress GetLongId()
        {
            return _longId;
        }
        
        /// <summary>
        ///  The path cost to the concentrator. The cost may decrease as additional route request packets
        /// for this discovery arrive, but the callback is made only once.
        /// Return the cost as <see cref="System.Int32"/>
        /// </summary>
        public int GetCost()
        {
            return _cost;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspIncomingManyToOneRouteRequestHandler [source=");
            builder.Append(string.Format("0x{0:X04}", _source));
            builder.Append(", longId=");
            builder.Append(_longId);
            builder.Append(", cost=");
            builder.Append(_cost);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
