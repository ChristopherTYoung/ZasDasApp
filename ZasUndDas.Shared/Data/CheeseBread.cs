using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class CheeseBread
{
    public int Id { set; get; }

    public int SizeId { set; get; }

    public bool? CookedAtHome { set; get; }

    public virtual ICollection<OrderItem> OrderItems { set; get; } = new List<OrderItem>();

    public virtual PizzaSize Size { set; get; } = null!;
}
