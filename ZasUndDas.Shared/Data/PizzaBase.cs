using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class PizzaBase
{
    public int Id { get; set; }

    public string PizzaName { get; set; } = null!;

    public string? Description { get; set; }

    public int BasePriceId { get; set; }
    public string? ImagePath { get; set; }

    public virtual PricePerItem BasePrice { get; set; } = null!;

    public virtual ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
}
public class PizzaBaseDTO : IStoreItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int BasePriceId { get; set; }
    public string? ImagePath { get; set; }
    public double Price { get; set; }

    public PizzaBase ToPizzaBase() => new PizzaBase() { BasePriceId = BasePriceId, PizzaName = Name, Description = Description };
}