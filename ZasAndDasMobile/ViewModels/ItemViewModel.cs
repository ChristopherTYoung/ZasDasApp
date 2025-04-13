using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile.ViewModels
{
    public partial class ItemViewModel : ObservableObject
    {
        Popup? _popup;
        IStoreItem item;
        CartService _cartService;
        MenuItemService _menuItemService;
        public string Name { get => item.Name; }
        public string? Description { get => item.Description; }
        public double Price { get => item.Price; }

        [ObservableProperty]
        public partial ObservableCollection<string> CookStyle { get; set; } = new ObservableCollection<string>() { "In Store", "Take and Bake" };
        [ObservableProperty]
        public partial string SelectedCookStyle { get; set; }

        [ObservableProperty]
        public partial List<PizzaSize> PizzaSizes { get; set; }
        [ObservableProperty]
        public partial PizzaSize? SelectedPizzaSize { get; set; }

        [ObservableProperty]
        public partial List<Sauce> PizzaSauces { get; set; }
        [ObservableProperty]
        public partial Sauce? SelectedPizzaSauce { get; set; }

        public bool CanAddToCart => SelectedPizzaSize != null && SelectedPizzaSauce != null;

        [RelayCommand]
        public void ClosePopup()
        {
            _popup?.Close();
        }

        [RelayCommand]
        public void AddItemToCart()
        {
            _cartService.AddToCart(new PizzaDTO((PizzaBaseDTO)item));
            _popup?.Close();
        }

        public ItemViewModel(IStoreItem item, CartService cartService, MenuItemService menuItemService)
        {
            this.item = item;
            _cartService = cartService;
            _menuItemService = menuItemService;
            PizzaSizes = _menuItemService.GetPizzaSizes().Result;
            PizzaSauces = _menuItemService.GetSauces().Result;
            SelectedCookStyle = CookStyle[0];
            OnPropertyChanged(nameof(CanAddToCart));
        }

        public void SetPopup(Popup popup)
        {
            _popup = popup;
        }
        partial void OnSelectedPizzaSizeChanged(PizzaSize? value)
        {
            OnPropertyChanged(nameof(CanAddToCart));
        }

        partial void OnSelectedPizzaSauceChanged(Sauce? value)
        {
            OnPropertyChanged(nameof(CanAddToCart));
        }
    }
}
