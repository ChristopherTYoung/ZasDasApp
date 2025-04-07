using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZasUndDas.Shared;
using Microsoft.EntityFrameworkCore;
using ZasUndDas.Shared.Data;
namespace ZasAndDasWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController(PostgresContext context, ILogger<InventoryController> logger) : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello this is the inventory controller endpoint";
        }
        [HttpGet("getallpizzabase")]
        public async Task<List<PizzaBaseDTO>> GetPizza()
        {
            logger.LogInformation("Getting all pizza bases");
            return await context.PizzaBases
                .Select(
                    p => new PizzaBaseDTO(p, (double)context.PricePerItems.First(
                        x => p.BasePriceId == x.Id).Price))
                .ToListAsync();
        }
        [HttpPost("addpizzabase")]
        public async Task<IResult> MakePizza(PizzaBaseDTO pizza)
        {
            logger.LogInformation($"Adding pizza: {pizza.Name}");
            await context.PizzaBases.AddAsync(pizza.ToPizzaBase());
            await context.SaveChangesAsync();
            return Results.Ok();
        }
        [HttpPost("addbaseprice")]
        public async Task<IResult> AddBasePrice(PricePerItem item)
        {
            logger.LogInformation($"Adding base price: {item.Price}");
            await context.PricePerItems.AddAsync(item);
            await context.SaveChangesAsync();
            return Results.Ok();
        }
        [HttpGet("getallprices")]
        public async Task<List<PricePerItem>> GetAllPrices()
        {
            logger.LogInformation($"Getting all prices");
            return await context.PricePerItems.ToListAsync();
        }
        [HttpPost("addstockitem")]
        public async Task<IResult> AddStockItem(StockItemDTO stockItem)
        {
            logger.LogInformation($"Adding stock item: {stockItem.Name}");
            await context.StockItems.AddAsync(stockItem.ToStockItem());
            await context.SaveChangesAsync();

            return Results.Ok();
        }
        [HttpGet("getallstockitems")]
        public async Task<List<StockItemDTO>> GetAllStockItems()
        {
            logger.LogInformation($"Getting all stock items");
            return await context.StockItems.Select(p => new StockItemDTO(p)).ToListAsync();
        }

    }
}