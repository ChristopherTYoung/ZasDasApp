using System.Collections.Generic;
using System.Threading.Tasks;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

public class MockAPIService : IAPIService
{
    public Uri? BaseAddress => new("https://zasanddas-fraxcmfwaxd3hjbn.westus3-01.azurewebsites.net/");
    public async Task<List<DrinkBaseDTO>> GetDrinks()
    {
        // Simulating a predefined list of drinks
        return await Task.FromResult(new List<DrinkBaseDTO>
        {
            new DrinkBaseDTO { Id = 1, Name = "Kyle's Monster", Price = 1.99m },
            new DrinkBaseDTO { Id = 2, Name = "Badger Bite", Price = 1.89m },
            new DrinkBaseDTO { Id = 3, Name = "Sea Breeze", Price = 1.79m }
        });
    }

    public async Task<List<PizzaBaseDTO>> GetPizzas()
    {
        // Simulating a predefined list of pizzas
        return await Task.FromResult(new List<PizzaBaseDTO>
        {
            new PizzaBaseDTO { Id = 1, Name = "Margherita", Price = 8.99m },
            new PizzaBaseDTO { Id = 2, Name = "Pepperoni", Price = 9.99m },
            new PizzaBaseDTO { Id = 3, Name = "Vegetarian", Price = 9.49m },
            new PizzaBaseDTO { Id = 4, Name = "CYO", Price = 8.99m }

        });
    }

    public async Task<List<PAddinDTO>> GetPizzaToppings()
    {
        // Simulating a predefined list of pizza toppings
        return await Task.FromResult(new List<PAddinDTO>
        {
            new PAddinDTO { Id = 1, AddinName = "Olives", Price = 0.99m },
            new PAddinDTO { Id = 2, AddinName = "Mushrooms", Price = 1.29m },
            new PAddinDTO { Id = 3, AddinName = "Extra Cheese", Price = 1.49m }
        });
    }

    public async Task<List<PizzaSize>> GetPizzaSizes()
    {
        return await Task.FromResult(new List<PizzaSize>
        {
            new PizzaSize { Id = 0, SizeName = "8\" Pizza" },
            new PizzaSize { Id = 1, SizeName ="12\" Pizza" },
            new PizzaSize { Id = 2, SizeName ="16\" Pizza" }
        });
    }


    public async Task<List<Sauce>> GetSauces()
    {
        return await Task.FromResult(new List<Sauce>
        {
            new Sauce { Id = 0, SauceName = "Default" },
            new Sauce { Id = 1, SauceName = "Ranch" },
            new Sauce { Id = 2, SauceName ="Marinaria" },
            new Sauce { Id = 3, SauceName ="BBQ" }
        });
    }


    public async Task Order(OrderDTO order)
    {
        // Simulating a successful order processing
        await Task.CompletedTask;
    }

    public async Task<List<DAddinDTO>> GetDrinkAddins()
    {
        return await Task.FromResult(new List<DAddinDTO>
        {
            new DAddinDTO { Id = 0, AddinName = "Cherry", Price = 1.50m },
            new DAddinDTO { Id = 1, AddinName = "Lime", Price = 1.50m },
            new DAddinDTO { Id = 2, AddinName = "Grape", Price = 1.50m },
        });
    }
}