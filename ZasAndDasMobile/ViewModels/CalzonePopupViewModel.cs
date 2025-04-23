using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile.ViewModels
{
    public partial class CalzonePopupViewModel : ObservableObject
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
        public partial List<PAddinDTO> CalzoneAddins { get; set; }
        [ObservableProperty]
        public partial List<Sauce> PizzaSauces { get; set; }
        [ObservableProperty]
        public partial Sauce? SelectedPizzaSauce { get; set; }


        [RelayCommand]
        public void ClosePopup()
        {
            _popup?.Close();
        }

        [RelayCommand]
        public void AddItemToCart()
        {
            var calzone = new CalzoneDTO((CalzoneBase)item);
            foreach (var addin in CalzoneAddins.Where(addin => addin.IsChecked))
            {
                calzone.AddTopping(addin);
            }
            _cartService.AddToCart(calzone);
            _popup?.Close();
        }

        public CalzonePopupViewModel(IStoreItem item, CartService cartService, MenuItemService menuItemService)
        {
            this.item = item;
            _cartService = cartService;
            _menuItemService = menuItemService;
            CalzoneAddins = _menuItemService.GetPAddonDTOs().Result;
            PizzaSauces = _menuItemService.GetSauces().Result;
            SelectedPizzaSauce = PizzaSauces.FirstOrDefault(s => s.Id == 1);
            SelectedCookStyle = CookStyle[0];
        }

        public void SetPopup(Popup popup)
        {
            _popup = popup;
        }
    }
}
