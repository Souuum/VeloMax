using System;
using System.Collections.Generic;

namespace VeloMax.Core
{
    class Supplier
    {
        public string Siret { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public int Street_number { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Postal_code { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Label { get; set; }

        public Supplier()
        {

        }

        public Supplier(List<string> list)
        {
            Siret = list[0];
            Name = list[1];
            Contact = list[2];
            Street_number = Convert.ToInt32(list[3]);
            Street = list[4];
            City = list[5];
            Postal_code = list[6];
            State = list[7];
            Country = list[8];
            Label = list[9];
        }

        public override string ToString()
        {
            return Siret.ToString() + " " + Name.ToString() + " " + Contact.ToString() + " " + Street_number.ToString() + " " + Street.ToString() + " " + City.ToString() + " " + Postal_code.ToString() + " " + State.ToString() + " " + Country.ToString()+ " " + Label.ToString() ;
        }
    }
}
