using ZasAndDasMobile.ViewModels;
using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm, ISyncingService ss)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}