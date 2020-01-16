//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:3.0.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZigBeeNet.Hardware.Ember.Ezsp.Structure
{
    
    
    /// <summary>
    /// Class to implement the Ember Enumeration <b>EmberMacPassthroughType</b>
    /// </summary>
    public enum EmberMacPassthroughType
    {
        
        /// <summary>
        /// Default unknown value
        /// </summary>
        UNKNOWN = -1,
        
        /// <summary>
        ///  [0] No MAC passthrough messages.
        /// </summary>
        EMBER_MAC_PASSTHROUGH_NONE = 0x0000,
        
        /// <summary>
        ///  [1] SE InterPAN messages.
        /// </summary>
        EMBER_MAC_PASSTHROUGH_SE_INTERPAN = 0x0001,
        
        /// <summary>
        ///  [2] Legacy EmberNet messages.
        /// </summary>
        EMBER_MAC_PASSTHROUGH_EMBERNET = 0x0002,
        
        /// <summary>
        ///  [4] Legacy EmberNet messages filtered by their source address.
        /// </summary>
        EMBER_MAC_PASSTHROUGH_EMBERNET_SOURCE = 0x0004,
    }
}
