using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile.ViewModels
{

    public partial class MainPageViewModel : ObservableObject
    {
        MenuItemService _service;
        public string Rows => $"{DeviceDisplay.Current.MainDisplayInfo.Height - 200}";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ShowPizzasCommand))]
        [NotifyCanExecuteChangedFor(nameof(ShowDrinksCommand))]
        public partial bool PizzasAreVisible { set; get; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ShowDrinksCommand))]
        [NotifyCanExecuteChangedFor(nameof(ShowPizzasCommand))]
        public partial bool DrinksAreVisible { set; get; }

        [ObservableProperty]
        public partial ObservableCollection<PizzaViewModel> PizzaList { set; get; }

        public MainPageViewModel(MenuItemService service)
        {
            PizzasAreVisible = true;
            DrinksAreVisible = false;
            _service = service;
            PizzaList = new ObservableCollection<PizzaViewModel>();
            service.Update += Sync;
            service.ForceSync();
            Sync();
        }
        private void Sync()
        {
            PizzaList = new ObservableCollection<PizzaViewModel>();
            foreach (PizzaBaseDTO pizza in _service.GetAllPizzas())
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

        [RelayCommand(CanExecute = nameof(DrinksVis))]
        private void ShowDrinks()
        {
            DrinksAreVisible = true;
            PizzasAreVisible = false;
        }

        private void UpdateTabs()
        {
        }

        [RelayCommand]
        private async Task GoToCart()
        {
            await Shell.Current.GoToAsync("//Cart");
        }
    }
}