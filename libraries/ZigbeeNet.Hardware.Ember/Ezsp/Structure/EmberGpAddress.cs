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
    using ZigBeeNet.Hardware.Ember.Internal.Serializer;
    using ZigBeeNet.Hardware.Ember.Ezsp.Structure;
    
    
    /// <summary>
    /// Class to implement the Ember Structure " EmberGpAddress ".
    /// A GP address structure.
    /// </summary>
    public class EmberGpAddress
    {
        
        /// <summary>
        ///  The GPD's EUI64.
        /// </summary>
        private IeeeAddress _gpdIeeeAddress;
        
        /// <summary>
        ///  The GPD's source ID.
        /// </summary>
        private int _sourceId;
        
        /// <summary>
        ///  The GPD Application ID.
        /// </summary>
        private EmberGpApplicationId _applicationId;
        
        /// <summary>
        ///  The GPD endpoint.
        /// </summary>
        private int _endpoint;
        
        public EmberGpAddress()
        {
        }
        
        public EmberGpAddress(EzspDeserializer deserializer)
        {
            Deserialize(deserializer);
        }
        
        /// <summary>
        /// The gpdIeeeAddress to set as <see cref="EmberEUI64"/> </summary>
        public void SetGpdIeeeAddress(IeeeAddress gpdIeeeAddress)
        {
            _gpdIeeeAddress = gpdIeeeAddress;
        }
        
        /// <summary>
        /// The sourceId to set as <see cref="uint32_t"/> </summary>
        public void SetSourceId(int sourceId)
        {
            _sourceId = sourceId;
        }
        
        /// <summary>
        /// The applicationId to set as <see cref="EmberGpApplicationId"/> </summary>
        public void SetApplicationId(EmberGpApplicationId applicationId)
        {
            _applicationId = applicationId;
        }
        
        /// <summary>
        /// The endpoint to set as <see cref="uint8_t"/> </summary>
        public void SetEndpoint(int endpoint)
        {
            _endpoint = endpoint;
        }
        
        /// <summary>
        ///  The GPD's EUI64.
        /// Return the gpdIeeeAddress as <see cref="IeeeAddress"/>
        /// </summary>
        public IeeeAddress GetGpdIeeeAddress()
        {
            return _gpdIeeeAddress;
        }
        
        /// <summary>
        ///  The GPD's source ID.
        /// Return the sourceId as <see cref="System.Int32"/>
        /// </summary>
        public int GetSourceId()
        {
            return _sourceId;
        }
        
        /// <summary>
        ///  The GPD Application ID.
        /// Return the applicationId as <see cref="EmberGpApplicationId"/>
        /// </summary>
        public EmberGpApplicationId GetApplicationId()
        {
            return _applicationId;
        }
        
        /// <summary>
        ///  The GPD endpoint.
        /// Return the endpoint as <see cref="System.Int32"/>
        /// </summary>
        public int GetEndpoint()
        {
            return _endpoint;
        }
        
        /// <summary>
        /// Serialise the contents of the EZSP structure. </summary>
        public int[] Serialize(EzspSerializer serializer)
        {
            serializer.SerializeEmberEui64(_gpdIeeeAddress);
            serializer.SerializeUInt32(_sourceId);
            serializer.SerializeEmberGpApplicationId(_applicationId);
            serializer.SerializeUInt8(_endpoint);
            return serializer.GetPayload();
        }
        
        /// <summary>
        /// Deserialise the contents of the EZSP structure. </summary>
        public void Deserialize(EzspDeserializer deserializer)
        {
            _gpdIeeeAddress = deserializer.DeserializeEmberEui64();
            _sourceId = deserializer.DeserializeUInt32();
            _applicationId = deserializer.DeserializeEmberGpApplicationId();
            _endpoint = deserializer.DeserializeUInt8();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EmberGpAddress [gpdIeeeAddress=");
            builder.Append(_gpdIeeeAddress);
            builder.Append(", sourceId=");
            builder.Append(string.Format("0x{0:X08}", _sourceId));
            builder.Append(", applicationId=");
            builder.Append(_applicationId);
            builder.Append(", endpoint=");
            builder.Append(_endpoint);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
