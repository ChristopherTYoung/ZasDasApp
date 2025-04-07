using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Salad
{
    public int Id { set; get; }

    public int BasePriceId { set; get; }

    public virtual PricePerItem BasePrice { set; get; } = null!;

    public virtual ICollection<OrderItem> OrderItems { set; get; } = new List<OrderItem>();

    public virtual ICollection<SaladAddin> SaladAddins { set; get; } = new List<SaladAddin>();
}
