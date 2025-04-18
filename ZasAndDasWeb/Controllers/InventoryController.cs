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
            return await context.PizzaBases.ToListAsync();
        }
        [HttpGet("getalldrinkbase")]
        public async Task<List<DrinkBaseDTO>> GetAllDrink()
        {
            logger.LogInformation("Getting all Drink bases");
            return await context.DrinkBases.ToListAsync();
        }
        [HttpPost("addpizzabase")]
        public async Task<IResult> MakePizza(PizzaBaseDTO pizza)
        {
            logger.LogInformation($"Adding pizza: {pizza.Name}");
            await context.PizzaBases.AddAsync(pizza);
            await context.SaveChangesAsync();
            return Results.Ok();
        }

        [HttpPost("addstockitem")]
        public async Task<IResult> AddStockItem(StockItemDTO stockItem)
        {
            logger.LogInformation($"Adding stock item: {stockItem.Name}");
            await context.StockItems.AddAsync(stockItem);
            await context.SaveChangesAsync();

            return Results.Ok();
        }

        [HttpPost("adddrinkbase")]
        public async Task<IResult> AddDrinkBase(DrinkBaseDTO drink)
        {
            logger.LogInformation($"Adding drink base: {drink.Name}");
            await context.DrinkBases.AddAsync(drink);
            await context.SaveChangesAsync();

            return Results.Ok();
        }

        [HttpGet("getallstockitems")]
        public async Task<List<StockItemDTO>> GetAllStockItems()
        {
            logger.LogInformation($"Getting all stock items");
            return await context.StockItems.ToListAsync();
        }

        [HttpGet("getpizzatoppings")]
        public async Task<List<PAddinDTO>> GetPizzaToppings()
        {
            return await context.PAddins.ToListAsync();
        }

        [HttpGet("getdrinkaddins")]
        public async Task<List<DAddinDTO>> GetDrinkAddins()
        {
            return await context.DAddins.ToListAsync();
        }

        [HttpGet("getpizzasizes")]
        public async Task<List<PizzaSize>> GetSizes()
        {
            return await context.PizzaSizes.ToListAsync();
        }

        [HttpGet("getsauces")]
        public async Task<List<Sauce>> GetSauces()
        {
            return await context.Sauces.ToListAsync();
        }
    }
}