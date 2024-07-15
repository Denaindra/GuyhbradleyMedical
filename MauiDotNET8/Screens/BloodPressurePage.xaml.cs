using MauiDotNET8.ViewModels;

namespace MauiDotNET8.Screens;

public partial class BloodPressurePage : ContentPage
{
	private readonly BloodPressureViewModel vm;
	public BloodPressurePage(BloodPressureViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
        BindingContext = vm;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var results = await vm.GetBloodPressureTests();
    }

    private async void AddBtnClick(object sender, EventArgs e)
    {
       
    }
}