using System;
using System.Collections.Generic;

namespace ZasAndDasWeb.Data;

public partial class PizzaOrder
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? DateOrdered { get; set; }

    public decimal? GrossAmount { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? NetAmount { get; set; }

    public decimal? SalesTax { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<OrderPromotion> OrderPromotions { get; set; } = new List<OrderPromotion>();
}
