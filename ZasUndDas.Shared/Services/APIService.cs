using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;
namespace ZasUndDas.Shared.Services;
public class APIService : IAPIService
{
    public APIService(HttpClient client)
    {
        Client = client;
    }
    HttpClient Client { get; init; }
    public async Task<List<PizzaBaseDTO>> GetPizzas()
    {
        return await Client.GetFromJsonAsync<List<PizzaBaseDTO>>("/api/inventory/getallpizzabase") ?? new List<PizzaBaseDTO>();
    }
    public async Task<List<DrinkBaseDTO>> GetDrinks()
    {
        return await Client.GetFromJsonAsync<List<DrinkBaseDTO>>("/api/inventory/getalldrinkbase") ?? new List<DrinkBaseDTO>();

    }
    public async Task<List<PAddinDTO>> GetPizzaToppings()
    {
        return await Client.GetFromJsonAsync<List<PAddinDTO>>("/api/inventory/getpizzatoppings") ?? new List<PAddinDTO>();
    }

    public Task Order(OrderDTO order)
    {
        throw new NotImplementedException();
    }

}

public interface IAPIService
{
    public Task<List<DrinkBaseDTO>> GetDrinks();
    public Task<List<PizzaBaseDTO>> GetPizzas();
    public Task<List<PAddinDTO>> GetPizzaToppings();
    public Task Order(OrderDTO order);
}