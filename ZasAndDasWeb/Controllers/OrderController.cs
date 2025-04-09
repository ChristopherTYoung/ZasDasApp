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
        public IResult SendOrder(OrderDTO order)
        {
            if (order.Items.Count() < 1)
                return Results.BadRequest();
            return Results.Ok();
        }
        [HttpGet("allorders")]
        public IEnumerable<OrderDTO> GetOrders()
        {
            return context.PizzaOrders.Select(o => o.ToOrderDTO(context));
        }
    }
}
