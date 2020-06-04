using Newtonsoft.Json;
using System.Collections.Generic;

namespace XsdManager.ViewObjects
{
    class ChoicesVO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SortedList<string, string> Attributes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ElementVO> Elements { get; set; }

        public ChoicesVO()
        {
            Attributes = new SortedList<string, string>();
            Elements = new List<ElementVO>();
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Count > 0;
        }

        public bool ShouldSerializeElements()
        {
            return Elements.Count > 0;
        }
    }
}
