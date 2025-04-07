using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class Customer
{
    public int Id { set; get; }

    public string CustomerName { set; get; } = null!;

    public string Email { set; get; } = null!;

    public string? Phone { set; get; }
    public string ApiKey { set; get; } = null!;

    public virtual ICollection<PizzaOrder> PizzaOrders { set; get; } = new List<PizzaOrder>();
}
