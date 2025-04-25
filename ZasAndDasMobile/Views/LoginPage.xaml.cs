using ZasAndDasMobile.ViewModels;

namespace ZasAndDasMobile;

public partial class LoginPage : ContentPage
{
    public LoginViewModel ViewModel { get; set; }

    public LoginPage(LoginViewModel context)
    {
        ViewModel = context;
        BindingContext = context;
        InitializeComponent();
    }
}