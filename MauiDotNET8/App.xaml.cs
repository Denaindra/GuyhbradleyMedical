using MauiDotNET8.Screens;

namespace MauiDotNET8
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();

            MainPage = mainPage;
        }
    }
}
