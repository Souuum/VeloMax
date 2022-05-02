using System;
using VeloMax.Core;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Caliburn.Micro;
namespace VeloMax.MVVM.ViewModel
{
    class PartViewModel : ObservableObject, INotifyPropertyChanged
    {
        public List<List<string>> Parts_data { get; set; }
        public BindableCollection<Part> Parts { get; set; }
        public MySqlConnection Connection { get; set; }


        public PartViewModel()
        {
            InitData();
        }

        public void InitData()
        {
            Parts_data = SelectAllListRow("*", "Parts");
            Console.WriteLine("ici");
            Console.WriteLine(Parts_data.Count);
            Part part = new Part();
            Parts = new BindableCollection<Part>();
            foreach (var item in Parts_data)
            {
                part = new Part(item);
                Parts.Add(part);
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
