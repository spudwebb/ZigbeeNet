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
    /// Class to implement the Ember EZSP command " setRadioPower ".
    /// Sets the radio output power at which a node is operating. Ember radios have discrete power
    /// settings. For a list of available power settings, see the technical specification for the RF
    /// communication module in your Developer Kit.
    /// * <p>
    /// * <b>Note:</b> Care should be taken when using this API on a
    /// running network, as it will directly impact the established link qualities neighboring
    /// nodes have with the node on which it is called. This can lead to disruption of existing routes
    /// and erratic network behavior.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspSetRadioPowerResponse : EzspFrameResponse
    {
        
        public const int FRAME_ID = 153;
        
        /// <summary>
        ///  An EmberStatus value indicating the success or failure of the command.
        /// </summary>
        private EmberStatus _status;
        
        public EzspSetRadioPowerResponse(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _status = deserializer.DeserializeEmberStatus();
        }
        
        /// <summary>
        /// The status to set as <see cref="EmberStatus"/> </summary>
        public void SetStatus(EmberStatus status)
        {
            _status = status;
        }
        
        /// <summary>
        ///  An EmberStatus value indicating the success or failure of the command.
        /// Return the status as <see cref="EmberStatus"/>
        /// </summary>
        public EmberStatus GetStatus()
        {
            return _status;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspSetRadioPowerResponse [status=");
            builder.Append(_status);
            builder.Append(']');
            return builder.ToString();
        }
    }
}