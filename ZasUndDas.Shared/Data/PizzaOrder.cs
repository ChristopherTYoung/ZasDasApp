using Square;
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

    public async Task<OrderDTO> ToOrderDTO(PostgresContext context)
    {
        var items = context.OrderItems.Where(i => i.OrderId == Id)
                                       .Select(i => i.ToOrderItemDTO(context))
                                       .ToList();
        var orderItemDTOs = await Task.WhenAll(items);
        return new OrderDTO
        {
            Id = Id,
            CustomerId = CustomerId,
            DateOrdered = DateOrdered,
            GrossAmount = GrossAmount,
            DiscountAmount = DiscountAmount,
            NetAmount = NetAmount,
            SalesTax = SalesTax,
            Items = orderItemDTOs.ToList()
        };
    }
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
