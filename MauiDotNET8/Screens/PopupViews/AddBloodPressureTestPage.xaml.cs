using CommunityToolkit.Maui.Views;
using MauiDotNET8.ViewModels;

namespace MauiDotNET8.Screens.PopupViews;

public partial class AddBloodPressureTestPage : Popup
{
    private readonly AddBloodPressureTestViewModel vm;
    private readonly BloodPressureViewModel vm1;
    public AddBloodPressureTestPage(AddBloodPressureTestViewModel vm, BloodPressureViewModel vm1)
	{
        InitializeComponent();
        this.vm = vm;
        this.vm1 = vm1;
        BindingContext = vm;
        SetupUI();
    }
    private void SetupUI()
    {
        ResultTime.SetBinding(TimePicker.TimeProperty, "Time", BindingMode.TwoWay, null, null);
        ResultDate.SetBinding(DatePicker.DateProperty, "Date", BindingMode.TwoWay, null, null);
        ResultDate.Format = "dd/MM/yyyy";
        HeadachePicker.SetBinding(Picker.ItemsSourceProperty, "Headaches", BindingMode.TwoWay);
        HeadachePicker.ItemDisplayBinding = new Binding("Name");
        HeadachePicker.SetBinding(Picker.SelectedItemProperty, "SelectedHeadache");
        BlurredVisionPicker.SetBinding(Picker.ItemsSourceProperty, "BlurredVision", BindingMode.TwoWay);
        BlurredVisionPicker.ItemDisplayBinding = new Binding("Name");
        BlurredVisionPicker.SetBinding(Picker.SelectedItemProperty, "SelectedBlurredVision");
        AbdominalPainsPicker.SetBinding(Picker.ItemsSourceProperty, "AbdominalPains", BindingMode.TwoWay);
        AbdominalPainsPicker.ItemDisplayBinding = new Binding("Name");
        AbdominalPainsPicker.SetBinding(Picker.SelectedItemProperty, "SelectedAbdominalPains");
    }
    private async void CancleTapped(object sender, TappedEventArgs e)
    {
        var results =  await vm1.GetBloodPressureTests();
        if (results)
        {
            this.Close();
        }
    }

    private void EntryTextChanged(object sender, TextChangedEventArgs e)
    {
        Entry entry = sender as Entry;
        String val = entry.Text;

        //is the current text numeric?
        int outInt;
        var isNumeric = int.TryParse(val, out outInt);

        if (!string.IsNullOrWhiteSpace(val) && (val.Length > 3 || !isNumeric))
        {
            val = val.Remove(val.Length - 1);
            entry.Text = val;
        }
    }

    private async void SaveResultsButtonClicked(object sender, EventArgs e)
    {
        bool answer = await App.Current.MainPage.DisplayAlert("Confirmation Required", GetConfirmationAlertText(), "Yes", "No");
        if (answer)
        {
            var isDataSave = await vm.SaveDetails(ActivityIndicator, SaveResultsButton);
            if (isDataSave)
            {
                this.Close();
                vm.AlertPopupCommand.Execute(null);
               
            }
        }
    }
    private string GetConfirmationAlertText()
    {
        var viewModel = (AddBloodPressureTestViewModel)BindingContext;
        var text = "Please confirm you wish to save the following blood pressure test details:" + Environment.NewLine +
            Environment.NewLine +
            "Top Reading (Systolic): " + viewModel.Systolic.Value + Environment.NewLine +
            "Bottom Reading (Diastolic): " + viewModel.Diastolic.Value + Environment.NewLine;
        if (viewModel.SelectedHeadache != null) text += "Headaches: " +
                viewModel.SelectedHeadache.Name + Environment.NewLine;
        if (viewModel.SelectedBlurredVision != null) text += "Blurred Vision: " +
                viewModel.SelectedBlurredVision.Name + Environment.NewLine;
        if (viewModel.SelectedAbdominalPains != null) text += "Andominal Pains: " +
                viewModel.SelectedAbdominalPains.Name + Environment.NewLine;

        return text;
    }
}