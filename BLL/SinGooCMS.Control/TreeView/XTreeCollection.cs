using System;
using System.Collections.Generic;
using System.Text;

using SinGooCMS.Utility;

namespace SinGooCMS.Control
{
    public class XTreeCollection : List<XTreeItem>
    {
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            builder.Append("<tree>");
            foreach (XTreeItem item in this)
            {
                builder.Append("<tree ");
                builder.Append("text=\"" + XmlHelper.XmlEncode(item.Text) + "\" ");
                builder.Append("title=\"" + XmlHelper.XmlEncode(item.Title) + "\" ");
                builder.Append("arrModelId=\"" + XmlHelper.XmlEncode(item.ArrModelId) + "\" ");
                builder.Append("arrModelName=\"" + XmlHelper.XmlEncode(item.ArrModelName) + "\" ");
                builder.Append("arrPurview=\"" + XmlHelper.XmlEncode(item.ArrPurview) + "\" ");
                builder.Append("nodeId=\"" + XmlHelper.XmlEncode(item.NodeId) + "\" ");
                builder.Append("target=\"" + XmlHelper.XmlEncode(item.Target) + "\" ");
                builder.Append("expand=\"" + XmlHelper.XmlEncode(item.Expand) + "\" ");
                builder.Append("action=\"" + XmlHelper.XmlEncode(item.Action) + "\" ");
                builder.Append("src=\"" + XmlHelper.XmlEncode(item.XmlSrc) + "\" ");
                builder.Append("anchorType=\"" + XmlHelper.XmlEncode(item.AnchorType) + "\" ");
                builder.Append("icon=\"" + XmlHelper.XmlEncode(item.Icon) + "\" ");
                builder.Append("nodeType=\"" + XmlHelper.XmlEncode(item.NodeType) + "\" ");
                builder.Append("enable=\"" + XmlHelper.XmlEncode(item.Enable) + "\" ");
                builder.Append(" />");
            }
            builder.Append("</tree>\n");
            return builder.ToString();
        }
    }
}

