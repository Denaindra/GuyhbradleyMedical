using MauiDotNET8.Interface;
using MauiDotNET8.ViewModels.Base;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.ViewModels
{
    public class FlyoutHeaderViewModel : BaseViewModel
    {
        private readonly IMediaPickerService mediaPickerService;
        private ImageSource mediaSource;

        public FlyoutHeaderViewModel(IMediaPickerService mediaPickerService)
        {
            this.mediaPickerService = mediaPickerService;
        }

        public ImageSource MediaSource
        {
            get
            {
                return mediaSource;
            }
            set
            {
                mediaSource = value;
                OnPropertyChanged(nameof(MediaSource));
            }
        }

        public async void CapturePhoto()
        {
            var photo = await mediaPickerService.CapturePhoto();
            if (photo != null)
            {
                MediaSource = photo;
            }
        }

        public async void TakePhotoFromgallery()
        {
            var photo = await mediaPickerService.TakePhotoFromgallery();
            if (photo != null)
            {
                MediaSource = photo;
            }
        }

    }
}
