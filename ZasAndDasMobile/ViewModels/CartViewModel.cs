using CommunityToolkit.Mvvm.ComponentModel;
using ZasUndDas.Shared;

namespace ZasAndDasMobile.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        private readonly CartService _cartService;

        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
        }
    }
}
