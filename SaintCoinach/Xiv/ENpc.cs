using System;
using System.Collections.Generic;
using System.Linq;

using SaintCoinach.Xiv.Collections;

namespace SaintCoinach.Xiv {
    public class ENpc : ILocatable, IQuantifiableXivString {
        #region Fields

        private ENpcBase _Base;
        private ENpcResident _Resident;
        private ILocation[] _Locations;

        #endregion

        #region Properties

        public int Key { get; private set; }
        public ENpcCollection Collection { get; private set; }
        public ENpcResident Resident { get { return _Resident ?? (_Resident = Collection.ResidentSheet[Key]); } }
        public ENpcBase Base { get { return _Base ?? (_Base = Collection.BaseSheet[Key]); } }
        public Text.XivString Singular { get { return Resident.Singular; } }
        public Text.XivString Plural { get { return Collection.Collection.ActiveLanguage == Ex.Language.Japanese ? Singular : Resident.Plural; } }
        public Text.XivString Title { get { return Resident.Title; } }

        public IEnumerable<ILocation> Locations { get { return _Locations ?? (_Locations = BuildLocations()); } }

        #endregion

        #region Constructors

        public ENpc(ENpcCollection collection, int key) {
            Key = key;
            Collection = collection;
        }

        #endregion

        #region Build

        private Level[] BuildLevels() {
            return Collection.Collection.GetSheet<Level>().Where(_ => _.ObjectKey == Key).ToArray();
        }

        private ILocation[] BuildLocations() {
            var levelLocations = BuildLevels();

            var coll = Collection.Collection;
            if (!coll.IsLibraAvailable)
                return levelLocations.Cast<ILocation>().ToArray();

            
            var locations = new List<ILocation>();
            locations.AddRange(levelLocations.Cast<ILocation>());

            var placeNames = coll.GetSheet<PlaceName>();
            var maps = coll.GetSheet<Map>();

            

            return locations.ToArray();
        }
        #endregion

        public override string ToString() {
            return Resident.Singular;
        }

        #region IQuantifiableName Members
        string IQuantifiable.Singular {
            get { return Singular; }
        }

        string IQuantifiable.Plural {
            get { return Plural; }
        }
        #endregion
    }
}
