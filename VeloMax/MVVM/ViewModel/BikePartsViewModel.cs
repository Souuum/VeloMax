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
    class BikePartsViewModel : ObservableObject, INotifyPropertyChanged
    {
        private List<List<string>> bikes_data { get; set; }

        private string tName;
        private string tSize;
        private string tPrice;
        private string tType;
        private string tDateS;
        private string tDateE;
        private string tStock;
        private BindableCollection<Part> parts;
        private string tLabel;
        private string tDelay;

        public string PartRow { get; set; }
        private List<List<string>> parts_data;
        public BindableCollection<Part> Parts
        { 
            get => parts; 
            set
            {
                parts = value;
                OnPropertyChanged();
            } }
        public DataBase Db { get; set; }


        public string TLabel
        {
            get => tLabel; set
            {
                tLabel = value;
                OnPropertyChanged(nameof(TLabel));
            }

        }
        public string TDelay
        {
            get => tDelay; set
            {
                tDelay = value;
                OnPropertyChanged(nameof(TDelay));
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

        private string rBikeRow;
        

        public string RBikeRow
        {
            get =>rBikeRow; set
            {
                rBikeRow = value;
                OnPropertyChanged();
                
            }
        }

        private string rBikeName;


        public string RBikeName
        {
            get => rBikeName; set
            {
                rBikeName = value;
                OnPropertyChanged();

            }
        }

        public BindableCollection<Bike> Bikes { get; set; }

        public List<List<string>> Parts_data
        {
            get => parts_data; set
            {
                parts_data = value;
                OnPropertyChanged(nameof(Parts_data));
            }
        }



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


        public BPViewModel PartVM { get; set; }

        public RelayCommand PartViewRefresh { get; set; }
        public RelayCommand PartAdd { get; set; }

        public RelayCommand PartDelete { get; set; }

        public RelayCommand PartUpdate { get; set; }

        public BikePartsViewModel()
        {
            PartVM = new BPViewModel();
            PartVM.BikeRow = RBikeRow;
            
            Db = new DataBase();
            InitData();

            PartViewRefresh = new RelayCommand(o =>
            {
                Console.WriteLine("Refreshing");
                Console.WriteLine(PartRow);
                Console.WriteLine(rBikeRow);
                Console.WriteLine(RBikeRow);
                InitCData();
            });

            PartAdd = new RelayCommand(o =>
            {
                List<string> cols = new List<string>() { "label", "price", "date_start", "date_end", "delay", "quantity", "part_type" };
                MySqlParameter label = new MySqlParameter("@name", MySqlDbType.VarChar);
                label.Value = tLabel;
                MySqlParameter delay = new MySqlParameter("@size", MySqlDbType.VarChar);
                delay.Value = tDelay;
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
                List<MySqlParameter> PartAddData = new List<MySqlParameter>();
                PartAddData.Add(label);
                PartAddData.Add(price);
                PartAddData.Add(dates);
                PartAddData.Add(datee);
                PartAddData.Add(delay);
                PartAddData.Add(stock);
                PartAddData.Add(type);

                Db.InsertRow("Parts", cols, PartAddData);

            });

            PartDelete = new RelayCommand(o =>
            {

                string id = PartRow.Split()[0];
                Db.DeleteDependencies("Bike_Parts", "parts_id", id, false);
                Db.DeleteDependencies("Parts_Ordered", "parts_id", id, false);
                Db.DeleteDependencies("Procurement", "parts_id", id, false);
                Db.DeleteRow("Parts", "parts_id", id, false);

                Console.WriteLine(PartRow);

            });

            PartUpdate = new RelayCommand(o =>
            {
                List<string> cols = new List<string>();
                string id = PartRow.Split()[0];
                Console.WriteLine(PartRow);
                List<MySqlParameter> PartUpdateData = new List<MySqlParameter>();
                if (tLabel != null)
                {
                    cols.Add("label");
                    MySqlParameter label = new MySqlParameter("@name", MySqlDbType.VarChar);
                    label.Value = tLabel;
                    PartUpdateData.Add(label);
                }

                if (tPrice != null)
                {
                    cols.Add("price");
                    MySqlParameter price = new MySqlParameter("@price", MySqlDbType.Float);
                    price.Value = float.Parse(tPrice);
                    PartUpdateData.Add(price);
                }
                if (tDateS != null)
                {
                    cols.Add("date_start");
                    MySqlParameter dates = new MySqlParameter("@dates", MySqlDbType.Date);
                    dates.Value = new DateTime(Convert.ToInt32(tDateS.Split('/')[2]), Convert.ToInt32(tDateS.Split('/')[1]), Convert.ToInt32(tDateS.Split('/')[0]));
                    PartUpdateData.Add(dates);
                }
                if (tDateE != null)
                {
                    cols.Add("date_end");
                    MySqlParameter datee = new MySqlParameter("@datee", MySqlDbType.Date);
                    datee.Value = new DateTime(Convert.ToInt32(tDateE.Split('/')[2]), Convert.ToInt32(tDateE.Split('/')[1]), Convert.ToInt32(tDateE.Split('/')[0]));
                    PartUpdateData.Add(datee);
                }
                if (tDelay != null)
                {
                    cols.Add("delay");
                    MySqlParameter size = new MySqlParameter("@size", MySqlDbType.VarChar);
                    size.Value = tDelay;
                    PartUpdateData.Add(size);
                }
                if (tStock != null)
                {
                    cols.Add("quantity");
                    MySqlParameter stock = new MySqlParameter("@stock", MySqlDbType.Int32);
                    stock.Value = Convert.ToInt32(tStock);
                    PartUpdateData.Add(stock);
                }
                if (tType != null)
                {
                    cols.Add("part_type");
                    MySqlParameter type = new MySqlParameter("@type", MySqlDbType.VarChar);
                    type.Value = tType;
                    PartUpdateData.Add(type);
                }

                Db.UpdateRow("Parts", "parts_id", id, false, cols, PartUpdateData);

            });

           

        }

        
        public void InitData()
        {
            Console.WriteLine("init");
            Console.WriteLine(RBikeRow);
            Console.WriteLine(rBikeRow);
            RBikeName = rBikeRow;
            Console.WriteLine(RBikeName);
        }
        public void InitCData()
        {
            RBikeName = rBikeRow.Split()[0];
            Console.WriteLine(RBikeName);
            Console.WriteLine("prout "+ RBikeRow + " prout");
            List<string> cols = new List<string>() { "Parts.parts_id","label", "price", "date_start", "date_end", "delay", "Parts.quantity", "part_type" };

            Parts_data = Db.BPInit("Parts,Bike_Parts",cols, " Parts.parts_id = Bike_Parts.parts_id", "bike_id",rBikeRow.Split()[0],false);
            Console.WriteLine(Parts_data.Count);
            Part part = new Part();
            Parts = new BindableCollection<Part>();
            foreach (var item in Parts_data)
            {
                part = new Part(item);
                Console.WriteLine(part.ToString());
                Parts.Add(part);
            }

        }
    }
}
