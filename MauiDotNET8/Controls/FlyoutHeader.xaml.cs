using MauiDotNET8.Helpers;
using MauiDotNET8.ViewModels;

namespace MauiDotNET8.Controls;

public partial class FlyoutHeader : ContentView
{
    private FlyoutHeaderViewModel vm;
	public FlyoutHeader()
	{
		InitializeComponent();
        vm = ServiceHelper.GetService<FlyoutHeaderViewModel>();
        BindingContext = vm;
        Task.Run(() => SetupUI());
    }

    private void SetupUI()
    {
        vm.MediaSource = ImageSource.FromFile("user.png");
    }
    private void TapGestureRecognizerTapped(object sender, TappedEventArgs e)
    {
        profilePhotoButtonStackLayout.IsVisible = false;
    }

    private void ImageGestureRecognizerTapped(object sender, TappedEventArgs e)
    {
        profilePhotoButtonStackLayout.IsVisible = true;
    }

    private void TakePhotoButtonClicked(object sender, EventArgs e)
    {
        vm.CapturePhoto();
    }

    private void SelectPhotoButtonClicked(object sender, EventArgs e)
    {
        vm.TakePhotoFromgallery();
    }
}