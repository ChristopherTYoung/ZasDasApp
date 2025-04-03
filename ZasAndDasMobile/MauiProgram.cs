using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZasAndDasMobile.ViewModels;
using ZasUndDas.Shared.Services;
namespace ZasAndDasMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton(_ => MenuItemService.TestPizzas());
            builder.Services.AddSingleton<HttpClient>(_ => new HttpClient() { BaseAddress = new Uri("http://localhost:5257/") });
            builder.Services.AddSingleton<IAPIService, APIService>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<SyncingService>();
            return builder.Build();
        }
    }
}