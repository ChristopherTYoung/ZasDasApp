using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ZasUndDas.Shared;

namespace ZasAndDasMobile.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial ObservableCollection<ICheckoutItem>? CheckoutItems { set; get; }
        private readonly CartService _cartService;

        [RelayCommand]
        public async Task ReturnToHome()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
        }
        public void OnLoad()
        {
            CheckoutItems = new(_cartService.GetCartItems());
        }

    }
}
