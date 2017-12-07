using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using JsonLeeCMS.Utility;

namespace JsonLeeCMS.Control
{
    [DefaultEvent("PageChanged"), Designer(typeof(PagerDesigner)), ToolboxData("<{0}:AspNetPager runat=server></{0}:AspNetPager>"), ANPDescription("desc_AspNetPager"), DefaultProperty("PageSize"), ParseChildren(false), PersistChildren(false), AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class AspNetPager : Panel, INamingContainer, IPostBackEventHandler, IPostBackDataHandler
    {
        private JsonLeeCMS.Control.AspNetPager cloneFrom;
        private string cssClassName;
        private string currentUrl;
        private static readonly object EventPageChanged = new object();
        private static readonly object EventPageChanging = new object();
        private string inputPageIndex;
        private NameValueCollection urlParams;
        private const string ver = "6.0.0";

        public event EventHandler PageChanged
        {
            add
            {
                base.Events.AddHandler(EventPageChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventPageChanged, value);
            }
        }

        public event PageChangingEventHandler PageChanging
        {
            add
            {
                base.Events.AddHandler(EventPageChanging, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventPageChanging, value);
            }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if ((this.Page != null) && !this.UrlPaging)
            {
                this.Page.VerifyRenderingInServerForm(this);
            }
            base.AddAttributesToRender(writer);
        }

        private void AddToolTip(HtmlTextWriter writer, int pageIndex)
        {
            if (this.ShowNavigationToolTip)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Title, string.Format(this.NavigationToolTipTextFormatString, pageIndex));
            }
        }

        private string BuildUrlString(NameValueCollection col)
        {
            int num;
            string str = "";
            if ((this.urlParams == null) || (this.urlParams.Count <= 0))
            {
                for (num = 0; num < col.Count; num++)
                {
                    str = str + ("&" + col.Keys[num] + "=" + col[num]);
                }
                return (Path.GetFileName(this.currentUrl) + "?" + str.Substring(1));
            }
            NameValueCollection values = new NameValueCollection(this.urlParams);
            string[] allKeys = values.AllKeys;
            for (num = 0; num < allKeys.Length; num++)
            {
                if (allKeys[num] != null)
                {
                    allKeys[num] = allKeys[num].ToLower();
                }
            }
            for (num = 0; num < col.Count; num++)
            {
                if (Array.IndexOf<string>(allKeys, col.Keys[num].ToLower()) < 0)
                {
                    values.Add(col.Keys[num], col[num]);
                }
                else
                {
                    values[col.Keys[num]] = col[num];
                }
            }
            StringBuilder builder = new StringBuilder();
            for (num = 0; num < values.Count; num++)
            {
                if (values.Keys[num] != null)
                {
                    builder.Append("&");
                    builder.Append(values.Keys[num]);
                    builder.Append("=");
                    if (values.Keys[num] == this.UrlPageIndexName)
                    {
                        builder.Append(values[num]);
                    }
                    else
                    {
                        builder.Append(this.Page.Server.UrlEncode(values[num]));
                    }
                }
            }
            return (Path.GetFileName(this.currentUrl) + "?" + builder.ToString().Substring(1));
        }

