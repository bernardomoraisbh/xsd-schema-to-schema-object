using Newtonsoft.Json;
using System.Collections.Generic;

namespace XsdManager.ViewObjects
{
    class ComplexContentVO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SortedList<string, string> Attributes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ExtensionVO Extension { get; set; }

        public ComplexContentVO()
        {
            Attributes = new SortedList<string, string>();
            Extension = new ExtensionVO();
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Count > 0;
        }

        public bool ShouldSerializeExtension()
        {
            return (Extension.ShouldSerializeAttributes() || Extension.ShouldSerializeSequence());
        }
    }
}
