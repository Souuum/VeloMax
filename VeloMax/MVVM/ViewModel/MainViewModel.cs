using System;
using VeloMax.Core;

namespace VeloMax.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand{ get; set; }
        public RelayCommand BikeViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public BikeViewModel BikeVM { get; set; }

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
            _CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                _CurrentView = HomeVM;
            });

            BikeViewCommand = new RelayCommand(o =>
            {
                _CurrentView = BikeVM;
            });
        }
    }
}
