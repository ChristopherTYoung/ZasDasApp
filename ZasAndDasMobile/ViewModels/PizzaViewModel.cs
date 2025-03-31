using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;

namespace ZasAndDasMobile.ViewModels
{
    public partial class PizzaViewModel : ObservableObject
    {
        PizzaDTO pizza;
        public string Name { get => pizza.Name; }
        public string Description { get => pizza.Description; }
        public double Price { get => pizza.Price; }
        public ObservableCollection<string> Ingredients { get => new ObservableCollection<string>(pizza.Ingredients); }
        [ObservableProperty]
        public partial int Quantity { set; get; }
        public PizzaViewModel(PizzaDTO pizza)
        {
            this.pizza = pizza;
        }
    }
}