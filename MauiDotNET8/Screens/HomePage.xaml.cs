using MauiDotNET8.ViewModels;

namespace MauiDotNET8.Screens;

public partial class HomePage : ContentPage
{
	private readonly HomePageViewModel vm;
	public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
		this.vm = viewModel;
		BindingContext = this.vm;
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
         await vm.GetClinicLogo();
    }

    private async void BloodPressureBtnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//bloodpressure");
    }

    private async void UrineProteinBtnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//urineprotein");
    }
}