using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZasUndDas.Shared;
using Microsoft.EntityFrameworkCore;
using ZasUndDas.Shared.Data;
namespace ZasAndDasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController(PostgresContext context) : ControllerBase
    {
        [HttpGet]
        public List<IStoreItem> GetAll()
        {
            return new();
        }
        [HttpGet("getallpizzabase")]
        public async Task<IEnumerable<PizzaBase>> GetPizza()
        {
            return await context.PizzaBases.ToListAsync();
        }
        [HttpPost("addpizzabase")]
        public async Task<IResult> MakePizza(PizzaBaseDTO pizza)
        {
            await context.PizzaBases.AddAsync(pizza.ToPizzaBase());
            await context.SaveChangesAsync();
            return Results.Ok();
        }
        [HttpPost("addbaseprice")]
        public async Task<IResult> AddBasePrice(PricePerItem item)
        {
            await context.PricePerItems.AddAsync(item);
            await context.SaveChangesAsync();
            return Results.Ok();
        }
        [HttpGet("getallprices")]
        public async Task<IEnumerable<PricePerItem>> GetAllPrices()
        {
            return await context.PricePerItems.ToListAsync();
        }
    }
}