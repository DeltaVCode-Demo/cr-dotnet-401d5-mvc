using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace DemoMvc.Services
{
    public class AzureFileUploadService : IFileUploadService
    {
        public const string AccountName_Key = "AzureStorageAccountName";
        private readonly CloudBlobClient cloudBlobClient;

        public AzureFileUploadService(IConfiguration configuration)
        {
            var accountName = configuration[AccountName_Key] ?? throw new InvalidOperationException("Missing AzureStorageAccountName");
            var blobKey = configuration["AzureBlobKey"] ?? throw new InvalidOperationException("Missing AzureBlobKey");

            var storageCredentials = new StorageCredentials(accountName, blobKey);
            var storageAccount = new CloudStorageAccount(storageCredentials, true);

            cloudBlobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<string> Upload(IFormFile profileImage)
        {
            // Access to a storage Container
            var container = cloudBlobClient.GetContainerReference("profiles");
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                // Allow anonymous access to individual files *if you have the link*
                PublicAccess = BlobContainerPublicAccessType.Blob,
            });

            // Actually do the upload
            var blobFile = container.GetBlockBlobReference(profileImage.FileName);

            // using = close the connection when we're done (IDisposable)
            using var imageStream = profileImage.OpenReadStream();
            await blobFile.UploadFromStreamAsync(imageStream);
            return blobFile.Uri.ToString();
        }
    }

}
