using MauiDotNET8.Interface;
using System;


namespace MauiDotNET8.Utilities
{
    public class MediaPickerService: IMediaPickerService
    {
        private ImageSource imageSource;
        public async Task<ImageSource> TakePhotoFromgallery()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                imageSource = await LoadPhotoAsync(photo);
                if (imageSource != null)
                {
                    return imageSource;
                }

            }
            catch (Exception ex)
            {

            }

            return imageSource;
        }

        public async Task<ImageSource> CapturePhoto()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                imageSource = await LoadPhotoAsync(photo);
                if (imageSource != null)
                {
                    return imageSource;
                }
            }
            catch (Exception ex)
            {

            }
            return imageSource;
        }
        public async Task<ImageSource> LoadPhotoAsync(FileResult photo)
        {
            using var stream = await photo.OpenReadAsync();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var loginUser = await SecureStorage.Default.GetAsync("userIdentifer");
            await SecureStorage.Default.SetAsync($"{loginUser}.png", Convert.ToBase64String(imageBytes));

            var newStream = new MemoryStream(imageBytes);
            var imageSource = ImageSource.FromStream(() => newStream);
            return imageSource;
        }
    }
}
