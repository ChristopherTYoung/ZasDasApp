using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;
using ZasAndDasMobile.Popups;
using CommunityToolkit.Maui.Views;
using ZasUndDas.Shared;
using System.ComponentModel;

namespace ZasAndDasMobile.ViewModels
{

    public partial class MainPageViewModel : ObservableObject
    {
        MenuItemService _service;
        CartService _cartService;

        string defaultShown = "pizza";

        [ObservableProperty]
        public partial bool PizzasAreVisible { set; get; } = true;

        [ObservableProperty]
        public partial bool DrinksAreVisible { set; get; } = false;

        [ObservableProperty]
        public partial ObservableCollection<PizzaBaseDTO> PizzaList { set; get; } = new ObservableCollection<PizzaBaseDTO>();

        [ObservableProperty]
        public partial int CartItemCount { get; set; } = 0;

        public MainPageViewModel(MenuItemService service, CartService cartService)
        {
            this._cartService = cartService;
            UpdateTabs(defaultShown);
            _service = service;
            _cartService.CartUpdated += OnCartUpdated!;
            CartItemCount = _cartService.GetItemCount;
            service.Update += Sync;
            service.ForceSync();
            Sync();
        }
        private void Sync()
        {
            PizzaList = new ObservableCollection<PizzaBaseDTO>();
            foreach (PizzaBaseDTO pizza in _service.GetAllPizzas())
                PizzaList.Add(pizza);
        }

        [RelayCommand]
        public void UpdateTabs(string itemCategory)
        {
            PizzasAreVisible = itemCategory == "pizza";
            DrinksAreVisible = itemCategory == "drink";
        }

        [RelayCommand]
        public async Task GoToCart()
        {
            await Shell.Current.GoToAsync("//Cart");
        }

        [RelayCommand]
        public void ShowItemPopup(IStoreItem item)
        {
            if (item.GetType() == typeof(PizzaBaseDTO))
            {
                var popup = new ItemPopup(new ItemViewModel(item, _cartService));
                Shell.Current.ShowPopup(popup);
            }
        }
        private void OnCartUpdated(object sender, EventArgs e)
        {
            CartItemCount = _cartService.GetItemCount;
        }
    }
}