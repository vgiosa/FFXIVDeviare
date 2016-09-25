using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Subpackets.Sent.ExpandedPackets
{
    public class CraftBuffAction : Subpacket
    {


        public override Int32 SubpacketId => 472;

        public override Int32 ExpandedId => 8;

        public override Type PacketDataFormatType => typeof(Data);

        public unsafe struct Data
        {


#pragma warning disable 649

            public UInt32 Unk1 { get; set; }
            public UInt32 Unk2 { get; set; }
            public UInt32 ExpandedId { get; set; }
            public UInt32 ActionKey;
            public string BuffAction
            {
                get
                {
                    UInt32 actionKey = ActionKey;
                    var action = Packet.RealmData.GameData.ClassJobActions.Where(a => a.Key == actionKey);
                    return action.FirstOrDefault().Name;

                }
            }
            
            public UInt32 Unk5 { get; set; }
            public UInt32 Unk6 { get; set; }

#pragma warning restore 649

        };
    }
}

