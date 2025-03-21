using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ZasAndDasMobile.ViewModels
{

    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string observableObject;
    }
}
