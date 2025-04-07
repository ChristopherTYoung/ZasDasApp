using CommunityToolkit.Maui.Views;
using ZasAndDasMobile.ViewModels;
using ZasUndDas.Shared;

namespace ZasAndDasMobile.Popups;

public partial class ItemPopup : Popup
{
    public ItemPopup(ItemViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}