using System;

namespace FFXIVDeviare.Packets.Subpackets
{
    public class Packet266 : Subpacket
    {
        public override Int32 SubpacketId => 266;        

        public override Type PacketDataFormatType => typeof(packet_subpacket_content_266);
        
        unsafe struct packet_subpacket_content_266
        {
            // sent or recv?
            // 266 - Retainer action: view sale history
            // 4 bytes of retainerId, 4 bytes of unkMagicData, 52 bytes of sale data repeated 20 times
#pragma warning disable 649
            public UInt32 retainerId;
            public UInt32 magicNumber7864343;

            public UInt32 itemId1;
            public UInt32 price1;
            public UInt32 timestamp1;
            public UInt32 qty1;
            public UInt16 hq1;
            public fixed Byte buyerName1[34];

            public UInt32 itemId2;
            public UInt32 price2;
            public UInt32 timestamp2;
            public UInt32 qty2;
            public UInt16 hq2;
            public fixed Byte buyerName2[34];

            public UInt32 itemId3;
            public UInt32 price3;
            public UInt32 timestamp3;
            public UInt32 qty3;
            public UInt16 hq3;
            public fixed Byte buyerName3[34];

            public UInt32 itemId4;
            public UInt32 price4;
            public UInt32 timestamp4;
            public UInt32 qty4;
            public UInt16 hq4;
            public fixed Byte buyerName4[34];

            public UInt32 itemId5;
            public UInt32 price5;
            public UInt32 timestamp5;
            public UInt32 qty5;
            public UInt16 hq5;
            public fixed Byte buyerName5[34];

            public UInt32 itemId6;
            public UInt32 price6;
            public UInt32 timestamp6;
            public UInt32 qty6;
            public UInt16 hq6;
            public fixed Byte buyerName6[34];

            public UInt32 itemId7;
            public UInt32 price7;
            public UInt32 timestamp7;
            public UInt32 qty7;
            public UInt16 hq7;
            public fixed Byte buyerName7[34];

            public UInt32 itemId8;
            public UInt32 price8;
            public UInt32 timestamp8;
            public UInt32 qty8;
            public UInt16 hq8;
            public fixed Byte buyerName8[34];

            public UInt32 itemId9;
            public UInt32 price9;
            public UInt32 timestamp9;
            public UInt32 qty9;
            public UInt16 hq9;
            public fixed Byte buyerName9[34];

            public UInt32 itemId10;
            public UInt32 price10;
            public UInt32 timestamp10;
            public UInt32 qty10;
            public UInt16 hq10;
            public fixed Byte buyerName10[34];

            public UInt32 itemId11;
            public UInt32 price11;
            public UInt32 timestamp11;
            public UInt32 qty11;
            public UInt16 hq11;
            public fixed Byte buyerName11[34];

            public UInt32 itemId12;
            public UInt32 price12;
            public UInt32 timestamp12;
            public UInt32 qty12;
            public UInt16 hq12;
            public fixed Byte buyerName12[34];

            public UInt32 itemId13;
            public UInt32 price13;
            public UInt32 timestamp13;
            public UInt32 qty13;
            public UInt16 hq13;
            public fixed Byte buyerName13[34];

            public UInt32 itemId14;
            public UInt32 price14;
            public UInt32 timestamp14;
            public UInt32 qty14;
            public UInt16 hq14;
            public fixed Byte buyerName14[34];

            public UInt32 itemId15;
            public UInt32 price15;
            public UInt32 timestamp15;
            public UInt32 qty15;
            public UInt16 hq15;
            public fixed Byte buyerName15[34];

            public UInt32 itemId16;
            public UInt32 price16;
            public UInt32 timestamp16;
            public UInt32 qty16;
            public UInt16 hq16;
            public fixed Byte buyerName16[34];

            public UInt32 itemId17;
            public UInt32 price17;
            public UInt32 timestamp17;
            public UInt32 qty17;
            public UInt16 hq17;
            public fixed Byte buyerName17[34];

            public UInt32 itemId18;
            public UInt32 price18;
            public UInt32 timestamp18;
            public UInt32 qty18;
            public UInt16 hq18;
            public fixed Byte buyerName18[34];

            public UInt32 itemId19;
            public UInt32 price19;
            public UInt32 timestamp19;
            public UInt32 qty19;
            public UInt16 hq19;
            public fixed Byte buyerName19[34];

            public UInt32 itemId20;
            public UInt32 price20;
            public UInt32 timestamp20;
            public UInt32 qty20;
            public UInt16 hq20;
            public fixed Byte buyerName20[34];
#pragma warning restore 649
        };
    }
}
