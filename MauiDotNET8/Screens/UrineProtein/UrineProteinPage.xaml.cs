using MauiDotNET8.ViewModels.UrineProtein;
using Syncfusion.Maui.Buttons;

namespace MauiDotNET8.Screens.UrineProtein;

public partial class UrineProteinPage : ContentPage
{
	private readonly UrineProteinViewModel vm;
	public UrineProteinPage(UrineProteinViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
        BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        pullToRefresh.Refreshing += PullToRefresh_Refreshing;
        await vm.GetUrineProteinTests();
    }

    public async void AddUrineProteinButton_Clicked(System.Object sender, System.EventArgs e)
    {
        SfButton button = ((SfButton)sender);
        button.IsEnabled = false;
        var page = new AddUrineProteinTestPage();
        //await PopupNavigation.Instance.PushAsync(page);
        button.IsEnabled = true;
    }

    private async void PullToRefresh_Refreshing(object sender, EventArgs args)
    {
        pullToRefresh.IsRefreshing = true;
        var vm = ((UrineProteinViewModel)BindingContext);
        await vm.GetUrineProteinTests();
        pullToRefresh.IsRefreshing = false;
    }
}