using ZasUndDas.Shared;

namespace ZasAndDasMobile;

public partial class PaymentPage : ContentPage
{
    CartService cart;
    public PaymentPage(CartService cart)
    {
        this.cart = cart;
        InitializeComponent();
    }
    public async void WebMessageReceived(object sender, WebNavigatingEventArgs e)
    {
        cart.AddNonce(e.Url);
        e.Cancel = true;
        await Shell.Current.GoToAsync("///MainPage");
    }
}