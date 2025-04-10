using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Calzone
{
    public int Id { set; get; }

    public int BasePriceId { set; get; }

    public int? SauceId { set; get; }

    public bool? CookedAtHome { set; get; }

    public double Price { set; get; }
    public virtual Sauce? Sauce { set; get; }
    public virtual Sauce? Sauce { set; get; }
}
