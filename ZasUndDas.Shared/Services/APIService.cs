using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;
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
            .Concat(await Client.GetFromJsonAsync<IEnumerable<IStoreItem>>("/api/inventory/getallpizzabase") ?? new List<IStoreItem>())
            .Concat(await Client.GetFromJsonAsync<IEnumerable<IStoreItem>>("/api/inventory/getallstockitems") ?? new List<IStoreItem>())
            .ToList();
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
    public Task Order(OrderDTO order);
}