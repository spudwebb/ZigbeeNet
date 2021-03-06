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
    /// Class to implement the Ember EZSP command " networkState ".
    /// Returns a value indicating whether the node is joining, joined to, or leaving a network.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspNetworkStateResponse : EzspFrameResponse
    {
        
        public const int FRAME_ID = 24;
        
        /// <summary>
        ///  An EmberNetworkStatus value indicating the current join status.
        /// </summary>
        private EmberNetworkStatus _status;
        
        public EzspNetworkStateResponse(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _status = deserializer.DeserializeEmberNetworkStatus();
        }
        
        /// <summary>
        /// The status to set as <see cref="EmberNetworkStatus"/> </summary>
        public void SetStatus(EmberNetworkStatus status)
        {
            _status = status;
        }
        
        /// <summary>
        ///  An EmberNetworkStatus value indicating the current join status.
        /// Return the status as <see cref="EmberNetworkStatus"/>
        /// </summary>
        public EmberNetworkStatus GetStatus()
        {
            return _status;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspNetworkStateResponse [status=");
            builder.Append(_status);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
