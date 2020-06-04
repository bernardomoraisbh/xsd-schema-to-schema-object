using Newtonsoft.Json;
using System.Collections.Generic;

namespace XsdManager.ViewObjects
{
    class ExtensionVO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SortedList<string, string> Attributes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SequenceVO Sequence { get; set; }

        public ExtensionVO()
        {
            Attributes = new SortedList<string, string>();
            Sequence = new SequenceVO();
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Count > 0;
        }

        public bool ShouldSerializeSequence()
        {
            return (Sequence.ShouldSerializeAnnotation() || Sequence.ShouldSerializeAttributes() || Sequence.ShouldSerializeElements());
        }
    }
}
