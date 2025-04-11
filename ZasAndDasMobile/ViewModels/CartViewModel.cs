using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ZasAndDasMobile.Popups;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasAndDasMobile.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        private readonly CartService _cartService;

        [ObservableProperty]
        public partial ObservableCollection<ICheckoutItem>? CartItems { get; set; }

        [RelayCommand]
        public async Task ReturnToHome()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
        [RelayCommand]
        public void OpenPayments()
        {
            var displayInfo = DeviceDisplay.MainDisplayInfo;
            var popup = new PaymentPopup(_cartService)
            {
                Size = new() { Height = displayInfo.Height * .5, Width = displayInfo.Width * .5 }
            };
            Shell.Current.ShowPopup(popup);
        }
        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
            CartItems = _cartService.GetCartItems;
            _cartService.CartUpdated += OnCartUpdated!;
        }
        public void OnLoad()
        {
            //CheckoutItems = new(_cartService.GetCartItems());
        }

        private void OnCartUpdated(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(CartItems));
        }

    }
}
