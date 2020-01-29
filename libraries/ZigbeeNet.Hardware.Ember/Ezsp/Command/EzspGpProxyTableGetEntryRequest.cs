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
    /// Class to implement the Ember EZSP command " gpProxyTableGetEntry ".
    /// Retrieves the proxy table entry stored at the passed index.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspGpProxyTableGetEntryRequest : EzspFrameRequest
    {
        
        public const int FRAME_ID = 200;
        
        /// <summary>
        ///  The index of the requested proxy table entry.
        /// </summary>
        private int _proxyIndex;
        
        private EzspSerializer _serializer;
        
        public EzspGpProxyTableGetEntryRequest()
        {
            _frameId = FRAME_ID;
            _serializer = new EzspSerializer();
        }
        
        /// <summary>
        /// The proxyIndex to set as <see cref="uint8_t"/> </summary>
        public void SetProxyIndex(int proxyIndex)
        {
            _proxyIndex = proxyIndex;
        }
        
        /// <summary>
        ///  The index of the requested proxy table entry.
        /// Return the proxyIndex as <see cref="System.Int32"/>
        /// </summary>
        public int GetProxyIndex()
        {
            return _proxyIndex;
        }
        
        /// <summary>
        /// Method for serializing the command fields </summary>
        public override int[] Serialize()
        {
            SerializeHeader(_serializer);
            _serializer.SerializeUInt8(_proxyIndex);
            return _serializer.GetPayload();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspGpProxyTableGetEntryRequest [proxyIndex=");
            builder.Append(_proxyIndex);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
