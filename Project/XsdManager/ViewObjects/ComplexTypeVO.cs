using Newtonsoft.Json;
using System.Collections.Generic;

namespace XsdManager.ViewObjects
{
    class ComplexTypeVO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AnnotationVO Annotation { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SortedList<string, string> Attributes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SortedList<string, string> Attribute { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ComplexContentVO ComplexContent { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SequenceVO Sequences { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ChoicesVO Choice { get; set; }

        public ComplexTypeVO()
        {
            Annotation = new AnnotationVO();
            Attributes = new SortedList<string, string>();
            Attribute = new SortedList<string, string>();
            Choice = new ChoicesVO();
            ComplexContent = new ComplexContentVO();
            Sequences = new SequenceVO();
        }

        public bool ShouldSerializeAnnotation()
        {
            return (Annotation.ShouldSerializeAttributes() || Annotation.ShouldSerializeDocumentation());
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Count > 0;
        }

        public bool ShouldSerializeAttribute()
        {
            return Attribute.Count > 0;
        }

        public bool ShouldSerializeComplexContent()
        {
            return (ComplexContent.ShouldSerializeAttributes() || ComplexContent.ShouldSerializeExtension());
        }

        public bool ShouldSerializeSequences()
        {
            return (Sequences.ShouldSerializeAttributes() || Sequences.ShouldSerializeElements() || Sequences.ShouldSerializeAnnotation());
        }

        public bool ShouldSerializeChoice()
        {
            return (Choice.ShouldSerializeAttributes() || Choice.ShouldSerializeElements());
        }
    }
}
