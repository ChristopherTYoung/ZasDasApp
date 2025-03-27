using System;
using System.Collections.Generic;

namespace ZasAndDasWeb.Data;

public partial class PizzaBase
{
    public int Id { get; set; }

    public string PizzaName { get; set; } = null!;

    public string? Description { get; set; }

    public int BasePriceId { get; set; }

    public virtual PricePerItem BasePrice { get; set; } = null!;

    public virtual ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
}
