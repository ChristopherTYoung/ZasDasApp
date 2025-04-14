using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ZasAndDasMobile.Popups;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        private readonly CartService _cartService;
        private INavigation? nav;

        [ObservableProperty]
        public partial ObservableCollection<ICheckoutItem>? CartItems { get; set; }

        [RelayCommand]
        public async Task ReturnToHome()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }


        [RelayCommand]
        public async Task OrderAndPay()
        {
            await OpenPayments(async () => { await _cartService.SendOrder(); _cartService.ClearCart(); });
        }
        public async Task OpenPayments(Func<Task> SendOrder)
        {
            var popup = new PaymentPage(_cartService, SendOrder);
            await nav!.PushAsync(popup);
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

        internal void setNavigation(INavigation navigation)
        {
            nav = navigation;
        }
    }
}
