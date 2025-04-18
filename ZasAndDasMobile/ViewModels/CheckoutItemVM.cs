using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;

namespace ZasAndDasMobile.ViewModels
{
    public class CheckoutItemVM
    {
        public CheckoutItemVM(ICheckoutItem item)
        {
            this.item = item;
        }
        ICheckoutItem item;
        public decimal Price => item.Price;
        public string? ImagePath => item.GetImagePath();
        public string? Name => item.Name;

    }
}
