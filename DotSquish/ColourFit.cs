using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotSquish {
    internal abstract class ColourFit {
        #region Fields
        protected ColourSet _Colours;
        private SquishOptions _Flags;
        #endregion

        #region Constructor
        protected ColourFit(ColourSet colours, SquishOptions flags) {
            this._Colours = colours;
            this._Flags = flags;
        }
        #endregion

        #region Public
        public void Compress(ref byte[] block) {
            throw new NotImplementedException();
        }
        #endregion


    }
}
