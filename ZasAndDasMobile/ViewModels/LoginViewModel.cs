using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZasAndDasMobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial string User { set; get; }
        [ObservableProperty]
        public partial string Password { set; get; }
        [ObservableProperty]
        public partial bool IsValid { set; get; }

        [ObservableProperty]
        public partial string? InvalidMessage { set; get; }
    }
}
