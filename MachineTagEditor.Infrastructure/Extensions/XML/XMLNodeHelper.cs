using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MachineTagEditor.Infrastructure.Extensions.XML
{
    public static class XMLNodeExtension
    {
        public static bool ContainsAttribute(this XmlNode baseNode,string Name)
        {
            return (baseNode.Attributes != null && baseNode.Attributes[Name] != null);
        }

        public static bool ContainsAttributeNonNull(this XmlNode baseNode, string Name)
        {
            return baseNode.ContainsAttribute(Name) && baseNode.Attributes[Name].Value != null;
        }

        public static bool ContainsAttributeWithValue(this XmlNode baseNode, string Name, string value)
        {
            return baseNode.ContainsAttribute(Name) && baseNode.Attributes[Name].Value == value;
        }

        public static string AttributeValue(this XmlNode baseNode, string Name)
        {
            return baseNode.Attributes[Name].Value;
        }
        public static string AttributeValueOrDefault(this XmlNode baseNode, string Name)
        {
            if (!baseNode.ContainsAttributeNonNull(Name))
                return null;
            else
               return baseNode.Attributes[Name].Value;
        }
    }
}
