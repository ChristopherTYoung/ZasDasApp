using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Drink
{
    public int Id { set; get; }

    public int BaseId { set; get; }

    public virtual DrinkBase Base { set; get; } = null!;

    public virtual ICollection<DrinkAddin> DrinkAddins { set; get; } = new List<DrinkAddin>();

    public virtual ICollection<OrderItem> OrderItems { set; get; } = new List<OrderItem>();
}
