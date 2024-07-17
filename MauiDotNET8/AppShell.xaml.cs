using MauiDotNET8.Helpers;
using MauiDotNET8.Screens;
using MauiDotNET8.Screens.PopupViews;
using MauiDotNET8.Screens.UrineProtein;
using MauiDotNET8.ViewModels;

namespace MauiDotNET8
{
    public partial class AppShell : Shell
    {
        private UserViewModal vm;
        private Label label;
        public AppShell()
        {
            InitializeComponent();
            label = (Label)profilerPage.FindByName("patientNameLabel");
            Routing.RegisterRoute("addbloodpressure", typeof(AddBloodPressureTestPage));
            Routing.RegisterRoute("addurineprotein", typeof(AddUrineProteinTestPage));
            Routing.RegisterRoute("home", typeof(HomePage));
            Routing.RegisterRoute("faq", typeof(FAQPage));
            MainFlyout.IsEnabled = true;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            vm = ServiceHelper.GetService<UserViewModal>();
            vm.navigation = Navigation;
            label.Text = await vm.GetLoginedUser();
        }
        private void FaqItemClicked(object sender, EventArgs e)
        {
           vm.NavigateToFAQPage();
        }

        private async void MenuItemLogoutClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation Required", "Are you sure you wish to sign out?", "Yes", "No");
            if (answer)
            {
                try
                {
                    vm.LogoutSignOut();
                    vm.NavigateBackToLoginPage();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
