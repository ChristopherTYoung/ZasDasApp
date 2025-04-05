using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasAndDasMobile.ViewModels
{
    public partial class ItemViewModel : ObservableObject
    {
        IStoreItem item;
        public string Name { get => item.Name; }
        public string? Description { get => item.Description; }
        public double Price { get => item.Price; }

        public ItemViewModel(IStoreItem item)
        {
            this.item = item;
        }
    }
}
