using System;
using System.Collections.Generic;

namespace VeloMax.Core
{
    class Order
    {
        public int Id { get; set; }
        public int Customer_id { get; set; }

        public int Street_number { get; set; }
        public string Street { get; set; }

        public string City { get; set; }

        public string Postal_code { get; set; }

        public string State { get; set; }
        
        public string Country { get; set; }

        public string Date { get; set; }

        public Order()
        {

        }

        public Order(List<string> list)
        { 
            Id = Convert.ToInt32(list[0]);
            Customer_id = Convert.ToInt32(list[1]);
            Date = list[2].Split()[0];
            Street_number = Convert.ToInt32(list[3]);
            Street = list[4];
            City = list[5];
            Postal_code = list[6];
            State = list[7];
            Country = list[8];
        }

        public override string ToString()
        {
            return Id.ToString() + " " + Id.ToString() + " " + Customer_id.ToString() + " " + Street_number.ToString() + " " + Street.ToString() + " " + City.ToString() + " " + Postal_code.ToString() + " " + State.ToString() + " " + Country.ToString();
        }

    }




}
