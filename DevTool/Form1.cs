using FFXIVDeviare.Game;
using FFXIVDeviare.Game.Events;

using FFXIVDeviare.Packets;
using FFXIVDeviare.Packets.Subpackets;
using FFXIVDeviare.Properties;
using FFXIVDeviare.Utility;
using SaintCoinach;
using SummoningBell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FFXIVDeviare
{
    public partial class Form1 : Form
    {
        //public SQLiteConnection sqliteConnection;
        PacketDatabaseManager packetDatabaseManager = null;

        public int unk33_105 = 0;
        public int unk33_209 = 0;
        public int unk33_286 = 0;

        public bool injected = false;

        public string openSQLiteFileName;
        public string username;
        public int currentZone = 0;
        public long characterId;
        public int gameSocket = 0;
        private int chatSocket = 0;
        List<GameManager> games = new List<GameManager>();

        TreeNode rootNode;
        TreeNode sentNode;
        TreeNode receivedNode;
        TreeNode unknownNode;
        TreeNode headsNode;

        TreeNode subheadsNode;


        Dictionary<Type, BindingList<Object>> receivedPackets = new Dictionary<Type, BindingList<Object>>();
        Dictionary<Type, BindingList<Object>> sentPackets = new Dictionary<Type, BindingList<Object>>();
        BindingListInvoked<PacketHeader> heads;
        BindingListInvoked<SubpacketHeader> subheads;
        BindingListInvoked<Object> unknownPackets;
        ARealmReversed realmData;
        public Form1()
        {
            InitializeComponent();
        }


        static ARealmReversed SetUpSaintCoinach(string gamePath)
        {
            
            var realm = new ARealmReversed(gamePath, @"SaintCoinach.History.zip", SaintCoinach.Ex.Language.English, @"app_data.sqlite");
            realm.Packs.GetPack(new SaintCoinach.IO.PackIdentifier("exd", SaintCoinach.IO.PackIdentifier.DefaultExpansion, 0)).KeepInMemory = true;
            return realm;
        }


        static string SearchForDataPath()
        {
            string programDir;
            if (Environment.Is64BitProcess)
                programDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            else
                programDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            return System.IO.Path.Combine(programDir, "SquareEnix", "FINAL FANTASY XIV - A Realm Reborn");
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
            int t = Subpacket.SubpacketHeaderSize;
            
            unknownPackets = new BindingListInvoked<Object>(dataGridView1);
            subheads = new BindingListInvoked<SubpacketHeader>(dataGridView1);
            heads = new BindingListInvoked<PacketHeader>(dataGridView1);
            //Subpacket sp = new Packet261();
            textBox2.ReadOnly = true;
            textBox2.Text = "Enter a username and then press start to begin logging data." + Environment.NewLine;
            textBox2.WordWrap = false;

            //subpacketList.DataSource = Subpacket.SubpacketSentDefinitions.Values.ToList<Type>();
            //subpacketList.ValueMember = "Name";

            rootNode = treeView1.Nodes.Add("Packets");
            sentNode = rootNode.Nodes.Add("Sent");
            receivedNode = rootNode.Nodes.Add("Received");
            unknownNode = rootNode.Nodes.Add("Unknown");
            headsNode = rootNode.Nodes.Add("Headers");
            subheadsNode = rootNode.Nodes.Add("Subheaders");
            /*
            foreach(int key in Subpacket.SubpacketRecievedDefinitions.Keys)
            {
                receivedNode.Nodes.Add(key.ToString(), Subpacket.SubpacketRecievedDefinitions[key].Name);
            }
            foreach (int key in Subpacket.SubpacketSentDefinitions.Keys)
            {
                sentNode.Nodes.Add(key.ToString(), Subpacket.SubpacketSentDefinitions[key].Name).;
            }
            */

        }

        private void button6_Click(object sender, EventArgs e)
        {

            /*
            SummoningBellDialogRequest
            SelectRetainer(retainerIndex)
            RepairAllArmor
            MarketBoardDialogRequest
            PostItemToMarketBoard(sourceBag, sourceSlot, quantity, price)
            */

            if (!injected) return;

            UpdateHeaderLog("Attempting to send packet...");
            GameManager game = games[0];

            string command = "";
            string[] strings = textBox1.Text.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            if (strings.Count() > 0)
            {
                command = strings[0];
            }

            switch (command)
            {

                case "SummoningBellDialogRequest":

                    game.SummoningBellDialogRequest();
                    UpdateHeaderLog("SummoningBellDialogRequest initiated");

                    break;

                case "SelectRetainer":
                    
                    // To use select retainer:
                    // Type SelectRetainer 0
                    // This will get your first retainer
                    // Type SelectRetainer 1
                    // This will get your second retainer
                    // This works for all 8 retainers

                    int retainerIndex = Int32.Parse(strings[1]);
                    game.SelectRetainer(retainerIndex);
                    UpdateHeaderLog("SelectRetainer initiated");

                    break;

                case "RepairAllArmor":

                    game.RepairAllArmor();
                    UpdateHeaderLog("RepairAllArmor initiated");

                    break;

                case "MarketBoardDialogRequest":

                    game.MarketBoardDialogRequest();
                    UpdateHeaderLog("MarketBoardDialogRequest initiated");

                    break;

                case "PostItemToMarketBoard":

                    // Example usage...
                    // PostItemToMarketBoard 0 1 99 1000
                    // 0 = bag 1 / 1 = slot 2 / 99 = quantity / 1000 = price

                    int sourceBag = Int32.Parse(strings[1]);
                    int sourceSlot = Int32.Parse(strings[2]);
                    int quantity = Int32.Parse(strings[3]);
                    int price = Int32.Parse(strings[4]);
                    game.PostItemToMarketBoard(sourceBag, sourceSlot, quantity, price);
                    UpdateHeaderLog("PostItemToMarketBoard initiated");

                    break;

                default:

                    UpdateHeaderLog("Invalid request...");

                    break;

            }

            textBox1.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (Process p in Process.GetProcessesByName("ffxiv"))
            {
                if (realmData == null) realmData = SetUpSaintCoinach(p.MainModule.FileName.Substring(0, p.MainModule.FileName.Length - p.MainModule.ModuleName.Length));
                GameManager game = new GameManager(p, realmData);
                game.PacketCaptured += PacketCaptured;
                game.Begin();
                games.Add(game);
            }
            UpdateHeaderLog("Injection to FFXIV successful.");
            injected = true;

        }

        public void PacketCaptured(object sender, GameEventArgs ev)
        {

            Packet p = ev.Packet;
            if (p.FailedMagic)
            {
                //if(p.RawDecompressedData != null) UpdateHeaderLog(p.IsSent + ": Failed Magic: " + ByteUtils.ByteArrayToString(p.RawDecompressedData));
                heads.Add(p.Header);
                return;
            }

            if (p.Subpackets.Count == 0) return;
            if (p.Subpackets[0].SubpacketHeader.ZoneId > 0) this.currentZone = p.Subpackets[0].SubpacketHeader.ZoneId;

            if (p.isChatPacket)
            {
                chatSocket = p.Socket;
            }
            else if (p.Socket != chatSocket)
            {
                gameSocket = p.Socket;
            }

            if (p.Subpackets[0].SubpacketHeader.SourceId > 0 && p.IsSent) this.characterId = p.Subpackets[0].SubpacketHeader.SourceId;
            toolStripStatusLabel1.Text = "Zone: " + this.currentZone;
            toolStripStatusLabel2.Text = "Game Socket: " + gameSocket;
            toolStripStatusLabel3.Text = "Chat Socket: " + chatSocket;
            toolStripStatusLabel4.Text = "Char Id: " + characterId;

            /*
            if(p.Subpackets.First().SubpacketId == 261)
            {
                if (p.Subpackets.First().PacketData is IEnumerable)
                {
                    foreach(Object o in (List<Object>)p.Subpackets.First().PacketData){
                        if(((Packet261.packet_subpacket_content_261)o).retainerId > 0)
                            UpdateHeaderLog(ByteUtils.StructToString(o));
                    }
                }
                //UpdateHeaderLog());
            }
            */
            Dictionary<Type, BindingList<Object>> activeDictionary = p.IsSent ? sentPackets : receivedPackets;


            TreeNode activeNode = p.IsSent ? sentNode : receivedNode;

            StringBuilder stringBuilder = new StringBuilder("");

            if (p.IsSent)
            {
                stringBuilder.Append("SENT:");
            } else
            {
                stringBuilder.Append("RECV:");
            }

            foreach (Subpacket subPacket in p.Subpackets)
            {

                if(subPacket.SubpacketId == 0)
                {
                    UpdateHeaderLog("Unkpacket" + subPacket.SubpacketHeader.Id + ": " + ByteUtils.ByteArrayToString(subPacket.RawPacketData));
                }

                if (p.Subpackets.Count() == 1)
                {

                    if (subPacket.SubpacketId == 320 || subPacket.SubpacketId == 321 || subPacket.SubpacketId == 322 || subPacket.SubpacketId == 323 || subPacket.SubpacketId == 324 || subPacket.SubpacketId == 325 || subPacket.SubpacketId == 101 || subPacket.SubpacketId == 402)
                    {
                        return;
                    }

                }

                stringBuilder.Append(" Id: " + subPacket.SubpacketId.ToString() + " Token: " + subPacket.SubpacketHeader.Token.ToString() + " Size: " + subPacket.SubpacketHeader.Size.ToString() + ", ");

                
                if (subPacket.SubpacketId == 475 || subPacket.SubpacketId == 476)
                {
                    UpdateHeaderLog("Examine " + subPacket.SubpacketHeader.Id + "(" + subPacket.RawPacketData.Length + ":" + subPacket.GetStructureSize() + ") : " + ByteUtils.ByteArrayToString(subPacket.RawPacketData));
                }
    
                subheads.Add(subPacket.SubpacketHeader);
                if (p.IsSent)
                {
                    //UpdateHeaderLog(subPacket.SubpacketId.ToString() + ": " + ByteUtils.ByteArrayToString(subPacket.RawPacketData));
                }
                if (subPacket.SubpacketId == 0)
                {
                    unknownPackets.Add(subPacket.PacketData);
                    continue;
                }
                if (!activeDictionary.ContainsKey(subPacket.GetType()))
                {
                    activeDictionary[subPacket.GetType()] = new BindingListInvoked<Object>(dataGridView1);
                    treeView1.Invoke((MethodInvoker)(() => activeNode.Nodes.Add(subPacket.SubpacketId.ToString(), subPacket.GetType().Name).Tag = subPacket.GetType()));
                    foreach(FieldInfo fi in subPacket.PacketData.GetType().GetFields())
                    {

                        if (fi.FieldType.IsArray)
                        {
                            activeDictionary[fi.FieldType.GetElementType()] = new BindingListInvoked<Object>(dataGridView1);
                            treeView1.Invoke((MethodInvoker)(() => activeNode.Nodes.Add(subPacket.SubpacketId.ToString() + " List", fi.FieldType.GetElementType().Name).Tag = fi.FieldType.GetElementType()));
                        }
                    }

                }
                if (subPacket.PacketData is IEnumerable)
                {
                    foreach (Object o in (IEnumerable)subPacket.PacketData)
                    {
                        activeDictionary[subPacket.GetType()].Add(o);
                    }


                }

                else {
                    activeDictionary[subPacket.GetType()].Add(subPacket.PacketData);

                    foreach (FieldInfo fi in subPacket.PacketData.GetType().GetFields())
                    {

                        if (fi.FieldType.IsArray)
                        {
                            foreach(var g in (Array) fi.GetValue(subPacket.PacketData)){
                                activeDictionary[fi.FieldType.GetElementType()].Add(g);
                            }
                        }
                        
                    }

                }
            }

            if (p.IsSent)
            {
                //UpdateHeaderLog(stringBuilder.ToString());
            }


            UpdateHeaderLog(stringBuilder.ToString());
            

        }

        public delegate void AddItem<T>(T item);

        private void printCapturedSubpacketIds()
        {

            StringBuilder sentSB = new StringBuilder();
            StringBuilder recvSB = new StringBuilder();

            sentSB.Append("SENT headers: ");
            recvSB.Append("RECV headers: ");



        }

        private void checkForSQLiteConnection()
        {
            /*
            if (sqliteConnection == null)
            {
                sqliteConnection = new SQLiteConnection("Data Source=PacketDatabase.sqlite;Version=3");
                sqliteConnection.Open();
            }
            */
        }

        // Constants for extern calls to various scrollbar functions
        private const int SB_HORZ = 0x0;
        private const int SB_VERT = 0x1;
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int SB_THUMBPOSITION = 4;
        private const int SB_BOTTOM = 7;
        private const int SB_OFFSET = 13;

        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetScrollPos(IntPtr hWnd, int nBar);
        [DllImport("user32.dll")]
        private static extern bool PostMessageA(IntPtr hWnd, int nBar, int wParam, int lParam);
        [DllImport("user32.dll")]
        static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

        private delegate void UpdateHeaderLogDelegate(string headerLogText);
        public void UpdateHeaderLog(string input)
        {
            bool bottomFlag = false;
            int VSmin;
            int VSmax;
            int sbOffset;
            int savedVpos;

            if (InvokeRequired)
            {
                textBox2.BeginInvoke(new UpdateHeaderLogDelegate(UpdateHeaderLog), new object[] { input });
            }
            else {

                // Win32 magic to keep the textbox scrolling to the newest append to the textbox unless
                // the user has moved the scrollbox up
                sbOffset = (int)((this.textBox2.ClientSize.Height - SystemInformation.HorizontalScrollBarHeight) / (this.textBox2.Font.Height));
                savedVpos = GetScrollPos(this.textBox2.Handle, SB_VERT);
                GetScrollRange(this.textBox2.Handle, SB_VERT, out VSmin, out VSmax);
                if (savedVpos >= (VSmax - sbOffset - 1))
                    bottomFlag = true;
                this.textBox2.AppendText(input + Environment.NewLine);
                if (bottomFlag)
                {
                    GetScrollRange(this.textBox2.Handle, SB_VERT, out VSmin, out VSmax);
                    savedVpos = VSmax - sbOffset;
                    bottomFlag = false;
                }
                SetScrollPos(this.textBox2.Handle, SB_VERT, savedVpos, true);
                PostMessageA(this.textBox2.Handle, WM_VSCROLL, SB_THUMBPOSITION + 0x10000 * savedVpos, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click_1(object sender, EventArgs e)
        {

        }

        private void openSQLiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog.Filter = "SQLite Files (.sqlite)|*.sqlite";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Debug.Print(openFileDialog.FileName);
                openSQLiteFileName = openFileDialog.FileName;
                //SQLiteFileNameSelected();
                
            }
        }

        private void startMySQLSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (packetDatabaseManager.syncMySQLDatabaseThreadRunning)
            {
                packetDatabaseManager.StopSyncMySQLDatabseThread();
                startMySQLSyncToolStripMenuItem.Text = "Start MySQL Sync";
            }
            else
            {
                packetDatabaseManager.StartSyncMySQLDatabseThread();
                startMySQLSyncToolStripMenuItem.Text = "Stop MySQL Sync";
            }
        }

        private DataGridView SQLiteDataGridView(TabPage tabPage)
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.Location = new Point(0, 0);
            dataGridView.Size = tabPage.Size;
            dataGridView.ReadOnly = true;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            /*
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + openSQLiteFileName + ";Version=3");
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * From packets", connection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView.DataSource = dataSet.Tables[0].DefaultView;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            */
            return dataGridView;
        }

 
        private void SQLiteFileNameSelected()
        {
            bool createSQLiteTabPage = true;

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                if (tabPage.Text == "SQLite")
                {
                    UpdateSQLiteTabPage(tabPage);
                    createSQLiteTabPage = false;
                    break;
                }
            }

            if (createSQLiteTabPage)
            {
                CreateSQLiteTabPage();
            }

            structureToolStripMenuItem.Enabled = true;
        }

        private void CreateSQLiteTabPage()
        {
            TabPage tabPage = new TabPage("SQLite");
            tabControl1.TabPages.Add(tabPage);
            DataGridView dataGridView = SQLiteDataGridView(tabPage);
            tabPage.Controls.Add(dataGridView);
            tabControl1.SelectedIndex = 2;
        }

        private void UpdateSQLiteTabPage(TabPage tabPage)
        {
            DataGridView dataGridView = SQLiteDataGridView(tabPage);
            tabPage.Controls.RemoveAt(0);
            tabPage.Controls.Add(dataGridView);
            tabControl1.SelectedIndex = 2;
        }

        private void packetHeadersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            TabPage tabPage = new TabPage("Packet Header");
            tabControl1.TabPages.Add(tabPage);
            DataGridView dataGridView = PacketHeaderDataGridView(tabPage);
            tabPage.Controls.Add(dataGridView);
            tabControl1.SelectedIndex = 3;

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + openSQLiteFileName + ";Version=3"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM packets WHERE type LIKE 'packet header%'", connection))
                {
                    using (SQLiteDataReader dataReader = command.ExecuteReader())
                    {
                        var stringBuilder = new StringBuilder("");
                        while (dataReader.Read())
                        {
                            byte[] bytes = StringToByteArray(dataReader["data"].ToString().Replace("new byte[]  ", "").Replace(" ", ""));
                            string hex = dataReader["data"].ToString().Replace("new byte[]  ", "");
                            string magic = hex.Substring(0, Math.Min(hex.Length, 48));
                            PacketHeader header = PacketHeader.ReadStruct(bytes);

                            DataGridViewRow row = (DataGridViewRow)dataGridView.Rows[0].Clone();
                            row.Cells[0].Value = dataReader["id"].ToString();
                            row.Cells[1].Value = dataReader["type"].ToString();
                            row.Cells[2].Value = dataReader["status"].ToString();
                            AddLineToPacketHeaderDataGridView(dataGridView, row, header, magic);
                        }
                    }
                }
            }*/
        }

        private DataGridView PacketHeaderDataGridView(TabPage tabPage)
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.Location = new Point(0, 0);
            dataGridView.Size = tabPage.Size;
            dataGridView.ReadOnly = true;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dataGridView.Columns.Add("id", "id");
            dataGridView.Columns.Add("type", "type");
            dataGridView.Columns.Add("status", "status");

            PacketHeader header = new PacketHeader();
            foreach (var field in header.GetType().GetFields())
            {
                dataGridView.Columns.Add(field.Name, field.Name);
            }

            return dataGridView;
        }

        private void AddLineToPacketHeaderDataGridView(DataGridView dataGridView, DataGridViewRow row, PacketHeader header, string magic)
        {
            int counter = 3;
            foreach (var field in header.GetType().GetFields())
            {

                bool magicField = field.Name.Equals("h_magic", StringComparison.Ordinal);
                if (magicField)
                {
                    row.Cells[counter].Value = magic;
                }
                else {
                    string value = field.GetValue(header).ToString();
                    row.Cells[counter].Value = value;
                }
                counter++;

            }
            dataGridView.Rows.Add(row);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            sentNode.Nodes.Clear();
            receivedNode.Nodes.Clear();
            sentPackets.Clear();
            receivedPackets.Clear();
        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (e.Node != null && e.Node.Parent != null)
            {
                dataGridView1.AutoGenerateColumns = true;
                if (e.Node.Parent.Equals(sentNode))
                {
                    if (sentPackets.ContainsKey((Type)e.Node.Tag))
                    {
                        dataGridView1.DataSource = sentPackets[(Type)e.Node.Tag];

                    }
                }
                else if (e.Node.Parent.Equals(receivedNode))
                {
                    if (receivedPackets.ContainsKey((Type)e.Node.Tag))
                    {
                        dataGridView1.DataSource = receivedPackets[(Type)e.Node.Tag];
                    }
                }

                else if (e.Node.Equals(unknownNode))
                {
                    dataGridView1.DataSource = unknownPackets;
                }
                else if (e.Node.Equals(subheadsNode))
                {
                    dataGridView1.DataSource = subheads;
                }
                else if (e.Node.Equals(headsNode))
                {
                    dataGridView1.DataSource = heads;
                }
            }
            dataGridView1.AutoResizeColumns();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void addSubpacket_Click(object sender, EventArgs e)
        {
            //if(subpacketList.SelectedValue != null) listBox1.Items.Add(subpacketList.SelectedValue);
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            UpdateHeaderLog("Data: " + ByteUtils.ByteArrayToString(ByteUtils.ConvertStructToByteArray(dataGridView1.Rows[e.RowIndex].DataBoundItem)));

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            games.ForEach(g => g.RepairAllArmor());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GameManager game = games.FirstOrDefault();
            ffxiv_dbEntities db = new ffxiv_dbEntities();
            var items = new Queue<dynamic>(from item in db.Items where item.IsUntradable == false select new { ItemId = item.ItemId });
            foreach (Process p in Process.GetProcessesByName("ffxiv"))
            {
                if (realmData == null) realmData = SetUpSaintCoinach(p.MainModule.FileName.Substring(0, p.MainModule.FileName.Length - p.MainModule.ModuleName.Length));
                game = new GameManager(p, realmData);
                game.PacketCaptured += PacketCaptured;
                game.Begin();
                break;
            }
            uint itemId = 0;
            Server server = db.Servers.Where(d => d.ServerName == "Behemoth").FirstOrDefault();
            FFXIVDeviare.Game.Market.MarketManager.MarketResponse mRepsonse = null;
            mRepsonse = (marketListing, historyListing) => {


                ItemByServer serverItem = db.ItemByServers.Where(i => i.ItemId == itemId && i.Server == server.ServerId).FirstOrDefault(); ;
                db.MarketListings.RemoveRange(db.MarketListings.Where(i => i.ItemByServer == serverItem));
                foreach (FFXIVDeviare.Packets.Subpackets.Received.MarketBoardDataForItem.MarketListing ml in marketListing)
                {
                    if (serverItem == null)
                    {
                        serverItem = db.ItemByServers.Add(new ItemByServer()
                        {
                            Server1 = server,
                            ItemId = (int)itemId
                        });
                    }

                    Player player = db.Players.Where(i => i.PlayerId == ml.sellerId).FirstOrDefault();
                    if (player == null) player = db.Players.Add(new Player()
                    {
                        Server1 = server,
                        PlayerId = (int)ml.sellerId,


                    });

                    Retainer retainer = db.Retainers.Where(i => i.RetainerId == ml.retainerId).FirstOrDefault();
                    if (retainer == null) retainer = db.Retainers.Add(new Retainer()
                    {
                        LastUpdated = new DateTime(ml.timestamp),
                        RetainerName = ml.name,
                        Server1 = server,
                        RetainerId = (int)ml.retainerId,
                    });

                    db.MarketListings.Add(new MarketListing()
                    {
                        City = ml.city,
                        HQ = ml.hq > 0,
                        ItemByServer = serverItem,
                        Materia1 = ml.materia1.ToString(),
                        Materia2 = ml.materia2.ToString(),
                        Materia3 = ml.materia3.ToString(),
                        Materia4 = ml.materia4.ToString(),
                        Materia5 = ml.materia5.ToString(),
                        Price = (int)ml.price,
                        Quantity = ml.qty.ToString(),
                        Retainer1 = retainer,
                    });

                }
                db.SaveChanges();

                if (items.Count > 0)
                {
                    Thread.Sleep(3000);
                    itemId = (uint)(items.Dequeue().ItemId);
                    game.MarketHandler.GetMarketBoardPriceTablesForItem(itemId, mRepsonse);
                }

            };

            if (items.Count > 1)
            {
                
                itemId = (uint)(items.Dequeue().ItemId);
                itemId = (uint)(items.Dequeue().ItemId);
                game.MarketHandler.GetMarketBoardPriceTablesForItem(itemId, mRepsonse);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            games.ForEach(g => g.MarketHandler.UndercutAllThoseHoes());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            games.ForEach(g => g.MarketHandler.GetRetainerSaleList(1248118383));
            
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
