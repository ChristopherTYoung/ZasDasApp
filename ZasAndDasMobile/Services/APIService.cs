using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;
namespace ZasAndDasMobile.Service;
public class APIService : IAPIService
{
    public APIService(HttpClient client)
    {
        Client = client;
    }
    HttpClient Client { get; init; }
    public List<IStoreItem> GetItems()
    {
        throw new NotImplementedException();
    }

    public Task Order(OrderDTO order)
    {
        throw new NotImplementedException();
    }
}

public interface IAPIService
{
    public List<IStoreItem> GetItems();
    public Task Order(OrderDTO order);
}