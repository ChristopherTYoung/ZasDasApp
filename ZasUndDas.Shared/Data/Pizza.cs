using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Pizza
{
    public int Id { get; set; }

    public int SizeId { get; set; }

    public int BaseId { get; set; }

    public bool CookedAtHome { get; set; }

    public virtual PizzaBase Base { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<PizzaAddin> PizzaAddins { get; set; } = new List<PizzaAddin>();

    public virtual PizzaSize Size { get; set; } = null!;
}
