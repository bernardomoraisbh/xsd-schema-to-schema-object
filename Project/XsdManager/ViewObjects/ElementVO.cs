using Newtonsoft.Json;
using System.Collections.Generic;

namespace XsdManager.ViewObjects
{
    class ElementVO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SortedList<string, string> Attributes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AnnotationVO Annotation { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ComplexTypeVO ComplexType { get; set; }

        public ElementVO()
        {
            Attributes  = new SortedList<string, string>();
            Annotation  = new AnnotationVO();
            ComplexType = new ComplexTypeVO();
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Count > 0;
        }

        public bool ShouldSerializeAnnotation()
        {
            return (Annotation.Attributes.Count > 0 || Annotation.Documentation.Count > 0);
        }

        public bool ShouldSerializeComplexType()
        {
            return (ComplexType.ShouldSerializeAnnotation() || ComplexType.ShouldSerializeAttribute()      || ComplexType.ShouldSerializeAttributes() ||
                    ComplexType.ShouldSerializeChoice()     || ComplexType.ShouldSerializeComplexContent() || ComplexType.ShouldSerializeSequences());
        }
    }
}
