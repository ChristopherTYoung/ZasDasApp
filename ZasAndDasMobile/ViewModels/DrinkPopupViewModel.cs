using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasAndDasMobile.Messages;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile.ViewModels
{
    public partial class DrinkPopupViewModel : ObservableObject, IRecipient<UpdatePopupMessage>
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
            DrinkAddins = new();
            WeakReferenceMessenger.Default.Register(this);
        }

        public async Task Sync()
        {
            DrinkAddins = await _menuItemService.GetDAddinDTOs();
        }

        public void SetPopup(Popup popup)
        {
            _popup = popup;
        }

        public async void Receive(UpdatePopupMessage message)
        {
            await Sync();
        }
    }
}
