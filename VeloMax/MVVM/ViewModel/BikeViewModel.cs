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
    class BikeViewModel : ObservableObject, INotifyPropertyChanged
    {
        private List<List<string>> bikes_data { get; set; }
        public BindableCollection<Bike> Bikes { get; set; }
        public DataBase Db { get; set; }

        

        public event PropertyChangedEventHandler PropertyChanged;

        public List<List<string>> Bikes_data
        {
            get => bikes_data; set
            {
                bikes_data = value;
                OnPropertyChanged(nameof(Bikes_data));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand BikeViewRefresh { get; set; }
        public BikeViewModel()
        {
            Db = new DataBase();
            InitData();

            BikeViewRefresh = new RelayCommand(o =>
            {
                Console.WriteLine("Refreshing");
                InitData();
            });

        }

 

        public void InitData()
        {
            bikes_data = Db.SelectAllListRow("*", "Bikes");
            Console.WriteLine("ici");
            Console.WriteLine(bikes_data.Count);
            Bike bike = new Bike();
            Bikes = new BindableCollection<Bike>();
            foreach (var item in bikes_data)
            {
                bike = new Bike(item);
                Bikes.Add(bike);
            }


        }
    }
}
