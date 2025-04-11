using CommunityToolkit.Maui.Views;
using ZasUndDas.Shared;

namespace ZasAndDasMobile.Popups;

public partial class PaymentPopup : Popup
{
    CartService cart;
    bool loaded = false;
    public PaymentPopup(CartService cart)
    {
        this.cart = cart;
        InitializeComponent();
    }
    public async void WebMessageReceived(object sender, WebNavigatingEventArgs e)
    {
        if (loaded)
        {
            cart.AddNonce(e.Url);
            e.Cancel = true;
            await this.CloseAsync();

        }
        else
        {
            loaded = true;
        }
    }
}