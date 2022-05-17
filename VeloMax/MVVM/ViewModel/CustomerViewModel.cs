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
    class CustomerViewModel : ObservableObject, INotifyPropertyChanged
    {
        private List<List<string>> customers_data { get; set; }
        private string tFullName;
        private string tCompanyName;
        private string tFullAddr;
        private string tPhone;
        private string tMail;
        private string tFP;
        public string CustomerRow { get; set; }
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
        public string TPhone
        {
            get => tPhone; set
            {
                tPhone = value;
                OnPropertyChanged(nameof(TPhone));
            }
        }

        public string TMail
        {
            get => tMail; set
            {
                tMail = value;
                OnPropertyChanged(nameof(TMail));
            }
        }
        public string TFP
        {
            get => tFP; set
            {
                tFP = value;
                OnPropertyChanged(nameof(TFP));
            }
        }

        public List<List<string>> Customer_data
        {
            get => Customer_data; set
            {
                Customer_data = value;
                OnPropertyChanged(nameof(Customer_data));
            }
        }

        public BindableCollection<Customer> Customers { get; set; }
        public DataBase Db { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        
        public List<List<string>> Customers_data
        {
            get => customers_data; set
            {
                customers_data = value;
                OnPropertyChanged(nameof(Customers_data));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand CustomerViewRefresh { get; set; }
        public RelayCommand CustomerAdd { get; set; }

        public RelayCommand CustomerDelete { get; set; }

        public RelayCommand CustomerUpdate { get; set; }
        public CustomerViewModel()
        {
            Db = new DataBase();
            InitData();

            CustomerViewRefresh = new RelayCommand(o =>
            {
                Console.WriteLine("Refreshing");
                Console.WriteLine(CustomerRow);
                InitData();
            });

            CustomerAdd = new RelayCommand(o =>
            {
                List<string> cols = new List<string>() { "customer_type", "company_name", "customer_name", "customer_firstname", "number", "street", "city","postal_code","state","country","phone","mail"};
                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar);
                name.Value = tFullName.Split('_')[0];
                MySqlParameter fname = new MySqlParameter("@fname", MySqlDbType.VarChar);
                fname.Value = tFullName.Split('_')[1];
                MySqlParameter company_name = new MySqlParameter("@company_name", MySqlDbType.VarChar);
                company_name.Value = "";
                MySqlParameter type = new MySqlParameter("@type", MySqlDbType.VarChar);
                type.Value = "particulier";
                if (tCompanyName != null)
                {
                    company_name.Value = tCompanyName;
                    type.Value = "entreprise";
                }
                MySqlParameter snumber = new MySqlParameter("@snumber",MySqlDbType.VarChar);
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
                MySqlParameter phone = new MySqlParameter("@phone", MySqlDbType.VarChar);
                phone.Value = tPhone;
                MySqlParameter mail = new MySqlParameter("@mail", MySqlDbType.VarChar);
                mail.Value = tMail;
                List<MySqlParameter> CustomerAddData = new List<MySqlParameter>();
                CustomerAddData.Add(type);
                CustomerAddData.Add(company_name);
                CustomerAddData.Add(name);
                CustomerAddData.Add(fname);
                CustomerAddData.Add(snumber);
                CustomerAddData.Add(street);
                CustomerAddData.Add(city);
                CustomerAddData.Add(pcode);
                CustomerAddData.Add(state);
                CustomerAddData.Add(country);
                CustomerAddData.Add(phone);
                CustomerAddData.Add(mail);
  

                Db.InsertRow("Customers", cols, CustomerAddData);

            });

            CustomerDelete = new RelayCommand(o =>
            {
                string id = CustomerRow.Split()[0];
                Db.DeleteRow("Customers", "customer_id", id, false);
                Console.WriteLine(CustomerRow);

            });

            CustomerUpdate = new RelayCommand(o =>
            {
                List<string> cols = new List<string>();
                string id = CustomerRow.Split()[0];
                Console.WriteLine(CustomerRow);
                List<MySqlParameter> CustomerUpdateData = new List<MySqlParameter>();
                if (tFullName != null)
                {
                    cols.Add("customer_name");
                    cols.Add("customer_firstname");
                    MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar);
                    name.Value = tFullName.Split('_')[0];
                    MySqlParameter fname = new MySqlParameter("@fname", MySqlDbType.VarChar);
                    fname.Value = tFullName.Split('_')[1];
                    CustomerUpdateData.Add(name);
                    CustomerUpdateData.Add(fname);
                }
                if (tCompanyName != null)
                {
                    cols.Add("company_name");
                    MySqlParameter company_name = new MySqlParameter("@company_name", MySqlDbType.VarChar);
                    company_name.Value = tCompanyName;
                    CustomerUpdateData.Add(company_name);
                    cols.Add("customer_type");
                    MySqlParameter type = new MySqlParameter("@type", MySqlDbType.VarChar);
                    type.Value = "entreprise";

                }
                if ( tFullAddr!= null)
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
                    CustomerUpdateData.Add(snumber);
                    CustomerUpdateData.Add(street);
                    CustomerUpdateData.Add(city);
                    CustomerUpdateData.Add(pcode);
                    CustomerUpdateData.Add(state);
                    CustomerUpdateData.Add(country);
                }
                if (tPhone!= null)
                {
                    cols.Add("phone");
                    MySqlParameter phone = new MySqlParameter("@phone", MySqlDbType.VarChar);
                    phone.Value = tPhone;
                    CustomerUpdateData.Add(phone);
                }
                if (tMail != null)
                {
                    cols.Add("mail");
                    MySqlParameter mail = new MySqlParameter("@mail", MySqlDbType.VarChar);
                    mail.Value = tMail;
                    CustomerUpdateData.Add(mail);
                }

                Db.UpdateRow("Customers", "customer_id", id, false, cols, CustomerUpdateData);

            });
        }

        public void InitData()
        {
            customers_data = Db.SelectAllListRow("*", "Customers");
            Console.WriteLine("ici");
            Console.WriteLine(customers_data.Count);
            Customer customer = new Customer();
            Customers = new BindableCollection<Customer>();
            foreach (var item in customers_data)
            {
                customer = new Customer(item);
                Customers.Add(customer);
            }


        }

        

    }
}
