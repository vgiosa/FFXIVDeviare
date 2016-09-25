using System;

namespace FFXIVDeviare.Packets.Subpackets
{
    public class Packet265 : Subpacket
    {
        public override Int32 SubpacketId => 265;        

        public override Type PacketDataFormatType => typeof(packet_subpacket_content_265);
        
        unsafe struct packet_subpacket_content_265
        {
            
            // sent AND recv
            // market history entry [20]

            #pragma warning disable 649

            public UInt32 itemIdOnce;   //0000
            public UInt32 itemId;       //0004
            public UInt32 price;        //0008
            public UInt32 timestamp;    //000C
            public UInt32 qty;          //0010
            public UInt16 hq;           //0014
            public fixed Byte name[30]; //
            
            #pragma warning restore 649
        };
    }
}
