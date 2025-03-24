using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZasAndDasMobile.Services;
using ZasUndDas.Shared;

namespace ZasAndDasMobile.ViewModels
{

    public partial class MainPageViewModel : ObservableObject
    {
        PizzaService _service;
        public string Rows => $"{DeviceDisplay.Current.MainDisplayInfo.Height - 200}";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ShowPizzasCommand))]
        [NotifyCanExecuteChangedFor(nameof(ShowDrinksCommand))]
        private bool pizzasAreVisible;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof (ShowDrinksCommand))]
        [NotifyCanExecuteChangedFor(nameof(ShowPizzasCommand))]
        private bool drinksAreVisible;

        [ObservableProperty]
        private ObservableCollection<PizzaViewModel> pizzaList;

        public MainPageViewModel(PizzaService service)
        {
            PizzasAreVisible = true;
            DrinksAreVisible = false;
            _service = service;
            Sync();
        }
        private void Sync()
        {
            PizzaList = new ObservableCollection<PizzaViewModel>();
            foreach (var pizza in _service.GetAllPizzas())
                PizzaList.Add(new PizzaViewModel(pizza));
        }

        bool PizzasVis() => !PizzasAreVisible;
        [RelayCommand(CanExecute = nameof(PizzasVis))]
        private void ShowPizzas()
        {
            PizzasAreVisible = true;
            DrinksAreVisible = false;
        }
        bool DrinksVis() => !DrinksAreVisible;

        [RelayCommand(CanExecute =  nameof(DrinksVis))]
        private void ShowDrinks()
        {
            DrinksAreVisible = true;
            PizzasAreVisible = false;
        }

        private void UpdateTabs()
        {
            
        }
    }
}
