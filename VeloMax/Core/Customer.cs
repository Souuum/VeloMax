using System;
using System.Collections.Generic;

namespace VeloMax.Core
{
    class Customer
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Company_name { get; set; }

        public string Customer_name { get; set; }

        public string Customer_firstname { get; set; }

        public int Street_number { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
        public string Phone { get; set; }

        public string Mail  { get; set; }

        public int Fidelity_program { get; set; }

        public string Is_member { get; set; }

        public string Customer_fullname { get; set; }

        public Customer()
        {

        }

        public Customer(List<string> list)
        {
            Id = Convert.ToInt32(list[0]);
            Type = list[1];
            Company_name = list[2];
            Customer_name = list[3];
            Customer_firstname = list[4];
            Street_number = Convert.ToInt32(list[5]);
            Street = list[6];
            City = list[7];
            Postcode = list[8];
            State = list[9];
            Country = list[10];
            Phone = list[11];
            Mail = list[12];
            Fidelity_program = Convert.ToInt32(list[13]);
            Is_member = list[13].ToString();
        }

        public override string ToString()
        {
            return Id.ToString() + " " + Id.ToString() + " " + Type.ToString() + " " + Company_name.ToString() + " " + Customer_name.ToString() + " " + Customer_firstname.ToString() + " " + Street_number.ToString() + " " + Street.ToString() + " " + City.ToString() + " " + Postcode.ToString() + " " + State.ToString() + " " + Country.ToString()+ " " + Phone.ToString() 
                + " " + Mail.ToString() + " " + Fidelity_program.ToString() + " " + Is_member.ToString();
        }
    }
}
