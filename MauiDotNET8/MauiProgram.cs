using Controls.UserDialogs.Maui;
using MauiDotNET8.Interface;
using MauiDotNET8.Screens;
using MauiDotNET8.Utilities;
using MauiDotNET8.Utilities.Auth;
using MauiDotNET8.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiDotNET8
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseUserDialogs(true, () =>
                {
#if ANDROID
                    var fontFamily = "OpenSans-Regular.ttf";
#else
                    var fontFamily = "OpenSans-Regular";
#endif
                    AlertConfig.DefaultMessageFontFamily = fontFamily;
                    AlertConfig.DefaultUserInterfaceStyle = UserInterfaceStyle.Dark;
                    AlertConfig.DefaultPositiveButtonTextColor = Colors.Purple;
                    ConfirmConfig.DefaultMessageFontFamily = fontFamily;
                    ActionSheetConfig.DefaultMessageFontFamily = fontFamily;
                    ToastConfig.DefaultMessageFontFamily = fontFamily;
                    SnackbarConfig.DefaultMessageFontFamily = fontFamily;
                    HudDialogConfig.DefaultMessageFontFamily = fontFamily;
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            //Views
            builder.Services.AddTransient<AboutPage>();
            builder.Services.AddTransient<AddBloodPressureTestPage>();
            builder.Services.AddTransient<BloodPressurePage>();
            builder.Services.AddTransient<FAQPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MorePage>();
            builder.Services.AddTransient<NotificationDetailPage>();

            //ViewModels
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<UserViewModal>();
            //Services
            builder.Services.AddSingleton<ILoading, Loading>();
            builder.Services.AddSingleton<IB2CAuthenticationService, B2CAuthenticationService>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
