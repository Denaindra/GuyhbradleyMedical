using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Interface
{
    public interface IAzureCloudStorageUtility
    {
        Task<bool> UploadBob(string blobName, Stream stream);
        string GetClinicLogoPictureURL(string clinicLogoPictureFileName);
    }
}
