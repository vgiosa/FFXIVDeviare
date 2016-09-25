using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SaintCoinach;
namespace FFXIVDeviare.Packets.Subpackets
{



    public unsafe struct SubpacketHeader
    {
        public UInt32 Size {get; set;}                  //0000 (size of subpacket INCLUDING THIS HEADER)
        public UInt32 SourceId { get; set; }              //0004 (0x1046B119)
        public UInt32 TargetId { get; set; }              //0008 (0x1046B119)
        public UInt16 Unk1 { get; set; }                  //000C (03 00 F4 2C)
        public UInt16 Token { get; set; }                  //000C (03 00 F4 2C)
                                                           //public Byte s_unk333;                  //000C (03 00 F4 2C)
        public UInt16 MagicNumber { get; set; }             //0010 (0x14)
        public UInt16 Id { get; set; }                    //0012
        public UInt16 Unk2 { get; set; }                  //0014 (0x390000) = (00 00 2F 00) also (00 00 39 00)??
        public UInt16 ZoneId { get; set; }                  //0014 (0x390000) = (00 00 2F 00) also (00 00 39 00)??
        public UInt32 Timestamp { get; set; }                  //0018 (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        public UInt32 Unk3 { get; set; }                  //001C (0)

        public static SubpacketHeader ReadStruct(byte[] data)
        {
            fixed (byte* pb = &data[0])
            {
                return *(SubpacketHeader*)pb;
            }
        }
    };


    public enum SubpacketType
    {
        Unknown = 0,
        MarketBoard = 1,
        Movement = 2,
        Crafting = 3,
        Inventory = 4,
        Chat = 5,
        FC = 6
    }

    

