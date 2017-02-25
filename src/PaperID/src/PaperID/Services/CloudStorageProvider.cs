using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Threading.Tasks;

namespace PaperID.Services
{
    internal class CloudStorageProvider : ICloudStorageProvider
    {
        private readonly Lazy<CloudBlobClient> lazyBlobClient;
        private CloudBlobContainer imageContainer;

        public CloudStorageProvider(string connectionString)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            this.lazyBlobClient = new Lazy<CloudBlobClient>(
                () => account.CreateCloudBlobClient());            
        }

        public CloudBlobClient BlobClient => this.lazyBlobClient.Value;

        public async Task<CloudBlobContainer> GetImageContainerAsync()
        {
            if (this.imageContainer == null)
            {
                this.imageContainer = await this.CreateContainer("images", true);
            }

            return this.imageContainer;
        }

        private async Task<CloudBlobContainer> CreateContainer(string name, bool allowPublicAccess)
        {
            var container = this.BlobClient.GetContainerReference(name);
            await container.CreateIfNotExistsAsync();
            if (allowPublicAccess)
            {
                await container.SetPermissionsAsync(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            return container;
        }
    }
}
