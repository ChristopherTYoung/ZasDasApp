using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class StockItem
{
    public int Id { get; set; }

    public string ItemName { get; set; } = null!;

    public string? Description { get; set; }

    public int BasePriceId { get; set; }

    public int ItemCategoryId { get; set; }

    public virtual PricePerItem BasePrice { get; set; } = null!;

    public virtual Category ItemCategory { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
public class StockItemDTO : IStoreItem
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
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int BasePriceId { get; set; }

    public int ItemCategoryId { get; set; }
    public double Price { get; set; }

    public StockItem ToStockItem() => new StockItem { BasePriceId = BasePriceId, ItemCategoryId = ItemCategoryId, ItemName = Name, Description = Description };
}

