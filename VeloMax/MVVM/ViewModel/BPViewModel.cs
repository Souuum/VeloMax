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
    class BPViewModel : ObservableObject, INotifyPropertyChanged
    {
        private string tLabel;
        private string tPrice;
        private string tDateS;
        private string tDateE;
        private string tDelay;
        private string tStock;
        private string tType;
        public string PartRow { get; set; }
        public string BikeRow { get; set; }
        private List<List<string>> parts_data;
        public BindableCollection<Part> Parts { get; set; }
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
        public List<List<string>> Parts_data
        {
            get => parts_data; set
            {
                parts_data = value;
                OnPropertyChanged(nameof(Parts_data));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RelayCommand PartViewRefresh { get; set; }
        public RelayCommand PartAdd { get; set; }

        public RelayCommand PartDelete { get; set; }

        public RelayCommand PartUpdate { get; set; }
        public BPViewModel()
        {
            Db = new DataBase();
            InitData();

            PartViewRefresh = new RelayCommand(o =>
            {
                Console.WriteLine("Refreshing");
                Console.WriteLine(PartRow);
                InitData();
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


        }

    }
}
