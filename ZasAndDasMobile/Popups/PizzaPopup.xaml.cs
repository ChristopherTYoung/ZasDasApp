using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using ZasAndDasMobile.Messages;
using ZasAndDasMobile.ViewModels;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasAndDasMobile.Popups;

public partial class PizzaPopup : Popup
{
    public PizzaPopup(PizzaPopupViewModel vm)
    {
        InitializeComponent();
        vm.SetPopup(this);
        BindingContext = vm;
        this.Opened += OnPopupOpened;
        WeakReferenceMessenger.Default.Send<UpdatePopupMessage>();
    }
    private async void OnPopupOpened(object? sender, EventArgs e)
    {
        // temporarily disable Editor so it doesn't grab focus
        CommentEditor.IsEnabled = false;
        await Task.Delay(100);

        await ItemScrollView.ScrollToAsync(0, 0, animated: false);

        CommentEditor.IsEnabled = true;
    }
}