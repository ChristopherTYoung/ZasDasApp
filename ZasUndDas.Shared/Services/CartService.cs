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
    public class CartService(IAPIService? api = null)
    {
        ObservableCollection<CheckoutItemVM> cart = new ObservableCollection<CheckoutItemVM>();

        public ObservableCollection<CheckoutItemVM> GetCartItems => cart;
        public int GetItemCount => cart.Count;
        public event EventHandler? CartUpdated;
        public decimal TipAmount { get; set; }
        public decimal EstimatedTaxRate { get; private set; } = 0.1135m;
        public decimal EstimatedTaxes { get; set; }
        public decimal SubTotal { get; set; }
        public decimal EstimatedTotal { get; set; }

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

            var last = cart.LastOrDefault()?.Id ?? 0;
            cart.Add(new(item, last + 1));
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
                    order.Items.Add(new(item.item));
                    total += (decimal)item.Price;
                }
                order.DateOrdered = DateTime.Now;
                order.GrossAmount = total;
                order.SalesTax = total * .0775m;
                order.NetAmount = order.GrossAmount + order.SalesTax;
                if (api != null)
                    await api.Order(order);

            }
        }
        public ICheckoutItem RemoveItem(int id)
        {
            var item = cart.FirstOrDefault(i => i.Id == id) ?? throw new InvalidOperationException();
            cart.Remove(item);
            return item.item;
        }

        public decimal CalculateSubTotal()
        {
            SubTotal = Math.Round(cart.Select(p => p.Price).Sum(), 2);
            return SubTotal;
        }

        public decimal CalculateEstimatedTaxes()
        {
            EstimatedTaxes = Math.Round(SubTotal * EstimatedTaxRate, 2);
            return EstimatedTaxes;
        }

        public decimal CalculateEstimatedTotal()
        {
            EstimatedTotal = SubTotal + EstimatedTaxes + TipAmount;
            return EstimatedTotal;
        }
        private void OnCartUpdated()
        {
            CartUpdated?.Invoke(this, EventArgs.Empty);
        }
        public void ClearCart()
        {
            cart.Clear();
        }
        public string PayUrl()
        {
            if (api != null && api.BaseAddress != null)
                return api.BaseAddress.OriginalString + "/pay";
            throw new Exception();
        }
        public void SetTipAmount(decimal tip)
        {
            TipAmount = tip;
        }
    }
}