using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;

namespace ZasUndDas.Shared
{
    public class CartService
    {
        List<IStoreItem> cart = new List<IStoreItem>();

        public List<IStoreItem> GetCartItems() => cart;

        public void AddToCart(IStoreItem item)
        {
            if (item.Price < 0)
                throw new InvalidOperationException();

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
            return Math.Round(cart.Select(p => p.Price).Sum(), 2);
        }
    }
}