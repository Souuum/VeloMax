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
        private string tFullName;
        private string tCompanyName;
        private string tFullAddr;
        private string tPhone;
        private string tMail;
        private string tDateS;
        private string tDateE;
        private string tStock;
        private string tFP;

        public string TFullName
        {
            get => tFullName; set
            {
                tFullName = value;
                OnPropertyChanged(nameof(TFullName));
            }
        }
        public string TCompanyName
        {
            get => tCompanyName; set
            {
                tFullName = value;
                OnPropertyChanged(nameof(TCompanyName));
            }
        }
        public string TFullAddr
        {
            get => tFullAddr; set
            {
                tFullAddr = value;
                OnPropertyChanged(nameof(TFullAddr));
            }
        }
        public string TPhone
        {
            get => tPhone; set
            {
                tPhone = value;
                OnPropertyChanged(nameof(TPhone));
            }
        }

        public string TMail
        {
            get => tMail; set
            {
                tMail = value;
                OnPropertyChanged(nameof(TMail));
            }
        }
        public string TFP
        {
            get => tFP; set
            {
                tFP = value;
                OnPropertyChanged(nameof(TFP));
            }
        }

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
