﻿using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class PricePerItem
{
    public int Id { set; get; }

    public decimal Price { set; get; }

    public virtual ICollection<CalzoneDTO> Calzones { set; get; } = new List<CalzoneDTO>();

    public virtual ICollection<DAddin> DAddins { set; get; } = new List<DAddin>();

    public virtual ICollection<DrinkBaseDTO> DrinkBases { set; get; } = new List<DrinkBaseDTO>();

    public virtual ICollection<PAddin> PAddins { set; get; } = new List<PAddin>();

    public virtual ICollection<PizzaBase> PizzaBases { set; get; } = new List<PizzaBase>();

    public virtual ICollection<PizzaSize> PizzaSizes { set; get; } = new List<PizzaSize>();

    public virtual ICollection<Salad> Salads { set; get; } = new List<Salad>();

    public virtual ICollection<StockItem> StockItems { set; get; } = new List<StockItem>();
}
