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
    /// Class to implement the Ember EZSP command " dGpSend ".
    /// Adds/removes an entry from the GP Tx Queue.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspDGpSendRequest : EzspFrameRequest
    {
        
        public const int FRAME_ID = 198;
        
        /// <summary>
        ///  The action to perform on the GP TX queue (true to add, false to remove).
        /// </summary>
        private bool _action;
        
        /// <summary>
        ///  Whether to use ClearChannelAssessment when transmitting the GPDF.
        /// </summary>
        private bool _useCca;
        
        /// <summary>
        ///  The Address of the destination GPD.
        /// </summary>
        private EmberGpAddress _addr;
        
        /// <summary>
        ///  The GPD command ID to send.
        /// </summary>
        private int _gpdCommandId;
        
        /// <summary>
        ///  The GP command payload.
        /// </summary>
        private int[] _gpdAsdu;
        
        /// <summary>
        ///  The handle to refer to the GPDF.
        /// </summary>
        private int _gpepHandle;
        
        /// <summary>
        ///  How long to keep the GPDF in the TX Queue.
        /// </summary>
        private int _gpTxQueueEntryLifetimeMs;
        
        private EzspSerializer _serializer;
        
        public EzspDGpSendRequest()
        {
            _frameId = FRAME_ID;
            _serializer = new EzspSerializer();
        }
        
        /// <summary>
        /// The action to set as <see cref="bool"/> </summary>
        public void SetAction(bool action)
        {
            _action = action;
        }
        
        /// <summary>
        /// The useCca to set as <see cref="bool"/> </summary>
        public void SetUseCca(bool useCca)
        {
            _useCca = useCca;
        }
        
        /// <summary>
        /// The addr to set as <see cref="EmberGpAddress"/> </summary>
        public void SetAddr(EmberGpAddress addr)
        {
            _addr = addr;
        }
        
        /// <summary>
        /// The gpdCommandId to set as <see cref="uint8_t"/> </summary>
        public void SetGpdCommandId(int gpdCommandId)
        {
            _gpdCommandId = gpdCommandId;
        }
        
        /// <summary>
        /// The gpdAsdu to set as <see cref="uint8_t[]"/> </summary>
        public void SetGpdAsdu(int[] gpdAsdu)
        {
            _gpdAsdu = gpdAsdu;
        }
        
        /// <summary>
        /// The gpepHandle to set as <see cref="uint8_t"/> </summary>
        public void SetGpepHandle(int gpepHandle)
        {
            _gpepHandle = gpepHandle;
        }
        
        /// <summary>
        /// The gpTxQueueEntryLifetimeMs to set as <see cref="uint16_t"/> </summary>
        public void SetGpTxQueueEntryLifetimeMs(int gpTxQueueEntryLifetimeMs)
        {
            _gpTxQueueEntryLifetimeMs = gpTxQueueEntryLifetimeMs;
        }
        
        /// <summary>
        ///  The action to perform on the GP TX queue (true to add, false to remove).
        /// Return the action as <see cref="System.Boolean"/>
        /// </summary>
        public bool GetAction()
        {
            return _action;
        }
        
        /// <summary>
        ///  Whether to use ClearChannelAssessment when transmitting the GPDF.
        /// Return the useCca as <see cref="System.Boolean"/>
        /// </summary>
        public bool GetUseCca()
        {
            return _useCca;
        }
        
        /// <summary>
        ///  The Address of the destination GPD.
        /// Return the addr as <see cref="EmberGpAddress"/>
        /// </summary>
        public EmberGpAddress GetAddr()
        {
            return _addr;
        }
        
        /// <summary>
        ///  The GPD command ID to send.
        /// Return the gpdCommandId as <see cref="System.Int32"/>
        /// </summary>
        public int GetGpdCommandId()
        {
            return _gpdCommandId;
        }
        
        /// <summary>
        ///  The GP command payload.
        /// Return the gpdAsdu as <see cref="System.Int32"/>
        /// </summary>
        public int[] GetGpdAsdu()
        {
            return _gpdAsdu;
        }
        
        /// <summary>
        ///  The handle to refer to the GPDF.
        /// Return the gpepHandle as <see cref="System.Int32"/>
        /// </summary>
        public int GetGpepHandle()
        {
            return _gpepHandle;
        }
        
        /// <summary>
        ///  How long to keep the GPDF in the TX Queue.
        /// Return the gpTxQueueEntryLifetimeMs as <see cref="System.Int32"/>
        /// </summary>
        public int GetGpTxQueueEntryLifetimeMs()
        {
            return _gpTxQueueEntryLifetimeMs;
        }
        
        /// <summary>
        /// Method for serializing the command fields </summary>
        public override int[] Serialize()
        {
            SerializeHeader(_serializer);
            _serializer.SerializeBool(_action);
            _serializer.SerializeBool(_useCca);
            _serializer.SerializeEmberGpAddress(_addr);
            _serializer.SerializeUInt8(_gpdCommandId);
            _serializer.SerializeUInt8(_gpdAsdu.Length);
            _serializer.SerializeUInt8Array(_gpdAsdu);
            _serializer.SerializeUInt8(_gpepHandle);
            _serializer.SerializeUInt16(_gpTxQueueEntryLifetimeMs);
            return _serializer.GetPayload();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspDGpSendRequest [action=");
            builder.Append(_action);
            builder.Append(", useCca=");
            builder.Append(_useCca);
            builder.Append(", addr=");
            builder.Append(_addr);
            builder.Append(", gpdCommandId=");
            builder.Append(_gpdCommandId);
            builder.Append(", gpdAsdu=");
            if (_gpdAsdu == null)
            {
                builder.Append("null");
            }
            else
            {
                for (int cnt = 0
                ; cnt < _gpdAsdu.Length; cnt++
                )
                {
                    if (cnt > 0)
                    {
                        builder.Append(' ');
                    }
                    builder.Append(string.Format("0x{0:X02}", _gpdAsdu[cnt]));
                }
            }
            builder.Append(", gpepHandle=");
            builder.Append(_gpepHandle);
            builder.Append(", gpTxQueueEntryLifetimeMs=");
            builder.Append(_gpTxQueueEntryLifetimeMs);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
