﻿using ConsertBookingApp.MAUI;
using Foundation;

namespace ConcertBookingApp.MAUI
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
