using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent
{
    class UnkPacket232 : Subpacket
    {


        public override Int32 SubpacketId => 232;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {

            // this is a sent packet
            // it's triggered via interactions with objects: market boards / summoning bells / npcs / aetherytes / probably other things
            // 16 bytes

#pragma warning disable 649
            public UInt32 Unk1 { get; set; }


#pragma warning restore 649

        };
    }
}

