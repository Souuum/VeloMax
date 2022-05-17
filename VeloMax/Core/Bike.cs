using System;
using System.Collections.Generic;

namespace VeloMax.Core
{
    class Bike
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Size { get; set; }

        public float Price { get; set; }

        public string Type { get; set; }

        public string Date_start { get; set; }

        public string Date_end { get; set; }

        public int Stock { get; set; }

        public Bike()
        {

        }

        public Bike(List<string> list)
        {
            Id = Convert.ToInt32(list[0]);
            Name = list[1];
            Size = list[2];
            Price = float.Parse(list[3]);
            Type = list[4];
            Date_start = list[5].Split()[0];
            Date_end = list[6].Split()[0];
            Stock = Convert.ToInt32(list[7]);
        }

        public override string ToString()
        {
            return Id.ToString() + " " + Name.ToString() + " " + Size.ToString() + " " + Price.ToString() + " " + Type.ToString() + " " + Date_start.ToString() + " " + Date_end.ToString() + " " + Stock.ToString();
        }
    }
}
