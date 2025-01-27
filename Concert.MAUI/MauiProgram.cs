using Concert.MAUI.Services;
using Microsoft.Extensions.Logging;

namespace Concert.MAUI
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

            builder.Services.AddSingleton<HttpClient>(sp =>
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("http://192.168.0.30:5134/") // Bas-URL för ditt API
                };
                return client;
            });

            builder.Services.AddSingleton<IRestService, RestService>();
            builder.Services.AddSingleton<ConcertService>();
            builder.Services.AddAutoMapper(typeof(ConcertService));


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
