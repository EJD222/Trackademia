using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls; // For TextBox
using Microsoft.UI.Xaml.Media; // For SolidColorBrush
using Microsoft.UI.Xaml; // For DependencyProperty

namespace Trackademia
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
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Semibold.ttf", "PoppinsSemiBold");
                    fonts.AddFont("Poppins-Bold.ttf", "Bold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Extend EntryHandler.Mapper for platform-specific customizations
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
                // Remove background color on Android
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif

#if WINDOWS
                if (handler.PlatformView is TextBox platformView)
                {
                    // Use explicit namespaces to resolve ambiguity
                    platformView.Resources["TextControlFocusVisualPrimaryBrush"] =
                        new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);

                    platformView.Resources["TextControlFocusVisualSecondaryBrush"] =
                        new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);
                }
#endif
            });

            return builder.Build();
        }
    }
}
