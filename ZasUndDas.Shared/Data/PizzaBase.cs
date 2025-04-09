using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class PizzaBase
{
    public int Id { set; get; }

    public string PizzaName { set; get; } = null!;

    public string? Description { set; get; }

    public int BasePriceId { set; get; }
    public string? ImagePath { set; get; }

    public virtual PricePerItem BasePrice { set; get; } = null!;

    public virtual ICollection<Pizza> Pizzas { set; get; } = new List<Pizza>();
}
public class PizzaBaseDTO : IStoreItem
{
    public PizzaBaseDTO()
    {
    }
    public PizzaBaseDTO(PizzaBase pizzza, double price)
    {
        Id = pizzza.Id;
        Name = pizzza.PizzaName;
        Description = pizzza.Description ?? String.Empty;
        BasePriceId = pizzza.BasePriceId;
        Price = price;
        ImagePath = pizzza.ImagePath;
    }
    public int Id { set; get; }

    public string Name { set; get; } = null!;

    public string Description { set; get; } = String.Empty;

    public int BasePriceId { set; get; }
    public string? ImagePath { set; get; }
    public double Price { set; get; }
    public PizzaDTO ToPizzaEmpty()
    {
        return new PizzaDTO() { Name = this.Name, Price = this.Price, Description = this.Description ?? String.Empty, Id = this.Id };
    }

    public PizzaBase ToPizzaBase() => new PizzaBase() { BasePriceId = BasePriceId, PizzaName = Name, Description = Description };
}