using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeloMax.Core;

namespace VeloMax.MVVM.ViewModel
{
    internal class OrderWindowModel : ObservableObject
    {
        private BindableCollection<Object> _orders;

        public BindableCollection<Object> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnProportyChanged();
            }
        }

        private string _orderRow;

        public string OrderRow
        {
            get { return _orderRow; }
            set { _orderRow = value; OnProportyChanged(); }
        }

        public DataBase Db { get; set; }

        public OrderWindowModel()
        {

            Db = new DataBase();
        }
    }
}
