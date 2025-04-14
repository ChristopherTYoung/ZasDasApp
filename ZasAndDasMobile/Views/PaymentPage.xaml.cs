using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile;

public partial class PaymentPage : ContentPage
{
    bool loaded = false;
    CartService cart;
    Func<Task>? SendOrder;
    public PaymentPage(CartService cart, Func<Task>? SendOrder = null)
    {
        this.cart = cart;
        InitializeComponent();
        this.SendOrder = SendOrder;
    }
    public async void WebMessageReceived(object sender, WebNavigatingEventArgs e)
    {
        if (loaded)
        {
            cart.AddNonce(e.Url);
            e.Cancel = true;
            if (SendOrder != null)
                await SendOrder();
            await Navigation.PopAsync();
        }
        else
        {
            loaded = true;
        }
    }
}