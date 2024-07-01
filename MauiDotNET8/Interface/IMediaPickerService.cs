using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Interface
{
    public interface IMediaPickerService
    {
        Task<ImageSource> TakePhotoFromgallery();
        Task<ImageSource> CapturePhoto();
    }
}
