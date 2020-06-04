using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using XsdManager.ViewObjects;

namespace XsdManager
{
    class Program
    {
        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> and convert it to a <see cref="SchemaVO">SchemaVO</see> type.<para />
        /// </summary>
        /// <param name="root">XmlNode with the default xs:schema</param>
        /// <param name="schema">SchemaVO to save values loaded from the root Node</param>
        private static void XSDtoSchema(XmlNode root, SchemaVO schema)
        {
            if (root.Name == "xs:schema")
            {
                if (root.Attributes != null)
                    ParseAttributes(root.Attributes, schema.Attributes);
                foreach (XmlNode child in root)
                {
                    if (child.Name == "xs:element")
                        ParseElementNode(child, schema.Element);
                    else if (child.Name == "xs:complexType")
                    {
                        ComplexTypeVO complexType = new ComplexTypeVO();
                        ParseComplexTypeNode(child, complexType);
                        schema.ComplexTypes.Add(complexType);
                    }
                    else if (child.Name == "xs:simpleType")
                    {
                        SimpleTypeVO simpleType = new SimpleTypeVO();
                        ParseSimpleTypeNode(child, simpleType);
                        schema.SimpleTypes.Add(simpleType);
                    }
                }
            }
        }

        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> with xs:simpleType name and convert it to a <see cref="SimpleTypeVO">SimpleTypeVO</see> type.<para />
        /// </summary>
        /// <param name="node">XmlNode named xs:simpleType</param>
        /// <param name="simpleType">SimpleTypeVO to save values loaded from the xs:simpleType node</param>
        private static void ParseSimpleTypeNode(XmlNode node, SimpleTypeVO simpleType)
        {
            if (node.Attributes != null)
                ParseAttributes(node.Attributes, simpleType.Attributes);
            if (node.HasChildNodes)
                foreach (XmlNode simpleTypeChild in node)
                {
                    if (simpleTypeChild.Name == "xs:annotation")
                        ParseAnnotations(simpleTypeChild, simpleType.Annotation);
                    else if (simpleTypeChild.Name == "xs:restriction")
                        ParseRestrictionNode(simpleTypeChild, simpleType.Restriction);
                }
        }

        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> with xs:simpleType name and convert it to a <see cref="RestrictionVO">RestrictionVO</see> type.<para />
        /// </summary>
        /// <param name="node">XmlNode named xs:restriction</param>
        /// <param name="restriction">RestrictionVO to save values loaded from the xs:restriction node</param>
        private static void ParseRestrictionNode(XmlNode node, RestrictionVO restriction)
        {
            if (node.Attributes != null)
                ParseAttributes(node.Attributes, restriction.Attributes);
            if (node.HasChildNodes)
                foreach (XmlNode restrictionEnumChild in node)
                {
                    if (restrictionEnumChild.Name == "xs:enumeration")
                        ParseEnumerationNodde(restrictionEnumChild, restriction.Enumerations);
                }

        }

        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> with xs:enumeration name and convert it to a <see cref="List">List EnumerationVO</see> type.<para />
        /// </summary>
        /// <param name="node">XmlNode named xs:enumeration</param>
        /// <param name="enumeration">List of <see cref="EnumerationVO">EnumerationVO</see> to save values loaded from the xs:enumeration node</param>
        private static void ParseEnumerationNodde(XmlNode node, List<EnumerationVO> enumeration)
        {
            EnumerationVO enumeration1 = new EnumerationVO();
            if (node.Attributes != null)
                ParseAttributes(node.Attributes, enumeration1.Attributes);
            if (node.HasChildNodes)
                foreach (XmlNode enumChild in node)
                {
                    if (enumChild.Name == "xsl:annotation")
                        ParseAnnotations(enumChild, enumeration1.Annotation);
                }
            enumeration.Add(enumeration1);
        }

        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> with xs:element name and convert it to a <see cref="ElementVO">ElementVO</see> type.<para />
        /// </summary>
        /// <param name="node">XmlNode named xs:element</param>
        /// <param name="element">ElementVO to save values loaded from the xs:element node</param>
        private static void ParseElementNode(XmlNode node, ElementVO element)
        {
            if (node.Attributes != null)
                ParseAttributes(node.Attributes, element.Attributes);
            if (node.HasChildNodes)
                foreach (XmlNode elementChild in node)
                {
                    if (elementChild.Name == "xs:annotation")
                        ParseAnnotations(elementChild, element.Annotation);
                    else if (elementChild.Name == "xs:complexType")
                        ParseComplexTypeNode(elementChild, element.ComplexType);
                }
        }

        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> with xs:complexType name and convert it to a <see cref="ComplexTypeVO">ComplexTypeVO</see> type.<para />
        /// </summary>
        /// <param name="node">XmlNode named xs:complexType</param>
        /// <param name="complexType">ComplexTypeVO to save values loaded from the xs:complexType node</param>
        private static void ParseComplexTypeNode(XmlNode node, ComplexTypeVO complexType)
        {
            if (node.Attributes != null)
                ParseAttributes(node.Attributes, complexType.Attributes);
            if (node.HasChildNodes)
                foreach (XmlNode complexChild in node)
                {
                    if (complexChild.Name == "xs:annotation")
                        ParseAnnotations(complexChild, complexType.Annotation);
                    else if (complexChild.Name == "xs:attribute")
                        ParseAttributes(complexChild.Attributes, complexType.Attribute);
                    else if (complexChild.Name == "xs:complexContent")
                        ParseComplexContent(complexChild, complexType.ComplexContent);
                    else if (complexChild.Name == "xs:choice")
                        ParseChoiceNode(complexChild, complexType.Choice);
                    else if (complexChild.Name == "xs:sequence")
                        ParseSequence(complexChild, complexType.Sequences);
                }
        }

        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> with xs:choice name and convert it to a <see cref="ChoicesVO">ChoicesVO</see> type.<para />
        /// </summary>
        /// <param name="node">XmlNode named xs:choice</param>
        /// <param name="choices">ChoicesVO to save values loaded from the xs:choice node</param>
        private static void ParseChoiceNode(XmlNode node, ChoicesVO choices)
        {
            if (node.Attributes != null)
                ParseAttributes(node.Attributes, choices.Attributes);
            if (node.HasChildNodes)
                foreach (XmlNode choicesOptions in node)
                {
                    if (choicesOptions.Name == "xs:element")
                    {
                        ElementVO element = new ElementVO();
                        ParseElementNode(choicesOptions, element);
                        choices.Elements.Add(element);
                    }
                }
        }

        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> with xs:complexContent name and convert it to a <see cref="ComplexContentVO">ComplexContentVO</see> type.<para />
        /// </summary>
        /// <param name="node">XmlNode named xs:complexContent</param>
        /// <param name="complexContent">ComplexContentVO to save values loaded from the xs:complexContent node</param>
        private static void ParseComplexContent(XmlNode node, ComplexContentVO complexContent)
        {
            if (node.Attributes != null)
                ParseAttributes(node.Attributes, complexContent.Attributes);
            if (node.HasChildNodes)
                foreach (XmlNode complexChild in node)
                {
                    if (complexChild.Name == "xs:extension")
                        ParseExtension(complexChild, complexContent.Extension);
                }
        }

        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> with xs:extension name and convert it to a <see cref="ExtensionVO">ExtensionVO</see> type.<para />
        /// </summary>
        /// <param name="node">XmlNode named xs:extension</param>
        /// <param name="extension">ExtensionVO to save values loaded from the xs:extension node</param>
        private static void ParseExtension(XmlNode node, ExtensionVO extension)
        {
            if (node.Attributes != null)
                ParseAttributes(node.Attributes, extension.Attributes);
            if (node.HasChildNodes)
                foreach (XmlNode extensionChild in node)
                {
                    if (extensionChild.Name == "xs:sequence")
                        ParseSequence(extensionChild, extension.Sequence);
                }
        }

        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> with xs:sequence name and convert it to a <see cref="SequenceVO">SequenceVO</see> type.<para />
        /// </summary>
        /// <param name="node">XmlNode named xs:sequence</param>
        /// <param name="sequence">SequenceVO to save values loaded from the xs:sequence node</param>
        private static void ParseSequence(XmlNode node, SequenceVO sequence)
        {
            if (node.Attributes != null)
                ParseAttributes(node.Attributes, sequence.Attributes);
            if (node.HasChildNodes)
                foreach (XmlNode sequenceChild in node)
                {
                    if (sequenceChild.Name == "xs:annotation")
                        ParseAnnotations(sequenceChild, sequence.Annotation);
                    if (sequenceChild.Name == "xs:element")
                    {
                        ElementVO element = new ElementVO();
                        ParseElementNode(sequenceChild, element);
                        sequence.Elements.Add(element);
                    }
                }
        }

        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> with xs:annotations name and convert it to a <see cref="AnnotationVO">AnnotationVO</see> type.<para />
        /// </summary>
        /// <param name="node">XmlNode named xs:annotations</param>
        /// <param name="annotation">AnnotationVO to save values loaded from the xs:annotations node</param>
        private static void ParseAnnotations(XmlNode node, AnnotationVO annotation)
        {
            if (node.Attributes != null)
                ParseAttributes(node.Attributes, annotation.Attributes);
            if (node.HasChildNodes)
                foreach (XmlNode documentation in node)
                {
                    if (documentation.Name == "xs:documentation")
                    {
                        DocumentationVO doc = new DocumentationVO();
                        ParseDocumentation(documentation, doc);
                        annotation.Documentation.Add(doc);
                    }
                }
        }

