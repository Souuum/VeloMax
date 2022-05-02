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

        public List<Object> Product { get; set; }

        public Order()
        {

        }

        public Order(List<string> list, List<Object> product)
        { 
            Id = Convert.ToInt32(list[0]);
            Customer_id = Convert.ToInt32(list[1]);
            Street_number = Convert.ToInt32(list[2]);
            Street = list[3];
            City = list[4];
            Postal_code = list[5];
            State = list[6];
            Product = product;
        }

    }




}
