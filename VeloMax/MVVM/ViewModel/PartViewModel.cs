using System;
using VeloMax.Core;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Caliburn.Micro;
namespace VeloMax.MVVM.ViewModel
{
    class PartViewModel : ObservableObject, INotifyPropertyChanged
    {
        public List<List<string>> Parts_data { get; set; }
        public BindableCollection<Part> Parts { get; set; }
        public DataBase Db { get; set; }


        public PartViewModel()
        {
            Db = new DataBase();
            InitData();
        }

        public void InitData()
        {
            Parts_data = Db.SelectAllListRow("*", "Parts");
            Console.WriteLine("ici");
            Console.WriteLine(Parts_data.Count);
            Part part = new Part();
            Parts = new BindableCollection<Part>();
            foreach (var item in Parts_data)
            {
                part = new Part(item);
                Parts.Add(part);
            }


        }

    }
}
