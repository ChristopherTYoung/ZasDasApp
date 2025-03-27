using System;
using System.Collections.Generic;

namespace ZasAndDasWeb.Data;

public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int? Quantity { get; set; }

    public int? StockItemId { get; set; }

    public int? PizzaId { get; set; }

    public int? DrinkId { get; set; }

    public int? CalzoneId { get; set; }

    public int? SaladId { get; set; }

    public int? CheeseBreadId { get; set; }

    public virtual Calzone? Calzone { get; set; }

    public virtual CheeseBread? CheeseBread { get; set; }

    public virtual Drink? Drink { get; set; }

    public virtual PizzaOrder Order { get; set; } = null!;

    public virtual Pizza? Pizza { get; set; }

    public virtual Salad? Salad { get; set; }

    public virtual StockItem? StockItem { get; set; }
}
