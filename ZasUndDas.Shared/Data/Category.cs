using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Category
{
    public int Id { set; get; }

    public string CategoryName { set; get; } = null!;

    public string? Description { set; get; }

    public virtual ICollection<StockItem> StockItems { set; get; } = new List<StockItem>();
}
