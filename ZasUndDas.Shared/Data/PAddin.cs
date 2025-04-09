using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class PAddin
{
    public int Id { set; get; }

    public string AddinName { set; get; } = null!;

    public int BasePriceId { set; get; }

    public virtual PricePerItem BasePrice { set; get; } = null!;

    public virtual ICollection<CalzonAddin> CalzonAddins { set; get; } = new List<CalzonAddin>();

    public virtual ICollection<PizzaAddin> PizzaAddins { set; get; } = new List<PizzaAddin>();

    public virtual ICollection<SaladAddin> SaladAddins { set; get; } = new List<SaladAddin>();
}
public class PAddinDTO
{
    public int Id { set; get; }
    public string AddinName { set; get; }
    public double BasePrice { set; get; }
}
