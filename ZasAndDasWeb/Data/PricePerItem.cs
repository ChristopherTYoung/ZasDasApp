using System;
using System.Collections.Generic;

namespace ZasAndDasWeb.Data;

public partial class PricePerItem
{
    public int Id { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Calzone> Calzones { get; set; } = new List<Calzone>();

    public virtual ICollection<DAddin> DAddins { get; set; } = new List<DAddin>();

    public virtual ICollection<DrinkBase> DrinkBases { get; set; } = new List<DrinkBase>();

    public virtual ICollection<PAddin> PAddins { get; set; } = new List<PAddin>();

    public virtual ICollection<PizzaBase> PizzaBases { get; set; } = new List<PizzaBase>();

    public virtual ICollection<PizzaSize> PizzaSizes { get; set; } = new List<PizzaSize>();

    public virtual ICollection<Salad> Salads { get; set; } = new List<Salad>();

    public virtual ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
}
