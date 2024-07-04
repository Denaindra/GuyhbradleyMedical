namespace MauiDotNET8.Screens;

public partial class FAQPage : ContentPage
{
	public FAQPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        FAQWebview.Source = "https://www.trakkaclinical.com/faq.html";
    }
}