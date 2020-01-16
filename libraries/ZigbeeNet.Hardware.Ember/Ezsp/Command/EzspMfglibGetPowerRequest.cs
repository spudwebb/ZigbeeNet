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
    /// Class to implement the Ember EZSP command " mfglibGetPower ".
    /// Returns the current radio power setting, as previously set via mfglibSetPower().
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspMfglibGetPowerRequest : EzspFrameRequest
    {
        
        public const int FRAME_ID = 141;
        
        private EzspSerializer _serializer;
        
        public EzspMfglibGetPowerRequest()
        {
            _frameId = FRAME_ID;
            _serializer = new EzspSerializer();
        }
        
        /// <summary>
        /// Method for serializing the command fields </summary>
        public override int[] Serialize()
        {
            SerializeHeader(_serializer);
            return _serializer.GetPayload();
        }
        
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
