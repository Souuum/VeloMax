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
    class OrderViewModel : ObservableObject, INotifyPropertyChanged
    {
        private string tCustomerid;
        private string tFullName;
        private string tCompanyName;
        private string tFullAddr;
        private string tLabel;
        private List<List<string>> orders_data;
        public string TCustomerid
        {
            get => tCustomerid; set
            {
                tCustomerid = value;
                OnPropertyChanged(nameof(TCustomerid));
            }
        }
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
                tCompanyName = value;
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

        public string TLabel
        {
            get => tLabel; set
            {
                tLabel = value;
                OnPropertyChanged(nameof(TLabel));
            }

        }
        public List<List<string>> Orders_data
        {
            get => orders_data; set
            {
                orders_data = value;
                OnPropertyChanged(nameof(Orders_data));
            }
        }

        public string OrderRow { get; set; }
        public BindableCollection<Order> Orders { get; set; }
        public DataBase Db { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand OrderViewRefresh { get; set; }
        public RelayCommand OrderAdd { get; set; }

        public RelayCommand OrderDelete { get; set; }

        public RelayCommand OrderUpdate { get; set; }
        public OrderViewModel()
        {
            Db = new DataBase();
            InitData();

            OrderViewRefresh = new RelayCommand(o =>
            {
                Console.WriteLine("Refreshing");
                Console.WriteLine(OrderRow);
                InitData();
            });

            OrderAdd = new RelayCommand(o =>
            {
                List<string> cols = new List<string>() { "customer_id", "shipping_number", "shipping_street", "shipping_city", "shipping_postal_code", "shipping_state", "shipping_country", "quantity" };
                MySqlParameter customer_id = new MySqlParameter("@customer_id", MySqlDbType.VarChar);
                customer_id.Value = tCustomerid;
                MySqlParameter snumber = new MySqlParameter("@snumber", MySqlDbType.VarChar);
                snumber.Value = tFullAddr.Split('_')[0];
                MySqlParameter street = new MySqlParameter("@street", MySqlDbType.VarChar);
                street.Value = tFullAddr.Split('_')[1];
                MySqlParameter city = new MySqlParameter("@city", MySqlDbType.VarChar);
                city.Value = tFullAddr.Split('_')[2];
                MySqlParameter pcode = new MySqlParameter("@pcode", MySqlDbType.VarChar);
                pcode.Value = tFullAddr.Split('_')[3];
                MySqlParameter state = new MySqlParameter("@state", MySqlDbType.VarChar);
                state.Value = tFullAddr.Split('_')[4];
                MySqlParameter country = new MySqlParameter("@country", MySqlDbType.VarChar);
                country.Value = tFullAddr.Split('_')[5];

                List<MySqlParameter> OrderAddData = new List<MySqlParameter>();

                OrderAddData.Add(customer_id);
                OrderAddData.Add(snumber);
                OrderAddData.Add(street);
                OrderAddData.Add(city);
                OrderAddData.Add(pcode);
                OrderAddData.Add(state);
                OrderAddData.Add(country);



                Db.InsertRow("Orders", cols, OrderAddData);

            });

            OrderDelete = new RelayCommand(o =>
            {

                string id = OrderRow.Split()[0];
                Db.DeleteDependencies("Bike_Ordered", "order_id", id, true);
                Db.DeleteDependencies("Part_Ordered", "order_id", id, true);
                Db.DeleteRow("Orders", "customer_id", id, true);
                Console.WriteLine(OrderRow);

            });

            OrderUpdate = new RelayCommand(o =>
            {
                List<string> cols = new List<string>();
                string id = OrderRow.Split()[0];
                Console.WriteLine(OrderRow);
                List<MySqlParameter> OrderUpdateData = new List<MySqlParameter>();

                if (tCustomerid != null)
                {
                    cols.Add("customer_id");

                    MySqlParameter customerid = new MySqlParameter("@customerid", MySqlDbType.VarChar);
                    customerid.Value = tCustomerid;
                    OrderUpdateData.Add(customerid);
                }
                if (tFullAddr != null)
                {
                    cols.Add("street_number");
                    cols.Add("street");
                    cols.Add("city");
                    cols.Add("postal_code");
                    cols.Add("state");
                    cols.Add("country");

                    MySqlParameter snumber = new MySqlParameter("@snumber", MySqlDbType.VarChar);
                    snumber.Value = tFullAddr.Split('_')[0];
                    MySqlParameter street = new MySqlParameter("@street", MySqlDbType.VarChar);
                    street.Value = tFullAddr.Split('_')[1];
                    MySqlParameter city = new MySqlParameter("@city", MySqlDbType.VarChar);
                    city.Value = tFullAddr.Split('_')[2];
                    MySqlParameter pcode = new MySqlParameter("@pcode", MySqlDbType.VarChar);
                    pcode.Value = tFullAddr.Split('_')[3];
                    MySqlParameter state = new MySqlParameter("@state", MySqlDbType.VarChar);
                    state.Value = tFullAddr.Split('_')[4];
                    MySqlParameter country = new MySqlParameter("@country", MySqlDbType.VarChar);
                    country.Value = tFullAddr.Split('_')[5];
                    OrderUpdateData.Add(snumber);
                    OrderUpdateData.Add(street);
                    OrderUpdateData.Add(city);
                    OrderUpdateData.Add(pcode);
                    OrderUpdateData.Add(state);
                    OrderUpdateData.Add(country);
                }

                Db.UpdateRow("Orders", "customer_id", id, true, cols, OrderUpdateData);

            });
        }

        public void InitData()
        {
            Orders_data = Db.SelectAllListRow("*", "Orders");
            Console.WriteLine("ici");
            Console.WriteLine(Orders_data.Count);
            Order order = new Order();
            Orders = new BindableCollection<Order>();
            foreach (var item in Orders_data)
            {
                order = new Order(item);
                Orders.Add(order);
            }


        }

    }
}
