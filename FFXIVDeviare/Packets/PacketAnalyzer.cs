using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FFXIVDeviare.Packets
{
    class PacketAnalyzer
    {

        private string sqliteFileName;
        private int packetId;
        private string status;
        private string type;

        public List<OffsetValues> offsetValuesList = new List<OffsetValues>();
        public class OffsetValues
        {

            public byte offset;
            public Dictionary<byte, int> byteOccurencesDictionary;
            public int totalOccurences;
            public OffsetValues(int offset, Dictionary<byte, int> byteOccurencesDictionary, int totalOccurences)
            {
                this.offset = Convert.ToByte(offset);
                this.byteOccurencesDictionary = byteOccurencesDictionary;
                this.totalOccurences = totalOccurences;
            }

        }

        private void WriteOffsetValues()
        {

            foreach (OffsetValues offsetValues in offsetValuesList)
            {

                Debug.WriteLine("PacketID: " + packetId.ToString() + " Offset: " + offsetValues.offset.ToString());
                Debug.WriteLine("Bytes at or above 0.1% occurence");

                foreach (KeyValuePair<byte, int> keyValuePair in offsetValues.byteOccurencesDictionary)
                {
                    byte b = keyValuePair.Key;
                    int value = keyValuePair.Value;
                    float percentage = (float)value / (float)offsetValues.totalOccurences;
                    if (percentage >= 0.001)
                    {
                        Debug.WriteLine("Byte: " + b.ToString() + " Occurence: " + percentage.ToString("N4"));

                        if (percentage == 1)
                        {

                        }

                    }
                }

                Debug.WriteLine("");

            }

        }

        public PacketAnalyzer (string sqliteFileName, int packetId, string status, string type)
        {
            this.sqliteFileName = sqliteFileName;
            this.packetId = packetId;
            this.status = status;
            this.type = type;
            this.DeconstructPacket();
            this.WriteOffsetValues();
        }

        private void DeconstructPacket()
        {
            /*
            List<List<byte>> encapsulatingByteList; = fillEncapsulatingByteList();
            int counter1 = 0;
            foreach (List<byte> byteList in encapsulatingByteList)
            {

                HashSet<byte> byteHashSet = new HashSet<byte>(byteList);
                Dictionary<byte, int> byteOccurencesDictionary = new Dictionary<byte, int>();
                foreach (byte b in byteHashSet)
                {
                    byteOccurencesDictionary.Add(b, 0);
                }

                int counter2 = 0;
                foreach (byte b in byteList)
                {
                    byteOccurencesDictionary[b]++;
                    counter2++;
                }

                OffsetValues offsetValues = new OffsetValues(counter1, byteOccurencesDictionary, counter2);
                offsetValuesList.Add(offsetValues);
                counter1++;

            }
            */

        }

        /*private List<List<byte>> fillEncapsulatingByteList ()
             {

                 List<List<byte>> encapsulatingByteList = new List<List<byte>>();
                 using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + sqliteFileName + ";Version=3"))
                 {
                     connection.Open();

                     Debug.WriteLine("SQLite connection open.");
                     String query = "SELECT * FROM packets WHERE header LIKE " + packetId + " AND type LIKE '" + type + "' AND status LIKE '" + status + "'";


                     using (SQLiteCommand command = new SQLiteCommand(query, connection))
                     {
                         using (SQLiteDataReader dataReader = command.ExecuteReader())
                         {

                             Debug.WriteLine("Executing reader.");

                             int counter1 = 0;
                             while (dataReader.Read())
                             {

                                 //byte[] bytes = Form1.StringToByteArray(dataReader["data"].ToString().Replace("new byte[]  ", "").Replace(" ", ""));
                                 byte[] bytes = (byte[])dataReader["data"];

                                 if (counter1 == 0)
                                 {

                                     Debug.WriteLine("Adding to encapsulating byte list.");

                                     Debug.WriteLine(bytes.Count());
                                     int counter2 = 0;
                                     foreach (byte b in bytes)
                                     {
                                         List<byte> byteList = new List<byte>();
                                         byteList.Add(b);
                                         encapsulatingByteList.Add(byteList);
                                         counter2++;
                                     }
                                 }
                                 else
                                 {
                                     int counter2 = 0;
                                     foreach (byte b in bytes)
                                     {
                                         encapsulatingByteList[counter2].Add(b);
                                         counter2++;
                                     }
                                 }
                                 counter1++;

                                 if (counter1 % 100 == 0)
                                 {
                                     Debug.WriteLine("Line: " + counter1);
                                 }


                             }
                         }
                     }
                 }
                 return encapsulatingByteList;

             }    */

    }
}
