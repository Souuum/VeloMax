using System;
using VeloMax.Core;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VeloMax.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject, INotifyPropertyChanged
    {
        private string nb_partsdata;
        private string nb_bikesdata;
        private string nb_customersdata;
        private string nb_companysdata;
        public DataBase Db { get; set; }

        public string Nb_partsdata
        {
            get => nb_partsdata; set
            {
                nb_partsdata = value;
                OnPropertyChanged(nameof(Nb_partsdata));
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

            connection.Close();
        }
    }
}
