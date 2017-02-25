using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace PaperID.Services
{
    public interface ICloudStorageProvider
    {
        CloudBlobClient BlobClient { get; }

        Task<CloudBlobContainer> GetImageContainerAsync();
    }
}