    public abstract class Subpacket
    {
        static Subpacket()
        {
            SubpacketSentDefinitions = new Dictionary<int, Type>();
            SubpacketRecievedDefinitions = new Dictionary<int, Type>();
            SubpacketSentExpandedDefinitions = new Dictionary<int, Dictionary<int, Type>>();
            SubpacketReceivedExpandedDefinitions = new Dictionary<int, Dictionary<int, Type>>();


            SubpacketChatSentDefinitions = new Dictionary<int, Type>();
            SubpacketChatRecievedDefinitions = new Dictionary<int, Type>();


            IEnumerable<Type> subpacketList = Assembly.GetAssembly(typeof(Subpacket)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Subpacket)));
            foreach (Type t in subpacketList)
            {
                Subpacket p = (Subpacket)Activator.CreateInstance(t);
                if (t.Namespace.EndsWith("Subpackets.Sent"))
                {
                    SubpacketSentDefinitions.Add(p.SubpacketId, t);
                }
                else if (t.Namespace.EndsWith("Subpackets.Received"))
                {
                    SubpacketRecievedDefinitions.Add(p.SubpacketId, t);
                }
                else if (t.Namespace.EndsWith("Sent.ExpandedPackets"))
                {
                    if (!SubpacketSentExpandedDefinitions.ContainsKey(p.SubpacketId))
                    {
                        SubpacketSentExpandedDefinitions.Add(p.SubpacketId, new Dictionary<int, Type>());
                    }
                    SubpacketSentExpandedDefinitions[p.SubpacketId].Add(p.ExpandedId, t);
                }
                else if (t.Namespace.EndsWith("Subpackets.Chat.Sent"))
                {
                    SubpacketChatSentDefinitions.Add(p.SubpacketId, t);
                }
                else if (t.Namespace.EndsWith("Subpackets.Chat.Received"))
                {
                    SubpacketChatRecievedDefinitions.Add(p.SubpacketId, t);
                }
            }
        }

        public static Dictionary<int, Type> SubpacketSentDefinitions;
        public static Dictionary<int, Type> SubpacketRecievedDefinitions;

        public static Dictionary<int, Type> SubpacketChatSentDefinitions;
        public static Dictionary<int, Type> SubpacketChatRecievedDefinitions;

        public static Dictionary<int, Dictionary<int, Type>> SubpacketSentExpandedDefinitions;
        public static Dictionary<int, Dictionary<int, Type>> SubpacketReceivedExpandedDefinitions;

        

        public abstract int SubpacketId { get; }
        public virtual int ExpandedId { get; }
        public virtual SubpacketType Type { get { return SubpacketType.Unknown; } }
        public abstract Type PacketDataFormatType { get; }

        public object PacketData;

        public byte[] RawPacketData;

        public SubpacketHeader SubpacketHeader {get;set;}


        public static int SubpacketHeaderSize { get { return System.Runtime.InteropServices.Marshal.SizeOf(typeof(SubpacketHeader)); } }



        public static Subpacket CreateSubpacket(SubpacketHeader header, byte[] body, bool isSent, bool isChat)
        {
            Subpacket sp;
            Dictionary<int, Type> subpacketDefinitions;
            if (isChat)
            {
                subpacketDefinitions = isSent ? SubpacketChatSentDefinitions : SubpacketChatRecievedDefinitions;
            }
            else
            {
                subpacketDefinitions = isSent ? SubpacketSentDefinitions : SubpacketRecievedDefinitions;
            }

            
            Dictionary<int, Dictionary<int, Type>> subpacketExpandedDefinitions = isSent ? SubpacketSentExpandedDefinitions : SubpacketReceivedExpandedDefinitions;
            if (!subpacketDefinitions.ContainsKey(header.Id))
            {
                sp = new UnknownSubpacket();
                //sp.PacketData = body;

                UnknownSubpacket.UnknownBodyData unkData = new UnknownSubpacket.UnknownBodyData();
                
                unkData.BodySize = body.Count();
                unkData.IsSent = isSent;
                unkData.Id = header.Id;
                sp.PacketData = unkData;
                sp.SubpacketHeader = header;
            }
            else { 
                Type subpacketDefinition = subpacketDefinitions[header.Id];
            
                sp = (Subpacket)Activator.CreateInstance(subpacketDefinition);
                sp.InitializeSubpacket(header, body);

                if (subpacketExpandedDefinitions.ContainsKey(header.Id))
                {
                    object data = sp.PacketData;
                    
                    if (data.GetType().GetProperty("ExpandedId") !=null  && subpacketExpandedDefinitions[header.Id].ContainsKey((int)(Convert.ToInt32(data.GetType().GetProperty("ExpandedId").GetValue(data)))))
                    {
                        sp = (Subpacket)Activator.CreateInstance(SubpacketSentExpandedDefinitions[header.Id][(int)((Convert.ToInt32(data.GetType().GetProperty("ExpandedId").GetValue(data))))]);
                        sp.InitializeSubpacket(header, body);

                    }
                }
            }
            sp.RawPacketData = body;
            return sp;


        }
        internal void InitializeSubpacket(SubpacketHeader header, byte[] body)
        {
            this.SubpacketHeader = header;
            if(body.Length == this.GetStructureSize())
            {
                this.PacketData = ReadStruct(body); 
            }
            else
            {
                int sizeCount = 0;
                List <Object> subpackets = new List<Object>();
                while(sizeCount < body.Length)
                {
                    subpackets.Add(ReadStruct(body.Skip(sizeCount).ToArray()));
                    sizeCount += GetStructureSize();
                }
                PacketData = subpackets;
            }

        }



        internal object ReadStruct(byte[] data)
        {
            unsafe
            {
                fixed (byte* pb = &data[0])
                {
                    //return *(packet_subpacket_content_264*) pb;
                    return Marshal.PtrToStructure(new IntPtr(pb), PacketDataFormatType);
                }
            }
        }


        protected internal Subpacket()
        {

        }

        
        public virtual string[] RelevantFieldNames {
            get {
                return new string[] { };
            }
        }

        public int GetStructureSize()
        {
            return System.Runtime.InteropServices.Marshal.SizeOf(PacketDataFormatType);
        }
    }

}

