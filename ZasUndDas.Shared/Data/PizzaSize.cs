using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class PizzaSize
{
    public int Id { get; set; }

    public string SizeName { get; set; } = null!;

    public int BasePriceId { get; set; }

    public virtual PricePerItem BasePrice { get; set; } = null!;

    public virtual ICollection<CheeseBread> CheeseBreads { get; set; } = new List<CheeseBread>();

    public virtual ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
}
