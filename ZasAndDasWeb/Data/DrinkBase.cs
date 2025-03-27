using System;
using System.Collections.Generic;

namespace ZasAndDasWeb.Data;

public partial class DrinkBase
{
    public int Id { get; set; }

    public string DrinkName { get; set; } = null!;

    public string? Description { get; set; }

    public int BasePriceId { get; set; }

    public virtual PricePerItem BasePrice { get; set; } = null!;

    public virtual ICollection<Drink> Drinks { get; set; } = new List<Drink>();
}
