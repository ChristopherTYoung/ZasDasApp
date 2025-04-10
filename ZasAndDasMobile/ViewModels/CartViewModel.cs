using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasAndDasMobile.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        private readonly CartService _cartService;

        [ObservableProperty]
        public partial ObservableCollection<ICheckoutItem> CartItems { get; set; }

        [RelayCommand]
        public async Task ReturnToHome()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
            CartItems = _cartService.GetCartItems;
            _cartService.CartUpdated += OnCartUpdated!;
        }

        private void OnCartUpdated(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(CartItems));
        }
    }
}
