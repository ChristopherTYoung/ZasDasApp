using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Salad
{
    public int Id { get; set; }

    public int BasePriceId { get; set; }

    public virtual PricePerItem BasePrice { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<SaladAddin> SaladAddins { get; set; } = new List<SaladAddin>();
}
