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
