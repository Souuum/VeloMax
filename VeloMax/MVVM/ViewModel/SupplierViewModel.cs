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
        public MySqlConnection Connection { get; set; }


        public SupplierViewModel()
        {
            InitData();
        }

        public void InitData()
        {
            Suppliers_data = SelectAllListRow("*", "Suppliers");
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

        public Boolean OpenConnection()
        {
            Connection = new MySqlConnection("database = VeloMax; server = localhost; user id = root; pwd=root;");
            try
            {

                Connection.Open();
                Console.WriteLine("Connected");
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Connexion Failed" + e.ToString());
                return false;
            }
        }

        public Boolean CloseConnection()
        {
            try
            {

                Connection.Close();
                Console.WriteLine("Connexion closed");
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Closing Connexion failed" + e.ToString());
                return false;
            }
        }

        public List<List<string>> SelectAllListRow(string target, string table)
        {
            string query = "SELECT " + target + " FROM " + table;
            List<List<string>> list = new List<List<string>>();
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                int j = 0;
                while (dataReader.Read())
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
                this.CloseConnection();
                return list;
            }
            return list;
        }

        public List<string> SelectListRow(string target, string table, string idef, int id)
        {
            string query = "SELECT " + target + " FROM " + table + " where " + idef + " = " + id;
            List<string> list = new List<string>();
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        {
                            list.Add(dataReader.GetString(i));
                        }
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return list;
                }

                return list;
            }
            return list;
        }

    }
}
