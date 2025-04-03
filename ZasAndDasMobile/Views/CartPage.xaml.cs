using ZasAndDasMobile.ViewModels;
namespace ZasAndDasMobile;

public partial class CartPage : ContentPage
{
    public CartPage(CartViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}