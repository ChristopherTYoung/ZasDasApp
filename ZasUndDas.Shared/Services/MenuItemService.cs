using ZasUndDas.Shared.Data;

namespace ZasUndDas.Shared.Services
{
    public class MenuItemService(IAPIService api)
    {
        List<PizzaBaseDTO>? Pizzas;
        List<DrinkBaseDTO>? Drinks;
        List<PAddin> pAddins = new List<PAddin>();
        public async Task<List<PizzaBaseDTO>> GetAllPizzas()
        {
            if (Pizzas == null)
            {
                Pizzas = await api.GetPizzas();
            }
            return Pizzas;
        }
        public async Task<List<DrinkBaseDTO>> GetAllDrinks()
        {
            if (Drinks == null)
            {
                Drinks = await api.GetDrinks();
            }
            return Drinks;
        }
        public static MenuItemService TestPizzas()
        {
            return (new MenuItemService(new MockAPIService()));
        }
    }
}