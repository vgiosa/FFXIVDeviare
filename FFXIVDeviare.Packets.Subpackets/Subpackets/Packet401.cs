using System;

namespace FFXIVDeviare.Packets.Subpackets
{
    public class Packet401 : Subpacket
    {
        public override Int32 SubpacketId => 401;        

        public override Type PacketDataFormatType => typeof(packet_subpacket_content_401);
        
        public unsafe struct packet_subpacket_content_401
        {
            
            // sent AND recv
            // mousever metadata
            // set global integer

            #pragma warning disable 649

            public UInt32 operation;   //0000 (400 = summoning bell item price adjust, others do different things...)
            public UInt32 target;      //0004 (0 = top item on the list, 1 = second, etc. It's a bit like bag slots?)
            public UInt32 value;       //0008
            public UInt32 unk4;        //000C (0)
            public UInt32 unk5;        //0010 (0)
            public UInt32 unk6;        //0014 (0)
            public UInt32 id;        //0018 (0)
            public UInt32 unk8;        //001C (0)
            
            #pragma warning restore 649

        };
    }
}
