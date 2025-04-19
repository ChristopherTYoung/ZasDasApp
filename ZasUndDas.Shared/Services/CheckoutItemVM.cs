﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ZasUndDas.Shared.Services
{
    public class CheckoutItemVM
    {
        public CheckoutItemVM(ICheckoutItem item)
        {
            this.item = item;
        }
        public ICheckoutItem item;
        public decimal Price => item.Price;
        public string? ImagePath => item.GetImagePath();
        public string? Name => item.Name;

    }
}
