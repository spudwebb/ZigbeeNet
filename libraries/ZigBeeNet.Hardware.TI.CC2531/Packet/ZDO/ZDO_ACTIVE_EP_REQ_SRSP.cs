﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_ACTIVE_EP_REQ_SRSP : ZToolPacket
    {
        public PacketStatus Status { get; private set; }

        public ZDO_ACTIVE_EP_REQ_SRSP(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket((ushort)ZToolCMD.ZDO_ACTIVE_EP_REQ_SRSP, framedata);
        }
    }
}
