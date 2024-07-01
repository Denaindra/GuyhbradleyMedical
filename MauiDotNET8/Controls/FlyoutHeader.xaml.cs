namespace MauiDotNET8.Controls;

public partial class FlyoutHeader : ContentView
{
	public FlyoutHeader()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizerTapped(object sender, TappedEventArgs e)
    {

    }

    private void ImageGestureRecognizerTapped(object sender, TappedEventArgs e)
    {
        profilePhotoButtonStackLayout.IsVisible = true;
    }

    private void TakePhotoButtonClicked(object sender, EventArgs e)
    {

    }

    private void SelectPhotoButtonClicked(object sender, EventArgs e)
    {

    }
}