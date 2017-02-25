using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PaperID.Models.TagStatusViewModels
{
    public class TagStatusViewModel
    {
        public TagStatusViewModel(string id)
        {
            this.ID = id;
        }

        public string ID { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TagInterest Interest { get; set; } = TagInterest.None;

        [JsonConverter(typeof(StringEnumConverter))]
        public TagProximity Proximity { get; set; } = TagProximity.Unknown;

        [JsonConverter(typeof(StringEnumConverter))]
        public TagLocation Location { get; set; } = TagLocation.Unknown;
    }
}
