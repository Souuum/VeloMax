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
    class SupplierViewModel : ObservableObject, INotifyPropertyChanged
    {
        private string tSiret;
        private string tFullName;
        private string tCompanyName;
        private string tFullAddr;
        private string tLabel;
        private List<List<string>> suppliers_data;
        public string TSiret
        {
            get => tSiret; set
            {
                tSiret = value;
                OnPropertyChanged(nameof(TSiret));
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
        public List<List<string>> Suppliers_data
        {
            get => suppliers_data; set
            {
                suppliers_data = value;
                OnPropertyChanged(nameof(Suppliers_data));
            }
        }

        public string SupplierRow { get; set; }
        public BindableCollection<Supplier> Suppliers { get; set; }
        public DataBase Db { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand SupplierViewRefresh { get; set; }
        public RelayCommand SupplierAdd { get; set; }

        public RelayCommand SupplierDelete { get; set; }

        public RelayCommand SupplierUpdate { get; set; }
        public SupplierViewModel()
        {
            Db = new DataBase();
            InitData();

            SupplierViewRefresh = new RelayCommand(o =>
            {
                Console.WriteLine("Refreshing");
                Console.WriteLine(SupplierRow);
                InitData();
            });

            SupplierAdd = new RelayCommand(o =>
            {
                List<string> cols = new List<string>() { "siret", "supplier_name", "contact", "number", "street", "city", "postal_code", "state", "country", "label" };
                MySqlParameter siret = new MySqlParameter("@siret", MySqlDbType.VarChar);
                siret.Value = tSiret;
                MySqlParameter contact = new MySqlParameter("@contact", MySqlDbType.VarChar);
                contact.Value = tFullName;
                MySqlParameter supplier_name = new MySqlParameter("@supplier_name", MySqlDbType.VarChar);
                supplier_name.Value = tCompanyName;
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
                MySqlParameter label = new MySqlParameter("@label", MySqlDbType.VarChar);
                label.Value = tLabel;
                List<MySqlParameter> SupplierAddData = new List<MySqlParameter>();

                SupplierAddData.Add(siret);
                SupplierAddData.Add(supplier_name);
                SupplierAddData.Add(contact);
                SupplierAddData.Add(snumber);
                SupplierAddData.Add(street);
                SupplierAddData.Add(city);
                SupplierAddData.Add(pcode);
                SupplierAddData.Add(state);
                SupplierAddData.Add(country);
                SupplierAddData.Add(label);



                Db.InsertRow("Suppliers", cols, SupplierAddData);

            });

            SupplierDelete = new RelayCommand(o =>
            {

                string id = SupplierRow.Split()[0];
                Db.DeleteDependencies("Procurement", "supplier_id", id, true);
                Db.DeleteRow("Suppliers", "siret", id, true);
                Console.WriteLine(SupplierRow);

            });

            SupplierUpdate = new RelayCommand(o =>
            {
                List<string> cols = new List<string>();
                string id = SupplierRow.Split()[0];
                Console.WriteLine(SupplierRow);
                List<MySqlParameter> SupplierUpdateData = new List<MySqlParameter>();

                if (tCompanyName != null)
                {
                    cols.Add("supplier_name");
                    MySqlParameter company_name = new MySqlParameter("@company_name", MySqlDbType.VarChar);
                    company_name.Value = tCompanyName;
                    SupplierUpdateData.Add(company_name);
                }
                if (tFullName != null)
                {
                    cols.Add("contact");

                    MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar);
                    name.Value = tFullName;
                    SupplierUpdateData.Add(name);
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
                    SupplierUpdateData.Add(snumber);
                    SupplierUpdateData.Add(street);
                    SupplierUpdateData.Add(city);
                    SupplierUpdateData.Add(pcode);
                    SupplierUpdateData.Add(state);
                    SupplierUpdateData.Add(country);
                }

                if (tLabel != null)
                {
                    cols.Add("label");
                    MySqlParameter label = new MySqlParameter("@name", MySqlDbType.VarChar);
                    label.Value = tLabel;
                    SupplierUpdateData.Add(label);
                }

                Db.UpdateRow("Suppliers", "siret", id, true, cols, SupplierUpdateData);

            });
        }

        public void InitData()
        {
            Suppliers_data = Db.SelectAllListRow("*", "Suppliers");
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

    }
}
