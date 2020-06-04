using Newtonsoft.Json;
using System.Collections.Generic;

namespace XsdManager.ViewObjects
{
    class RestrictionVO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SortedList<string, string> Attributes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<EnumerationVO> Enumerations { get; set; }

        public RestrictionVO()
        {
            Attributes = new SortedList<string, string>();
            Enumerations = new List<EnumerationVO>();
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Count > 0;
        }

        public bool ShouldSerializeEnumerations()
        {
            return Enumerations.Count > 0;
        }
    }
}
