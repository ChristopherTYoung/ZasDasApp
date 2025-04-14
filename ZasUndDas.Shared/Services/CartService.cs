using CommunityToolkit.Mvvm.ComponentModel;
using Square;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared.Data;

namespace ZasUndDas.Shared.Services
{
    public class CartService(IAPIService api)
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
            var price = item.Price;
            if (price < 0)
                throw new InvalidOperationException();
            cart.Add(item);
            OnCartUpdated();
        }
        public async Task SendOrder()
        {
            if (nonce != null)
            {
                var order = new OrderDTO() { Nonce = nonce };
                decimal total = 0;
                foreach (var item in cart)
                {
                    order.Items.Add(new(item));
                    total += (decimal)item.Price;
                }
                order.DateOrdered = DateTime.Now;
                order.GrossAmount = total;
                order.SalesTax = total * .0775m;
                order.NetAmount = order.GrossAmount + order.NetAmount;
                await api.Order(order);

            }
        }
        public ICheckoutItem RemoveItem(int id)
        {
            var item = cart.First(i => i.Id == id);
            cart.Remove(item);
            return item;
        }

        public decimal CalculateTotal()
        {
            return Math.Round(cart.Select(p => p.Price).Sum(), 2);
        }
        private void OnCartUpdated()
        {
            CartUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}