﻿using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ZasAndDasMobile.ViewModels;
using ZasUndDas.Shared.Services;
using Microsoft.Maui.Handlers;
using ZasAndDasMobile.Popups;
#if ANDROID
using Android.Graphics.Drawables;
#endif

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

            PickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
#if ANDROID

                handler.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

            EditorHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
#if ANDROID

                handler.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<HttpClient>(_ => new HttpClient() { BaseAddress = new Uri("https://rzw2zfkp-7197.usw3.devtunnels.ms/") });
            // I just set this here because android app can't read the env variables and I spent like an hour trying to get it to work
            // The Window Design works but only for the Windows App and not for the Phone app
            // Enjoy my rant. I have no clue why env variables are so difficult on mobile. Or I'm just dumb -Logan
            var IsModel = false;
            //var IsModel = (Environment.GetEnvironmentVariable("ISNT_MODEL") ?? "True") == "True";
            if (!IsModel)
            {
                builder.Services.AddSingleton<IAPIService, MockAPIService>();

            }
            else
            {
                builder.Services.AddSingleton<IAPIService, APIService>();

            }
            builder.Services.AddScoped<LoginViewModel>();
            builder.Services.AddScoped<LoginPage>();
            builder.Services.AddSingleton<MenuItemService>();
            builder.Services.AddSingleton<CartService>();
            builder.Services.AddSingleton<CartViewModel>();
            builder.Services.AddSingleton<CartPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<DrinkPopup>();
            builder.Services.AddTransient<PizzaPopupViewModel>();
            builder.Services.AddScoped<PaymentPage>();
            return builder.Build();
        }
    }
}