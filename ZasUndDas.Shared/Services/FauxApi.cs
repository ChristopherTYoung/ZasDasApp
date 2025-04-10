using System.Collections.Generic;
using System.Threading.Tasks;
using ZasUndDas.Shared.Data;
using ZasUndDas.Shared.Services;

public class MockAPIService : IAPIService
{
    public async Task<List<DrinkBaseDTO>> GetDrinks()
    {
        // Simulating a predefined list of drinks
        return await Task.FromResult(new List<DrinkBaseDTO>
        {
            new DrinkBaseDTO { Id = 1, DrinkName = "Coke", Price = 1.99 },
            new DrinkBaseDTO { Id = 2, DrinkName = "Pepsi", Price = 1.89 },
            new DrinkBaseDTO { Id = 3, DrinkName = "Sprite", Price = 1.79 }
        });
    }

    public async Task<List<PizzaBaseDTO>> GetPizzas()
    {
        // Simulating a predefined list of pizzas
        return await Task.FromResult(new List<PizzaBaseDTO>
        {
            new PizzaBaseDTO { Id = 1, Name = "Margherita", Price = 8.99 },
            new PizzaBaseDTO { Id = 2, Name = "Pepperoni", Price = 9.99 },
            new PizzaBaseDTO { Id = 3, Name = "Vegetarian", Price = 9.49 }
        });
    }

    public async Task<List<PAddinDTO>> GetPizzaToppings()
    {
        // Simulating a predefined list of pizza toppings
        return await Task.FromResult(new List<PAddinDTO>
        {
            new PAddinDTO { Id = 1, AddinName = "Olives", Price = 0.99 },
            new PAddinDTO { Id = 2, AddinName = "Mushrooms", Price = 1.29 },
            new PAddinDTO { Id = 3, AddinName = "Extra Cheese", Price = 1.49 }
        });
    }

    public async Task Order(OrderDTO order)
    {
        // Simulating a successful order processing
        await Task.CompletedTask;
    }
}