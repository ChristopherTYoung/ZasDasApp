using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasAndDasMobile.ViewModels
{
    public partial class ItemViewModel : ObservableObject
    {
        Popup? _popup;
        IStoreItem item;
        CartService _cartService;
        public string Name { get => item.Name; }
        public string? Description { get => item.Description; }
        public double Price { get => item.Price; }

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

        public ItemViewModel(IStoreItem item, CartService cartService)
        {
            this.item = item;
            _cartService = cartService;
        }

        public void SetPopup(Popup popup)
        {
            _popup = popup;
        }

    }
}