        private void CreateMoreButton(HtmlTextWriter writer, int pageIndex)
        {
            this.writeSpacingStyle(writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            this.WriteCssClass(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(pageIndex));
            this.AddToolTip(writer, pageIndex);
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            if ((this.PagingButtonType == JsonLeeCMS.Control.PagingButtonType.Image) && (this.MoreButtonType == JsonLeeCMS.Control.PagingButtonType.Image))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + "more" + this.ButtonImageNameExtension + this.ButtonImageExtension);
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                if (this.ButtonImageAlign != ImageAlign.NotSet)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }
            else
            {
                writer.Write("...");
            }
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        private void CreateNavigationButton(HtmlTextWriter writer, string btnname)
        {
            if ((this.ShowFirstLast || ((btnname != "first") && (btnname != "last"))) && (this.ShowPrevNext || ((btnname != "prev") && (btnname != "next"))))
            {
                bool flag;
                int num;
                string str = "";
                bool flag2 = (this.PagingButtonType == JsonLeeCMS.Control.PagingButtonType.Image) && (this.NavigationButtonType == JsonLeeCMS.Control.PagingButtonType.Image);
                if ((btnname == "prev") || (btnname == "first"))
                {
                    flag = this.CurrentPageIndex <= 1;
                    if (this.ShowDisabledButtons || !flag)
                    {
                        num = (btnname == "first") ? 1 : (this.CurrentPageIndex - 1);
                        this.writeSpacingStyle(writer);
                        if (flag2)
                        {
                            if (!flag)
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(num));
                                this.AddToolTip(writer, num);
                                writer.RenderBeginTag(HtmlTextWriterTag.A);
                                writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.ButtonImageNameExtension + this.ButtonImageExtension);
                                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                                if (this.ButtonImageAlign != ImageAlign.NotSet)
                                {
                                    writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                                }
                                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                                writer.RenderEndTag();
                                writer.RenderEndTag();
                            }
                            else
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.DisabledButtonImageNameExtension + this.ButtonImageExtension);
                                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                                if (this.ButtonImageAlign != ImageAlign.NotSet)
                                {
                                    writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                                }
                                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                                writer.RenderEndTag();
                            }
                        }
                        else
                        {
                            str = (btnname == "prev") ? this.PrevPageText : this.FirstPageText;
                            if (flag)
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
                            }
                            else
                            {
                                this.WriteCssClass(writer);
                                this.AddToolTip(writer, num);
                                writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(num));
                            }
                            writer.RenderBeginTag(HtmlTextWriterTag.A);
                            writer.Write(str);
                            writer.RenderEndTag();
                        }
                    }
                }
                else
                {
                    flag = this.CurrentPageIndex >= this.PageCount;
                    if (this.ShowDisabledButtons || !flag)
                    {
                        num = (btnname == "last") ? this.PageCount : (this.CurrentPageIndex + 1);
                        this.writeSpacingStyle(writer);
                        if (flag2)
                        {
                            if (!flag)
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(num));
                                this.AddToolTip(writer, num);
                                writer.RenderBeginTag(HtmlTextWriterTag.A);
                                writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.ButtonImageNameExtension + this.ButtonImageExtension);
                                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                                if (this.ButtonImageAlign != ImageAlign.NotSet)
                                {
                                    writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                                }
                                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                                writer.RenderEndTag();
                                writer.RenderEndTag();
                            }
                            else
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.DisabledButtonImageNameExtension + this.ButtonImageExtension);
                                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                                if (this.ButtonImageAlign != ImageAlign.NotSet)
                                {
                                    writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                                }
                                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                                writer.RenderEndTag();
                            }
                        }
                        else
                        {
                            str = (btnname == "next") ? this.NextPageText : this.LastPageText;
                            if (flag)
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
                            }
                            else
                            {
                                this.WriteCssClass(writer);
                                this.AddToolTip(writer, num);
                                writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(num));
                            }
                            writer.RenderBeginTag(HtmlTextWriterTag.A);
                            writer.Write(str);
                            writer.RenderEndTag();
                        }
                    }
                }
            }
        }

        private void CreateNumericButton(HtmlTextWriter writer, int index)
        {
            bool isCurrent = index == this.CurrentPageIndex;
            if ((this.PagingButtonType == JsonLeeCMS.Control.PagingButtonType.Image) && (this.NumericButtonType == JsonLeeCMS.Control.PagingButtonType.Image))
            {
                this.writeSpacingStyle(writer);
                if (!isCurrent)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(index));
                    this.AddToolTip(writer, index);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    this.CreateNumericImages(writer, index, isCurrent);
                    writer.RenderEndTag();
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.CurrentPageButtonClass))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CurrentPageButtonClass);
                    }
                    if (!string.IsNullOrEmpty(this.CurrentPageButtonStyle))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Style, this.CurrentPageButtonStyle);
                    }
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    this.CreateNumericImages(writer, index, isCurrent);
                    writer.RenderEndTag();
                }
            }
            else
            {
                this.writeSpacingStyle(writer);
                if (isCurrent)
                {
                    if (string.IsNullOrEmpty(this.CurrentPageButtonClass) && string.IsNullOrEmpty(this.CurrentPageButtonStyle))
                    {
                        writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "Bold");
                        writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "red");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.CurrentPageButtonClass))
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CurrentPageButtonClass);
                        }
                        if (!string.IsNullOrEmpty(this.CurrentPageButtonStyle))
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Style, this.CurrentPageButtonStyle);
                        }
                    }
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    if (this.NumericButtonTextFormatString.Length > 0)
                    {
                        writer.Write(string.Format(this.NumericButtonTextFormatString, index));
                    }
                    else
                    {
                        writer.Write(index);
                    }
                    writer.RenderEndTag();
                }
                else
                {
                    this.WriteCssClass(writer);
                    this.AddToolTip(writer, index);
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(index));
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    if (this.NumericButtonTextFormatString.Length > 0)
                    {
                        writer.Write(string.Format(this.NumericButtonTextFormatString, index));
                    }
                    else
                    {
                        writer.Write(index);
                    }
                    writer.RenderEndTag();
                }
            }
        }

        private void CreateNumericImages(HtmlTextWriter writer, int index, bool isCurrent)
        {
            string str = index.ToString();
            for (int i = 0; i < str.Length; i++)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, string.Concat(new object[] { this.ImagePath, str[i], isCurrent ? this.CpiButtonImageNameExtension : this.ButtonImageNameExtension, this.ButtonImageExtension }));
                if (this.ButtonImageAlign != ImageAlign.NotSet)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }
        }

        private string GetCustomInfoText(string origText)
        {
            if (string.IsNullOrEmpty(origText) || (origText.IndexOf('%') < 0))
            {
                return origText;
            }
            string[] array = new string[] { "recordcount", "pagecount", "currentpageindex", "startrecordindex", "endrecordindex", "pagesize", "pagesremain", "recordsremain" };
            StringBuilder builder = new StringBuilder(origText);
            Regex regex = new Regex(@"(?<ph>%(?<pname>\w{8,})%)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(origText))
            {
                string str = match.Groups["pname"].Value.ToLower();
                if (Array.IndexOf<string>(array, str) >= 0)
                {
                    string newValue = null;
                    switch (str)
                    {
                        case "recordcount":
                            newValue = this.RecordCount.ToString();
                            break;

                        case "pagecount":
                            newValue = this.PageCount.ToString();
                            break;

                        case "currentpageindex":
                            newValue = this.CurrentPageIndex.ToString();
                            break;

                        case "startrecordindex":
                            newValue = this.StartRecordIndex.ToString();
                            break;

                        case "endrecordindex":
                            newValue = this.EndRecordIndex.ToString();
                            break;

                        case "pagesize":
                            newValue = this.PageSize.ToString();
                            break;

                        case "pagesremain":
                            newValue = this.PagesRemain.ToString();
                            break;

                        case "recordsremain":
                            newValue = this.RecordsRemain.ToString();
                            break;
                    }
                    if (newValue != null)
                    {
                        builder.Replace(match.Groups["ph"].Value, newValue);
                    }
                }
            }
            return builder.ToString();
        }

        private string GetHrefString(int pageIndex)
        {
            if (!this.UrlPaging)
            {
                return this.Page.ClientScript.GetPostBackClientHyperlink(this, pageIndex.ToString());
            }
            if (this.EnableUrlRewriting)
            {
                Regex regex = new Regex("(?<p>%(?<m>[^%]+)%)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                foreach (Match match in regex.Matches(this.UrlRewritePattern))
                {
                    string newValue = this.urlParams[match.Groups["m"].Value];
                    if (newValue != null)
                    {
                        this.UrlRewritePattern = this.UrlRewritePattern.Replace(match.Groups["p"].Value, newValue);
                    }
                }
                return base.ResolveUrl(string.Format(this.UrlRewritePattern, (pageIndex == -1) ? "\"+pi+\"" : pageIndex.ToString()));
            }
            NameValueCollection col = new NameValueCollection();
            col.Add(this.UrlPageIndexName, (pageIndex == -1) ? "\"+pi+\"" : pageIndex.ToString());
            return this.BuildUrlString(col);
        }

        public virtual bool LoadPostData(string pkey, NameValueCollection pcol)
        {
            string s = pcol[this.UniqueID + "_input"];
            if ((s != null) && (s.Trim().Length > 0))
            {
                try
                {
                    int num = int.Parse(s);
                    if ((num > 0) && (num <= this.PageCount))
                    {
                        this.inputPageIndex = s;
                        this.Page.RegisterRequiresRaiseEvent(this);
                    }
                }
                catch
                {
                }
            }
            return false;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if ((this.CloneFrom != null) && (string.Empty != this.CloneFrom.Trim()))
            {
                JsonLeeCMS.Control.AspNetPager pager = this.Parent.FindControl(this.CloneFrom) as JsonLeeCMS.Control.AspNetPager;
                if (pager == null)
                {
                    string str = JsonLeeCMS.Control.SR.GetString("clonefromexeption");
                    if (str == null)
                    {
                        str = "The control \" %controlID% \" does not exist or is not of type Wuqi.Webdiyer.AspNetPager!";
                    }
                    throw new ArgumentException(str.Replace("%controlID%", this.CloneFrom), "CloneFrom");
                }
                if ((pager.cloneFrom != null) && (this == pager.cloneFrom))
                {
                    string message = JsonLeeCMS.Control.SR.GetString("recusiveclonefrom");
                    if (message == null)
                    {
                        message = "Invalid value for the CloneFrom property, AspNetPager controls can not to be cloned recursively!";
                    }
                    throw new ArgumentException(message, "CloneFrom");
                }
                this.cloneFrom = pager;
                this.CssClass = this.cloneFrom.CssClass;
                this.Width = this.cloneFrom.Width;
                this.Height = this.cloneFrom.Height;
                this.HorizontalAlign = this.cloneFrom.HorizontalAlign;
                this.BackColor = this.cloneFrom.BackColor;
                this.BackImageUrl = this.cloneFrom.BackImageUrl;
                this.BorderColor = this.cloneFrom.BorderColor;
                this.BorderStyle = this.cloneFrom.BorderStyle;
                this.BorderWidth = this.cloneFrom.BorderWidth;
                this.Font.CopyFrom(this.cloneFrom.Font);
                this.ForeColor = this.cloneFrom.ForeColor;
                this.EnableViewState = this.cloneFrom.EnableViewState;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.UrlPaging)
            {
                this.currentUrl = this.Page.Request.Path;
                this.urlParams = this.Page.Request.QueryString;
                if (!this.Page.IsPostBack && (this.cloneFrom == null))
                {
                    string str = this.Page.Request.QueryString[this.UrlPageIndexName];
                    int newPageIndex = 1;
                    if (!string.IsNullOrEmpty(str))
                    {
                        try
                        {
                            newPageIndex = int.Parse(str);
                        }
                        catch
                        {
                        }
                    }
                    PageChangingEventArgs args = new PageChangingEventArgs(newPageIndex);
                    this.OnPageChanging(args);
                }
            }
            else
            {
                this.inputPageIndex = this.Page.Request.Form[this.UniqueID + "_input"];
            }
            base.OnLoad(e);
        }

        protected virtual void OnPageChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventPageChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnPageChanging(PageChangingEventArgs e)
        {
            PageChangingEventHandler handler = (PageChangingEventHandler)base.Events[EventPageChanging];
            if (handler != null)
            {
                handler(this, e);
                if (!e.Cancel || this.UrlPaging)
                {
                    this.CurrentPageIndex = e.NewPageIndex;
                    this.OnPageChanged(EventArgs.Empty);
                }
            }
            else
            {
                this.CurrentPageIndex = e.NewPageIndex;
                this.OnPageChanged(EventArgs.Empty);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if ((this.PageCount > 1) && ((this.ShowInputBox == JsonLeeCMS.Control.ShowInputBox.Always) || ((this.ShowInputBox == JsonLeeCMS.Control.ShowInputBox.Auto) && (this.PageCount >= this.ShowBoxThreshold))))
            {
                StringBuilder builder = new StringBuilder("<script language=\"Javascript\"><!--\n");
                string str = JsonLeeCMS.Control.SR.GetString("checkinputscript");
                if (str != null)
                {
                    str = str.Replace("%PageIndexOutOfRangeErrorMessage%", this.PageIndexOutOfRangeErrorMessage).Replace("%InvalidPageIndexErrorMessage%", this.InvalidPageIndexErrorMessage);
                }
                builder.Append(str).Append("\n");
                string str2 = JsonLeeCMS.Control.SR.GetString("keydownscript");
                builder.Append(str2).Append("\n");
                if (this.UrlPaging)
                {
                    builder.Append("function ANP_goToPage(inputEl){var pi=inputEl.value;");
                    builder.Append("location.href=\"").Append(this.GetHrefString(-1)).Append("\";}");
                }
                builder.Append("\n--></script>");
                Type type = base.GetType();
                ClientScriptManager clientScript = this.Page.ClientScript;
                if (!clientScript.IsClientScriptBlockRegistered(type, "anp_script"))
                {
                    clientScript.RegisterClientScriptBlock(type, "anp_script", builder.ToString());
                }
            }
            base.OnPreRender(e);
        }

        public void RaisePostBackEvent(string args)
        {
            int currentPageIndex = this.CurrentPageIndex;
            try
            {
                if ((args == null) || (args == ""))
                {
                    args = this.inputPageIndex;
                }
                currentPageIndex = int.Parse(args);
            }
            catch
            {
            }
            PageChangingEventArgs e = new PageChangingEventArgs(currentPageIndex);
            if (this.cloneFrom != null)
            {
                this.cloneFrom.OnPageChanging(e);
            }
            else
            {
                this.OnPageChanging(e);
            }
        }

        public virtual void RaisePostDataChangedEvent()
        {
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            bool flag = (this.PageCount > 1) || ((this.PageCount <= 1) && this.AlwaysShow);
            writer.WriteLine();
            if (!flag)
            {
                writer.Write("<!--");
                writer.Write(JsonLeeCMS.Control.SR.GetString("autohideinfo"));
                writer.Write("-->");
            }
            else
            {
                base.RenderBeginTag(writer);
                if ((this.ShowCustomInfoSection == JsonLeeCMS.Control.ShowCustomInfoSection.Left) || (this.ShowCustomInfoSection == JsonLeeCMS.Control.ShowCustomInfoSection.Right))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, base.Style.Value);
                    if (this.Height != Unit.Empty)
                    {
                        writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
                    }
                    writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                    writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                    writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    this.WriteCellAttributes(writer, true);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                }
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if ((this.PageCount > 1) || this.AlwaysShow)
            {
                if (this.ShowCustomInfoSection == JsonLeeCMS.Control.ShowCustomInfoSection.Left)
                {
                    writer.Write(this.GetCustomInfoText(this.CustomInfoHTML));
                    writer.RenderEndTag();
                    this.WriteCellAttributes(writer, false);
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                }
                int num = (this.CurrentPageIndex - 1) / this.NumericButtonCount;
                int pageIndex = num * this.NumericButtonCount;
                int num3 = ((pageIndex + this.NumericButtonCount) > this.PageCount) ? this.PageCount : (pageIndex + this.NumericButtonCount);
                this.CreateNavigationButton(writer, "first");
                this.CreateNavigationButton(writer, "prev");
                if (this.ShowPageIndex)
                {
                    if (this.CurrentPageIndex > this.NumericButtonCount)
                    {
                        this.CreateMoreButton(writer, pageIndex);
                    }
                    for (int i = pageIndex + 1; i <= num3; i++)
                    {
                        this.CreateNumericButton(writer, i);
                    }
                    if ((this.PageCount > this.NumericButtonCount) && (num3 < this.PageCount))
                    {
                        this.CreateMoreButton(writer, num3 + 1);
                    }
                }
                this.CreateNavigationButton(writer, "next");
                this.CreateNavigationButton(writer, "last");
                if ((this.ShowInputBox == JsonLeeCMS.Control.ShowInputBox.Always) || ((this.ShowInputBox == JsonLeeCMS.Control.ShowInputBox.Auto) && (this.PageCount >= this.ShowBoxThreshold)))
                {
                    string str = this.UniqueID + "_input";
                    writer.Write("&nbsp;&nbsp;");
                    if (!string.IsNullOrEmpty(this.TextBeforeInputBox))
                    {
                        writer.Write(this.TextBeforeInputBox);
                    }
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "30px");
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, this.CurrentPageIndex.ToString());
                    if (!string.IsNullOrEmpty(this.InputBoxStyle))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Style, this.InputBoxStyle);
                    }
                    if (!string.IsNullOrEmpty(this.InputBoxClass))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, this.InputBoxClass);
                    }
                    if ((this.PageCount <= 1) && this.AlwaysShow)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "true");
                    }
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, str);
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, str);
                    string str2 = "ANP_checkInput('" + str + "'," + this.PageCount.ToString() + ")";
                    string str3 = "ANP_keydown(event,'" + this.UniqueID + "_btn');";
                    string str4 = "if(" + str2 + "){ANP_goToPage(document.getElementById('" + str + "'));}";
                    writer.AddAttribute("onkeydown", str3);
                    writer.RenderBeginTag(HtmlTextWriterTag.Input);
                    writer.RenderEndTag();
                    if (!string.IsNullOrEmpty(this.TextAfterInputBox))
                    {
                        writer.Write(this.TextAfterInputBox);
                    }
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, this.UrlPaging ? "Button" : "Submit");
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID + "_btn");
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, this.SubmitButtonText);
                    if (!string.IsNullOrEmpty(this.SubmitButtonClass))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, this.SubmitButtonClass);
                    }
                    if (!string.IsNullOrEmpty(this.SubmitButtonStyle))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Style, this.SubmitButtonStyle);
                    }
                    if ((this.PageCount <= 1) && this.AlwaysShow)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
                    }
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, this.UrlPaging ? str4 : ("return " + str2));
                    writer.RenderBeginTag(HtmlTextWriterTag.Input);
                    writer.RenderEndTag();
                }
                if (this.ShowCustomInfoSection == JsonLeeCMS.Control.ShowCustomInfoSection.Right)
                {
                    writer.RenderEndTag();
                    this.WriteCellAttributes(writer, false);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(this.GetCustomInfoText(this.CustomInfoHTML));
                }
            }
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            if ((this.PageCount > 1) || ((this.PageCount <= 1) && this.AlwaysShow))
            {
                if ((this.ShowCustomInfoSection == JsonLeeCMS.Control.ShowCustomInfoSection.Left) || (this.ShowCustomInfoSection == JsonLeeCMS.Control.ShowCustomInfoSection.Right))
                {
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                base.RenderEndTag(writer);
            }
            writer.WriteLine();
            writer.WriteLine();
        }

        private void WriteCellAttributes(HtmlTextWriter writer, bool leftCell)
        {
            string str = this.CustomInfoSectionWidth.ToString();
            if (((this.ShowCustomInfoSection == JsonLeeCMS.Control.ShowCustomInfoSection.Left) && leftCell) || ((this.ShowCustomInfoSection == JsonLeeCMS.Control.ShowCustomInfoSection.Right) && !leftCell))
            {
                if ((this.CustomInfoClass != null) && (this.CustomInfoClass.Trim().Length > 0))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CustomInfoClass);
                }
                if ((this.CustomInfoStyle != null) && (this.CustomInfoStyle.Trim().Length > 0))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, this.CustomInfoStyle);
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Valign, "bottom");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, str);
                writer.AddAttribute(HtmlTextWriterAttribute.Align, this.CustomInfoTextAlign.ToString().ToLower());
            }
            else
            {
                if (this.CustomInfoSectionWidth.Type == UnitType.Percentage)
                {
                    str = Unit.Percentage(100.0 - this.CustomInfoSectionWidth.Value).ToString();
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Width, str);
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Valign, "bottom");
                writer.AddAttribute(HtmlTextWriterAttribute.Align, this.HorizontalAlign.ToString().ToLower());
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "true");
        }

        private void WriteCssClass(HtmlTextWriter writer)
        {
            if ((this.cssClassName != null) && (this.cssClassName.Trim().Length > 0))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.cssClassName);
            }
        }

        private void writeSpacingStyle(HtmlTextWriter writer)
        {
            if (this.PagingButtonSpacing.Value != 0.0)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.MarginRight, this.PagingButtonSpacing.ToString());
            }
        }

        [Themeable(false), ANPDescription("desc_AlwaysShow"), Category("Behavior"), DefaultValue(false), Browsable(true)]
        public bool AlwaysShow
        {
            get
            {
                object obj2 = this.ViewState["AlwaysShow"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return ((this.cloneFrom != null) && this.cloneFrom.AlwaysShow);
            }
            set
            {
                this.ViewState["AlwaysShow"] = value;
            }
        }

        [DefaultValue(0), Category("Appearance"), ANPDescription("desc_ButtonImageAlign"), Browsable(true)]
        public ImageAlign ButtonImageAlign
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ButtonImageAlign;
                }
                object obj2 = this.ViewState["ButtonImageAlign"];
                if (obj2 != null)
                {
                    return (ImageAlign)obj2;
                }
                return ImageAlign.NotSet;
            }
            set
            {
                if ((value != ImageAlign.Right) && (value != ImageAlign.Left))
                {
                    this.ViewState["ButtonImageAlign"] = value;
                }
            }
        }

        [Category("Appearance"), DefaultValue(".gif"), ANPDescription("desc_ButtonImageExtension"), Themeable(false), Browsable(true)]
        public string ButtonImageExtension
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ButtonImageExtension;
                }
                object obj2 = this.ViewState["ButtonImageExtension"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return ".gif";
            }
            set
            {
                string str = value.Trim();
                this.ViewState["ButtonImageExtension"] = str.StartsWith(".") ? str : ("." + str);
            }
        }

        [DefaultValue((string)null), Browsable(true), ANPDescription("desc_ButtonImageNameExtension"), Category("Appearance"), Themeable(false)]
        public string ButtonImageNameExtension
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ButtonImageNameExtension;
                }
                object obj2 = this.ViewState["ButtonImageNameExtension"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return null;
            }
            set
            {
                this.ViewState["ButtonImageNameExtension"] = value;
            }
        }

        [TypeConverter(typeof(AspNetPagerIDConverter)), Category("Behavior"), DefaultValue(false), ANPDescription("desc_CloneFrom"), Themeable(false), Browsable(true)]
        public string CloneFrom
        {
            get
            {
                return (string)this.ViewState["CloneFrom"];
            }
            set
            {
                if ((value != null) && (string.Empty == value.Trim()))
                {
                    throw new ArgumentNullException("CloneFrom", "The Value of property CloneFrom can not be empty string!");
                }
                if (this.ID.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("The property value of CloneFrom can not be set to control itself!", "CloneFrom");
                }
                this.ViewState["CloneFrom"] = value;
            }
        }

        [DefaultValue((string)null), Category("Appearance"), ANPDescription("desc_CpiButtonImageNameExtension"), Themeable(false), Browsable(true)]
        public string CpiButtonImageNameExtension
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.CpiButtonImageNameExtension;
                }
                object obj2 = this.ViewState["CpiButtonImageNameExtension"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return this.ButtonImageNameExtension;
            }
            set
            {
                this.ViewState["CpiButtonImageNameExtension"] = value;
            }
        }

        [Category("Appearance"), DefaultValue((string)null), Browsable(true), ANPDescription("desc_CssClass")]
        public override string CssClass
        {
            get
            {
                return base.CssClass;
            }
            set
            {
                base.CssClass = value;
                this.cssClassName = value;
            }
        }

        [Browsable(true), Category("Appearance"), ANPDescription("desc_CurrentPageButtonClass"), DefaultValue((string)null)]
        public string CurrentPageButtonClass
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.CurrentPageButtonClass;
                }
                object obj2 = this.ViewState["CPBClass"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return null;
            }
            set
            {
                this.ViewState["CPBClass"] = value;
            }
        }

        [Category("Appearance"), Browsable(true), ANPDescription("desc_CurrentPageButtonStyle"), DefaultValue((string)null)]
        public string CurrentPageButtonStyle
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.CurrentPageButtonStyle;
                }
                object obj2 = this.ViewState["CPBStyle"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return null;
            }
            set
            {
                this.ViewState["CPBStyle"] = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), ReadOnly(true), Browsable(false), ANPDescription("desc_CurrentPageIndex"), ANPCategory("cat_Paging"), DefaultValue(1)]
        public int CurrentPageIndex
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.CurrentPageIndex;
                }
                object obj2 = this.ViewState["CurrentPageIndex"];
                int num = (obj2 == null) ? WebUtils.GetQueryInt(this.UrlPageIndexName, 1) : ((int)obj2);
                if ((num > this.PageCount) && (this.PageCount > 0))
                {
                    return this.PageCount;
                }
                if (num < 1)
                {
                    return 1;
                }
                return num;
            }
            set
            {
                int pageCount = value;
                if (pageCount < 1)
                {
                    pageCount = 1;
                }
                else if (pageCount > this.PageCount)
                {
                    pageCount = this.PageCount;
                }
                this.ViewState["CurrentPageIndex"] = pageCount;
            }
        }

        [Category("Appearance"), Browsable(true), ANPDescription("desc_CustomInfoClass"), DefaultValue((string)null)]
        public string CustomInfoClass
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.CustomInfoClass;
                }
                object obj2 = this.ViewState["CustomInfoClass"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return this.CssClass;
            }
            set
            {
                this.ViewState["CustomInfoClass"] = value;
            }
        }

        [DefaultValue((string)null), Themeable(false), ANPDescription("desc_CustomInfoHTML"), Category("Appearance"), Browsable(true)]
        public string CustomInfoHTML
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.CustomInfoHTML;
                }
                object obj2 = this.ViewState["CustomInfoText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return null;
            }
            set
            {
                this.ViewState["CustomInfoText"] = value;
            }
        }

        [Browsable(true), ANPDescription("desc_CustomInfoSectionWidth"), Category("Appearance"), DefaultValue(typeof(Unit), "40%")]
        public Unit CustomInfoSectionWidth
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.CustomInfoSectionWidth;
                }
                object obj2 = this.ViewState["CustomInfoSectionWidth"];
                if (obj2 != null)
                {
                    return (Unit)obj2;
                }
                return Unit.Percentage(40.0);
            }
            set
            {
                this.ViewState["CustomInfoSectionWidth"] = value;
            }
        }

        [Browsable(true), ANPDescription("desc_CustomInfoStyle"), Category("Appearance"), DefaultValue((string)null)]
        public string CustomInfoStyle
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.CustomInfoStyle;
                }
                object obj2 = this.ViewState["CustomInfoStyle"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return base.Style.Value;
            }
            set
            {
                this.ViewState["CustomInfoStyle"] = value;
            }
        }

        [ANPDescription("desc_CustomInfoTextAlign"), Browsable(true), Category("Appearance"), DefaultValue(1)]
        public HorizontalAlign CustomInfoTextAlign
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.CustomInfoTextAlign;
                }
                object obj2 = this.ViewState["CustomInfoTextAlign"];
                if (obj2 != null)
                {
                    return (HorizontalAlign)obj2;
                }
                return HorizontalAlign.Left;
            }
            set
            {
                this.ViewState["CustomInfoTextAlign"] = value;
            }
        }

        [Category("Appearance"), Browsable(true), ANPDescription("desc_DisabledButtonImageNameExtension"), Themeable(false), DefaultValue((string)null)]
        public string DisabledButtonImageNameExtension
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.DisabledButtonImageNameExtension;
                }
                object obj2 = this.ViewState["DisabledButtonImageNameExtension"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return this.ButtonImageNameExtension;
            }
            set
            {
                this.ViewState["DisabledButtonImageNameExtension"] = value;
            }
        }

        public override bool EnableTheming
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.EnableTheming;
                }
                return base.EnableTheming;
            }
            set
            {
                base.EnableTheming = value;
            }
        }

        [DefaultValue(false), ANPDescription("desc_EnableUrlWriting"), Browsable(true), Themeable(false), Category("Behavior")]
        public bool EnableUrlRewriting
        {
            get
            {
                object obj2 = this.ViewState["UrlRewriting"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return ((this.cloneFrom != null) && this.cloneFrom.EnableUrlRewriting);
            }
            set
            {
                this.ViewState["UrlRewriting"] = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int EndRecordIndex
        {
            get
            {
                return (this.RecordCount - this.RecordsRemain);
            }
        }

        [ANPDescription("desc_FirstPageText"), DefaultValue("&lt;&lt;"), Browsable(true), Themeable(false), ANPCategory("cat_Navigation")]
        public string FirstPageText
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.FirstPageText;
                }
                object obj2 = this.ViewState["FirstPageText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "<font face=\"webdings\">9</font>";
            }
            set
            {
                this.ViewState["FirstPageText"] = value;
            }
        }

        [ANPDescription("desc_ImagePath"), Browsable(true), Category("Appearance"), DefaultValue((string)null)]
        public string ImagePath
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ImagePath;
                }
                string relativeUrl = (string)this.ViewState["ImagePath"];
                if (relativeUrl != null)
                {
                    relativeUrl = base.ResolveUrl(relativeUrl);
                }
                return relativeUrl;
            }
            set
            {
                string str = value.Trim().Replace(@"\", "/");
                this.ViewState["ImagePath"] = str.EndsWith("/") ? str : (str + "/");
            }
        }

        [Browsable(true), ANPDescription("desc_InputBoxClass"), ANPCategory("cat_InputBox"), DefaultValue((string)null)]
        public string InputBoxClass
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.InputBoxClass;
                }
                object obj2 = this.ViewState["InputBoxClass"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return null;
            }
            set
            {
                if (value.Trim().Length > 0)
                {
                    this.ViewState["InputBoxClass"] = value;
                }
            }
        }

        [Browsable(true), ANPDescription("desc_InputBoxStyle"), ANPCategory("cat_InputBox"), DefaultValue((string)null)]
        public string InputBoxStyle
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.InputBoxStyle;
                }
                object obj2 = this.ViewState["InputBoxStyle"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return null;
            }
            set
            {
                if (value.Trim().Length > 0)
                {
                    this.ViewState["InputBoxStyle"] = value;
                }
            }
        }

        [ANPDescription("desc_InvalidPIErrorMsg"), Themeable(false), Browsable(true), ANPDefaultValue("def_InvalidPIErrorMsg"), Category("Data")]
        public string InvalidPageIndexErrorMessage
        {
            get
            {
                object obj2 = this.ViewState["InvalidPIErrorMsg"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.InvalidPageIndexErrorMessage;
                }
                return JsonLeeCMS.Control.SR.GetString("def_InvalidPIErrorMsg");
            }
            set
            {
                this.ViewState["InvalidPIErrorMsg"] = value;
            }
        }

        [ANPCategory("cat_Navigation"), DefaultValue("&gt;&gt;"), Browsable(true), Themeable(false), ANPDescription("desc_LastPageText")]
        public string LastPageText
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.LastPageText;
                }
                object obj2 = this.ViewState["LastPageText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "<font face=\"webdings\">:</font>";
            }
            set
            {
                this.ViewState["LastPageText"] = value;
            }
        }

        [Themeable(false), ANPDescription("desc_MoreButtonType"), ANPCategory("cat_Navigation"), DefaultValue(0), Browsable(true)]
        public JsonLeeCMS.Control.PagingButtonType MoreButtonType
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.MoreButtonType;
                }
                object obj2 = this.ViewState["MoreButtonType"];
                if (obj2 != null)
                {
                    return (JsonLeeCMS.Control.PagingButtonType)obj2;
                }
                return this.PagingButtonType;
            }
            set
            {
                this.ViewState["MoreButtonType"] = value;
            }
        }

        [ANPDescription("desc_NavigationButtonType"), ANPCategory("cat_Navigation"), Themeable(false), DefaultValue(0), Browsable(true)]
        public JsonLeeCMS.Control.PagingButtonType NavigationButtonType
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.NavigationButtonType;
                }
                object obj2 = this.ViewState["NavigationButtonType"];
                if (obj2 != null)
                {
                    return (JsonLeeCMS.Control.PagingButtonType)obj2;
                }
                return this.PagingButtonType;
            }
            set
            {
                this.ViewState["NavigationButtonType"] = value;
            }
        }

        [Browsable(true), Themeable(false), ANPCategory("cat_Navigation"), ANPDefaultValue("def_NavigationToolTipTextFormatString"), ANPDescription("desc_NavigationToolTipTextFormatString")]
        public string NavigationToolTipTextFormatString
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.NavigationToolTipTextFormatString;
                }
                object obj2 = this.ViewState["NvToolTipFormatString"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                if (this.ShowNavigationToolTip)
                {
                    return JsonLeeCMS.Control.SR.GetString("def_NavigationToolTipTextFormatString");
                }
                return null;
            }
            set
            {
                string str = value;
                if ((str.Trim().Length < 1) && (str.IndexOf("{0}") < 0))
                {
                    str = "{0}";
                }
                this.ViewState["NvToolTipFormatString"] = str;
            }
        }

        [ANPCategory("cat_Navigation"), ANPDescription("desc_NextPageText"), DefaultValue("&gt;"), Themeable(false), Browsable(true)]
        public string NextPageText
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.NextPageText;
                }
                object obj2 = this.ViewState["NextPageText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "<font face=\"webdings\">4</font>";
            }
            set
            {
                this.ViewState["NextPageText"] = value;
            }
        }

        [Themeable(false), ANPCategory("cat_Navigation"), Browsable(true), DefaultValue(10), ANPDescription("desc_NumericButtonCount")]
        public int NumericButtonCount
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.NumericButtonCount;
                }
                object obj2 = this.ViewState["NumericButtonCount"];
                if (obj2 != null)
                {
                    return (int)obj2;
                }
                return 10;
            }
            set
            {
                this.ViewState["NumericButtonCount"] = value;
            }
        }

        [Themeable(false), Browsable(true), DefaultValue(""), ANPCategory("cat_Navigation"), ANPDescription("desc_NBTFormatString")]
        public string NumericButtonTextFormatString
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.NumericButtonTextFormatString;
                }
                object obj2 = this.ViewState["NumericButtonTextFormatString"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["NumericButtonTextFormatString"] = value;
            }
        }

        [Browsable(true), ANPDescription("desc_NumericButtonType"), Themeable(false), DefaultValue(0), ANPCategory("cat_Navigation")]
        public JsonLeeCMS.Control.PagingButtonType NumericButtonType
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.NumericButtonType;
                }
                object obj2 = this.ViewState["NumericButtonType"];
                if (obj2 != null)
                {
                    return (JsonLeeCMS.Control.PagingButtonType)obj2;
                }
                return this.PagingButtonType;
            }
            set
            {
                this.ViewState["NumericButtonType"] = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int PageCount
        {
            get
            {
                if (this.RecordCount == 0)
                {
                    return 1;
                }
                return (int)Math.Ceiling((double)(((double)this.RecordCount) / ((double)this.PageSize)));
            }
        }

        [Browsable(true), Themeable(false), ANPDescription("desc_PIOutOfRangeMsg"), ANPDefaultValue("def_PIOutOfRangerMsg"), Category("Data")]
        public string PageIndexOutOfRangeErrorMessage
        {
            get
            {
                object obj2 = this.ViewState["PIOutOfRangeErrorMsg"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.PageIndexOutOfRangeErrorMessage;
                }
                return JsonLeeCMS.Control.SR.GetString("def_PIOutOfRangerMsg");
            }
            set
            {
                this.ViewState["PIOutOfRangeErrorMsg"] = value;
            }
        }

        [DefaultValue(10), Browsable(true), ANPDescription("desc_PageSize"), ANPCategory("cat_Paging")]
        public int PageSize
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.PageSize;
                }
                object obj2 = this.ViewState["PageSize"];
                if (obj2 != null)
                {
                    return (int)obj2;
                }
                return 10;
            }
            set
            {
                this.ViewState["PageSize"] = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PagesRemain
        {
            get
            {
                return (this.PageCount - this.CurrentPageIndex);
            }
        }

        [DefaultValue(typeof(Unit), "5px"), Themeable(false), ANPCategory("cat_Navigation"), Browsable(true), ANPDescription("desc_PagingButtonSpacing")]
        public Unit PagingButtonSpacing
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.PagingButtonSpacing;
                }
                object obj2 = this.ViewState["PagingButtonSpacing"];
                if (obj2 != null)
                {
                    return Unit.Parse(obj2.ToString());
                }
                return Unit.Pixel(5);
            }
            set
            {
                this.ViewState["PagingButtonSpacing"] = value;
            }
        }

        [Themeable(false), Browsable(true), ANPDescription("desc_PagingButtonType"), DefaultValue(0), ANPCategory("cat_Navigation")]
        public JsonLeeCMS.Control.PagingButtonType PagingButtonType
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.PagingButtonType;
                }
                object obj2 = this.ViewState["PagingButtonType"];
                if (obj2 != null)
                {
                    return (JsonLeeCMS.Control.PagingButtonType)obj2;
                }
                return JsonLeeCMS.Control.PagingButtonType.Text;
            }
            set
            {
                this.ViewState["PagingButtonType"] = value;
            }
        }

        [Browsable(true), ANPDescription("desc_PrevPageText"), ANPCategory("cat_Navigation"), DefaultValue("&lt;"), Themeable(false)]
        public string PrevPageText
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.PrevPageText;
                }
                object obj2 = this.ViewState["PrevPageText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "<font face=\"webdings\">3</font>";
            }
            set
            {
                this.ViewState["PrevPageText"] = value;
            }
        }

        [ANPDescription("desc_RecordCount"), DefaultValue(0), Browsable(false), Category("Data")]
        public int RecordCount
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.RecordCount;
                }
                object obj2 = this.ViewState["Recordcount"];
                if (obj2 != null)
                {
                    return (int)obj2;
                }
                return 0;
            }
            set
            {
                this.ViewState["Recordcount"] = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RecordsRemain
        {
            get
            {
                if (this.CurrentPageIndex < this.PageCount)
                {
                    return (this.RecordCount - (this.CurrentPageIndex * this.PageSize));
                }
                return 0;
            }
        }

        [ANPDescription("desc_ShowBoxThreshold"), Themeable(false), DefaultValue(30), ANPCategory("cat_InputBox"), Browsable(true)]
        public int ShowBoxThreshold
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ShowBoxThreshold;
                }
                object obj2 = this.ViewState["ShowBoxThreshold"];
                if (obj2 != null)
                {
                    return (int)obj2;
                }
                return 30;
            }
            set
            {
                this.ViewState["ShowBoxThreshold"] = value;
            }
        }

        [DefaultValue(0), ANPDescription("desc_ShowCustomInfoSection"), Browsable(true), Themeable(false), Category("Appearance")]
        public JsonLeeCMS.Control.ShowCustomInfoSection ShowCustomInfoSection
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ShowCustomInfoSection;
                }
                object obj2 = this.ViewState["ShowCustomInfoSection"];
                if (obj2 != null)
                {
                    return (JsonLeeCMS.Control.ShowCustomInfoSection)obj2;
                }
                return JsonLeeCMS.Control.ShowCustomInfoSection.Never;
            }
            set
            {
                this.ViewState["ShowCustomInfoSection"] = value;
            }
        }

        [Browsable(true), DefaultValue(true), Themeable(false), ANPCategory("cat_Navigation"), ANPDescription("desc_ShowDisabledButtons")]
        public bool ShowDisabledButtons
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ShowDisabledButtons;
                }
                object obj2 = this.ViewState["ShowDisabledButtons"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowDisabledButtons"] = value;
            }
        }

        [ANPCategory("cat_Navigation"), ANPDescription("desc_ShowFirstLast"), DefaultValue(true), Browsable(true), Themeable(false)]
        public bool ShowFirstLast
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ShowFirstLast;
                }
                object obj2 = this.ViewState["ShowFirstLast"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowFirstLast"] = value;
            }
        }

        [Browsable(true), Themeable(false), ANPDescription("desc_ShowInputBox"), DefaultValue(1), ANPCategory("cat_InputBox")]
        public JsonLeeCMS.Control.ShowInputBox ShowInputBox
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ShowInputBox;
                }
                object obj2 = this.ViewState["ShowInputBox"];
                if (obj2 != null)
                {
                    return (JsonLeeCMS.Control.ShowInputBox)obj2;
                }
                return JsonLeeCMS.Control.ShowInputBox.Auto;
            }
            set
            {
                this.ViewState["ShowInputBox"] = value;
            }
        }

        [ANPCategory("cat_Navigation"), Browsable(true), DefaultValue(false), ANPDescription("desc_ShowNavigationToolTip"), Themeable(false)]
        public bool ShowNavigationToolTip
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ShowNavigationToolTip;
                }
                object obj2 = this.ViewState["ShowNvToolTip"];
                return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["ShowNvToolTip"] = value;
            }
        }

        [DefaultValue(true), Browsable(true), Themeable(false), ANPDescription("desc_ShowPageIndex"), ANPCategory("cat_Navigation")]
        public bool ShowPageIndex
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ShowPageIndex;
                }
                object obj2 = this.ViewState["ShowPageIndex"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowPageIndex"] = value;
            }
        }

        [ANPDescription("desc_ShowPrevNext"), ANPCategory("cat_Navigation"), DefaultValue(true), Themeable(false), Browsable(true)]
        public bool ShowPrevNext
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.ShowPrevNext;
                }
                object obj2 = this.ViewState["ShowPrevNext"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowPrevNext"] = value;
            }
        }

        public override string SkinID
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.SkinID;
                }
                return base.SkinID;
            }
            set
            {
                base.SkinID = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int StartRecordIndex
        {
            get
            {
                return (((this.CurrentPageIndex - 1) * this.PageSize) + 1);
            }
        }

        [DefaultValue((string)null), ANPDescription("desc_SubmitButtonClass"), ANPCategory("cat_InputBox"), Browsable(true)]
        public string SubmitButtonClass
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.SubmitButtonClass;
                }
                object obj2 = this.ViewState["SubmitButtonClass"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return null;
            }
            set
            {
                this.ViewState["SubmitButtonClass"] = value;
            }
        }

        [ANPCategory("cat_InputBox"), DefaultValue((string)null), ANPDescription("desc_SubmitButtonStyle"), Browsable(true)]
        public string SubmitButtonStyle
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.SubmitButtonStyle;
                }
                object obj2 = this.ViewState["SubmitButtonStyle"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return null;
            }
            set
            {
                this.ViewState["SubmitButtonStyle"] = value;
            }
        }

        [DefaultValue("go"), ANPDescription("desc_SubmitButtonText"), Themeable(false), ANPCategory("cat_InputBox"), Browsable(true)]
        public string SubmitButtonText
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.SubmitButtonText;
                }
                object obj2 = this.ViewState["SubmitButtonText"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "go";
            }
            set
            {
                if ((value == null) || (value.Trim().Length == 0))
                {
                    value = "go";
                }
                this.ViewState["SubmitButtonText"] = value;
            }
        }

        [ANPDescription("desc_TextAfterInputBox"), Themeable(false), DefaultValue((string)null), ANPCategory("cat_InputBox"), Browsable(true)]
        public string TextAfterInputBox
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.TextAfterInputBox;
                }
                object obj2 = this.ViewState["TextAfterInputBox"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return null;
            }
            set
            {
                this.ViewState["TextAfterInputBox"] = value;
            }
        }

        [Browsable(true), ANPDescription("desc_TextBeforeInputBox"), Themeable(false), ANPCategory("cat_InputBox"), DefaultValue((string)null)]
        public string TextBeforeInputBox
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.TextBeforeInputBox;
                }
                object obj2 = this.ViewState["TextBeforeInputBox"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return null;
            }
            set
            {
                this.ViewState["TextBeforeInputBox"] = value;
            }
        }

        [Browsable(true), ANPDescription("desc_UrlPageIndexName"), DefaultValue("page"), ANPCategory("cat_Paging")]
        public string UrlPageIndexName
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.UrlPageIndexName;
                }
                object obj2 = this.ViewState["UrlPageIndexName"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return "page";
            }
            set
            {
                this.ViewState["UrlPageIndexName"] = value;
            }
        }

        [ANPDescription("desc_UrlPaging"), Browsable(true), ANPCategory("cat_Paging"), DefaultValue(false)]
        public bool UrlPaging
        {
            get
            {
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.UrlPaging;
                }
                object obj2 = this.ViewState["UrlPaging"];
                return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["UrlPaging"] = value;
            }
        }

        [ANPDescription("desc_UrlRewritePattern"), DefaultValue((string)null), Browsable(true), Themeable(false), Category("Behavior")]
        public string UrlRewritePattern
        {
            get
            {
                object obj2 = this.ViewState["URPattern"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                if (this.cloneFrom != null)
                {
                    return this.cloneFrom.UrlRewritePattern;
                }
                if (this.EnableUrlRewriting)
                {
                    string filePath = this.Page.Request.FilePath;
                    return (Path.GetFileNameWithoutExtension(filePath) + "_{0}" + Path.GetExtension(filePath));
                }
                return null;
            }
            set
            {
                this.ViewState["URPattern"] = value;
            }
        }

        public override bool Wrap
        {
            get
            {
                return base.Wrap;
            }
            set
            {
                base.Wrap = false;
            }
        }
    }
}

