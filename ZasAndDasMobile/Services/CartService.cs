using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;

namespace ZasAndDasMobile.Services
{
    public class CartService
    {
        List<IStoreItem> cart = new List<IStoreItem>();

        public List<IStoreItem> GetCartItems() => cart;

        public void AddToCart(IStoreItem item)
        {
            cart.Add(item);
        }

        public IStoreItem RemoveItem(int id)
        {
            var item = cart.First(i => i.Id == id);
            cart.Remove(item);
            return item;
        }

        public double CalculateTotal()
        {
            if (cart.Count == 0)
                return 0;

            return cart.Select(p => p.Price).Sum();
        }
    }
}
