﻿using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class PizzaBase
{
    public int Id { get; set; }

    public string PizzaName { get; set; } = null!;

    public string? Description { get; set; }

    public int BasePriceId { get; set; }

    public virtual PricePerItem BasePrice { get; set; } = null!;

    public virtual ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
}
public class PizzaBaseDTO
{
    public int Id { get; set; }

    public string PizzaName { get; set; } = null!;

    public string? Description { get; set; }

    public int BasePriceId { get; set; }
    public PizzaBase ToPizzaBase() => new PizzaBase() { BasePriceId = BasePriceId, PizzaName = PizzaName, Description = Description };
}