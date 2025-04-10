using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class StockItem
{
    public int Id { set; get; }

    public string ItemName { set; get; } = null!;

    public string? Description { set; get; }

    public int BasePriceId { set; get; }

    public int ItemCategoryId { set; get; }

    public virtual PricePerItem BasePrice { set; get; } = null!;

    public virtual Category ItemCategory { set; get; } = null!;

}
public class StockItemDTO : IStoreItem, ICheckoutItem
{
    public StockItemDTO()
    {
    }
    public int Id { set; get; }

    public string Name { set; get; } = null!;

    public string? Description { set; get; }

    public int ItemCategoryId { set; get; }
    public double Price { set; get; }

    public double GetPrice()
    {
        return Price;
    }
}

