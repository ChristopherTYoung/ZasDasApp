using ZasUndDas.Shared.Services;
using ZasAndDasMobile.ViewModels;
namespace ZasAndDasMobile;

public partial class CreateAccountPage : ContentPage
{
    public CreateAccountPage(IAPIService service)
    {
        BindingContext = new CreateAccountViewModel(service);
        InitializeComponent();
    }
}