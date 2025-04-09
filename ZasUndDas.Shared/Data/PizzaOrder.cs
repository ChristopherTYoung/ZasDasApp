using System;
using System.Collections.Generic;

namespace ZasUndDas.Shared.Data;

public partial class PizzaOrder
{
    public int Id { set; get; }

    public int? CustomerId { set; get; }

    public DateTime? DateOrdered { set; get; }

    public decimal GrossAmount { set; get; }

    public decimal DiscountAmount { set; get; }

    public decimal NetAmount { set; get; }

    public decimal SalesTax { set; get; }

    public virtual Customer? Customer { set; get; }

    public virtual ICollection<OrderItem> OrderItems { set; get; } = new List<OrderItem>();

    public virtual ICollection<OrderPromotion> OrderPromotions { set; get; } = new List<OrderPromotion>();
}
public class OrderDTO
{
    public int Id { set; get; }
    public int? CustomerId { set; get; }
    public decimal DiscountAmount { set; get; }
    public decimal GrossAmount { set; get; }
    public decimal NetAmount { set; get; }
    public decimal SalesTax { set; get; }
    public List<OrderItemDTO> Items { set; get; } = new List<OrderItemDTO>();
    public DateTime? DateOrdered { set; get; }
}
