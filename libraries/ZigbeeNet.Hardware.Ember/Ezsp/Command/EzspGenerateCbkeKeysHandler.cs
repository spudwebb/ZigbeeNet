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
    /// Class to implement the Ember EZSP command " generateCbkeKeysHandler ".
    /// A callback by the Crypto Engine indicating that a new ephemeral public/private key pair has
    /// been generated. The pub- lic/private key pair is stored on the NCP, but only the associated
    /// public key is returned to the host. The node's associated certificate is also returned.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspGenerateCbkeKeysHandler : EzspFrameResponse
    {
        
        public const int FRAME_ID = 158;
        
        /// <summary>
        ///  The result of the CBKE operation.
        /// </summary>
        private EmberStatus _status;
        
        /// <summary>
        ///  The generated ephemeral public key.
        /// </summary>
        private EmberPublicKeyData _ephemeralPublicKey;
        
        public EzspGenerateCbkeKeysHandler(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _status = deserializer.DeserializeEmberStatus();
            _ephemeralPublicKey = deserializer.DeserializeEmberPublicKeyData();
        }
        
        /// <summary>
        /// The status to set as <see cref="EmberStatus"/> </summary>
        public void SetStatus(EmberStatus status)
        {
            _status = status;
        }
        
        /// <summary>
        /// The ephemeralPublicKey to set as <see cref="EmberPublicKeyData"/> </summary>
        public void SetEphemeralPublicKey(EmberPublicKeyData ephemeralPublicKey)
        {
            _ephemeralPublicKey = ephemeralPublicKey;
        }
        
        /// <summary>
        ///  The result of the CBKE operation.
        /// Return the status as <see cref="EmberStatus"/>
        /// </summary>
        public EmberStatus GetStatus()
        {
            return _status;
        }
        
        /// <summary>
        ///  The generated ephemeral public key.
        /// Return the ephemeralPublicKey as <see cref="EmberPublicKeyData"/>
        /// </summary>
        public EmberPublicKeyData GetEphemeralPublicKey()
        {
            return _ephemeralPublicKey;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspGenerateCbkeKeysHandler [status=");
            builder.Append(_status);
            builder.Append(", ephemeralPublicKey=");
            builder.Append(_ephemeralPublicKey);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
