using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OneOf.Types;
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
        public partial ObservableCollection<CheckoutItemVM>? CartItems { get; set; }
        [ObservableProperty]
        public partial decimal TipAmount { get; set; }
        [ObservableProperty]
        public partial bool CustomTipSelected { get; set; } = false;

        [ObservableProperty]
        public partial string SelectedTip { get; set; } = ".15";

        [ObservableProperty]
        public partial decimal EstimatedTaxes { get; set; }

        [ObservableProperty]
        public partial decimal SubTotal { get; set; }

        [ObservableProperty]
        public partial decimal Total { get; set; }


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

        [RelayCommand]
        public void SetTipAmount(string tip)
        {
            SelectedTip = tip;
            if (decimal.TryParse(tip, out decimal result))
            {
                CustomTipSelected = false;
                UpdateTip(result * SubTotal);
            }
            else
            {
                CustomTipSelected = true;
            }
            OnPropertyChanged(nameof(TipAmount));
        }
        public async Task OpenPayments(Func<Task> SendOrder)
        {
            var popup = new PaymentPage(_cartService, SendOrder);
            await nav!.PushAsync(popup);
        }
        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;

            var items = _cartService.GetCartItems;
            CartItems = items;
            _cartService.CartUpdated += OnCartUpdated!;

            SubTotal = _cartService.CalculateSubTotal();
            EstimatedTaxes = _cartService.CalculateEstimatedTaxes();
            UpdateTip(SubTotal * .15m);
            GetTotal();

        }

        private void OnCartUpdated(object sender, EventArgs e)
        {
            SubTotal = _cartService.CalculateSubTotal();
            EstimatedTaxes = _cartService.CalculateEstimatedTaxes();
            SetTipAmount(SelectedTip);
            GetTotal();
            OnPropertyChanged(nameof(SubTotal));
            OnPropertyChanged(nameof(EstimatedTaxes));
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(CartItems));
        }
        partial void OnTipAmountChanged(decimal oldValue, decimal newValue)
        {
            UpdateTip(newValue);
            GetTotal();
            OnPropertyChanged(nameof(Total));
        }
        private void UpdateTip(decimal newValue)
        {
            if (newValue < 0) _cartService.SetTipAmount(0);
            else _cartService.SetTipAmount(Math.Round((decimal)newValue, 2));
            TipAmount = _cartService.TipAmount;
        }

        private void GetTotal()
        {
            Total = _cartService.CalculateEstimatedTotal();
        }
        internal void setNavigation(INavigation navigation)
        {
            nav = navigation;
        }
    }
}
