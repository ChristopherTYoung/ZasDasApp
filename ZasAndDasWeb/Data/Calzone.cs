using System;
using System.Collections.Generic;

namespace ZasAndDasWeb.Data;

public partial class Calzone
{
    public int Id { get; set; }

    public int BasePriceId { get; set; }

    public int? SauceId { get; set; }

    public bool? CookedAtHome { get; set; }

    public virtual PricePerItem BasePrice { get; set; } = null!;

    public virtual ICollection<CalzonAddin> CalzonAddins { get; set; } = new List<CalzonAddin>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Sauce? Sauce { get; set; }
}
