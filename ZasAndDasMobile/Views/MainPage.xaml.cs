using ZasAndDasMobile.ViewModels;
using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm, SyncingService ss)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}