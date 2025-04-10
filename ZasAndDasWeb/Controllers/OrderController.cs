using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasAndDasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(PostgresContext context) : ControllerBase
    {
        [HttpPost("sendorder")]
        public async Task<IResult> SendOrder(OrderDTO order)
        {
            if (order.Items.Count() < 1)
                return Results.BadRequest();

            await context.PizzaOrders.AddAsync(order);
            await context.SaveChangesAsync();
            return Results.Ok();
        }
        [HttpGet("allorders")]
        public async Task<List<OrderDTO>> GetOrders()
        {
            return await context.PizzaOrders.ToListAsync();
        }
    }
}
