using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Drink
{
    public int Id { get; set; }

    public int BaseId { get; set; }

    public virtual DrinkBase Base { get; set; } = null!;

    public virtual ICollection<DrinkAddin> DrinkAddins { get; set; } = new List<DrinkAddin>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
