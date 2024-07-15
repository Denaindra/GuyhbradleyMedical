using MauiDotNET8.ViewModels;
using Syncfusion.Maui.Charts;

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
        await vm.GetBloodPressureTests();
    }

    private async void AddBtnClick(object sender, EventArgs e)
    {
       
    }

    private async void PullToRefreshRefreshing(object sender, EventArgs e)
    {
        pullToRefresh.IsRefreshing = true;
        await vm.GetBloodPressureTests();
        pullToRefresh.IsRefreshing = false;
    }

    private void BloodPressureListViewSelectionChanging(object sender, Syncfusion.Maui.ListView.ItemSelectionChangingEventArgs e)
    {
       // var index = (bloodPressureListView.ItemsSource as IList<BloodPressureTestAndResponse>).IndexOf(e.AddedItems[0] as BloodPressureTestAndResponse);
       // splineSeries.SelectedDataPointIndex = index;
    }
}