        /// <summary>
        /// Parse a <see cref="XmlNode">XmlNode</see> with xs:documentation name and convert it to a <see cref="DocumentationVO">DocumentationVO</see> type.<para />
        /// </summary>
        /// <param name="node">XmlNode named xs:documentation</param>
        /// <param name="documentation">DocumentationVO to save values loaded from the xs:documentation node</param>
        private static void ParseDocumentation(XmlNode node, DocumentationVO documentation)
        {
            if (node.Attributes != null)
                ParseAttributes(node.Attributes, documentation.Attributes);
            if (node.InnerText != null)
                documentation.text = node.InnerText;
        }

        /// <summary>
        /// Parse a <see cref="XmlAttributeCollection">XmlAttributeCollection</see> and convert it to a <see cref="SortedList">SortedList</see>.<para />
        /// </summary>
        /// <param name="attributes">XmlAttributeCollection, a collection of attributes</param>
        /// <param name="attlist">SortedList<string, string> to save values loaded from the collection (key attribute, value).</param>
        private static void ParseAttributes(XmlAttributeCollection attributes, SortedList<string, string> attlist)
        {
            foreach (XmlAttribute attr in attributes)
            {
                attlist.Add(attr.Name, attr.InnerText);
            }
        }

        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            doc.Load("XMLSchema.xsd");

            SchemaVO schema = new SchemaVO();

            XSDtoSchema(doc.DocumentElement, schema);

            string xsdJson = JsonConvert.SerializeObject(schema, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("output.json", xsdJson);
            Console.WriteLine("Hello World!");
        }

    }
}
