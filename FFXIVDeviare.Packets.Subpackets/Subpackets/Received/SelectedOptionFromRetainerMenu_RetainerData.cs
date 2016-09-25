using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class SelectedOptionFromRetainerMenu_RetainerData : Subpacket
    {
        public override Int32 SubpacketId => 262;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {

            // RECV
            // sent 262 is just 8 bytes
            // 262 - Retainer action: entrust or withdraw gil / sell items in your inventory / sell items in your retainer's inventory / view sale history
            // 48 bytes of retainer data repeated 8 times
            
            #pragma warning disable 649

            public UInt32 id1{get;set;}
            public UInt32 unkMagicData1{get;set;}
            public UInt32 unkTimestamp1{get;set;}
            public Byte location1{get;set;}
            public Byte notSelected1{get;set;}
            public Byte unk1{get;set;}
            public fixed Byte name1[33];

            public UInt32 id2{get;set;}
            public UInt32 unkMagicData2{get;set;}
            public UInt32 unkTimestamp2{get;set;}
            public Byte location2{get;set;}
            public Byte notSelected2{get;set;}
            public Byte unk2{get;set;}
            public fixed Byte name2[33];

            public UInt32 id3{get;set;}
            public UInt32 unkMagicData3{get;set;}
            public UInt32 unkTimestamp3{get;set;}
            public Byte location3{get;set;}
            public Byte notSelected3{get;set;}
            public Byte unk3{get;set;}
            public fixed Byte name3[33];

            public UInt32 id4{get;set;}
            public UInt32 unkMagicData4{get;set;}
            public UInt32 unkTimestamp4{get;set;}
            public Byte location4{get;set;}
            public Byte notSelected4{get;set;}
            public Byte unk4{get;set;}
            public fixed Byte name4[33];

            public UInt32 id5{get;set;}
            public UInt32 unkMagicData5{get;set;}
            public UInt32 unkTimestamp5{get;set;}
            public Byte location5{get;set;}
            public Byte notSelected5{get;set;}
            public Byte unk5{get;set;}
            public fixed Byte name5[33];

            public UInt32 id6{get;set;}
            public UInt32 unkMagicData6{get;set;}
            public UInt32 unkTimestamp6{get;set;}
            public Byte location6{get;set;}
            public Byte notSelected6{get;set;}
            public Byte unk6{get;set;}
            public fixed Byte name6[33];

            public UInt32 id7{get;set;}
            public UInt32 unkMagicData7{get;set;}
            public UInt32 unkTimestamp7{get;set;}
            public Byte location7{get;set;}
            public Byte notSelected7{get;set;}
            public Byte unk7{get;set;}
            public fixed Byte name7[33];

            public UInt32 id8{get;set;}
            public UInt32 unkMagicData8{get;set;}
            public UInt32 unkTimestamp8{get;set;}
            public Byte location8{get;set;}
            public Byte notSelected8{get;set;}
            public Byte unk8{get;set;}
            public fixed Byte name8[33];
            
            #pragma warning restore 649
        };
    }
}
