﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SummoningBell
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ffxiv_dbEntities : DbContext
    {
        public ffxiv_dbEntities()
            : base("name=ffxiv_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CategoryGroup> CategoryGroups { get; set; }
        public virtual DbSet<HistoryListing> HistoryListings { get; set; }
        public virtual DbSet<ItemByServer> ItemByServers { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<MarketListing> MarketListings { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<RecipeElement> RecipeElements { get; set; }
        public virtual DbSet<RecipeLevel> RecipeLevels { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RetainerLog> RetainerLogs { get; set; }
        public virtual DbSet<Retainer> Retainers { get; set; }
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<ShopItem> ShopItems { get; set; }
        public virtual DbSet<ShopItemMap> ShopItemMaps { get; set; }
    }
}
