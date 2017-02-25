using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaperID.Models.TagStatusViewModels;
using PaperID.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PaperID.Controllers
{
    [Produces("application/json")]
    [Route("api/TagsStatus")]
    public class TagStatusController : Controller
    {
        private ICloudStorageProvider provider;
        private static readonly Dictionary<string, TagStatusViewModel> statuses = new Dictionary<string, TagStatusViewModel>();

        public TagStatusController(ICloudStorageProvider p)
        {
            this.provider = p;
        }

        public IActionResult Get()
        {
            return this.Ok(value: statuses);
        }

        [HttpPost]
        [Route("data")]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, long> data)
        {
            return await this.PostData("data", data);
        }

        [HttpPost]
        [Route("location")]
        public async Task<IActionResult> PostLocation([FromBody] Dictionary<string, long> data)
        {
            UpdateStatuses(
                data,
                (vm, val) => vm.Location = TagStatusHelpers.ToTagLocation(val));

            return this.NoContent();
        }

        [HttpPost]
        [Route("motion")]
        public async Task<IActionResult> PostMotion([FromBody] Dictionary<string, long> data)
        {
            UpdateStatuses(
                data, 
                (vm, val) => vm.Interest = TagStatusHelpers.ToTagInterest(val));

            return this.NoContent();
        }

        [HttpPost]
        [Route("proximity")]
        public async Task<IActionResult> PostProximity([FromBody] Dictionary<string, long> data)
        {
            UpdateStatuses(
                data,
                (vm, val) => vm.Proximity = TagStatusHelpers.ToTagProximity(val));

            return this.NoContent();
        }

        private static void UpdateStatuses(Dictionary<string, long> data, Action<TagStatusViewModel, long> updater)
        {
            foreach (var kvp in data)
            {
                var status = GetStatus(kvp.Key);
                updater(status, kvp.Value);                
            }
        }

        private static TagStatusViewModel GetStatus(string key)
        {
            TagStatusViewModel vm;
            if (!statuses.TryGetValue(key, out vm))
            {
                vm = new TagStatusViewModel(key);
                statuses[key] = vm;
            }

            return vm;
        }
        
        private dynamic ReadData()
        {
            string jsonString = null;
            using (var streamReader = new StreamReader(this.Request.Body))
            {
                jsonString = streamReader.ReadToEnd();
            }

            if (this.Request.Body.CanSeek)
            {
                this.Request.Body.Seek(0, SeekOrigin.Begin);
            }

            if (jsonString == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject(jsonString);

        }
        private async Task<IActionResult> PostData(string logPrefix, Dictionary<string, long> data)
        {
            var container = await this.provider.GetImageContainerAsync();
            var blob = container.GetBlockBlobReference($"log-{logPrefix}-{Guid.NewGuid().ToString()}");
            blob.Properties.ContentType = "application/json";

            var jsonObj = new { host = this.Request.Host, path = this.Request.Path, query = this.Request.QueryString, value = data };
            var json = JsonConvert.SerializeObject(jsonObj);

            await blob.UploadTextAsync(json);
            return this.Ok(jsonObj);
        }
    }
}