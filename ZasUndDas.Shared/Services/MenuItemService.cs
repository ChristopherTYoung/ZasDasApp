using ZasUndDas.Shared.Data;

namespace ZasUndDas.Shared.Services
{
    public class MenuItemService
    {
        public Task? ToSync;
        public MenuItemService()
        {
            Update = () => { };
        }
        public Action Update;
        List<IStoreItem> menuItems = new List<IStoreItem>();
        public List<PizzaBaseDTO> GetAllPizzas()
        {
            return menuItems.Where(i => i.GetType() == typeof(PizzaBaseDTO))
                            .Select(p => (PizzaBaseDTO)p)
                            .ToList();
        }

        public MenuItemService AddItemToMenu(IStoreItem item)
        {
            menuItems.Add(item);
            return this;
        }
        public void UpdateMenu(IEnumerable<IStoreItem> storeItems)
        {
            menuItems = storeItems.ToList();
            Update.Invoke();
        }
        public List<DrinkDTO> GetAllDrinks()
        {
            return menuItems.Where(i => i.GetType() == typeof(DrinkDTO))
                            .Select(p => (DrinkDTO)p)
                            .ToList();
        }
        public async void ForceSync()
        {
            if (ToSync != null)
                await ToSync;
        }
        public static MenuItemService TestPizzas()
        {
            return new MenuItemService().AddItemToMenu(new PizzaDTO() { Name = "Pizza1", Description = "This is a pizza", Price = 4.99 })
                                       .AddItemToMenu(new PizzaDTO() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new PizzaDTO() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new PizzaDTO() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new PizzaDTO() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new PizzaDTO() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new PizzaDTO() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new PizzaDTO() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new PizzaDTO() { Name = "Pizza3", Description = "This is not a pizza", Price = 6.99 });
        }


    }
}