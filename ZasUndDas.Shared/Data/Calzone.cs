using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Calzone
{
    public int Id { set; get; }

    public int BasePriceId { set; get; }

    public int? SauceId { set; get; }

    public bool? CookedAtHome { set; get; }


    public virtual ICollection<CalzonAddin> CalzonAddins { set; get; } = new List<CalzonAddin>();

    public virtual ICollection<OrderItem> OrderItems { set; get; } = new List<OrderItem>();

    public virtual Sauce? Sauce { set; get; }
}
