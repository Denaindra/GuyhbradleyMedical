using CommunityToolkit.Maui.Views;
using MauiDotNET8.Screens.PopupNotify;
using MauiDotNET8.Screens.PopupViews;
using MauiDotNET8.ViewModels.UrineProtein;
using Syncfusion.Maui.Buttons;
using System.Windows.Input;

namespace MauiDotNET8.Screens.UrineProtein;

public partial class UrineProteinPage : ContentPage
{
	private readonly UrineProteinViewModel vm;
    private readonly AddUrineProteinTestViewModel uvm;
    private ICommand alertPopup;
    private AddUrineProteinTestPage addUrineProteinTestPage;

    public UrineProteinPage(UrineProteinViewModel vm, AddUrineProteinTestViewModel uvm)
	{
		InitializeComponent();
		this.vm = vm;
        this.uvm = uvm;
        BindingContext = vm;
        alertPopup = new Command(AlertPopupBtnClick);
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
        uvm.AlertPopupCommand = alertPopup;
        addUrineProteinTestPage = new AddUrineProteinTestPage(uvm);
        this.ShowPopupAsync(addUrineProteinTestPage);

        button.IsEnabled = true;
    }
    private async void PullToRefresh_Refreshing(object sender, EventArgs args)
    {
        pullToRefresh.IsRefreshing = true;
        await vm.GetUrineProteinTests();
        pullToRefresh.IsRefreshing = false;
    }
    private void AlertPopupBtnClick(object obj)
    {
        addUrineProteinTestPage.CloseAsync();
        var alertPopup = new AlertPopup(uvm);
        alertPopup.AlertMessages.Add("Your urine protein reading is outside the normal range.");
        this.ShowPopupAsync(alertPopup);
    }

}