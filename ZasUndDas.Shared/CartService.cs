using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared;

namespace ZasUndDas.Shared
{
    public class CartService
    {
        ObservableCollection<ICheckoutItem> cart = new ObservableCollection<ICheckoutItem>();

        public ObservableCollection<ICheckoutItem> GetCartItems => cart;
        public int GetItemCount => cart.Count;
        public event EventHandler? CartUpdated;
        string? nonce;
        public bool IsNonce { get => nonce != null; }
        public void AddNonce(string nonce)
        {
            this.nonce = nonce;
        }
        public void AddToCart(ICheckoutItem item)
        {
            var price = item.GetPrice();
            if (price < 0)
                throw new InvalidOperationException();
            cart.Add(item);
            OnCartUpdated();
        }

        public ICheckoutItem RemoveItem(int id)
        {
            var item = cart.First(i => i.Id == id);
            cart.Remove(item);
            return item;
        }

        public double CalculateTotal()
        {
            return Math.Round(cart.Select(p => p.GetPrice()).Sum(), 2);
        }
        private void OnCartUpdated()
        {
            CartUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}