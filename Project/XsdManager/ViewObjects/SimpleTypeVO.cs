using System;
using System.Collections.Generic;
using System.Text;

namespace XsdManager.ViewObjects
{
    class SimpleTypeVO
    {
        public SortedList<string, string> Attributes { get; set; }

        public AnnotationVO Annotation { get; set; }

        public RestrictionVO Restriction { get; set; }

        public SimpleTypeVO()
        {
            Attributes = new SortedList<string, string>();
            Annotation = new AnnotationVO();
            Restriction = new RestrictionVO();
        }

        public bool ShouldSerializeAttributes()
        {
            return Attributes.Count > 0;
        }

        public bool ShouldSerializeAnnotation()
        {
            return (Annotation.ShouldSerializeAttributes() || Annotation.ShouldSerializeDocumentation());
        }

        public bool ShouldSerializeRestriction()
        {
            return (Restriction.ShouldSerializeAttributes() || Restriction.ShouldSerializeEnumerations());
        }
    }
}
