using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets.Subpackets.Sent
{

    public class NPCDialogRequest : Subpacket
    {
        public override Int32 SubpacketId => 450;

        public override Type PacketDataFormatType => typeof(Data);

        public unsafe struct Data
        {
            
            // this is a sent packet
            // it's triggered via interactions with objects: market boards / summoning bells / npcs / aetherytes / probably other things
            // 16 bytes
            
            #pragma warning disable 649

            public UInt32 objectId { get; set; } // id of the object being interacted with
            public UInt32 unk2 { get; set; } // always 1?
            public UInt32 requestId { get; set; } // summoning bells draw the same values; market boards draw the same values
            public UInt32 unk4 { get; set; } // always 0?

#pragma warning restore 649

        };
    }

}
