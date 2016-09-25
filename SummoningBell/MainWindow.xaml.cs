using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FFXIVDeviare;
using FFXIVDeviare.Game.Events;
using System.Diagnostics;
using FFXIVDeviare.Game;
using SaintCoinach;
using System.Collections;

namespace SummoningBell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private ARealmReversed realmData;
        private GameManager game;

        static ARealmReversed SetUpSaintCoinach(string gamePath)
        {

            var realm = new ARealmReversed(gamePath, @"SaintCoinach.History.zip", SaintCoinach.Ex.Language.English, @"app_data.sqlite");
            realm.Packs.GetPack(new SaintCoinach.IO.PackIdentifier("exd", SaintCoinach.IO.PackIdentifier.DefaultExpansion, 0)).KeepInMemory = true;
            return realm;
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
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
            FFXIVDeviare.Game.Market.MarketManager.MarketResponse mRepsonse = (marketListing, historyListing) => {


                ItemByServer serverItem = db.ItemByServers.Where(i => i.ItemId == itemId && i.Server == server.ServerId).FirstOrDefault(); ;
                db.MarketListings.RemoveRange(db.MarketListings.Where(i => i.ItemByServer == serverItem ));
                foreach(FFXIVDeviare.Packets.Subpackets.Received.MarketBoardDataForItem.MarketListing ml in marketListing)
                {
                    if(serverItem == null)
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
                        PlayerId = (int) ml.sellerId,
                        
                              
                    });

                    Retainer retainer = db.Retainers.Where(i => i.RetainerId == ml.retainerId).FirstOrDefault();
                    if (retainer == null) retainer = db.Retainers.Add(new Retainer()
                    {
                        LastUpdated = new DateTime(ml.timestamp),
                        RetainerName = ml.name,
                        Server1 = server,
                        RetainerId = (int) ml.retainerId,
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
                        Price = (int) ml.price,
                        Quantity = ml.qty.ToString(),
                        Retainer1 = retainer,
                    });

                }
                db.SaveChanges();



            };

            if (items.Count > 0) {
                itemId = (uint) (items.Dequeue().ItemId) ;
                game.MarketHandler.GetMarketBoardPriceTablesForItem(itemId, mRepsonse);
            }
        }


        
        public void PacketCaptured(object sender, GameEventArgs ev)
        {


        }

    }
}
