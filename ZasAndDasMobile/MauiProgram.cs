﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ZasAndDasMobile.ViewModels;
using ZasUndDas.Shared;
namespace ZasAndDasMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton(_ => MenuItemService.TestPizzas());
            builder.Services.AddSingleton<CartService>();
            builder.Services.AddSingleton<CartViewModel>();
            builder.Services.AddSingleton<CartPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();
            return builder.Build();
        }
    }
}