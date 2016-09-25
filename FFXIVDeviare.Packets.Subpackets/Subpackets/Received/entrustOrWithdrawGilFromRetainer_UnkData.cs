using System;

namespace FFXIVDeviare.Packets.Subpackets.Received
{
    public class EntrustOrWithdrawGilFromRetainer_UnkData : Subpacket
    {
        public override Int32 SubpacketId => 433;

        public override Type PacketDataFormatType => typeof(Data);

        unsafe struct Data
        {
            
            // recv
            // 433 - Entrust or withdraw gil packet chain
            // 16 bytes
#pragma warning disable 649
            public UInt32 incrementingNumber{get;set;}
            public UInt32 incrementingNumberDuplicate{get;set;}
            public UInt16 unk3{get;set;}
            public Byte unk4{get;set;}
            public Byte unk5{get;set;}
            public UInt32 unk6{get;set;}
#pragma warning disable 649
        };
    }
}
