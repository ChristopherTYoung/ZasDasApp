﻿using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class StockItem
{
    public int Id { set; get; }

    public string ItemName { set; get; } = null!;

    public string? Description { set; get; }

    public int BasePriceId { set; get; }

    public int ItemCategoryId { set; get; }

}
public class StockItemDTO : IStoreItem, ICheckoutItem
{
    public StockItemDTO()
    {
    }
    public int Id { set; get; }

    public string Name { get; set; } = null!;

    public string? Description { set; get; }

    public int ItemCategoryId { set; get; }
    public decimal Price { set; get; }
    public string? ImagePath { set; get; }
    public string? GetImagePath() => ImagePath;
    public int Quantity { set; get; }
}

