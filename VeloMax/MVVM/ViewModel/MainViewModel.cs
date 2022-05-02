using System;
using VeloMax.Core;

namespace VeloMax.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand{ get; set; }
        public RelayCommand BikeViewCommand { get; set; }
        public RelayCommand PartViewCommand { get; set; }
        public RelayCommand CustomerViewCommand { get; set; }
        public RelayCommand SupplierViewCommand { get; set; }
        public RelayCommand OrderViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public BikeViewModel BikeVM { get; set; }
        public PartViewModel PartVM { get; set; }
        public CustomerViewModel CustomerVM { get; set; }
        public SupplierViewModel SupplierVM { get; set; }
        public OrderViewModel OrderVM { get; set; }


        private object _currentView;

        public object _CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                OnProportyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            BikeVM = new BikeViewModel();
            PartVM = new PartViewModel();
            CustomerVM = new CustomerViewModel();
            SupplierVM = new SupplierViewModel();
            OrderVM = new OrderViewModel();

            _CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                _CurrentView = HomeVM;
            });

            BikeViewCommand = new RelayCommand(o =>
            {
                _CurrentView = BikeVM;
            });

            PartViewCommand = new RelayCommand(o =>
            {
                _CurrentView = PartVM;
            });

            CustomerViewCommand = new RelayCommand(o =>
            {
                _CurrentView = CustomerVM;
            }); 
            
            SupplierViewCommand = new RelayCommand(o =>
            {
                _CurrentView = SupplierVM;
            }); 
            
            OrderViewCommand = new RelayCommand(o =>
            {
                _CurrentView = OrderVM;
            });
        }
    }
}
