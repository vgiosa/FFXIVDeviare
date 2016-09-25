using FFXIVDeviare.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Game.Events
{
    public class GameEventArgs : EventArgs
    {
        public Packet Packet { get; set; }

    }
}
