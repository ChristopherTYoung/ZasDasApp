using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        IAPIService API;
        public LoginViewModel(IAPIService API)
        {
            this.API = API;
            LoggedOut = !API.LoggedIn;
            LoggedIn = !LoggedOut;
            User = string.Empty;
            Password = string.Empty;
        }
        [ObservableProperty]
        public partial string User { set; get; }
        [ObservableProperty]
        public partial string Password { set; get; }
        [ObservableProperty]
        public partial bool IsValid { set; get; }

        [ObservableProperty]
        public partial string? InvalidMessage { set; get; }
        [ObservableProperty]
        public partial bool LoggedOut { set; get; }
        [ObservableProperty]

        public partial bool LoggedIn { set; get; }
        partial void OnLoggedOutChanged(bool value)
        {
            LoggedIn = !LoggedOut;
        }

        [RelayCommand]
        public async Task Login()
        {

            try
            {
                await API.Authorize(new() { Email = User ?? "", PassCode = Password ?? "" });
                User = User!.Split("@")[0];
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains(":"))
                {
                    IsValid = true;
                    InvalidMessage = ex.Message;
                }
            }
        }
    }
}