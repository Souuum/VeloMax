using System;
using VeloMax.Core;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using System.Runtime.CompilerServices;
using System.Windows;

namespace VeloMax.MVVM.ViewModel
{
    class BikeViewModel : ObservableObject, INotifyPropertyChanged
    {
        private List<List<string>> bikes_data { get; set; }

        private string tName;
        private string tSize;
        private string tPrice;
        private string tType;
        private string tDateS;
        private string tDateE;
        private string tStock; 



        

        public string BikeRow { get; set; }

        
        public BindableCollection<Bike> Bikes { get; set; }
        public DataBase Db { get; set; }

        
        


        public event PropertyChangedEventHandler PropertyChanged;

        public string TName 
        { 
            get => tName; set 
            { 
                tName = value;
                OnPropertyChanged(nameof(TName));
            } 
        }
        public string TSize
        {
            get => tSize; set
            {
                tSize = value;
                OnPropertyChanged(nameof(TSize));
            }
        }
        public string TPrice
        {
            get => tPrice; set
            {
                tPrice = value;
                OnPropertyChanged(nameof(TPrice));
            }
        }

        public string TType
        {
            get => tType; set
            {
                tType = value;
                OnPropertyChanged(nameof(TType));
            }
        }
        public string TDateS
        {
            get => tDateS; set
            {
                tDateS = value;
                OnPropertyChanged(nameof(TDateS));
            }
        }
        public string TDateE
        {
            get => tDateE; set
            {
                tDateE = value;
                OnPropertyChanged(nameof(TDateE));
            }
        }
        public string TStock
        {
            get => tStock; set
            {
                tStock = value;
                OnPropertyChanged(nameof(tStock));
            }
        }

 

        

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
        public RelayCommand BikeAdd { get; set; }

        public RelayCommand BikeDelete { get; set; }

        public RelayCommand BikeUpdate { get; set; }

        public RelayCommand BikePartsOpen { get; set; }



        public BikeViewModel()
        {
            
            Db = new DataBase();
            InitData();
            BikePartsOpen = new RelayCommand(o =>
            {
                Console.WriteLine("Opening 1");
                BikePartWindow BP = new BikePartWindow();
                BP.DataContext = new BikePartsViewModel();
                ((BikePartsViewModel)BP.DataContext).RBikeRow = BikeRow;
                BP.Show();
                Console.WriteLine(tStock);
            });


            BikeViewRefresh = new RelayCommand(o =>
            {
                Console.WriteLine("Refreshing");
                Console.WriteLine(tName);
                Console.WriteLine(tType);
                Console.WriteLine(tStock);
                if (tName == null)
                {
                    Console.WriteLine("PROUUUT");
                }
                Console.WriteLine(TName);
                Console.WriteLine(BikeRow);
                InitData();
            });

            BikeAdd = new RelayCommand(o =>
            {
                List<string> cols = new List<string>() { "bike_name", "size", "price", "type", "date_start", "date_end", "stock" };
                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar);
                name.Value = tName;
                MySqlParameter size = new MySqlParameter("@size", MySqlDbType.VarChar);
                size.Value = tSize;
                MySqlParameter price = new MySqlParameter("@price", MySqlDbType.Float);
                price.Value = float.Parse(tPrice);
                MySqlParameter type = new MySqlParameter("@type", MySqlDbType.VarChar);
                type.Value = tType;
                MySqlParameter dates = new MySqlParameter("@dates", MySqlDbType.Date);
                dates.Value = new DateTime(Convert.ToInt32(tDateS.Split('/')[2]), Convert.ToInt32(tDateS.Split('/')[1]), Convert.ToInt32(tDateS.Split('/')[0]));
                MySqlParameter datee = new MySqlParameter("@datee", MySqlDbType.Date);
                datee.Value = new DateTime(Convert.ToInt32(tDateE.Split('/')[2]), Convert.ToInt32(tDateE.Split('/')[1]), Convert.ToInt32(tDateE.Split('/')[0]));
                MySqlParameter stock = new MySqlParameter("@stock", MySqlDbType.Int32);
                stock.Value = Convert.ToInt32(tStock);
                List<MySqlParameter>  BikeAddData = new List<MySqlParameter>();
                BikeAddData.Add(name);
                BikeAddData.Add(size);
                BikeAddData.Add(price);
                BikeAddData.Add(type);
                BikeAddData.Add(dates);
                BikeAddData.Add(datee);
                BikeAddData.Add(stock);

                Db.InsertRow("Bikes",cols,BikeAddData);

            });

            BikeDelete = new RelayCommand(o =>
            {
                
                string id = BikeRow.Split()[0];
                Db.DeleteDependencies("Bike_Parts", "bike_id", id, false);
                Db.DeleteDependencies("Bike_Ordered", "bike_id", id, false);
                Db.DeleteRow("Bikes","bike_id", id, false);
                
                Console.WriteLine(BikeRow);

            });

            BikeUpdate = new RelayCommand(o =>
            {
                List<string> cols = new List<string>();
                string id = BikeRow.Split()[0];
                Console.WriteLine(BikeRow);
                List<MySqlParameter> BikeUpdateData = new List<MySqlParameter>();
                if (tName != null) 
                {
                    cols.Add("bike_name");
                    MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar);
                    name.Value = tName;
                    BikeUpdateData.Add(name);
                }
                if(tSize != null)
                {
                    cols.Add("size");
                    MySqlParameter size = new MySqlParameter("@size", MySqlDbType.VarChar);
                    size.Value = tSize;
                    BikeUpdateData.Add(size);
                }
                if(tPrice != null)
                {
                    cols.Add("price");
                    MySqlParameter price = new MySqlParameter("@price", MySqlDbType.Float);
                    price.Value = float.Parse(tPrice);
                    BikeUpdateData.Add(price);
                }
                if(tType != null)
                {
                    cols.Add("type");
                    MySqlParameter type = new MySqlParameter("@type", MySqlDbType.VarChar);
                    type.Value = tType;
                    BikeUpdateData.Add(type);
                }
                if(tDateS != null)
                {
                    cols.Add("date_start");
                    MySqlParameter dates = new MySqlParameter("@dates", MySqlDbType.Date);
                    dates.Value = new DateTime(Convert.ToInt32(tDateS.Split('/')[2]), Convert.ToInt32(tDateS.Split('/')[1]), Convert.ToInt32(tDateS.Split('/')[0]));
                    BikeUpdateData.Add(dates);
                }
                if (tDateE != null)
                {
                    cols.Add("date_end");
                    MySqlParameter datee = new MySqlParameter("@datee", MySqlDbType.Date);
                    datee.Value = new DateTime(Convert.ToInt32(tDateE.Split('/')[2]), Convert.ToInt32(tDateE.Split('/')[1]), Convert.ToInt32(tDateE.Split('/')[0]));
                    BikeUpdateData.Add(datee);
                }
                if(tStock != null)
                {
                    cols.Add("stock");
                    MySqlParameter stock = new MySqlParameter("@stock", MySqlDbType.Int32);
                    stock.Value = Convert.ToInt32(tStock);
                    BikeUpdateData.Add(stock);
                }

                Db.UpdateRow("Bikes", "bike_id", id, false, cols, BikeUpdateData);

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
                Console.WriteLine(bike);
                Bikes.Add(bike);
            }


        }
    }
}
