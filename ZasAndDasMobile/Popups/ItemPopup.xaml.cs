using CommunityToolkit.Maui.Views;
using ZasAndDasMobile.ViewModels;
using ZasUndDas.Shared;
using ZasUndDas.Shared.Data;

namespace ZasAndDasMobile.Popups;

public partial class ItemPopup : Popup
{
    public ItemPopup(ItemViewModel<PizzaDTO> vm)
    {
        InitializeComponent();
        vm.SetPopup(this);
        BindingContext = vm;

        this.Opened += OnPopupOpened;
    }
    private async void OnPopupOpened(object? sender, EventArgs e)
    {
        // Optional: temporarily disable Editor so it doesn't grab focus
        CommentEditor.IsEnabled = false;

        await Task.Delay(100); // Let layout settle

        await ItemScrollView.ScrollToAsync(0, 0, animated: false);

        CommentEditor.IsEnabled = true;
    }
}