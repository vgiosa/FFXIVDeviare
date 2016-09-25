using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFXIVDeviare.Packets.Subpackets.Sent;
using FFXIVDeviare.Packets.Subpackets;
using FFXIVDeviare.Game.Events;
using FFXIVDeviare.Packets;
using FFXIVDeviare.Packets.Subpackets.Received;
using System.Threading;
using FFXIVDeviare.Game.Inventory;
using FFXIVDeviare.Packets.Subpackets.Subpackets.Sent.ExpandedPackets;

namespace FFXIVDeviare.Game.Market
{
    public class MarketManager
    {

        public GameManager GameHandler { get; }
        private List<MarketBoardDataForItem> marketListingQueue = new List<MarketBoardDataForItem>();
        private List<MarketBoardHistoryForItem.HistoryListing> historyListingQueue = new List<MarketBoardHistoryForItem.HistoryListing>();
        public delegate void MarketResponse(List<MarketBoardDataForItem.MarketListing> marketData, List<MarketBoardHistoryForItem.HistoryListing> marketHistory);
        MarketResponse marketResponse = null;


        public MarketManager(GameManager g)
        {
            GameHandler = g;
            g.MarketBoardPacketCaptured += this.MarketPacketProcessor;
        }
        


        private void MarketPacketProcessor(Object o, GameEventArgs arg)
        {
            Packet p = arg.Packet;
            MarketBoardHistoryForItem historySubpacket = p.Subpackets.OfType<MarketBoardHistoryForItem>().FirstOrDefault();
            if(historySubpacket != null && marketResponse != null)
            {
                historyListingQueue.AddRange(((MarketBoardHistoryForItem.Data)(historySubpacket.PacketData)).Listings);
            }
            marketListingQueue.AddRange(p.Subpackets.OfType<MarketBoardDataForItem>());



            if(marketListingQueue.Where(s => ((MarketBoardDataForItem.Data)(s.PacketData)).unk1 == 0).Count() > 0 && historyListingQueue.Count > 0) {
                List<MarketBoardDataForItem.MarketListing> dataList = new List<MarketBoardDataForItem.MarketListing>();
                foreach (MarketBoardDataForItem sb in marketListingQueue)
                {
                    dataList.AddRange(((MarketBoardDataForItem.Data)(sb.PacketData)).Listings);
                }
                marketResponse.Invoke(dataList, historyListingQueue);
                marketListingQueue = new List<MarketBoardDataForItem>();
                historyListingQueue = new List<MarketBoardHistoryForItem.HistoryListing>();
            }




        }

        public void GetRetainerSaleList(uint retainerId)
        {
            NPCRepair npcRepair = new NPCRepair();
            NPCRepair.Data npcRepairData = new NPCRepair.Data();
            npcRepairData.ExpandedId = 1132;
            npcRepairData.Unk6 = retainerId;
            npcRepair.PacketData = npcRepairData;

            GameHandler.SendPacket(new List<Subpacket>() { npcRepair });


        }


        public bool GetMarketBoardPriceTablesForItem(uint itemId,MarketResponse marketResponse)
        {
            
            RequestMarketBoardDataForItem marketRequest = new RequestMarketBoardDataForItem();
            RequestMarketBoardDataForItem.Data marketRequestData = new RequestMarketBoardDataForItem.Data();
            marketRequestData.itemId = itemId;
            marketRequestData.unk2 = 2306;
            marketRequest.PacketData = marketRequestData;
            this.GameHandler.SendPacket(new List<Subpacket>() { marketRequest });
            this.marketResponse = marketResponse;
            return true;

        }


        public void UndercutAllThoseHoes()
        {

            
            var retainerMarketItems = GameHandler.InventoryHandler.Inventory[(int) (Inventory.BagType.Retainer_Market)].Values;
            Queue<Item> queue = new Queue<Item>(retainerMarketItems);
            MarketResponse response = null;
            response = (marketList, historyList) =>
            {
                Item currentSale = queue.Dequeue();
                MarketBoardDataForItem.MarketListing lowest = marketList.Where(i => i.hq == currentSale.IsHQ && i.price > 0).OrderBy(i => i.price).FirstOrDefault();
                if (lowest.price != 0) SetRetainerPrice(currentSale.Slot, lowest.price - 1);

                if (queue.Count > 0)
                {
                    Thread.Sleep(3000);
                    GetMarketBoardPriceTablesForItem(queue.First().ItemId, response);
                }
                else
                {
                    this.marketResponse = null;
                }
            };
            if(queue.Count > 0)
            {
                GetMarketBoardPriceTablesForItem(queue.First().ItemId, response);
            }
        }

        public void SetRetainerPrice(uint slot, uint price)
        {
            RetainerSetItemPrice retainerSetPrice = new RetainerSetItemPrice();
            RetainerSetItemPrice.Data retainerSetPriceData = new RetainerSetItemPrice.Data();
            retainerSetPriceData.ExpandedId = (uint)retainerSetPrice.ExpandedId;
            retainerSetPriceData.SlotId = slot;
            retainerSetPriceData.Price = price;
            retainerSetPrice.PacketData = retainerSetPriceData;

            GameHandler.SendPacket(new List<Subpacket>() { retainerSetPrice });
        }

        public bool BuyAllItems(uint itemId, uint maxPrice)
        {
            
            GetMarketBoardPriceTablesForItem(itemId, (marketList, historyList) => {
                
                Queue<MarketBoardDataForItem.MarketListing> itemsToBuy = new Queue<MarketBoardDataForItem.MarketListing>();
                foreach(var marketItem in marketList)
                {
                    if(marketItem.price <= maxPrice)
                    {
                        itemsToBuy.Enqueue(marketItem);
                    }
                    //433
                }


                GameManager.PacketResponse response = null;

                
                response = (packet) =>
                {
                    if (itemsToBuy.Count > 0)
                    {
                        
                        BuyItemFromMarketBoard(itemsToBuy.Dequeue(), response);
                    }

                };
                if (itemsToBuy.Count > 0) {
                    
                    BuyItemFromMarketBoard(itemsToBuy.Dequeue(), response);
                }

            });
            return true;
        }


        public void BuyItemFromMarketBoard(MarketBoardDataForItem.MarketListing listing, GameManager.PacketResponse finished)
        {


                BuyItemFromMarketBoard marketRequest = new BuyItemFromMarketBoard();
                BuyItemFromMarketBoard.Data marketRequestData = new BuyItemFromMarketBoard.Data()
                {
                    hq = listing.hq,
                    quantity = listing.qty,
                    itemId = listing.itemId,
                    magic_7864343 = 7864343,
                    postId = listing.postId,
                    price = listing.price,
                    retainerId = listing.retainerId,
                    City = listing.city,
                    Token = listing.Token,
                    OtherToken = listing.OtherToken,
                    QVal_1 = listing.qval_1


                };

                marketRequest.PacketData = marketRequestData;
                this.GameHandler.SendPacket(new List<Subpacket>() { marketRequest }, finished, 433);


        }

    }
}
