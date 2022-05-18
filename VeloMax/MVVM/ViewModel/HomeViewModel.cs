using System;
using VeloMax.Core;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Caliburn.Micro;

namespace VeloMax.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject, INotifyPropertyChanged
    {
        private string nb_partsdata;
        private string nb_bikesdata;
        private string nb_customersdata;
        private string nb_companysdata;
        private string nb_orderdata;
        private BindableCollection<HomeOrder> homeOrder;
        private BindableCollection<BestClient> bclients;

        public BindableCollection<BestClient> BestClients
        {
            get { return bclients; }
            set { bclients = value;
            OnPropertyChanged();}
        }

        public DataBase Db { get; set; }

        public BindableCollection<HomeOrder> HomeOrders
        {
            get { return homeOrder; }
            set { homeOrder = value;
                OnPropertyChanged();
            }
        }
        public string Nb_partsdata
        {
            get => nb_partsdata; set
            {
                nb_partsdata = value;
                OnPropertyChanged(nameof(Nb_partsdata));
            }
        }
        public string Nb_orderdata
        {
            get => nb_orderdata; set
            {
                nb_orderdata = value;
                OnPropertyChanged(nameof(Nb_orderdata));
            }
        }
        public string Nb_bikedata 
        {
            get => nb_bikesdata; set
            {
                nb_bikesdata = value;
                OnPropertyChanged(nameof(Nb_bikedata));
            }
        }

        public string Nb_Customersdata
        {
            get => nb_customersdata; set
            {
                nb_customersdata = value;
                OnPropertyChanged(nameof(Nb_Customersdata));
            }
        }

        public string Nb_Companysdata
        {
            get => nb_companysdata; set
            {
                nb_companysdata = value;
                OnPropertyChanged(nameof(Nb_Companysdata));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand HomeViewRefresh { get; set; }
        public HomeViewModel()
        {


            //Init dashboard data

            InitData();

            HomeViewRefresh = new RelayCommand(o =>
            {
                Console.WriteLine("Refreshing");
                InitData();
            });
        }

        public void InitData()
        {
            MySqlConnection connection = new MySqlConnection("database = VeloMax; server = localhost; user id = root; pwd=root;");
            try
            {

                connection.Open();
                Console.WriteLine("Connected");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Connexion Failed" + e.ToString());
            }
            MySqlCommand nb_parts = new MySqlCommand("SELECT COUNT(*) FROM Parts;", connection);

            MySqlDataReader reader_parts = nb_parts.ExecuteReader();
            reader_parts.Read();
            Nb_partsdata = reader_parts.GetString(0) + " pièces";
            reader_parts.Close();

            MySqlCommand nb_bikes = new MySqlCommand("SELECT COUNT(*) FROM Bikes;", connection);

            MySqlDataReader reader_bikes = nb_bikes.ExecuteReader();
            reader_bikes.Read();
            Nb_bikedata = reader_bikes.GetString(0) + " vélos";
            Console.WriteLine(Nb_bikedata);
            reader_bikes.Close();

            MySqlCommand nb_customers = new MySqlCommand("SELECT COUNT(*) FROM Customers WHERE `customer_type` = 'particulier';", connection);

            MySqlDataReader reader_customers = nb_customers.ExecuteReader();
            reader_customers.Read();
            Nb_Customersdata = reader_customers.GetString(0) + " particuliers";
            Console.WriteLine(Nb_Customersdata);
            reader_customers.Close();

            MySqlCommand nb_companys = new MySqlCommand("SELECT COUNT(*) FROM Customers WHERE `customer_type` = 'entreprise';", connection);

            MySqlDataReader reader_companys = nb_companys.ExecuteReader();
            reader_companys.Read();
            Nb_Companysdata = reader_companys.GetString(0) + " entreprises";
            Console.WriteLine(Nb_Companysdata);
            reader_companys.Close();

            MySqlCommand nb_orders = new MySqlCommand("SELECT COUNT(*) FROM Orders;", connection);

            MySqlDataReader reader_orders = nb_orders.ExecuteReader();
            reader_orders.Read();
            Nb_orderdata = reader_orders.GetString(0) + " Commandes";
            Console.WriteLine(Nb_orderdata);
            reader_orders.Close();

            MySqlCommand bestclient = new MySqlCommand("SELECT c.customer_firstname, c.customer_name, SUM(o.quantity) FROM customers as c JOIN orders as o ON o.customer_id = c.customer_id GROUP BY c.customer_id ORDER BY SUM(o.quantity) DESC LIMIT 10;",connection);
            MySqlDataReader read_bestclient = bestclient.ExecuteReader();
            int j = 0;
            List<List<string>> listbestclient = new List<List<string>>();
            
            while(read_bestclient.Read() && j<3)
            {
                List<string> bcrow = new List<string>();
                for (int i = 0; i < read_bestclient.FieldCount; i++)
                {
                    {
                        bcrow.Add(read_bestclient.GetString(i));
                    }
                }
                j++;
            }
            BestClient bclient = new BestClient();
            BestClients = new BindableCollection<BestClient>();
            foreach(var item in listbestclient)
            {
                Console.WriteLine(item);
                bclient = new BestClient(item);
                BestClients.Add(bclient);
            }

            read_bestclient.Close();
            MySqlCommand lastorder = new MySqlCommand("SELECT DISTINCT Orders.order_id,shipping_date,customer_name,shipping_city, Bikes.price,Parts.price FROM Orders, Customers, Bikes, Parts, Bike_Ordered, Parts_Ordered WHERE Orders.customer_id = Customers.customer_id AND Orders.order_id = Bike_Ordered.order_id AND Bikes.bike_id = Bike_Ordered.bike_id AND Orders.order_id = Parts_Ordered.order_id AND Parts_Ordered.parts_id = Parts.parts_id ORDER BY shipping_date DESC",connection);
            MySqlDataReader dataReader = lastorder.ExecuteReader();
            j = 0;
            List<List<string>> list = new List<List<string>>();
            while (dataReader.Read() && j<3)
            {

                List<string> row = new List<string>();
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    {
                        row.Add(dataReader.GetString(i));
                    }
                }
                list.Add(row);
                j++;
            }
             dataReader.Close();
            HomeOrder order = new HomeOrder();
            HomeOrders = new BindableCollection<HomeOrder>();
            foreach (var item in list)
            {
                order = new HomeOrder(item);
                HomeOrders.Add(order);
            }

            connection.Close();

        }
    }

    class HomeOrder : ObservableObject
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Price { get; set; }

        public HomeOrder()
        {

        }

        public HomeOrder(List<string> list)
        {
            Id = list[0];
            Date = list[1];
            Name = list[2];
            City = list[3];
            Price = Convert.ToInt32(list[4]) + Convert.ToInt32(list[5]);
        }

    }

    
    class BestClient : ObservableObject
    {
        public string Fname {get; set;}
        public string Name {get; set;}
        public string Quantity {get; set;}

        public BestClient()
        {

        }
        public BestClient(List<string> list)
        {
            Fname = list[0];
            Name = list[1];
            Quantity = list[2];
        }
    }
}
