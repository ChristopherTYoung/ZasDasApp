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

    public virtual ICollection<OrderItem> OrderItems { set; get; } = new List<OrderItem>();
}
public class StockItemDTO : IStoreItem, ICheckoutItem
{
    public StockItemDTO()
    {
    }
    public StockItemDTO(StockItem item)
    {
        this.Description = item.Description;
        this.Price = (double)(item.BasePrice?.Price ?? 0.0m);
        this.ItemCategoryId = item.ItemCategoryId;
        this.Name = item.ItemName;
        this.BasePriceId = item?.BasePriceId ?? 0;
    }
    public int Id { set; get; }

    public string Name { set; get; } = null!;

    public string? Description { set; get; }

    public int BasePriceId { set; get; }

    public int ItemCategoryId { set; get; }
    public double Price { set; get; }

    public double GetPrice()
    {
        return Price;
    }

    public StockItem ToStockItem() => new StockItem { BasePriceId = BasePriceId, ItemCategoryId = ItemCategoryId, ItemName = Name, Description = Description };
}

