using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class Packet423 : Subpacket
    {
        public override Int32 SubpacketId => 423; 

        public override Type PacketDataFormatType => typeof(Data);
        
        unsafe struct Data
        {
            
            // recv
            // mouseover metadata
#pragma warning disable 649
            public UInt32 itemRequestId { get; set; }
            public UInt32 unk2 { get; set; }
            public fixed Byte name[32];
#pragma warning restore 649
        };
    }
}
