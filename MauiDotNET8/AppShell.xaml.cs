using MauiDotNET8.Helpers;
using MauiDotNET8.ViewModels;

namespace MauiDotNET8
{
    public partial class AppShell : Shell
    {
        private UserViewModal vm;
        public AppShell()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm = ServiceHelper.GetService<UserViewModal>();
        }
        private void MenuItem_Clicked(object sender, EventArgs e)
        {

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
