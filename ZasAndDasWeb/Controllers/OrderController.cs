using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZasAndDasWeb.Services;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasAndDasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(PostgresContext context, OrderService orderService) : ControllerBase
    {
        [HttpPost("sendorder")]
        public async Task<IResult> SendOrder(OrderDTO order)
        {
            if (order.Items.Count() < 1)
                return Results.BadRequest();

            await orderService.AddOrderToDatabase(order);
            return Results.Ok();
        }
        [HttpGet("allorders")]
        public async Task<List<OrderDTO>> GetOrders()
        {
            return await context.PizzaOrders.ToListAsync();
        }
    }
}
