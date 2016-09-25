using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace SaintCoinach.Libra {
   public partial class BNpcName {
        #region Fields



        #endregion

        #region Properties
        public IEnumerable<Tuple<int, Tuple<int, int[]>[]>> Regions { get { return null; } }
        public IEnumerable<int> Items { get {  return null; } }
        public IEnumerable<int> NonPops { get {  return null; } }
        public IEnumerable<int> InstanceContents { get {  return null; } }

        public long NameKey { get { return 0 % 10000000000; } }
        public long BaseKey { get { return 0 / 10000000000; } }
        #endregion


    }
}
