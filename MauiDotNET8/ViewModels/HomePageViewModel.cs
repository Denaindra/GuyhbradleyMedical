using MauiDotNET8.Interface;
using MauiDotNET8.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.ViewModels
{
    public class HomePageViewModel:BaseViewModel
    {
        private readonly IAzureCloudStorageUtility azureCloudStorage;
        private string imageLogoPath;
        private bool isActivityIndicator;

        public HomePageViewModel( IAzureCloudStorageUtility azureCloudStorageUtility)
        {
            this.azureCloudStorage = azureCloudStorageUtility;
        }


        public bool IsActivityIndicator
        {
            get { return isActivityIndicator; }
            set { isActivityIndicator = value;
                OnPropertyChanged(nameof(IsActivityIndicator));
            }
        }

        public string ImageLogoPath
        {
            get { 
                return imageLogoPath; 
            }
            set
            {
                imageLogoPath = value;
                OnPropertyChanged(nameof(ImageLogoPath));
            }
        }

        public async Task GetClinicLogo()
        {
            IsActivityIndicator = true;
            var logoClinicIdentifier =  await SecureStorage.Default.GetAsync("clinicIdentifier");
            ImageLogoPath = azureCloudStorage.GetClinicLogoPictureURL(logoClinicIdentifier + ".png");
            IsActivityIndicator = false;
        }
    }
}
