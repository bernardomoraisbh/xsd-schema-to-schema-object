using Newtonsoft.Json;
using System.Collections.Generic;

namespace XsdManager.ViewObjects
{
    class EnumerationVO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SortedList<string, string> Attributes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AnnotationVO Annotation { get; set; }

        public EnumerationVO()
        {
            Attributes = new SortedList<string, string>();
            Annotation = new AnnotationVO();
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Count > 0;
        }

        public bool ShouldSerializeAnnotation()
        {
            return (Annotation.ShouldSerializeAttributes() || Annotation.ShouldSerializeDocumentation());
        }
    }
}
