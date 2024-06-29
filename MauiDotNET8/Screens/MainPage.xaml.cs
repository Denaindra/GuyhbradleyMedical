using MauiDotNET8.ViewModels;

namespace MauiDotNET8.Screens;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel viewModels;

    public MainPage(MainPageViewModel viewModels)
	{
		InitializeComponent();
        this.viewModels = viewModels;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        viewModels.StartLoading();
        await Task.Delay(3000);
        viewModels.EndLoading();
    }
}