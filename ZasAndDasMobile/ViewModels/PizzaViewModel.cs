using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasAndDasMobile.ViewModels
{
    public partial class PizzaViewModel : ObservableObject
    {
        PizzaBaseDTO pizza;
        public string Name { get => pizza.Name; }
        public string? Description { get => pizza.Description; }
        public double Price { get => pizza.Price; }
        // temporary fix shh..
        //public ObservableCollection<int> Ingredients { get => new ObservableCollection<int>(pizza.Ingredients); }
        public ObservableCollection<int> Ingredients { get; }
        [ObservableProperty]
        public partial int Quantity { set; get; }
        public PizzaViewModel(PizzaBaseDTO pizza)
        {
            this.pizza = pizza;
            Ingredients = new ObservableCollection<int>();
        }
    }
}