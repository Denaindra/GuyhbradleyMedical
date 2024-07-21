using CommunityToolkit.Maui.Views;
using MauiDotNET8.Enumerations;
using MauiDotNET8.Screens.PopupNotify;
using MauiDotNET8.ViewModels.UrineProtein;
using System.Runtime.CompilerServices;

namespace MauiDotNET8.Screens.PopupViews;

public partial class AddUrineProteinTestPage : Popup
{
    private readonly AddUrineProteinTestViewModel vm;
    public AddUrineProteinTestPage(AddUrineProteinTestViewModel vm)
	{
		InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
        SetupPopup();
    }
    private void SetupPopup()
    {
        vm.SetPropertyValues();
        vm.AddValidations();

        ResultTime.SetBinding(TimePicker.TimeProperty, "Time", BindingMode.TwoWay, null, null);
        ResultDate.SetBinding(DatePicker.DateProperty, "Date", BindingMode.TwoWay, null, null);
        ResultDate.Format = "dd/MM/yyyy";
        proteinLevelPicker.SetBinding(Picker.ItemsSourceProperty, "ProteinLevel", BindingMode.TwoWay);
        proteinLevelPicker.ItemDisplayBinding = new Binding("Value.Name");
        proteinLevelPicker.SetBinding(Picker.SelectedItemProperty, "SelectedProteinLevel", BindingMode.TwoWay);
    }

    private void Cancel_Clicked(object sender, TappedEventArgs e)
    {
        this.CloseAsync();
    }

    private async void SaveResultClicked(object sender, EventArgs e)
    {
       vm.SaveResultsPopups(ActivityIndicator.IsRunning, SaveResultsButton);
    }
}