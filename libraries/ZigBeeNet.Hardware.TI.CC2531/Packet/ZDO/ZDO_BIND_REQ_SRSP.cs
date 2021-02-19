﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_BIND_REQ_SRSP : ZToolPacket
    {
        public PacketStatus Status { get; private set; }

        public ZDO_BIND_REQ_SRSP(byte[] data)
        {
            Status = (PacketStatus)data[0];

            BuildPacket((ushort)ZToolCMD.ZDO_BIND_REQ_SRSP, data);
        }
    }
}
