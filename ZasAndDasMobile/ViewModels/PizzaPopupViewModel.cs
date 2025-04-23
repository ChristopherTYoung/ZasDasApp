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
    public partial class PizzaPopupViewModel : ObservableObject, IRecipient<UpdatePopupMessage>
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
        public partial ObservableCollection<string> CookStyle { get; set; } = new ObservableCollection<string>() { "In Store", "Take and Bake" };
        [ObservableProperty]
        public partial string SelectedCookStyle { get; set; }

        [ObservableProperty]
        public partial List<PizzaSize> PizzaSizes { get; set; }
        [ObservableProperty]
        public partial PizzaSize? SelectedPizzaSize { get; set; }

        [ObservableProperty]
        public partial List<Sauce> PizzaSauces { get; set; }
        [ObservableProperty]
        public partial Sauce? SelectedPizzaSauce { get; set; }
        [ObservableProperty]
        public partial List<PAddinDTO> PizzaAddins { get; set; }

        public bool CanAddToCart => SelectedPizzaSize != null;

        [RelayCommand]
        public void ClosePopup()
        {
            _popup?.Close();
        }

        [RelayCommand]
        public void AddItemToCart()
        {
            var pizza = new PizzaDTO((PizzaBaseDTO)item);
            foreach (var addin in PizzaAddins.Where(addin => addin.IsChecked))
            {
                pizza.AddTopping(addin);
            }
            pizza.ChangeSize(SelectedPizzaSize!);
            pizza.ChangeSauce(SelectedPizzaSauce!);
            pizza.CookedAtHome = SelectedCookStyle == "Take and Bake";
            _cartService.AddToCart(pizza);
            _popup?.Close();
        }

        public PizzaPopupViewModel(IStoreItem item, CartService cartService, MenuItemService menuItemService)
        {
            WeakReferenceMessenger.Default.Register(this);
            this.item = item;
            _cartService = cartService;
            _menuItemService = menuItemService;
            SelectedCookStyle = CookStyle[0];
            PizzaSizes = new List<PizzaSize>();
            PizzaSauces = new List<Sauce>();
            PizzaAddins = new List<PAddinDTO>();
            OnPropertyChanged(nameof(CanAddToCart));
        }

        public async Task Sync()
        {
            PizzaSizes = SetPizzaSizes(await _menuItemService.GetPizzaSizes());
            PizzaSauces = await _menuItemService.GetSauces();
            PizzaAddins = await _menuItemService.GetPAddonDTOs();
            SelectedPizzaSauce = PizzaSauces.FirstOrDefault(s => s.Id == 1);
        }

        public void SetPopup(Popup popup)
        {
            _popup = popup;
        }

        private List<PizzaSize> SetPizzaSizes(List<PizzaSize> pizzaSizes)
        {
            if (Name != "CYO") pizzaSizes.RemoveAll(size => size.Id == 1);
            return pizzaSizes;
        }
        partial void OnSelectedPizzaSizeChanged(PizzaSize? value)
        {
            OnPropertyChanged(nameof(CanAddToCart));
        }

        public async void Receive(UpdatePopupMessage message)
        {
            await Sync();
        }
    }
}
