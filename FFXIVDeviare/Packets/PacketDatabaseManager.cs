using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace FFXIVDeviare.Packets
{
    public class PacketDatabaseManager
    {

        Thread syncMySQLDatabaseThread;
        public volatile bool syncMySQLDatabaseThreadRunning = false;
        
        public void StartSyncMySQLDatabseThread()
        {
            if (syncMySQLDatabaseThread == null)
            {
                syncMySQLDatabaseThread = new Thread(this.SyncMySQLDatabase);
            }
            syncMySQLDatabaseThreadRunning = true;
            syncMySQLDatabaseThread.Start();

        }

        public void StopSyncMySQLDatabseThread()
        {
            syncMySQLDatabaseThreadRunning = false;
        }

        private void SyncMySQLDatabase()
        {
            /*
            bool newDatabase = NewDatabase();

            if (newDatabase)
            {

                // lets pull everything...
                using (SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source=PacketDatabase.sqlite;Version=3"))
                {
                    sqliteConnection.Open();
                    using (MySqlConnection mysqlConnection = new MySqlConnection("datasource=Giosa.net;port=3306;username=giosa0_perse;password=Unodos12"))
                    {
                        string query = "SELECT * FROM giosa0_packet_log.packet_log_revision1";
                        using (MySqlCommand command = new MySqlCommand(query, mysqlConnection))
                        {
                            mysqlConnection.Open();
                            using (MySqlDataReader dataReader = command.ExecuteReader())
                            {
                                int counter = 1;
                                form1.UpdateHeaderLog("Starting MySQL database sync");
                                while (dataReader.Read())
                                {

                                    if (syncMySQLDatabaseThreadRunning)
                                    {
                                        UpdateLocalDatabase(sqliteConnection, dataReader["id"].ToString(), (int)dataReader["timestamp"], (int)dataReader["line"], dataReader["type"].ToString(), (int)dataReader["header"], dataReader["headerChain"].ToString(), (int)dataReader["size"], dataReader["status"].ToString(), (byte[])dataReader["data"], (int)dataReader["socket"]);
                                        if (counter % 100 == 0)
                                        {
                                            form1.UpdateHeaderLog("MySQL database sync at line: " + counter);
                                        }
                                        counter++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                form1.UpdateHeaderLog("MySQL database sync finished.");
                            }
                        }
                    }
                }
            }
            else
            {
                // lets figure out how big our current db is...
                int totalLines = 0;
                using (SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source=PacketDatabase.sqlite;Version=3"))
                {
                    string query = "SELECT * FROM packets";
                    using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
                    {
                        sqliteConnection.Open();
                        using (SQLiteDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                totalLines++;
                            }
                            Debug.WriteLine("Total lines: " + totalLines);
                        }
                    }
                    // now lets update the database
                    using (MySqlConnection mysqlConnection = new MySqlConnection("datasource=Giosa.net;port=3306;username=giosa0_perse;password=Unodos12"))
                    {
                        string mysqlQuery = "SELECT * FROM giosa0_packet_log.packet_log_revision1 WHERE autoIncrementingValue > " + totalLines;
                        using (MySqlCommand command = new MySqlCommand(mysqlQuery, mysqlConnection))
                        {
                            mysqlConnection.Open();
                            using (MySqlDataReader dataReader = command.ExecuteReader())
                            {
                                form1.UpdateHeaderLog("Starting MySQL database sync");
                                while (dataReader.Read())
                                {

                                    if (syncMySQLDatabaseThreadRunning)
                                    {
                                        UpdateLocalDatabase(sqliteConnection, dataReader["id"].ToString(), (int)dataReader["timestamp"], (int)dataReader["line"], dataReader["type"].ToString(), (int)dataReader["header"], dataReader["headerChain"].ToString(), (int)dataReader["size"], dataReader["status"].ToString(), (byte[])dataReader["data"], (int)dataReader["socket"]);
                                        if (totalLines % 100 == 0)
                                        {
                                            form1.UpdateHeaderLog("MySQL database sync at line: " + totalLines);
                                        }
                                        totalLines++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                form1.UpdateHeaderLog("MySQL database sync finished.");
                            }
                        }
                    }
                }
            }*/
        }
        /*
        public static bool NewDatabase()
        {

            if (!System.IO.File.Exists("PacketDatabase.sqlite"))
            {

                SQLiteConnection.CreateFile("PacketDatabase.sqlite");

                SQLiteConnection connection = new SQLiteConnection("Data Source=PacketDatabase.sqlite;Version=3");
                connection.Open();

                SQLiteCommand command = new SQLiteCommand("CREATE TABLE packets (id TEXT, timestamp INT, line INT, type TEXT, header INT, headerChain TEXT, size INT, status TEXT, data BLOB, socket INT)", connection);

                command.ExecuteNonQuery();

                return true;

            } else
            {
                return false;
            }

        }

        public static void OverwriteDatabase()
        {

            SQLiteConnection.CreateFile("PacketDatabase.sqlite");

            SQLiteConnection connection = new SQLiteConnection("Data Source=PacketDatabase.sqlite;Version=3");
            connection.Open();

            SQLiteCommand command = new SQLiteCommand("CREATE TABLE packets (id TEXT, timestamp INT, line INT, type TEXT, header INT, headerChain TEXT, size INT, status TEXT, data BLOB, socket INT)", connection);

            command.ExecuteNonQuery();

        }

        public static async Task UpdateLocalDatabaseTask(SQLiteConnection sqliteConnection, String id, long timestamp, int line, String type, int header, String headerChain, int size, String status, Byte[] data, int socket)
        {
            await Task.Run(() => UpdateLocalDatabase(sqliteConnection, id, timestamp, line, type, header, headerChain, size, status, data, socket));
        }

        public static void UpdateLocalDatabase(SQLiteConnection sqliteConnection, string id, long timestamp, int line, String type, int header, String headerChain, int size, String status, Byte[] data, int socket)
        {

            string query = "INSERT INTO packets (id, timestamp, line, type, header, headerChain, size, status, data, socket) VALUES (@id, @timestamp, @line, @type, @header, @headerChain, @size, @status, @data, @socket);";


            using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
            {

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@timestamp", timestamp);
                command.Parameters.AddWithValue("@line", line);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@header", header);
                command.Parameters.AddWithValue("@headerChain",headerChain);
                command.Parameters.AddWithValue("@size", size);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@data", data);
                command.Parameters.AddWithValue("@socket", socket);

                command.ExecuteNonQuery();

            }

        }
        */
        public static void UpdateWebDatabase(MySqlConnection connection, string id, long timestamp, int line, String type, int header, StringBuilder headerChain, int size, String status, byte[] bytes, int socket)
        {

            // Here we have to create a "try - catch" block, this makes sure your app
            // catches a MySqlException if the connection can't be opened, 
            // or if any other error occurs.

            try
            {

                // Here we already start using parameters in the query to prevent
                // SQL injection.
                string query = "INSERT INTO giosa0_packet_log.packet_log_revision1 (id, timestamp, line, type, header, headerChain, size, status, data, socket) VALUES (@id, @timestamp, @line, @type, @header, @headerChain, @size, @status, @data, @socket);";

                // Yet again, we are creating a new object that implements the IDisposable
                // interface. So we create a new using statement.

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Now we can start using the passed values in our parameters:

                    //MySqlParameter dataParameter = new MySqlParameter("?data", MySqlDbType.Blob, bytes);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@timestamp", timestamp);
                    command.Parameters.AddWithValue("@line", line);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@header", header);
                    command.Parameters.AddWithValue("@headerChain", headerChain.ToString());
                    command.Parameters.AddWithValue("@size", size);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@data", bytes);
                    command.Parameters.AddWithValue("@socket", socket);
                    // Execute the query
                    command.ExecuteNonQuery();

                }

                // All went well

            }
            catch (MySqlException)
                {
                    // Here we got an error
                }

            }

        }

    }


