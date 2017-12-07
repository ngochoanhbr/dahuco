using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace SinGooCMS.Control
{
    [ToolboxData("<{0}:XLoadTree ID=\"XLoadTree1\" runat=\"server\" />"), Themeable(true)]
    public class XLoadTree : System.Web.UI.Control
    {
        protected override void OnLoad(EventArgs e)
        {
            Type type = base.GetType();
            bool flag = string.IsNullOrEmpty(this.ScriptPath);
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(type, "xtree.js"))
            {
                string url = flag ? this.Page.ClientScript.GetWebResourceUrl(type,global::SinGooCMS.Control.Properties.Resources.xtree1) : (this.ScriptPath + "xtree.js");
                this.Page.ClientScript.RegisterClientScriptInclude(type, "xtree.js", url);
            }
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(type, "xmlextras.js"))
            {
                string str2 = flag ? this.Page.ClientScript.GetWebResourceUrl(type,global::SinGooCMS.Control.Properties.Resources.xmlextras) : (this.ScriptPath + "xmlextras.js");
                this.Page.ClientScript.RegisterClientScriptInclude(type, "xmlextras.js", str2);
            }
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(type, "xloadtree.js"))
            {
                string str3 = flag ? this.Page.ClientScript.GetWebResourceUrl(type,global::SinGooCMS.Control.Properties.Resources.xloadtree) : (this.ScriptPath + "xloadtree.js");
                this.Page.ClientScript.RegisterClientScriptInclude(type, "xloadtree.js", str3);
            }
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(type, "xmenu.js"))
            {
                string str4 = flag ? this.Page.ClientScript.GetWebResourceUrl(type,global::SinGooCMS.Control.Properties.Resources.xmenu) : (this.ScriptPath + "xmenu.js");
                this.Page.ClientScript.RegisterClientScriptInclude(type, "xmenu.js", str4);
            }
            if (!this.IsApplyStyleSheetCss && !this.Page.ClientScript.IsClientScriptBlockRegistered(type, "Resources.css"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(type, "Resources.css", "");
                bool flag2 = string.IsNullOrEmpty(this.CssPath);
                string str5 = flag2 ? this.Page.ClientScript.GetWebResourceUrl(type,global::SinGooCMS.Control.Properties.Resources.xmenu) : (this.CssPath + "xmenu.css");
                HtmlLink child = new HtmlLink();
                child.Attributes.Add("type", "text/css");
                child.Attributes.Add("rel", "stylesheet");
                child.Attributes.Add("href", str5);
                try
                {
                    this.Page.Header.Controls.Add(child);
                }
                catch
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(type, "xmenu.css", "<link type=\"text/css\" rel=\"stylesheet\" href=\"" + str5 + "\" />");
                }
                string str6 = flag2 ? this.Page.ClientScript.GetWebResourceUrl(type,global::SinGooCMS.Control.Properties.Resources.xtree) : (this.CssPath + "xtree.css");
                HtmlLink link2 = new HtmlLink();
                link2.Attributes.Add("type", "text/css");
                link2.Attributes.Add("rel", "stylesheet");
                link2.Attributes.Add("href", str6);
                try
                {
                    this.Page.Header.Controls.Add(link2);
                }
                catch
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(type, "xtree.css", "<link type=\"text/css\" rel=\"stylesheet\" href=\"" + str6 + "\" />");
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string webResourceUrl;
            Type type = base.GetType();
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered("rightMenujs"))
            {
                writer.Write("<div id=\"menudata\"></div>");
            }
            writer.Write("\r<script type=\"text/javascript\">\n");
            if (string.IsNullOrEmpty(this.IconPath))
            {
                webResourceUrl = string.IsNullOrEmpty(this.RootIcon) ? this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.closefolder.gif") : this.RootIcon;
                if (this.RootIcon == "WebSite")
                {
                    webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.WebSite.gif");
                }
                string str2 = string.IsNullOrEmpty(this.OpenRootIcon) ? this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.closefolder.gif") : this.OpenRootIcon;
                string str3 = string.IsNullOrEmpty(this.FolderIcon) ? this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.closefolder.gif") : this.FolderIcon;
                string str4 = string.IsNullOrEmpty(this.OpenFolderIcon) ? this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.openfolder.gif") : this.OpenFolderIcon;
                string str5 = string.IsNullOrEmpty(this.FileIcon) ? this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.closefolder.gif") : this.FileIcon;
                writer.Write("webFXTreeConfig.rootIcon           = \"" + webResourceUrl + "\";\n");
                writer.Write("webFXTreeConfig.openRootIcon       = \"" + str2 + "\";\n");
                writer.Write("webFXTreeConfig.folderIcon         = \"" + str3 + "\";\n");
                writer.Write("webFXTreeConfig.openFolderIcon     = \"" + str4 + "\";\n");
                writer.Write("webFXTreeConfig.fileIcon           = \"" + str5 + "\";\n");
                writer.Write("webFXTreeConfig.containerIcon      = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.closefolder.gif") + "\";\n");
                writer.Write("webFXTreeConfig.linkIcon           = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.outlink.gif") + "\";\n");
                writer.Write("webFXTreeConfig.singleIcon         = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.singlepage.gif") + "\";\n");
                writer.Write("webFXTreeConfig.forbidclosefolder  = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.forbidclosefolder.gif") + "\";\n");
                writer.Write("webFXTreeConfig.forbidopenfolder   = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.forbidopenfolder.gif") + "\";\n");
                writer.Write("webFXTreeConfig.lockclosefolder    = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.lockclosefolder.gif") + "\";\n");
                writer.Write("webFXTreeConfig.lockopenfolder     = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.lockopenfolder.gif") + "\";\n");
                writer.Write("webFXTreeConfig.purviewclosefolder = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.purviewclosefolder.gif") + "\";\n");
                writer.Write("webFXTreeConfig.purviewopenfolder  = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.purviewopenfolder.gif") + "\";\n");
                writer.Write("webFXTreeConfig.halfclosefolder    = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.halfcolsefolder.gif") + "\";\n");
                writer.Write("webFXTreeConfig.halfopenfolder     = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.halfopenfolder.gif") + "\";\n");
                writer.Write("webFXTreeConfig.Archiving          = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.Archiving.gif") + "\";\n");
                writer.Write("webFXTreeConfig.openArchiving      = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.openArchiving.gif") + "\";\n");
                writer.Write("webFXTreeConfig.lMinusIcon         = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.Lminus.png") + "\";\n");
                writer.Write("webFXTreeConfig.lPlusIcon          = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.Lplus.png") + "\";\n");
                writer.Write("webFXTreeConfig.tMinusIcon         = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.Tminus.png") + "\";\n");
                writer.Write("webFXTreeConfig.tPlusIcon          = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.Tplus.png") + "\";\n");
                writer.Write("webFXTreeConfig.iIcon              = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.I.png") + "\";\n");
                writer.Write("webFXTreeConfig.lIcon              = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.L.png") + "\";\n");
                writer.Write("webFXTreeConfig.tIcon              = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.T.png") + "\";\n");
                writer.Write("webFXTreeConfig.blankIcon          = \"" + this.Page.ClientScript.GetWebResourceUrl(type, "SinGooCMS.Control.Resources.blank.png") + "\";\n\n");
            }
            else
            {
                webResourceUrl = string.IsNullOrEmpty(this.RootIcon) ? (this.IconPath + "closefolder.gif") : this.RootIcon;
                if (this.RootIcon == "WebSite")
                {
                    webResourceUrl = this.IconPath + "WebSite.gif";
                }
                else if (this.RootIcon.IndexOf(this.IconPath) < 0)
                {
                    webResourceUrl = this.IconPath + this.RootIcon;
                }
                string str6 = string.IsNullOrEmpty(this.OpenRootIcon) ? (this.IconPath + "closefolder.gif") : this.OpenRootIcon;
                string str7 = string.IsNullOrEmpty(this.FolderIcon) ? (this.IconPath + "closefolder.gif") : this.FolderIcon;
                string str8 = string.IsNullOrEmpty(this.OpenFolderIcon) ? (this.IconPath + "openfolder.gif") : this.OpenFolderIcon;
                string str9 = string.IsNullOrEmpty(this.FileIcon) ? (this.IconPath + "closefolder.gif") : this.FileIcon;
                writer.Write("webFXTreeConfig.rootIcon           = \"" + webResourceUrl + "\";\n");
                writer.Write("webFXTreeConfig.openRootIcon       = \"" + str6 + "\";\n");
                writer.Write("webFXTreeConfig.folderIcon         = \"" + str7 + "\";\n");
                writer.Write("webFXTreeConfig.openFolderIcon     = \"" + str8 + "\";\n");
                writer.Write("webFXTreeConfig.fileIcon           = \"" + str9 + "\";\n");
                writer.Write("webFXTreeConfig.containerIcon      = \"" + this.IconPath + "closefolder.gif\";\n");
                writer.Write("webFXTreeConfig.linkIcon           = \"" + this.IconPath + "outlink.gif\";\n");
                writer.Write("webFXTreeConfig.singleIcon         = \"" + this.IconPath + "singlepage.gif\";\n");
                writer.Write("webFXTreeConfig.forbidclosefolder  = \"" + this.IconPath + "forbidclosefolder.gif\";\n");
                writer.Write("webFXTreeConfig.forbidopenfolder   = \"" + this.IconPath + "forbidopenfolder.gif\";\n");
                writer.Write("webFXTreeConfig.lockclosefolder    = \"" + this.IconPath + "lockclosefolder.gif\";\n");
                writer.Write("webFXTreeConfig.lockopenfolder     = \"" + this.IconPath + "lockopenfolder.gif\";\n");
                writer.Write("webFXTreeConfig.purviewclosefolder = \"" + this.IconPath + "purviewclosefolder.gif\";\n");
                writer.Write("webFXTreeConfig.purviewopenfolder  = \"" + this.IconPath + "purviewopenfolder.gif\";\n");
                writer.Write("webFXTreeConfig.halfclosefolder    = \"" + this.IconPath + "halfcolsefolder.gif\";\n");
                writer.Write("webFXTreeConfig.halfopenfolder     = \"" + this.IconPath + "halfopenfolder.gif\";\n");
                writer.Write("webFXTreeConfig.Archiving          = \"" + this.IconPath + "Archiving.gif\";\n");
                writer.Write("webFXTreeConfig.openArchiving      = \"" + this.IconPath + "openArchiving.gif\";\n");
                writer.Write("webFXTreeConfig.lMinusIcon         = \"" + this.IconPath + "Lminus.png\";\n");
                writer.Write("webFXTreeConfig.lPlusIcon          = \"" + this.IconPath + "Lplus.png\";\n");
                writer.Write("webFXTreeConfig.tMinusIcon         = \"" + this.IconPath + "Tminus.png\";\n");
                writer.Write("webFXTreeConfig.tPlusIcon          = \"" + this.IconPath + "Tplus.png\";\n");
                writer.Write("webFXTreeConfig.iIcon              = \"" + this.IconPath + "I.png\";\n");
                writer.Write("webFXTreeConfig.lIcon              = \"" + this.IconPath + "L.png\";\n");
                writer.Write("webFXTreeConfig.tIcon              = \"" + this.IconPath + "T.png\";\n");
                writer.Write("webFXTreeConfig.blankIcon          = \"" + this.IconPath + "blank.png\";\n\n");
            }
            if (this.CheckBox)
            {
                writer.Write("webFXTreeConfig.checkbox = true ;");
            }
            writer.Write("var rti;\n");
            writer.Write("var tree = new WebFXLoadTree(\"" + this.RootText + "\",\"" + this.XmlSrc + "\",\"" + this.RootAction + "\",\"\",\"" + webResourceUrl + "\",\"" + webResourceUrl + "\",\"" + this.RootTarget + "\");\n");
            writer.Write("document.write(tree);\n");
            writer.Write("if (webFXTreeConfig.expanIds != \"\") {\n");
            writer.Write("    var arrId = webFXTreeConfig.expanIds.split(\",\");\n");
            writer.Write("    for (i=0; i < arrId.length;i++){\n");
            writer.Write("        webFXTreeHandler.toggle(arrId[i]);\n");
            writer.Write("    }\n");
            writer.Write("}\n");
            writer.Write("</script>\n");
        }

        #region 公共属性
        public bool CheckBox
        {
            get
            {
                object obj2 = this.ViewState["CheckBox"];
                return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["CheckBox"] = value;
            }
        }

        public string CssPath
        {
            get
            {
                object obj2 = this.ViewState["CSSPath"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                string relativeUrl = value;
                if (relativeUrl.StartsWith("/"))
                {
                    relativeUrl = "~" + relativeUrl;
                }
                this.ViewState["CSSPath"] = base.ResolveUrl(relativeUrl);
            }
        }

        public string FileIcon
        {
            get
            {
                object obj2 = this.ViewState["FileIcon"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["FileIcon"] = value;
            }
        }

        public string FolderIcon
        {
            get
            {
                object obj2 = this.ViewState["FolderIcon"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["FolderIcon"] = value;
            }
        }

        public string IconPath
        {
            get
            {
                object obj2 = this.ViewState["IconPath"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                string relativeUrl = value;
                if (relativeUrl.StartsWith("/"))
                {
                    relativeUrl = "~" + relativeUrl;
                }
                this.ViewState["IconPath"] = base.ResolveUrl(relativeUrl);
            }
        }

        public bool IsApplyStyleSheetCss
        {
            get
            {
                object obj2 = this.ViewState["IsApplyStyleSheetCss"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["IsApplyStyleSheetCss"] = value;
            }
        }

        public string OpenFolderIcon
        {
            get
            {
                object obj2 = this.ViewState["OpenFolderIcon"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["OpenFolderIcon"] = value;
            }
        }

        public string OpenRootIcon
        {
            get
            {
                object obj2 = this.ViewState["OpenRootIcon"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["OpenRootIcon"] = value;
            }
        }

        public string RootAction
        {
            get
            {
                object obj2 = this.ViewState["RootAction"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RootAction"] = value;
            }
        }

        public string RootIcon
        {
            get
            {
                object obj2 = this.ViewState["RootIcon"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RootIcon"] = value;
            }
        }

        public string RootTarget
        {
            get
            {
                object obj2 = this.ViewState["RootTarget"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "main_right";
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.ViewState["RootTarget"] = "main_right";
                }
                else
                {
                    this.ViewState["RootTarget"] = value;
                }
            }
        }

        public string RootText
        {
            get
            {
                object obj2 = this.ViewState["RootText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RootText"] = value;
            }
        }

        public string ScriptPath
        {
            get
            {
                object obj2 = this.ViewState["ScriptPath"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                string relativeUrl = value;
                if (relativeUrl.StartsWith("/"))
                {
                    relativeUrl = "~" + relativeUrl;
                }
                this.ViewState["ScriptPath"] = base.ResolveUrl(relativeUrl);
            }
        }

        public string XmlSrc
        {
            get
            {
                object obj2 = this.ViewState["XmlSrc"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["XmlSrc"] = value;
            }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public override string SkinID
        {
            get
            {
                return base.SkinID;
            }
            set
            {
                base.SkinID = value;
            }
        }
        #endregion
    }
}