namespace Concert.MAUI.Services
{
    public class HttpsClientHandlerService : IHttpsClientHandlerService
    {
        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
#if NET6_0
                var handler = new CustomAndroidMessageHandler();
#elif NET7_0_OR_GREATER
            var handler = new Xamarin.Android.Net.AndroidMessageHandler();
#endif
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
#elif IOS
            var handler = new NSUrlSessionHandler
            {
                TrustOverrideForUrl = IsHttpsLocalhost
            };
            return handler;
#elif WINDOWS || MACCATALYST
            return null!;
#else
                throw new PlatformNotSupportedException("Supported: Android, iOS, MacCatalyst, Windows.");
#endif
        }

#if IOS
        public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
        {
            return url.StartsWith("https://localhost");
        }
#endif
    }
}
