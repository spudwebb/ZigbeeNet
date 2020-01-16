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
    using ZigBeeNet.Hardware.Ember.Ezsp.Structure;
    
    
    /// <summary>
    /// Class to implement the Ember EZSP command " getChildData ".
    /// Returns information about a child of the local node.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspGetChildDataResponse : EzspFrameResponse
    {
        
        public const int FRAME_ID = 74;
        
        /// <summary>
        ///  EMBER_SUCCESS if there is a child at index. EMBER_NOT_JOINED if there is no child at index.
        /// </summary>
        private EmberStatus _status;
        
        /// <summary>
        ///  The node ID of the child.
        /// </summary>
        private int _childId;
        
        /// <summary>
        ///  The EUI64 of the child
        /// </summary>
        private IeeeAddress _childEui64;
        
        /// <summary>
        ///  The EmberNodeType value for the child.
        /// </summary>
        private EmberNodeType _childType;
        
        public EzspGetChildDataResponse(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _status = deserializer.DeserializeEmberStatus();
            _childId = deserializer.DeserializeUInt16();
            _childEui64 = deserializer.DeserializeEmberEui64();
            _childType = deserializer.DeserializeEmberNodeType();
        }
        
        /// <summary>
        /// The status to set as <see cref="EmberStatus"/> </summary>
        public void SetStatus(EmberStatus status)
        {
            _status = status;
        }
        
        /// <summary>
        /// The childId to set as <see cref="EmberNodeId"/> </summary>
        public void SetChildId(int childId)
        {
            _childId = childId;
        }
        
        /// <summary>
        /// The childEui64 to set as <see cref="EmberEUI64"/> </summary>
        public void SetChildEui64(IeeeAddress childEui64)
        {
            _childEui64 = childEui64;
        }
        
        /// <summary>
        /// The childType to set as <see cref="EmberNodeType"/> </summary>
        public void SetChildType(EmberNodeType childType)
        {
            _childType = childType;
        }
        
        /// <summary>
        ///  EMBER_SUCCESS if there is a child at index. EMBER_NOT_JOINED if there is no child at index.
        /// Return the status as <see cref="EmberStatus"/>
        /// </summary>
        public EmberStatus GetStatus()
        {
            return _status;
        }
        
        /// <summary>
        ///  The node ID of the child.
        /// Return the childId as <see cref="System.Int32"/>
        /// </summary>
        public int GetChildId()
        {
            return _childId;
        }
        
        /// <summary>
        ///  The EUI64 of the child
        /// Return the childEui64 as <see cref="IeeeAddress"/>
        /// </summary>
        public IeeeAddress GetChildEui64()
        {
            return _childEui64;
        }
        
        /// <summary>
        ///  The EmberNodeType value for the child.
        /// Return the childType as <see cref="EmberNodeType"/>
        /// </summary>
        public EmberNodeType GetChildType()
        {
            return _childType;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspGetChildDataResponse [status=");
            builder.Append(_status);
            builder.Append(", childId=");
            builder.Append(string.Format("0x{0:X04}", _childId));
            builder.Append(", childEui64=");
            builder.Append(_childEui64);
            builder.Append(", childType=");
            builder.Append(_childType);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
