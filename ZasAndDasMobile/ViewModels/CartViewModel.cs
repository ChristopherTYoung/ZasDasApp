using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZasUndDas.Shared;

namespace ZasAndDasMobile.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        private readonly CartService _cartService;

        [RelayCommand]
        public async Task ReturnToHome()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
        }

    }
}
