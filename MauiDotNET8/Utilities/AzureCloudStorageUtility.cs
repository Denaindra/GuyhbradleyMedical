using Azure.Storage.Blobs;
using MauiDotNET8.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Utilities
{
    //private string azureBlobConnectionString = "DefaultEndpointsProtocol=https;AccountName=trakkaclinical;AccountKey=VpvweM1Qu+0EuJCm/ajavza5/aWrOKU4bRbOllNgPtda31fXIEOf6NFwyz3dJp5pCopRQi+OzotLSjpMOMVANA==;EndpointSuffix=core.windows.net";
    //static readonly string azureBlobRootURL = HamptonMedical.Services.Constants.BlobStorageRootURL;
    //static readonly string azureBlobProfilePictureContainerName = HamptonMedical.Services.Constants.BlobStorageProfileImagesContainerName;
    //static readonly string azureBlobClinicLogoPictureContainerName = HamptonMedical.Services.Constants.BlobStorageClinicLogoImagesContainerName;

    //readonly static CloudStorageAccount _cloudStorageAccount = CloudStorageAccount.Parse(azureBlobConnectionString);
    //readonly static CloudBlobClient _blobClient = _cloudStorageAccount.CreateCloudBlobClient();
    public class AzureCloudStorageUtility: IAzureCloudStorageUtility
    {

        public async Task<bool> UploadBob(string blobName, Stream stream)
        {
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=trakkaclinical;AccountKey=VpvweM1Qu+0EuJCm/ajavza5/aWrOKU4bRbOllNgPtda31fXIEOf6NFwyz3dJp5pCopRQi+OzotLSjpMOMVANA==;EndpointSuffix=core.windows.net");
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("profile-pictures");
                await containerClient.CreateIfNotExistsAsync();
                BlobClient blobClient = containerClient.GetBlobClient(blobName);
                stream.Position = 0;
                var resuklts = await blobClient.UploadAsync(stream, overwrite: true);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
