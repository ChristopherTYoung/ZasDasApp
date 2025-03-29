using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Customer
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<PizzaOrder> PizzaOrders { get; set; } = new List<PizzaOrder>();
}
