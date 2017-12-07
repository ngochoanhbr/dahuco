using System;
using System.Xml;

namespace SinGooCMS.Utility
{
    public class XmlHelper
    {
        public static XmlDocument CreateXmlDocument()
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "utf-8", "");
            document.InsertBefore(newChild, document.DocumentElement);
            return document;
        }

        public static XmlDocument CreateXmlDocument(string rootName)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", "utf-8", "");
            document.InsertBefore(newChild, document.DocumentElement);
            XmlNode node = document.CreateElement(rootName);
            document.AppendChild(node);
            return document;
        }

        public static string GetAttributeValue(XmlNode node, string attributeName)
        {
            XmlAttribute attribute = node.Attributes[attributeName];
            if (attribute == null)
            {
                return null;
            }
            return attribute.Value;
        }

        public static string GetItemValue(XmlNode node, string itemName)
        {
            if ((node != null) && (node[itemName] != null))
            {
                return node[itemName].InnerText;
            }
            return null;
        }

        public static XmlNode SelectNode(XmlNode parentNode, string attributeName, string attributeValue)
        {
            XmlNode node = null;
            if (parentNode.HasChildNodes)
            {
                string name = parentNode.ChildNodes[0].Name;
                string xpath = name + "[@" + attributeName + "='" + attributeValue + "']";
                node = parentNode.SelectSingleNode(xpath);
            }
            return node;
        }

        public static bool SetAttributeValue(XmlNode node, string attributeName, string attributeValue)
        {
            if (node != null)
            {
                XmlAttribute attribute = node.Attributes[attributeName];
                if (attribute != null)
                {
                    attribute.Value = attributeValue;
                    return true;
                }
            }
            return false;
        }

        public static bool SetItemValue(XmlNode node, string itemName, string itemValue)
        {
            if ((node != null) && (node[itemName] != null))
            {
                node[itemName].InnerText = itemValue;
                return true;
            }
            return false;
        }

        public static string XmlEncode(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace("&", "&amp;");
                value = value.Replace("<", "&lt;");
                value = value.Replace(">", "&gt;");
                value = value.Replace("'", "&apos;");
                value = value.Replace("\"", "&quot;");
            }
            return value;
        }
    }
}

