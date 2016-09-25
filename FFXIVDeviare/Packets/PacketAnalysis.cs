using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Packets
{
    class PacketAnalysis
    {

        public static void AnalyzePacket(string fileName, int packetId)
        {



            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + fileName + ";Version=3"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM packets WHERE header LIKE " + packetId, connection))
                {

                    using (SQLiteDataReader dataReader = command.ExecuteReader())
                    {

                        var stringBuilder = new StringBuilder("");
                        while (dataReader.Read())
                        {

                            byte[] bytes = Form1.StringToByteArray(dataReader["data"].ToString().Replace("new byte[]  ", "").Replace(" ", ""));

                        }

                    }

                }

            }

        }

    }
}
