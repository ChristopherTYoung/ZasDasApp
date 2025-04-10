using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ZasUndDas.Shared.Data;

public partial class OrderItemDTO
{

    public int Id { set; get; }

    public int OrderId { set; get; }

    public int? Quantity { set; get; }

    public int? StockItemId { set; get; }

    public int? PizzaId { set; get; }

    public int? DrinkId { set; get; }

    public int? CalzoneId { set; get; }

    public int? SaladId { set; get; }

    public int? CheeseBreadId { set; get; }

    public virtual Calzone? Calzone { set; get; }

    public virtual CheeseBread? CheeseBread { set; get; }

    public virtual Drink? Drink { set; get; }

    public virtual Pizza? Pizza { set; get; }

    public virtual Salad? Salad { set; get; }

    public virtual StockItem? StockItem { set; get; }
}
