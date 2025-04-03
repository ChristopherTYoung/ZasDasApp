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
        [HttpGet("/")]
        public string Get()
        {
            return "Hello this is the inventory controller endpoint";
        }
        [HttpGet("getallpizzabase")]
        public async Task<List<PizzaBase>> GetPizza()
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
        public async Task<List<PricePerItem>> GetAllPrices()
        {
            return await context.PricePerItems.ToListAsync();
        }
        [HttpPost("addstockitem")]
        public async Task<IResult> AddStockItem(StockItemDTO stockItem)
        {
            await context.StockItems.AddAsync(stockItem.ToStockItem());
            await context.SaveChangesAsync();

            return Results.Ok();
        }
        [HttpGet("getallstockitems")]
        public async Task<List<StockItem>> GetAllStockItems()
        {
            return await context.StockItems.ToListAsync();
        }

    }
}