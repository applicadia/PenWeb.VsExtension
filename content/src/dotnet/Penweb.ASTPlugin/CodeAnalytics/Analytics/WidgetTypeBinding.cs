using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penweb.CodeAnalytics
{
    [JsonObject(MemberSerialization=MemberSerialization.OptIn,IsReference =false)]
    public class WidgetTypeBinding
    {
        [JsonProperty] public string ResourceLabel { get; set; }
        [JsonProperty] public int    ResourceId { get; set; }
        [JsonProperty] public string FileName { get; set; }
        [JsonProperty] public int    LineNumber { get; set; }

        [JsonProperty] 
        [JsonConverter(typeof(StringEnumConverter))]
        public PenWebWidgetType PenWebWidgetType { get; set; }

        public WidgetTypeBinding()
        {
        }
    }
}
