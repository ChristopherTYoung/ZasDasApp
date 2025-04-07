using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class DrinkBase
{
    public int Id { set; get; }

    public string DrinkName { set; get; } = null!;

    public string? Description { set; get; }

    public int BasePriceId { set; get; }

    public virtual PricePerItem BasePrice { set; get; } = null!;

    public virtual ICollection<Drink> Drinks { set; get; } = new List<Drink>();
}
