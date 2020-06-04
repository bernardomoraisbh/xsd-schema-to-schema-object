using System.Collections.Generic;

namespace XsdManager.ViewObjects
{
    class SequenceVO
    {
        public AnnotationVO Annotation { get; set; }

        public SortedList<string, string> Attributes { get; set; }

        public List<ElementVO> Elements { get; set; }

        public SequenceVO()
        {
            Annotation = new AnnotationVO();
            Attributes = new SortedList<string, string>();
            Elements = new List<ElementVO>();
        }

        public bool ShouldSerializeAnnotation()
        {
            return (Annotation.ShouldSerializeAttributes() || Annotation.ShouldSerializeDocumentation());
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
