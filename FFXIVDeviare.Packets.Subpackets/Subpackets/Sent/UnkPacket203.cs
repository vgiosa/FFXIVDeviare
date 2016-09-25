using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent
{
    class UnkPacket203 : Subpacket
    {


        public override Int32 SubpacketId => 203;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {

            // this is a sent packet
            // it's triggered via interactions with objects: market boards / summoning bells / npcs / aetherytes / probably other things
            // 16 bytes

#pragma warning disable 649

            public UInt32 Unk1 { get; set; }
            public UInt32 Unk2 { get; set; }
            public UInt32 Unk3 { get; set; }
            public UInt32 Unk4 { get; set; }



#pragma warning restore 649

        };
    }
}

