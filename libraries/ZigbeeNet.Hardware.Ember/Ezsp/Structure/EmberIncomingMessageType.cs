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
    /// Class to implement the Ember Enumeration <b>EmberIncomingMessageType</b>
    /// </summary>
    public enum EmberIncomingMessageType
    {
        
        /// <summary>
        /// Default unknown value
        /// </summary>
        UNKNOWN = -1,
        
        /// <summary>
        ///  [0] Unicast
        /// </summary>
        EMBER_INCOMING_UNICAST = 0x0000,
        
        /// <summary>
        ///  [1] Unicast reply
        /// </summary>
        EMBER_INCOMING_UNICAST_REPLY = 0x0001,
        
        /// <summary>
        ///  [2] Multicast
        /// </summary>
        EMBER_INCOMING_MULTICAST = 0x0002,
        
        /// <summary>
        ///  [3] Multicast sent by the local device
        /// </summary>
        EMBER_INCOMING_MULTICAST_LOOPBACK = 0x0003,
        
        /// <summary>
        ///  [4] Broadcast
        /// </summary>
        EMBER_INCOMING_BROADCAST = 0x0004,
        
        /// <summary>
        ///  [5] Broadcast sent by the local device.
        /// </summary>
        EMBER_INCOMING_BROADCAST_LOOPBACK = 0x0005,
        
        /// <summary>
        ///  [6] Many to one route request
        /// </summary>
        EMBER_INCOMING_MANY_TO_ONE_ROUTE_REQUEST = 0x0006,
    }
}
