using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;

namespace ZasUndDas.Shared
{
    public class MenuItemService
    {
        List<IStoreItem> menuItems = new List<IStoreItem>();
        public List<Pizza> GetAllPizzas()
        {
            return menuItems.Where(i => i.GetType() == typeof(Pizza))
                            .Select(p => (Pizza)p)
                            .ToList();
        }

        public MenuItemService AddItemToMenu(IStoreItem item)
        {
            menuItems.Add(item);
            return this;
        }

        public List<Drink> GetAllDrinks()
        {
            return menuItems.Where(i => i.GetType() == typeof(Drink))
                            .Select(p => (Drink)p)
                            .ToList();
        }

        public static MenuItemService TestPizzas()
        {
            return (new MenuItemService()).AddItemToMenu(new Pizza() { Name = "Pizza1", Description = "This is a pizza", Price = 4.99 })
                                       .AddItemToMenu(new Pizza() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new Pizza() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new Pizza() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new Pizza() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new Pizza() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new Pizza() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new Pizza() { Name = "Pizza2", Description = "This is also a pizza", Price = 5.99 })
                                       .AddItemToMenu(new Pizza() { Name = "Pizza3", Description = "This is not a pizza", Price = 6.99 });
        }


    }
}