using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class CheeseBread
{
    public int Id { get; set; }

    public int SizeId { get; set; }

    public bool? CookedAtHome { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual PizzaSize Size { get; set; } = null!;
}
