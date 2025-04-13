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
}
public class PizzaBaseDTO : IStoreItem
{
    public PizzaBaseDTO()
    {
    }
    public int Id { set; get; }

    public string Name { set; get; } = null!;

    public string Description { set; get; } = "";
    public string? ImagePath { set; get; }
    public double Price { set; get; }
}