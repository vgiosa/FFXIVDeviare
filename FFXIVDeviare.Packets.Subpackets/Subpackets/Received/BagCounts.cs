using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class BagCounts : Subpacket
    {
        public override Int32 SubpacketId => 432;
        public override SubpacketType Type { get { return SubpacketType.Inventory; } }
        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {
            
            // recv
            // recv when entrusting/withdrawing gil
            // recv when selecting retainer
#pragma warning disable 649
            public UInt32 id { get; set; }
            public UInt32 count { get; set; }
            public UInt32 retainer_page { get; set; }                    //0008 (it seems like bag number at lower numbers, and some weird shit later)
            public UInt32 unk4 { get; set; }                    //000C (seems to be zero for all ive seen)
#pragma warning restore 649
        };
    }
}
