using ZasUndDas.Shared.Data;
namespace ZasUndDas.Shared.Services;

public interface IAPIService
{
    public bool LoggedIn { get; }
    public Task Authorize(AuthRequest request);
    public Task CreateAccount(CreateRequest request);
    public Task<List<DrinkBaseDTO>> GetDrinks();
    public Task<List<PizzaBaseDTO>> GetPizzas();
    public Task<List<PAddinDTO>> GetPizzaToppings();
    public Task<List<DAddinDTO>> GetDrinkAddins();
    public Task<List<PizzaSize>> GetPizzaSizes();
    public Task<List<Sauce>> GetSauces();
    public Task Order(OrderDTO order);
    public Uri? BaseAddress { get; }
    public void LogOut();
}