using Newtonsoft.Json;
using System.Collections.Generic;

namespace XsdManager.ViewObjects
{
    class SchemaVO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SortedList<string, string> Attributes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Annotation { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ComplexTypeVO> ComplexTypes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<SimpleTypeVO> SimpleTypes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ElementVO Element { get; set; }

        public SchemaVO()
        {
            Attributes = new SortedList<string, string>();
            ComplexTypes = new List<ComplexTypeVO>();
            SimpleTypes = new List<SimpleTypeVO>();
            Element = new ElementVO();
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Count > 0;
        }

        public bool ShouldSerializeComplexTypes()
        {
            return ComplexTypes.Count > 0;
        }

        public bool ShouldSerializeSimpleTypes()
        {
            return SimpleTypes.Count > 0;
        }

        public bool ShouldSerializeElement()
        {
            return (Element.ShouldSerializeAnnotation() || Element.ShouldSerializeAttributes() || Element.ShouldSerializeComplexType());
        }

    }
}
