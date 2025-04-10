using ZasAndDasMobile.ViewModels;
using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            (new Task(async () => await vm.Initialize())).RunSynchronously();
        }
    }

}