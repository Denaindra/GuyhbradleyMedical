using MauiDotNET8.Screens;
using Syncfusion.Licensing;

namespace MauiDotNET8
{
    public partial class App : Application
    {
        public App(LoginPage loginPage)
        {
            InitializeComponent();
            SyncfusionLicenseProvider.RegisterLicense("Mjk1OTg0QDMxMzgyZTMyMmUzMG52bDE0Vk9wWlRseVhMdnhUM3dlVlpwYW9GM0xHV0MrY1d6VTV1bERKMmc9");
            MainPage = new NavigationPage(loginPage);
        }
    }
}
