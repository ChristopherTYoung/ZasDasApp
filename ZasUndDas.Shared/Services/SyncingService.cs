using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasUndDas.Shared.Services;

public class FauxSyncingService : ISyncingService
{


    public Task Sync()
    {
        return Task.CompletedTask;
    }
}

public interface ISyncingService
{
    Task Sync();
}

public class SyncingService : ISyncingService
{
    MenuItemService menuservice { set; get; }
    IAPIService apiservice { set; get; }

    public SyncingService(MenuItemService menuservice, IAPIService apiservice)
    {
        this.menuservice = menuservice;
        this.apiservice = apiservice;
        this.menuservice.ToSync = Sync();
        (new Thread(Initialize)).Start();
    }
    public async Task Sync()
    {
        menuservice.UpdateMenu(await apiservice.GetItems());
    }
    public async void Initialize()
    {
        await Sync();
        var x = new Thread(thread);
        x.Start();
    }
    async void thread()
    {
        while (true)
        {
            await Sync();
            Thread.Sleep(3600000);
        }
    }
}