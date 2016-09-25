using FFXIVDeviare.Packets.Subpackets;
using FFXIVDeviare.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFXIVDeviare.Packets
{
    class PacketProcessor
    {

        private volatile ConcurrentQueue<QueueObject> queue = new ConcurrentQueue<QueueObject>();

        Thread mainThread;
        private volatile bool threadRunning = false;

        public void ProcessPacket(Packet packet)
        {

            // Let's create a unique ID for the packet
            int packetID = Settings.Default.NextPacketID;
            string packetIDString = "";
            packetID++;
            Settings.Default.NextPacketID = packetID;
            Settings.Default.Save();
            string sentStr = packet.IsSent ? "sent" : "received";

            // Let's save the packet header to our SQL database


            //TODO Fix this to be a proper header chain
            StringBuilder headerChain = new StringBuilder("");

            //TODO removed packet header bytes need refactor
            //QueueObject headerQueueOjbect = new QueueObject(packetIDString, DateTime.UtcNow.Ticks, 0, "packet header", 0, headerChain, packet.packetHeaderBytes.Count() + packet.totalBytesLength, sentStr, packet.packetHeaderBytes, packet.Socket);
            //queue.Enqueue(headerQueueOjbect);

            PacketHeader header = packet.Header;
            //form1.UpdateHeaderLog("h_magic: " + ByteArrayToString(packet.packetHeaderBytes.Take(16).ToArray()) + ", h_timestampOffset: " + header.h_timestampOffset + ", h_unk3: " + header.h_unk3 + ", h_size: " + header.h_size + ", h_unk5: " + header.h_unk5 + ", h_subpacketCount: " + header.h_subpacketCount + ", h_unk6: " + header.h_unk6 + ", h_unk7: " + header.h_unk7 + ", h_deflated: " + header.h_deflated + ", h_unk9: " + header.h_unk9 + ", h_firstSubheadSize: " + header.h_firstSubheadSize);

            

            // Let's save the subpacket headers to our SQL database
            int counter = 0;
            foreach (Subpacket sp in packet.Subpackets)
            {

                int subpacketHeaderId = sp.SubpacketId;;
                counter++;
                QueueObject subpacketHeaderQueueObject = new QueueObject(packetIDString, DateTime.UtcNow.Ticks, counter, "subpacket header", subpacketHeaderId, headerChain, sp.RawPacketData.Count(), sentStr, sp.RawPacketData, packet.Socket);
                queue.Enqueue(subpacketHeaderQueueObject);

                headerChain.Append(subpacketHeaderId + " ");
               
            }



            // Let's save the subpacket headers to our SQL database
            counter = 0;
            foreach (Subpacket sp in packet.Subpackets)
            {
                int subpacketHeaderId = sp.SubpacketHeader.Id;
                counter++;
                QueueObject subpacketBodyQueueObject = new QueueObject(packetIDString, DateTime.UtcNow.Ticks, counter, "subpacket body", subpacketHeaderId, headerChain, sp.RawPacketData.Count(), sentStr, sp.RawPacketData, packet.Socket);
                queue.Enqueue(subpacketBodyQueueObject);
                
            }
            
        }

        
        private string ByteArrayToString(byte[] bytes)
        {
            var sb = new StringBuilder("");
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2") + " ");
            }

            return sb.ToString();
        }
        

        public void Start()
        {
            if(mainThread == null) { 
                mainThread = new Thread(this.ProcessingLoop);
            }
            mainThread.Start();
            threadRunning = true;
        }
        public void Stop()
        {
            threadRunning = false;
        }

        private bool connectionIsOpen = false;
        private void ProcessingLoop()
        {
            MySqlConnection connection = new MySqlConnection("datasource=Giosa.net;port=3306;username=giosa0_perse;password=Unodos12");

            while (threadRunning)
            {

                if (connection.State == System.Data.ConnectionState.Closed)
                {

                    if (connectionIsOpen)
                    {
                        connectionIsOpen = false;
                    }
                    else
                    {
                        
                    }

                    connection.Open();
                }

                if (connection.State != System.Data.ConnectionState.Open)
                {

                    if (connectionIsOpen)
                    {
                        connectionIsOpen = false;
                    }
                    else
                    {

                    }
                    continue;
                }

                if (!connectionIsOpen)
                {
                    connectionIsOpen = true;
                }

                QueueObject queueObject;
                if (queue.TryDequeue(out queueObject))
                {
                    UploadQueueObject(queueObject, connection);
                }

            }
        }

        public class QueueObject
        {

            public string id;
            public long timestamp;
            public int line;
            public string type;
            public int header;
            public StringBuilder headerChain;
            public int size;
            public string status;
            public byte[] bytes;
            public int socket;

            public QueueObject(string id, long timestamp, int line, String type, int header, StringBuilder headerChain, int size, String status, byte[] bytes, int socket)
            {

                this.id = id;
                this.timestamp = timestamp;
                this.line = line;
                this.type = type;
                this.header = header;
                this.headerChain = headerChain;
                this.size = size;
                this.status = status;
                this.bytes = bytes;
                this.socket = socket;

            }

        }

        private void UploadQueueObject(QueueObject queueObject, MySqlConnection connection)
        {

            PacketDatabaseManager.UpdateWebDatabase(connection, queueObject.id, queueObject.timestamp, queueObject.line, queueObject.type, queueObject.header, queueObject.headerChain, queueObject.size, queueObject.status, queueObject.bytes, queueObject.socket);
            //form1.UpdateHeaderLog("Uploaded object to MySQL server:   id = " + queueObject.id + "   type = " + queueObject.type + "   header = " + queueObject.header + "   headerChain = " + queueObject.headerChain);
        }

    }
}
