using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZasAndDasWeb.Services;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;
using static Square.CatalogObject;

namespace ZasAndDasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(PostgresContext context, OrderService orderService, IAPIKeyValidationService validator) : ControllerBase
    {
        [HttpPost("sendorder")]
        public async Task<IResult> SendOrder(OrderDTO order, [FromHeader] string? APIKEY = null)
        {
            validator.ToString();
            if (APIKEY != null)
            {
                order.CustomerId = validator.GetCustomer(APIKEY).Id;
            }
            if (order.Items.Count() < 1)
                return Results.BadRequest();

            await orderService.AddOrderToDatabase(order);
            return Results.Ok();
        }
        [HttpGet("allorders")]
        public async Task<List<OrderDTO>> GetOrders()
        {
            var orders = await context.PizzaOrders.ToListAsync();
            var orderDTOs = await Task.WhenAll(orders.Select(i => i.ToOrderDTO(context)));
            var list = orderDTOs.ToList();
            return list;
        }
    }
}
