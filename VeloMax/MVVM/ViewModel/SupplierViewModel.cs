using System;
using VeloMax.Core;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace VeloMax.MVVM.ViewModel
{
    class SupplierViewModel : ObservableObject, INotifyPropertyChanged
    {
        public List<List<string>> Suppliers_data { get; set; }
        public BindableCollection<Supplier> Suppliers { get; set; }
        public DataBase Db { get; set; }


        public SupplierViewModel()
        {
            Db = new DataBase();
            InitData();
        }

        public void InitData()
        {
            Suppliers_data = Db.SelectAllListRow("*", "Suppliers");
            Console.WriteLine("ici");
            Console.WriteLine(Suppliers_data.Count);
            Supplier supplier = new Supplier();
            Suppliers = new BindableCollection<Supplier>();
            foreach (var item in Suppliers_data)
            {
                supplier = new Supplier(item);
                Suppliers.Add(supplier);
            }


        }

    }
}
