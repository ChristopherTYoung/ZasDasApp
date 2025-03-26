using ZasAndDasMobile.ViewModels;

namespace ZasAndDasMobile
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