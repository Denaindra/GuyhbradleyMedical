using CommunityToolkit.Maui.Views;
using MauiDotNET8.Modals.API;
using MauiDotNET8.ViewModels;
using MauiDotNET8.ViewModels.UrineProtein;
using Microsoft.Maui.Graphics;
using System.Drawing;

namespace MauiDotNET8.Screens.PopupNotify;

public partial class AlertPopup : Popup
{
    IList<EmergencyContact> emergencyContacts = new List<EmergencyContact>();
    IList<Button> emergencyContactButtons = new List<Button>();
    private bool contactButtonClicked = false;
    public List<string> AlertMessages { get; set; } = new List<string>();
    private readonly AddBloodPressureTestViewModel vm;
    private readonly AddUrineProteinTestViewModel uvm;
    public AlertPopup(AddBloodPressureTestViewModel vm)
	{
		InitializeComponent();
        this.vm = vm;
    }

    public AlertPopup(AddUrineProteinTestViewModel uvm)
    {
        InitializeComponent();
        this.uvm = uvm;
    }
    private async Task<bool> SetupPopup()
    {

        foreach (var message in AlertMessages)
        {
            Label alertMessageLabel = new Label()
            {
                Text = message,
                TextColor = Colors.White
            };
            alertMessagesStackLayout.Children.Add(alertMessageLabel);
        };

        View loadingNumbersView = GetLoadingNumbersView();
        dialerButtonsStackLayout.Children.Add(loadingNumbersView);

        ClinicContactModal configuration;
        if (vm != null)
        {
            configuration = await vm.GetMobileServiceString();
        }
        else
        {
            configuration = await uvm.GetMobileServiceString();
        }
        

        dialerButtonsStackLayout.Children.Remove(loadingNumbersView);

        foreach (var contact in configuration.ClinicContactNumbers)
        {
            Button emergencyContactButton = new Button
            {
                Text = string.Format("{0} ({1}): {2}", contact.Name, contact.ContactType.ToString(), contact.Number),
                CommandParameter = contact.Number,
                BorderColor = Colors.DarkGray,
                BackgroundColor = Colors.White,
                BorderWidth = 1,
                CornerRadius = 5,
                TextColor = Colors.Black
            };
            emergencyContactButton.Clicked += EmergencyContactButton_Clicked;
            dialerButtonsStackLayout.Children.Add(emergencyContactButton);
        }

        return true;
    }

    private View GetLoadingNumbersView()
    {
        ActivityIndicator loadingNumbersActivityIndicator = new ActivityIndicator
        {
            IsRunning = true,
            Color = Colors.White
        };

        Label loadingNumbersLabel = new Label
        {
            TextColor = Colors.White,
            FontAttributes = FontAttributes.Bold,
            Text = "Loading contact numbers..."
        };

        StackLayout loadingNumbersStack = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Center
        };

        loadingNumbersStack.Children.Add(loadingNumbersActivityIndicator);
        loadingNumbersStack.Children.Add(loadingNumbersLabel);

        return loadingNumbersStack;
    }

    private void EmergencyContactButton_Clicked(object sender, EventArgs e)
    {
        contactButtonClicked = true;
        Button button = (Button)sender;
        PlacePhoneCall(button.CommandParameter.ToString());
    }

    public void PlacePhoneCall(string number)
    {
        try
        {
            PhoneDialer.Open(number);
        }
        catch (ArgumentNullException anEx)
        {
            // Number was null or white space
        }
        catch (FeatureNotSupportedException ex)
        {
            // Phone Dialer is not supported on this device.
        }
        catch (Exception ex)
        {
            // Other error has occurred.
        }
    }

    async void CloseBtnClicked(System.Object sender, System.EventArgs e)
    {
        bool closeAlert = false;
        if (!contactButtonClicked)
        {
            closeAlert = await App.Current.MainPage.DisplayAlert("Confirmation Required", "Are you sure you wish to close this alert?", "Yes", "No");
        }
        else
        {
            closeAlert = true;
        }
        if (closeAlert) await this.CloseAsync();
    }

    private async void PopupOpened(object sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
    {
        await SetupPopup();
    }
}
public class EmergencyContact
{
    public ContactType ContactType { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}

public enum ContactType
{
    Doctor,
    Midwife
}