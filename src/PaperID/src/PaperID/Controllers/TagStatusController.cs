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

        private static readonly Dictionary<string, TagStatusViewModel> mockStatuses = new Dictionary<string, TagStatusViewModel> {
            { "a001", new TagStatusViewModel("a001") { Interest = TagInterest.Browse, Location = TagLocation.InFittingRoom, Proximity = TagProximity.Unknown }},
            { "a002", new TagStatusViewModel("a002") { Interest = TagInterest.Interested, Location = TagLocation.OnShelf, Proximity = TagProximity.Unknown }},
            { "a003", new TagStatusViewModel("a003") { Interest = TagInterest.None, Location = TagLocation.OnShelf, Proximity = TagProximity.NearShopper }},
            { "a004", new TagStatusViewModel("a004") { Interest = TagInterest.Browse, Location = TagLocation.InFittingRoom, Proximity = TagProximity.NearShopper }},
            { "a005", new TagStatusViewModel("a005") { Interest = TagInterest.Interested, Location = TagLocation.OnShelf, Proximity = TagProximity.NearShopper }},
        };

        public TagStatusController(ICloudStorageProvider provider)
        {
            this.provider = provider;
        }

        public IActionResult Get()
        {
            var status = statuses;
            var referrer = this.Request.Headers["Referer"].ToString();
            if (referrer != null && referrer.Contains("mockdata=1"))
            {
                status = mockStatuses;
            }
            
            return this.Ok(value: status);
        }

        [HttpPost]
        [Route("location")]
        public IActionResult PostLocation([FromBody] Dictionary<string, long> data)
        {
            UpdateStatuses(
                data,
                (vm, val) => vm.Location = TagStatusHelpers.ToTagLocation(val));

            return this.NoContent();
        }

        [HttpPost]
        [Route("motion")]
        public IActionResult PostMotion([FromBody] Dictionary<string, long> data)
        {
            UpdateStatuses(
                data, 
                (vm, val) => vm.Interest = TagStatusHelpers.ToTagInterest(val));

            return this.NoContent();
        }

        [HttpPost]
        [Route("proximity")]
        public IActionResult PostProximity([FromBody] Dictionary<string, long> data)
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
    }
}