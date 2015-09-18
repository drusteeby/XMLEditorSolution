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

        public static bool ContainsAttributeWithExactValue(this XmlNode baseNode, string Name, string value)
        {
            return baseNode.ContainsAttribute(Name) && baseNode.Attributes[Name].Value == value;
        }
        public static bool ContainsAttributeContainsValue(this XmlNode baseNode, string attrName, string value)
        {
            return baseNode.ContainsAttribute(attrName) && baseNode.Attributes[attrName].Value.Contains(value);
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

        public static void AddOrAppendAttribute(this XmlNode baseNode, string Name, bool createsOnNull = false, string Value = null)
        {
            if (baseNode.ContainsAttribute(Name))
                baseNode.Attributes[Name].Value = Value;
            else if (!createsOnNull && Value == null) return;
            else
            {
                XmlAttribute attr = baseNode.OwnerDocument.CreateAttribute(Name);
                attr.Value = Value;
                baseNode.Attributes.Append(attr);
            }

        }

        public static bool IsEnumeration(this XmlNode baseNode)
        {
            return baseNode.Name.Contains("bit") || baseNode.Name.Contains("enum");
        }
        public static bool IsAlarm(this XmlNode baseNode)
        {
            return baseNode.ContainsAttributeContainsValue("group", "alarm");
        }
        public static bool IsWarning(this XmlNode baseNode)
        {
            return baseNode.ContainsAttributeContainsValue("group", "warning");
        }
        public static bool IsDataType(this XmlNode baseNode)
        {
            return baseNode.Name.Contains("virtual") && (baseNode.ContainsAttribute("dataType") || baseNode.ContainsAttribute("parent"));
        }
    }
}
