using Newtonsoft.Json;
using System.Collections.Generic;

namespace XsdManager.ViewObjects
{
    class AnnotationVO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SortedList<string, string> Attributes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DocumentationVO> Documentation { get; set; }

        public AnnotationVO()
        {
            Attributes = new SortedList<string, string>();
            Documentation = new List<DocumentationVO>();
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Count > 0;
        }

        public bool ShouldSerializeDocumentation()
        {
            return Documentation.Count > 0;
        }
    }
}
