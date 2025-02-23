using CommunityToolkit.Maui;
using Concert.MAUI.Services;
using Concert.MAUI.ViewModels;
using Concert.MAUI.Views;
using AutoMapper;


namespace Concert.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                // Initialize the .NET MAUI Community Toolkit by adding the below line of code
                .UseMauiCommunityToolkit()
                // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Continue initializing your .NET MAUI App here
            builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();
            builder.Services.AddSingleton<IRestService, RestService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IBookingService, BookingService>();
            builder.Services.AddSingleton<IPerformanceService, PerformanceService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddSingleton<IConcertService, ConcertService>();
            builder.Services.AddTransient<HomepageViewModel>();
            builder.Services.AddTransient<Homepage>();
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<PerformancePageViewModel>();
            builder.Services.AddTransient<PerformanceDetailsPage>();
            builder.Services.AddTransient<BookingPageViewModel>();
            builder.Services.AddTransient<BookingPage>();



            return builder.Build();

        }
    }
}
