using ZasAndDasMobile.ViewModels;

namespace ZasAndDasMobile;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel context)
    {
        BindingContext = context;
        InitializeComponent();
    }
}