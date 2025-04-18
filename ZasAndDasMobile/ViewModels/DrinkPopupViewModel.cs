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
    public partial class DrinkPopupViewModel : ObservableObject
    {
        Popup? _popup;
        IStoreItem item;
        CartService _cartService;
        MenuItemService _menuItemService;
        public string Name { get => item.Name; }
        public string? Description { get => item.Description; }
        public decimal Price { get => item.Price; }
        public string? ImagePath { get => item.ImagePath; }


        [ObservableProperty]
        public partial List<DAddinDTO> DrinkAddins { get; set; }


        [RelayCommand]
        public void ClosePopup()
        {
            _popup?.Close();
        }

        [RelayCommand]
        public void AddItemToCart()
        {
            var drink = new DrinkDTO((DrinkBaseDTO)item);
            foreach (var addin in DrinkAddins.Where(addin => addin.IsChecked))
            {
                drink.AddDrinkAddin(addin);
            }
            _cartService.AddToCart(drink);
            _popup?.Close();
        }

        public DrinkPopupViewModel(IStoreItem item, CartService cartService, MenuItemService menuItemService)
        {
            this.item = item;
            _cartService = cartService;
            _menuItemService = menuItemService;
            DrinkAddins = _menuItemService.GetDAddinDTOs().Result;
        }

        public void SetPopup(Popup popup)
        {
            _popup = popup;
        }
    }
}
