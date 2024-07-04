using MauiDotNET8.Helpers;
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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            vm = ServiceHelper.GetService<UserViewModal>();
            label.Text = await vm.GetLoginedUser();
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
