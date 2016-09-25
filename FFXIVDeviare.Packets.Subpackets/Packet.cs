using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using FFXIVDeviare.Packets.Subpackets;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SaintCoinach;

namespace FFXIVDeviare.Packets
{

    public unsafe struct PacketHeader
    {
        public fixed byte MagicHeader[16];         //0000

        public String MagicHeaderProp
        {
            get
            {
                String magicStr = "";
                fixed (byte* pmessage = MagicHeader)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        byte magic = *(pmessage + i);
                        magicStr += magic.ToString("X2") + " ";
                    }
                }
                return magicStr;
            }
        }


        public UInt32 TimestampToken { get; set; } //0010 make_time_special_10()
        public UInt32 Version { get; set; }                  //0014 = 0x153 Always Maybe version specific?
        public UInt32 Size { get; set; }                  //0018 (length)
        public UInt16 UnkAlways0_1 { get; set; }                  //001C Always 0
        public UInt16 SubpacketCount { get; set; }                  //001C
        public Byte h_unk6 { get; set; }        //001E seems to always be 1
        public Byte IsDeflated { get; set; }               //0021 always 0 for sent
        public Byte UnkAlways0_2 { get; set; }                   //0020 seems to always be 0
        public Byte h_unk9 { get; set; }                  //0022 always 0 for sent
        public UInt32 UnkAlways0_3 { get; set; }                  //0024 still 0s



            
        

        public static PacketHeader ReadStruct(byte[] data)
        {
            fixed (byte* pb = &data[0])
            {
                return *(PacketHeader*)pb;
            }
        }
    };

    public class Packet
    {
        public static byte[] ARR_HEADER_MAGIC = new byte[] { 0x52, 0x52, 0xa0, 0x41, 0xff, 0x5d, 0x46, 0xe2, 0x7f, 0x2a, 0x64, 0x4d, 0x7b, 0x99, 0xc4, 0x75 };

        //private static readonly ILog logger = LogManager.GetLogger(typeof(Packet));
        public PacketHeader Header = new PacketHeader();
        public bool FailedMagic { get; set; }
        public bool IsSent{get; set;}
        public int Socket{get; set;}

        public int leftoverBytesLength;
        public int totalBytesLength;
        public List<Subpacket> Subpackets = new List<Subpacket>();
        public List<string> logList = new List<string>();
        public byte[] packetHeaderBytes;
        public Boolean isChatPacket = false;
        public byte[] RawData;
        public byte[] RawDecompressedData = { };
        public static ARealmReversed RealmData;

        public Packet() { }

        public Packet (byte[] rawData, bool isSent, int socket)
        {

            this.IsSent = isSent;
            this.Socket = socket;
            this.RawData = rawData;
            if(rawData.Length < 40)
            {
                //logger.Info("invalid packet size packet < 40");
                return;
            }
            

            PacketHeader header = PacketHeader.ReadStruct(rawData);
            this.Header = header;
            this.packetHeaderBytes = (byte[])(Array)rawData.Skip(0).Take(40).ToArray();
            FailedMagic = false;
            unsafe
            {
                for(int i = 0; i < 16; i++) { 
                    byte magic = *(header.MagicHeader + i);
                    
                    if (magic != ARR_HEADER_MAGIC[i] && header.Version !=340)
                    {
                        
                        FailedMagic = true;
                        byte currentByte = 0;
                        byte previousByte = 0;

                        //Breaking out here because this is failing.
                        return;
                        if(rawData[40] != 0x78 && rawData[41] != 0x9C)
                        {
                            RawDecompressedData = rawData;
                            return;
                        }
                        List<byte> bytesToDecompress = null;
                        for (int ii = 0; ii < rawData.Length; ii++)
                        {
                            previousByte = currentByte;
                            currentByte = rawData[ii];
                            if(bytesToDecompress != null) bytesToDecompress.Add(currentByte);
                            if (previousByte == 0x78 && currentByte == 0x9c)
                            {
                                if (bytesToDecompress != null) {
                                    byte[] compArray = bytesToDecompress.Take(bytesToDecompress.Count - 2).ToArray();
                                    byte[] decompressed = DeflateData(compArray);

                                    byte[] tempArray = new byte[decompressed.Length + RawDecompressedData.Length];
                                    RawDecompressedData.CopyTo(tempArray, 0);
                                    decompressed.CopyTo(tempArray, RawDecompressedData.Length);
                                    RawDecompressedData = tempArray;


                                }
                                bytesToDecompress = new List<byte>();
                            }


                        }
                        return;
                    }
                }

            }
            
            if (rawData.Count() < 40)
            {
                return;
            }

            // Lets add our header bytes to our bytesToStore list
            

            byte[] bytes = null;
            
            if (isSent || header.IsDeflated == 0)
            {
                bytes = (byte[])(Array)rawData.Skip(40).ToArray();
            }
            else
            {
                
                using (var input = new MemoryStream((byte[]) (Array) rawData, 42, rawData.Length - 42)){
                    using (var output = new MemoryStream(rawData.Length)){
                        using (var deflate = new DeflateStream(input, CompressionMode.Decompress)){
                                deflate.CopyTo(output);
                        }
                        bytes = output.ToArray();
                    }
                }
            }
            totalBytesLength = bytes.Length;
            int counter = 0;

            while (bytes.Length > 32)
            {
                SubpacketHeader subhead = SubpacketHeader.ReadStruct(bytes);
                UInt16 id = subhead.Id;


                if (subhead.Timestamp == 10) isChatPacket = true;

                // Why did this happen, what did we fuck up?
                if (subhead.Id < 100 || subhead.Id > 1000)
                {
                    break;
                }


                // Why did this happen, what did we fuck up?
                if (subhead.Id == 0 || subhead.Size == 0)
                {
                    break;
                }

                counter++;
                int subheadSize = Subpacket.SubpacketHeaderSize;
                byte[] tempBytesSubHead = bytes.Take<Byte>(subheadSize).ToArray();
                subhead = SubpacketHeader.ReadStruct(tempBytesSubHead);
                byte[] tempBytesBody = bytes.Skip(subheadSize).Take<byte>((int)(subhead.Size - subheadSize)).ToArray();

                Subpackets.Add(Subpacket.CreateSubpacket(subhead, tempBytesBody, IsSent, isChatPacket));
                

                // Skip ahead to the next subpacket
                bytes = bytes.Skip<byte>((int)(subhead.Size)).ToArray();


            }
            leftoverBytesLength = bytes.Count();
        }

        


        private byte[] DeflateData(byte[] data)
        {
            using (var input = new MemoryStream((byte[])(Array)data, 0, data.Length))
            {
                using (var output = new MemoryStream(data.Length))
                {
                    using (var deflate = new DeflateStream(input, CompressionMode.Decompress))
                    {
                        
                        deflate.CopyTo(output);
                    }
                    return output.ToArray();
                }
            }
        }




    }
}
