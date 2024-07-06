using MauiDotNET8.Interface;
using System;


namespace MauiDotNET8.Utilities
{
    public class MediaPickerService: IMediaPickerService
    {
        private ImageSource imageSource;
        private readonly IAzureCloudStorageUtility azureCloudStorageUtility;

        public MediaPickerService(IAzureCloudStorageUtility azureCloudStorageUtility)
        {
            this.azureCloudStorageUtility = azureCloudStorageUtility;
        }
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
                var imageSource = await LoadPhotoAsync(photo);
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
            byte[] imageBytes;
            using (var stream = await photo.OpenReadAsync())
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();

                var loginUser = await SecureStorage.Default.GetAsync("userIdentifer");
                await SecureStorage.Default.SetAsync($"{loginUser}.png", Convert.ToBase64String(imageBytes));

               // using var newStream = new MemoryStream(imageBytes);
                var results = await azureCloudStorageUtility.UploadBob($"{loginUser}.png", memoryStream);
            }
            return ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }
    }
}
