using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VeloMax.Core
{
    class DataBase
    {
        public MySqlConnection Connection { get; set; }



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
