using MauiDotNET8.ViewModels;

namespace MauiDotNET8.Screens;

public partial class LoginPage : ContentPage
{
	private UserViewModal vm;
	public LoginPage(UserViewModal userViewModal)
	{
		InitializeComponent();
		this.vm = userViewModal;
		userViewModal.navigation = Navigation;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.BuildTheApplicationbundle();
    }
    private  void OnSignInButtonClicked(object sender, EventArgs e)
    {
       vm.LoginSigin();
    }
}