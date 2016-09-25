using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Sent
{

    public class BagOperation : Subpacket
    {
        public override Int32 SubpacketId => 430;
        public override SubpacketType Type
        {
            get
            {
                return SubpacketType.Inventory;
            }
        }
        public override Type PacketDataFormatType => typeof(Data);

        /*
            23 - Withdraw/Deposit from retainer
            8 - From Player Inventory to Sale, Crystal movement
            22 - From Retainer Sale to Player Inventory
            22 - From Retainer to Player Inventory
            21 - From Retainer Sale to Retainer Inventory
            21 - From Inventory to Retainer Inventory
            43 - Withdraw from FC Bank
            4 - Withdraw from FC Bank 2nd op

        */


        public unsafe struct Data
        {
            // bag operation
            #pragma warning disable 649

            public UInt16 incrementingValue { get; set; }    //0000 (Seems to increment with each sale)
            public UInt16 someSize { get; set; }             //0002 (4096... always?)
            public Byte operation { get; set; }            //0004 (always 10 OR 8 it seems, lol) - 10 is partial stack, 8 is full stack
            public Byte operation1 { get; set; }            //0004 (always 10 OR 8 it seems, lol) - 10 is partial stack, 8 is full stack
            public Byte operation2 { get; set; }            //0004 (always 10 OR 8 it seems, lol) - 10 is partial stack, 8 is full stack
            public Byte operation3 { get; set; }            //0004 (always 10 OR 8 it seems, lol) - 10 is partial stack, 8 is full stack
            public UInt32 always0_1 { get; set; }            //0008 (always 0)
            public UInt32 sourceBag { get; set; }            //000C
            public Byte sourceSlot { get; set; }             //000D
            public Byte always0_2 { get; set; }              //000E (always 0)
            public UInt16 always0_3 { get; set; }            //000F (always 0)
            public UInt32 sourceCount { get; set; }          //0014 (How many you have in this slot, total)
            public UInt32 always0_4 { get; set; }            //0018 (always 0)
            public UInt32 always0_5 { get; set; }            //001C (always 0)
            public UInt32 destinationBag { get; set; }       //0020
            public UInt16 destinationSlot { get; set; }      //0024
            public UInt16 unk1 { get; set; }      //0024
            public UInt32 quantity { get; set; }             //0028 (How many you want to sell, seems to be 0 with armoury items)
            public UInt32 always0_7 { get; set; }            //002C (always 0)

#pragma warning disable 649

        };
    }

}
