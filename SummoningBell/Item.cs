//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.ItemByServers = new HashSet<ItemByServer>();
            this.Recipes = new HashSet<Recipe>();
            this.Recipes1 = new HashSet<Recipe>();
            this.Recipes2 = new HashSet<Recipe>();
            this.Recipes3 = new HashSet<Recipe>();
            this.Recipes4 = new HashSet<Recipe>();
            this.Recipes5 = new HashSet<Recipe>();
            this.Recipes6 = new HashSet<Recipe>();
            this.Recipes7 = new HashSet<Recipe>();
            this.Recipes8 = new HashSet<Recipe>();
            this.Recipes9 = new HashSet<Recipe>();
            this.Recipes10 = new HashSet<Recipe>();
            this.ShopItems = new HashSet<ShopItem>();
        }
    
        public int ItemId { get; set; }
        public int Category { get; set; }
        public string Singular { get; set; }
        public string Plural { get; set; }
        public bool StartsWithVowel { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public byte[] Icon { get; set; }
        public int ItemLevel { get; set; }
        public int EquipLevel { get; set; }
        public int PvpRank { get; set; }
        public long StackSize { get; set; }
        public int Stain { get; set; }
        public int ItemSearchCategory { get; set; }
        public int Rarity { get; set; }
        public bool CanBeHQ { get; set; }
        public bool IsDyeable { get; set; }
        public bool IsCrestWorthy { get; set; }
        public int MaterializeType { get; set; }
        public int MateriaSlotCount { get; set; }
        public int EquipSlotCategory { get; set; }
        public long MainModel { get; set; }
        public long SubModel { get; set; }
        public byte BaseParam0 { get; set; }
        public byte BaseParam1 { get; set; }
        public byte BaseParam2 { get; set; }
        public byte BaseParam3 { get; set; }
        public byte BaseParam4 { get; set; }
        public byte BaseParam5 { get; set; }
        public int BaseParamValue0 { get; set; }
        public int BaseParamValue1 { get; set; }
        public int BaseParamValue2 { get; set; }
        public int BaseParamValue3 { get; set; }
        public int BaseParamValue4 { get; set; }
        public int BaseParamValue5 { get; set; }
        public byte SpecialBaseParam0 { get; set; }
        public byte SpecialBaseParam1 { get; set; }
        public byte SpecialBaseParam2 { get; set; }
        public byte SpecialBaseParam3 { get; set; }
        public byte SpecialBaseParam4 { get; set; }
        public byte SpecialBaseParam5 { get; set; }
        public int SpecialBaseParamValue0 { get; set; }
        public int SpecialBaseParamValue1 { get; set; }
        public int SpecialBaseParamValue2 { get; set; }
        public int SpecialBaseParamValue3 { get; set; }
        public int SpecialBaseParamValue4 { get; set; }
        public int SpecialBaseParamValue5 { get; set; }
        public int SpecialBonus { get; set; }
        public byte RepairClasses { get; set; }
        public int ItemRepair { get; set; }
        public byte UsableClasses { get; set; }
        public int DamagePhysical { get; set; }
        public int DamageMagical { get; set; }
        public int Delay { get; set; }
        public int BlockRate { get; set; }
        public int Block { get; set; }
        public int DefensePhysical { get; set; }
        public int DefenseMagical { get; set; }
        public int Cooldown { get; set; }
        public int ItemAction { get; set; }
        public bool IsDisposable { get; set; }
        public bool IsUnique { get; set; }
        public bool IsUntradable { get; set; }
        public int PriceLow { get; set; }
        public int PriceMid { get; set; }
        public bool PriceHigh { get; set; }
        public bool EquipableByRace0 { get; set; }
        public bool EquipableByRace1 { get; set; }
        public bool EquipableByRace2 { get; set; }
        public bool EquipableByRace3 { get; set; }
        public bool EquipableByRace4 { get; set; }
        public bool EquipableByRace5 { get; set; }
        public bool EquipableByRace6 { get; set; }
        public bool EquipableByMales { get; set; }
        public byte EquipableByFemales { get; set; }
        public int ClassJobCategory { get; set; }
        public byte GrandCompany { get; set; }
        public byte GCTurnIn { get; set; }
        public int ItemSeries { get; set; }
        public bool BaseParamModifier { get; set; }
        public int IsPVP { get; set; }
        public int Glamour { get; set; }
        public bool Desynthesizable { get; set; }
        public int IsCollectable { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemByServer> ItemByServers { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe> Recipes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe> Recipes1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe> Recipes2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe> Recipes3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe> Recipes4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe> Recipes5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe> Recipes6 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe> Recipes7 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe> Recipes8 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe> Recipes9 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recipe> Recipes10 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShopItem> ShopItems { get; set; }
    }
}
