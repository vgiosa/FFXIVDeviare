using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Utility
{

    public class BindingListInvoked<T> : BindingList<T>
    {
        public BindingListInvoked() { }

        private ISynchronizeInvoke _invoke;
        public BindingListInvoked(ISynchronizeInvoke invoke) { _invoke = invoke; }
        public BindingListInvoked(IList<T> items) { this.DataSource = items; }
        delegate void ListChangedDelegate(ListChangedEventArgs e);

        protected override void OnListChanged(ListChangedEventArgs e)
        {

            if ((_invoke != null) && (_invoke.InvokeRequired))
            {
                IAsyncResult ar = _invoke.BeginInvoke(new ListChangedDelegate(base.OnListChanged), new object[] { e });
            }
            else
            {
                base.OnListChanged(e);
            }
        }
        public IList<T> DataSource
        {
            get
            {
                return this;
            }
            set
            {
                if (value != null)
                {
                    this.ClearItems();
                    RaiseListChangedEvents = false;

                    foreach (T item in value)
                    {
                        this.Add(item);
                    }
                    RaiseListChangedEvents = true;
                    OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
                }
            }
        }
    }
}
