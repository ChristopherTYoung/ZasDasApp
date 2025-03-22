using ZasAndDasMobile.ViewModels;

namespace ZasAndDasMobile.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}
