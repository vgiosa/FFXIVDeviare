using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent
{
    class GearSetChange : Subpacket
    {


        public override Int32 SubpacketId => 431;
        public override SubpacketType Type { get { return SubpacketType.Inventory; } }
        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {

            // this is a sent packet
            // it's triggered via interactions with objects: market boards / summoning bells / npcs / aetherytes / probably other things
            // 16 bytes

#pragma warning disable 649
            public UInt32 GearSetId { get; set; }

            public UInt16 MainHandBag { get; set; }
            public UInt16 OffHandBag { get; set; }

            public UInt16 HeadBag { get; set; }
            public UInt16 BodyBag { get; set; }

            public UInt16 HandBag { get; set; }
            public UInt16 WaistBag { get; set; }

            public UInt16 LegBag { get; set; }
            public UInt16 FeetBag { get; set; }

            public UInt16 Neckbag { get; set; }
            public UInt16 EarringBag { get; set; }

            public UInt16 WristBag { get; set; }
            public UInt16 Ring1Bag { get; set; }

            public UInt16 Ring2Bag { get; set; }
            public UInt16 SoulcrystalBag { get; set; }


            public UInt16 MainHandSlot { get; set; }
            public UInt16 OffHandSlot { get; set; }

            public UInt16 HeadSlot { get; set; }
            public UInt16 BodySlot { get; set; }

            public UInt16 HandSlot { get; set; }
            public UInt16 WaistSlot { get; set; }

            public UInt16 LegSlot { get; set; }
            public UInt16 FeetSlot { get; set; }

            public UInt16 NeckSlot { get; set; }
            public UInt16 EarringSlot { get; set; }

            public UInt16 WristSlot { get; set; }
            public UInt16 Ring1Slot { get; set; }

            public UInt16 Ring2Slot { get; set; }
            public UInt16 SoulcrystalSlot { get; set; }

            public UInt16 Unk16 { get; set; }
            public UInt16 Unk1616 { get; set; }

#pragma warning restore 649

        };
    }
}

