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


        public void InsertRow(string table, List<string> cols, List<MySqlParameter> values)
        {

            if (this.OpenConnection())
            {
                
                MySqlCommand cmd = Connection.CreateCommand();
                string col = "( ";
                for (int i = 0; i < cols.Count; i++)
                {
                    col += i != cols.Count - 1 ? cols[i] + ", " : cols[i] + " )";
                }
                string val = "VALUES ( ";
                for (int i = 0; i < values.Count; i++)
                {   
                    Console.WriteLine(values[i].Value);
                    cmd.Parameters.Add(values[i].ParameterName,values[i].MySqlDbType).Value = values[i].Value;
                    val += i != values.Count - 1 ? values[i] + ", " : values[i] + " );";
                }
                string query = "INSERT INTO " + table + col + val;
                Console.WriteLine(query);
                cmd.CommandText = query;
                try
                {
                    
                    
                    
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Insert Success");
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Insert Failed " + e.ToString());
                }
                this.CloseConnection();
            }


        }

        public void UpdateRow(string table, string id_name, string id, bool isString, List<String> cols, List<MySqlParameter> values)
        {
            if (this.OpenConnection())
            {

                MySqlCommand cmd = Connection.CreateCommand();
                string query = "UPDATE " + table + " SET ";

                for (int i = 0; i < cols.Count; i++)
                {
                    cmd.Parameters.Add(values[i].ParameterName, values[i].MySqlDbType).Value = values[i].Value;
                    query += i != cols.Count - 1 ? cols[i] + " = " + values[i] + ",": cols[i] + " = " + values[i];
                }
                if (isString == false)
                {
                    int idint = Convert.ToInt32(id);
                    query += " WHERE " + id_name + " = " + idint;

                }
                else
                {
                    query += " WHERE " + id_name + " = " + id;
                }
                Console.WriteLine(query);
                cmd.CommandText = query;
                try
                {



                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Update Success");
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Update Failed " + e.ToString());
                }
                this.CloseConnection();
            }

        }

        public void DeleteRow(string table,string id_name, string id, bool isString)
        {

            if(this.OpenConnection())
            {
                MySqlCommand cmd = Connection.CreateCommand();
                if (isString == false)
                {
                    int idint = Convert.ToInt32(id);
                    string query = "DELETE FROM " + table + " WHERE " + id_name +  " = " + idint;
                    cmd.CommandText = query;
                }
                else
                {
                    string query = "DELETE FROM " + table + " WHERE " + id_name + " = " + id;
                    cmd.CommandText = query;
                }
                
                try {
                
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Delete Success");
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Delete Failed " + e.ToString());
                }
                this.CloseConnection();
            }
        }
    }
}
