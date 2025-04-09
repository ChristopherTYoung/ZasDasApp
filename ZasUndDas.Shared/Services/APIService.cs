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
    public async Task<List<IStoreItem>> GetItems()
    {
        var result = new List<IStoreItem>();
        result = result
            .Concat(await Client.GetFromJsonAsync<IEnumerable<PizzaBaseDTO>>("/api/inventory/getallpizzabase") ?? new List<PizzaBaseDTO>())
            .Concat(await Client.GetFromJsonAsync<IEnumerable<StockItemDTO>>("/api/inventory/getallstockitems") ?? new List<StockItemDTO>())
            .ToList();
        return result;
    }

    public async Task<List<PAddin>> GetPizzaToppings()
    {
        var result = new List<PAddin>();
        result = await Client.GetFromJsonAsync<List<PAddin>>("/api/inventory/getpizzatoppings");
        return result;
    }

    public Task Order(OrderDTO order)
    {
        throw new NotImplementedException();
    }
}

public interface IAPIService
{
    public Task<List<IStoreItem>> GetItems();
    public Task<List<PAddin>> GetPizzaToppings();
    public Task Order(OrderDTO order);
}