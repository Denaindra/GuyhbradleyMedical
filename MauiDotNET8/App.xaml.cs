using MauiDotNET8.Screens;

namespace MauiDotNET8
{
    public partial class App : Application
    {
        public App(LoginPage loginPage)
        {
            InitializeComponent();

            MainPage = loginPage;
        }
    }
}
