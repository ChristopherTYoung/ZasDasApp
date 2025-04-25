using Microsoft.Extensions.DependencyInjection;
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

            //builder.Services.AddSingleton<HttpClient>(_ => new HttpClient() { BaseAddress = new Uri("https://rzw2zfkp-7197.usw3.devtunnels.ms/") });
            builder.Services.AddSingleton<HttpClient>(_ => new HttpClient() { BaseAddress = new Uri("https://zasanddas-fraxcmfwaxd3hjbn.westus3-01.azurewebsites.net/") });

            // set this to false for the mock service
            var IsModel = true;
            if (!IsModel)
            {
                builder.Services.AddSingleton<IAPIService, MockAPIService>();

            }
            else
            {
                builder.Services.AddSingleton<IAPIService, APIService>();

            }
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<CreateAccountViewModel>();
            builder.Services.AddSingleton<CreateAccountPage>();
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