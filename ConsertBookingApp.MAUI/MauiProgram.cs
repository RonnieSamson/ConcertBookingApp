using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui;
using ConcertBookingApp.MAUI.Services;
using ConcertBookingApp.MAUI.ViewModel;
using ConcertBookingApp.MAUI.View;
using ConcertBookingApp.MAUI.Profiles;
using ConcertBookingApp.MAUI;

namespace ConsertBookingApp.MAUI
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

            builder.Services.AddSingleton<IRestService, RestService>();
            builder.Services.AddSingleton<IConcertService, ConcertService>();
            builder.Services.AddSingleton<ConcertViewModel>();
            builder.Services.AddSingleton<ConcertPage>();
            builder.Services.AddAutoMapper(typeof(ConcertProfile));





#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
