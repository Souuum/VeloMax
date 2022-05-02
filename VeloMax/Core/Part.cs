using System;
using System.Collections.Generic;

namespace VeloMax.Core
{
    class Part
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public float Price { get; set; }
        public string Date_start { get; set; }
        public string Date_end { get; set; }
        public int Delay { get; set; }
        public int Quantity { get; set; }

        public string Part_type { get; set; }

        public Part()
        {

        }

        public Part(List<string> list)
        {
            Id = Convert.ToInt32(list[0]);
            Label = list[1];
            Price = Convert.ToInt32(list[2]);
            Date_start = list[3].Split()[0];
            Date_end = list[4].Split()[0];
            Delay = Convert.ToInt32(list[5]);
            Quantity = Convert.ToInt32(list[6]);
            Part_type = list[7];
        }

        public override string ToString()
        {
            return Id.ToString() + " " + Label.ToString() + " " + Part_type.ToString() + " " + Price.ToString() + " " + Date_start.ToString() + " " + Date_end.ToString() + " " + Quantity.ToString();
        }
    }
}
