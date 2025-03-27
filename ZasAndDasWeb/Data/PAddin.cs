using System;
using System.Collections.Generic;

namespace ZasAndDasWeb.Data;

public partial class PAddin
{
    public int Id { get; set; }

    public string AddinName { get; set; } = null!;

    public int BasePriceId { get; set; }

    public virtual PricePerItem BasePrice { get; set; } = null!;

    public virtual ICollection<CalzonAddin> CalzonAddins { get; set; } = new List<CalzonAddin>();

    public virtual ICollection<PizzaAddin> PizzaAddins { get; set; } = new List<PizzaAddin>();

    public virtual ICollection<SaladAddin> SaladAddins { get; set; } = new List<SaladAddin>();
}
