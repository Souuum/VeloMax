using System;
using VeloMax.Core;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using System.Runtime.CompilerServices;

namespace VeloMax.MVVM.ViewModel
{
    class CustomerViewModel : ObservableObject, INotifyPropertyChanged
    {
        private List<List<string>> customers_data { get; set; }
        public BindableCollection<Customer> Customers { get; set; }
        public DataBase Db { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        
        public List<List<string>> Customers_data
        {
            get => customers_data; set
            {
                customers_data = value;
                OnPropertyChanged(nameof(Customers_data));
            }
        }
        public RelayCommand CustomerViewRefresh { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CustomerViewModel()
        {
            Db = new DataBase();
            InitData();

            CustomerViewRefresh = new RelayCommand(o =>
            {
                Console.WriteLine("Refreshing");
                InitData();
            });
        }

        public void InitData()
        {
            customers_data = Db.SelectAllListRow("*", "Customers");
            Console.WriteLine("ici");
            Console.WriteLine(customers_data.Count);
            Customer customer = new Customer();
            Customers = new BindableCollection<Customer>();
            foreach (var item in customers_data)
            {
                customer = new Customer(item);
                Customers.Add(customer);
            }


        }

        

    }
}
