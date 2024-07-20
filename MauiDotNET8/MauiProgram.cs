using CommunityToolkit.Maui;
using Controls.UserDialogs.Maui;
using MauiDotNET8.Effects;
using MauiDotNET8.Interface;
using MauiDotNET8.Screens;
using MauiDotNET8.Screens.PopupNotify;
using MauiDotNET8.Screens.PopupViews;
using MauiDotNET8.Screens.UrineProtein;
using MauiDotNET8.Utilities;
using MauiDotNET8.Utilities.Auth;
using MauiDotNET8.ViewModels;
using MauiDotNET8.ViewModels.UrineProtein;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace MauiDotNET8
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                   .ConfigureMauiHandlers((handlers) =>
                   {
#if ANDROID
                      handlers.AddHandler(typeof(EntryLineColorEffect), typeof(MauiDotNET8.Platforms.Android.Effects.EntryLineColorEffect));

                     //  handlers.AddHandler(typeof(EntryLineColorEffect), typeof(MauiDotNET8));
#endif
                   })
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
                }).ConfigureSyncfusionCore();
            //Views
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<AboutPage>();
            builder.Services.AddTransient<BloodPressurePage>();
            builder.Services.AddTransient<UrineProteinPage>();
            builder.Services.AddTransient<FAQPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MorePage>();
            builder.Services.AddTransient<NotificationDetailPage>();

            //ViewModels
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<FlyoutHeaderViewModel>();
            builder.Services.AddTransient<UserViewModal>();
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<BloodPressureViewModel>();
            builder.Services.AddTransient<AddBloodPressureTestViewModel>();
            builder.Services.AddTransient<UrineProteinViewModel>();

            //Services
            builder.Services.AddSingleton<ILoading, Loading>();
            builder.Services.AddSingleton<IMediaPickerService, MediaPickerService>();
            builder.Services.AddSingleton<IB2CAuthenticationService, B2CAuthenticationService>();
            builder.Services.AddSingleton<IAzureCloudStorageUtility, AzureCloudStorageUtility>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
