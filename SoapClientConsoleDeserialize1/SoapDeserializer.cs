using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SoapClientConsoleDeserialize1
{
    internal class SoapDeserializer
    {
        public static helloResponse DeserializeSoapResponse(string soapXml, string innerNode)
        {
            // 1. Parse the entire SOAP string into an XDocument
            XDocument xDoc = XDocument.Parse(soapXml);

            // 2. Find the target element inside the SOAP Body by its local name
            XElement payloadElementBody = xDoc.Descendants()
                .FirstOrDefault(x => x.Name.LocalName == "Body");
 

            if (payloadElementBody == null)
            {
                throw new InvalidOperationException("Target XML element not found in SOAP body.");
            }

            // Obtain service namespace from HelloResponse attributes (XmlRoot or XmlType)
            string svcNamespace = GetXmlNamespaceFromType(typeof(helloResponse));
            XNamespace svcNs = !string.IsNullOrEmpty(svcNamespace) ? XNamespace.Get(svcNamespace) : XNamespace.None;
            string rootNoteName = GetXmlRootNameFromType(typeof(helloResponse));

            XElement helloResponseElem = payloadElementBody.Element(svcNs + innerNode)
                            ?? payloadElementBody.Elements().FirstOrDefault(e => e.Name.LocalName == innerNode);

            if (helloResponseElem == null)
            {
                throw new InvalidOperationException("Target XML element not found in SOAP body.");
            }

            // 3. Create the XmlSerializer for your target class type
            XmlSerializer serializer = new XmlSerializer(typeof(helloResponse));

            // 4. Use an XmlReader from the XElement to deserialize it
            using (var reader = helloResponseElem.CreateReader())
            {
                return (helloResponse)serializer.Deserialize(reader);
            }
        }

        private static string? GetXmlNamespaceFromType(Type type)
        {
            if (type == null) return null;

            var rootAttr = (XmlRootAttribute?)Attribute.GetCustomAttribute(type, typeof(XmlRootAttribute));
            if (!string.IsNullOrEmpty(rootAttr?.Namespace))
                return rootAttr.Namespace; 

            return null;
        }

        private static string? GetXmlRootNameFromType(Type type)
        {
            if (type == null) return null;

            var rootAttr = (XmlRootAttribute?)Attribute.GetCustomAttribute(type, typeof(XmlRootAttribute));
            if (!string.IsNullOrEmpty(rootAttr?.Namespace))
                return rootAttr.ElementName;

            return null;
        }
    }
}
