﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent.ExpandedPackets
{
    class SetCrossClass : Subpacket
    {


        public override Int32 SubpacketId => 401;

        public override Int32 ExpandedId => 304;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {


#pragma warning disable 649

            public UInt32 ExpandedId { get; set; }
            public UInt32 Unk1 { get; set; }
            public UInt32 SlotId { get; set; }
            public UInt32 ActionId { get; set; }
            public UInt32 Unk4 { get; set; } 
            public UInt32 Unk6 { get; set; }
            public UInt32 Unk7 { get; set; }
            public UInt32 Unk8 { get; set; }

#pragma warning restore 649

        };
    }
}

