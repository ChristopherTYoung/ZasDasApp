using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile;

public partial class PaymentPage : ContentPage
{
    bool loaded = false;
    CartService cart;
    Func<Task>? SendOrder;
    public string Url => cart.PayUrl();
    public PaymentPage(CartService cart, Func<Task>? SendOrder = null)
    {
        this.cart = cart;
        InitializeComponent();
        this.SendOrder = SendOrder;
        BindingContext = this;
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