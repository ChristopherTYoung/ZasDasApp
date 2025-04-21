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
        public partial string LabelText { get; set; } = "Specialty Pizzas";

        [ObservableProperty]
        public partial bool PizzasAreVisible { set; get; } = true;

        [ObservableProperty]
        public partial bool DrinksAreVisible { set; get; } = false;
        [ObservableProperty]
        public partial bool SidesAreVisible { get; set; } = false;

        [ObservableProperty]
        public partial ObservableCollection<PizzaBaseDTO> PizzaList { set; get; } = new ObservableCollection<PizzaBaseDTO>();
        [ObservableProperty]
        public partial ObservableCollection<DrinkBaseDTO> DrinkList { get; set; } = new ObservableCollection<DrinkBaseDTO>();

        [ObservableProperty]
        public partial int CartItemCount { get; set; }

        public MainPageViewModel(MenuItemService service, CartService cartService)
        {
            this._cartService = cartService;
            UpdateTabs(defaultShown);
            _service = service;
            _cartService.CartUpdated += OnCartUpdated!;
            CartItemCount = _cartService.GetItemCount;
        }
        public async Task Initialize()
        {
            await PizzaSetup();
            await DrinkSetup();
        }
        private async Task PizzaSetup()
        {
            PizzaList = new ObservableCollection<PizzaBaseDTO>();
            foreach (PizzaBaseDTO pizza in await _service.GetAllPizzas())
                PizzaList.Add(pizza);
        }
        private async Task DrinkSetup()
        {
            DrinkList = new ObservableCollection<DrinkBaseDTO>();
            foreach (DrinkBaseDTO drink in await _service.GetAllDrinks())
                DrinkList.Add(drink);
        }

        [RelayCommand]
        public void UpdateTabs(string itemCategory)
        {
            PizzasAreVisible = itemCategory == "pizza";
            DrinksAreVisible = itemCategory == "drink";
            SidesAreVisible = itemCategory == "side";
            UpdateTitleText(itemCategory);
        }

        [RelayCommand]
        public void UpdateTitleText(string itemCategory)
        {
            if (itemCategory == "pizza") LabelText = "Specialty Pizzas";
            else if (itemCategory == "drink") LabelText = "Mixed Drinks";
            else if (itemCategory == "side") LabelText = "Sides";
            else LabelText = "Unknown";
        }

        [RelayCommand]
        public async Task GoToCart()
        {
            await Shell.Current.GoToAsync("///Cart");
        }

        [RelayCommand]
        public void ShowItemPopup(IStoreItem item)
        {
            if (item.GetType() == typeof(PizzaBaseDTO))
            {
                var popup = new PizzaPopup(new PizzaPopupViewModel(item, _cartService, _service));
                Shell.Current.ShowPopup(popup);
            }
            else if (item.GetType() == typeof(DrinkBaseDTO))
            {
                var popup = new DrinkPopup(new DrinkPopupViewModel(item, _cartService, _service));
                Shell.Current.ShowPopup(popup);
            }
        }
        private void OnCartUpdated(object sender, EventArgs e)
        {
            CartItemCount = _cartService.GetItemCount;
        }
    }
}