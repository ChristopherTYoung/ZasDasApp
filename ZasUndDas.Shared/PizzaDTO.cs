using System.Data.SqlTypes;
using System.Drawing;
using ZasUndDas.Shared.Data;
namespace ZasUndDas.Shared;

public record PizzaDTO : IStoreItem
{
    public int Id { set; get; }
    public PizzaSize Size { set; get; } //size indeed matters
    public string Name { set; get; } = "CYO";
    public double Price { set; get; } = 0d;
    public string Description { set; get; } = string.Empty;
    public List<string> Ingredients { set; get; } = new List<string>();
    public async Task<Pizza> ToPizza(PostgresContext context)
    {
        List<PizzaAddin> addins = new List<PizzaAddin>();
        Pizza pizza = new Pizza();
        foreach (var ingredient in Ingredients)
        {
            await context.PizzaAddins.AddAsync(new PizzaAddin() { _Pizza = pizza, Addin = context.PAddins.First(p => p.AddinName == ingredient) });
        }
        throw new NotImplementedException();
    }
}
public enum PizzaSize { small, medium, large }