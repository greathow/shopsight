using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaperID.Services;
using System;
using System.Threading.Tasks;

namespace PaperID.Controllers
{
    [Produces("application/json")]
    [Route("api/ImageUpload")]
    public class ImageUploadController : Controller
    {
        private readonly ICloudStorageProvider cloudStorageProvider;

        public ImageUploadController(ICloudStorageProvider cloudStorageProvider)
        {
            this.cloudStorageProvider = cloudStorageProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            var imageContainer = await this.cloudStorageProvider.GetImageContainerAsync();
            var guid = Guid.NewGuid().ToString();
            var blob = imageContainer.GetBlockBlobReference(guid);
            blob.Properties.ContentType = file.ContentType;
            using (var stream = file.OpenReadStream())
            {
                await blob.UploadFromStreamAsync(stream);
            }
            
            return this.Ok(new { ImageUrl = blob.Uri.ToString() });
        }
    }
}