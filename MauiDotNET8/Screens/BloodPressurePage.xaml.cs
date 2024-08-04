using CommunityToolkit.Maui.Views;
using MauiDotNET8.Enumerations;
using MauiDotNET8.Modals.API;
using MauiDotNET8.Screens.PopupNotify;
using MauiDotNET8.Screens.PopupViews;
using MauiDotNET8.ViewModels;
using Syncfusion.Maui.Charts;
using System.Windows.Input;

namespace MauiDotNET8.Screens;

public partial class BloodPressurePage : ContentPage
{
	private readonly BloodPressureViewModel vm;
    private readonly AddBloodPressureTestViewModel pressureTestViewModel;
    public ICommand alertPopup;
    public BloodPressurePage(BloodPressureViewModel vm, AddBloodPressureTestViewModel pressureTestViewModel)
	{
		InitializeComponent();
		this.vm = vm;
        this.pressureTestViewModel = pressureTestViewModel;
        BindingContext = vm;
        alertPopup = new Command(AlertPopupBtnClick);

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.GetBloodPressureTests();
    }

    private async void AddBtnClick(object sender, EventArgs e)
    {
        pressureTestViewModel.AlertPopupCommand = alertPopup;
        var popup = new AddBloodPressureTestPage(pressureTestViewModel,vm);
        await this.ShowPopupAsync(popup);
    }

    private async void AlertPopupBtnClick()
    {
        var alertPopup = new AlertPopup(pressureTestViewModel);
        if (pressureTestViewModel.IsAlert)
        {
            foreach (TestResponse testResponse in pressureTestViewModel.TestResponses)
            {
                if (testResponse.TestResponseLevel == TestResponseLevel.Alert)
                {
                    switch (testResponse.TestResponseType)
                    {
                        case TestResponseType.BloodPressureDiastolic:
                            alertPopup.AlertMessages.Add("Your diastolic blood pressure reading is outside the normal range.");
                            break;
                        case TestResponseType.BloodPressureSystolic:
                            alertPopup.AlertMessages.Add("Your systolic blood pressure reading is outside the normal range.");
                            break;
                        case TestResponseType.BloodPressureHeadaches:
                            alertPopup.AlertMessages.Add("Your persistent headache is not normal.");
                            break;
                        case TestResponseType.BloodPressureAbdominalPain:
                            alertPopup.AlertMessages.Add("Your persistent abdominal pain is not normal.");
                            break;
                        case TestResponseType.BloodPressureBlurredVision:
                            alertPopup.AlertMessages.Add("Your persistent blurred vision is not normal.");
                            break;
                        default:
                            break;
                    }

                }
            }
        }
        await this.ShowPopupAsync(alertPopup);   
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