using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class PizzaSize
{
    public int Id { set; get; }

    public string SizeName { set; get; } = null!;

    public int BasePriceId { set; get; }

    public virtual PricePerItem BasePrice { set; get; } = null!;

    public virtual ICollection<CheeseBread> CheeseBreads { set; get; } = new List<CheeseBread>();

    public virtual ICollection<Pizza> Pizzas { set; get; } = new List<Pizza>();
}
