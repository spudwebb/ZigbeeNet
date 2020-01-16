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
    /// Class to implement the Ember EZSP command " setManufacturerCode ".
    /// Sets the manufacturer code to the specified value. The manufacturer code is one of the fields
    /// of the node descriptor.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspSetManufacturerCodeResponse : EzspFrameResponse
    {
        
        public const int FRAME_ID = 21;
        
        public EzspSetManufacturerCodeResponse(int[] inputBuffer) : 
                base(inputBuffer)
        {
        }
        
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
