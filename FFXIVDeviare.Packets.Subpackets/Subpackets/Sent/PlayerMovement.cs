using System;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent
{
    class PlayerMovement : Subpacket
    {

          
        public override Int32 SubpacketId => 410;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {

            // this is a sent packet
            // it's triggered via interactions with objects: market boards / summoning bells / npcs / aetherytes / probably other things
            // 16 bytes

#pragma warning disable 649

            public UInt32 Direction { get; set; }
            public UInt32 Unk2 { get; set; }
            public UInt32 Unk3 { get; set; }
            public UInt32 X { get; set; }
            public UInt32 Y { get; set; }
            public UInt32 Z { get; set; }


#pragma warning restore 649

        };
    }
}

