using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using ZasAndDasMobile.Services;
using ZasUndDas.Shared;

namespace ZasAndDasMobile.ViewModels
{

    public partial class MainPageViewModel : ObservableObject
    {
        PizzaService _service;
        public string Rows => $"{DeviceDisplay.Current.MainDisplayInfo.Height - 200}";

        [ObservableProperty]
        
        private ObservableCollection<PizzaViewModel> pizzaList;

        public MainPageViewModel(PizzaService service)
        {
            _service = service;
            PizzaList = new ObservableCollection<PizzaViewModel>();
            foreach (var pizza in _service.GetAllPizzas())
            {
                PizzaList.Add(new PizzaViewModel(pizza));
            }
        }
    }
}
