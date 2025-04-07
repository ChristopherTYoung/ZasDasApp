using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Pizza
{
    public int Id { set; get; }

    public int SizeId { set; get; }

    public int BaseId { set; get; }

    public bool CookedAtHome { set; get; }

    public virtual PizzaBase Base { set; get; } = null!;

    public virtual ICollection<OrderItem> OrderItems { set; get; } = new List<OrderItem>();

    public virtual ICollection<PizzaAddin> PizzaAddins { set; get; } = new List<PizzaAddin>();

    public virtual PizzaSize Size { set; get; } = null!;
}
