using MauiDotNET8.CustomViews.Controls;
using MauiDotNET8.Screens;
using Syncfusion.Licensing;

namespace MauiDotNET8
{
    public partial class App : Application
    {
        public App(LoginPage loginPage)
        {
            InitializeComponent();
            InititateAllCutomUICmsponets();
            SyncfusionLicenseProvider.RegisterLicense("Mjk1OTg0QDMxMzgyZTMyMmUzMG52bDE0Vk9wWlRseVhMdnhUM3dlVlpwYW9GM0xHV0MrY1d6VTV1bERKMmc9");
            MainPage = new NavigationPage(loginPage);
        }

        private void InititateAllCutomUICmsponets()
        {
            //            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            //            {
            //#if __ANDROID__
            //            if (view is NoUnderlineEntry)
            //            {
            //                handler.PlatformView.Background = null;
            //            }
            //#endif

            //#if IOS
            //                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
            //#endif
            //            });


            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
              if (view is NoUnderlineEntry)
              {
                 handler.PlatformView.Background = null;
              } 
              else if (view is BlackUnderlineEntry)
              {
               handler.PlatformView.Background.SetColorFilter(Android.Graphics.Color.Black,Android.Graphics.PorterDuff.Mode.SrcAtop);
              }
              else if (view is BlackUnderlineEntry)
              {
               handler.PlatformView.Background.SetColorFilter(Android.Graphics.Color.Black,Android.Graphics.PorterDuff.Mode.SrcAtop);
              }
                
#endif
#if IOS
                handler.PlatformView.TintColor = UIKit.UIColor.Black;
#endif
            });
        }
    }
}
