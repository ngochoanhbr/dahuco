using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.Control
{
    [DefaultProperty("Text"), ToolboxData("<{0}:Pager runat=server></{0}:Pager>"), Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public class Pager : CompositeControl, INamingContainer
    {
        #region 公共属性

        [Browsable(true), Category("分页"), DefaultValue("0"), Localizable(true), Description("当前页的索引")]
        public int PageIndex
        {
            get
            {
                return ViewState["PageIndex"] == null ? 0 : (int)ViewState["PageIndex"];
            }

            set
            {
                ViewState["PageIndex"] = value;
            }
        }

        [Browsable(true), Category("分页"), DefaultValue("10"), Localizable(true), Description("数据源中每页要显示的行的数目")]
        public int PageSize
        {
            get
            {
                return ViewState["PageSize"] == null ? 10 : (int)ViewState["PageSize"];
            }

            set
            {
                ViewState["PageSize"] = value;
            }
        }

        [Browsable(true), Category("数据"), DefaultValue("0"), Localizable(true), Description("数据源的总行数")]
        public int RecordCount
        {
            get
            {
                return ViewState["RecordCount"] == null ? 0 : (int)ViewState["RecordCount"];
            }

            set
            {
                ViewState["RecordCount"] = value;
            }
        }

        #endregion

        #region 私有属性

        private int PageCount
        {
            get
            {
                if (this.RecordCount == 0 || this.PageSize == 0)
                    return 0;
                return (int)Math.Ceiling(this.RecordCount / (this.PageSize * 1.0));
            }
        }

        #endregion

        #region 私有变量

        private static readonly object EventPageIndexChanged = new object();
        private PagerSettings _pagerSettings = new PagerSettings();

        private LinkButton _lbtnFirst = new LinkButton();
        private LinkButton _lbtnPrev = new LinkButton();
        private LinkButton _lbtnNext = new LinkButton();
        private LinkButton _lbtnLast = new LinkButton();

        private TextBox txtPageIndex = new TextBox();

        #endregion

        #region 事件相关

        [Description("Pager_OnPageIndexChanged"), Category("Action")]
        public event EventHandler PageIndexChanged
        {
            add
            {
                base.Events.AddHandler(EventPageIndexChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventPageIndexChanged, value);
            }
        }

        protected void OnPageChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventPageIndexChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region 重写基类方法

        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            this._lbtnFirst.Text = "首页";//this._pagerSettings.FirstPageText;
            this._lbtnFirst.CommandName = "First";
            this._lbtnFirst.Click += new EventHandler(LinkButtion_Click);

            this._lbtnPrev.Text = "上一页";//this._pagerSettings.PreviousPageText;
            this._lbtnPrev.CommandName = "Prev";
            this._lbtnPrev.Click += new EventHandler(LinkButtion_Click);

            this._lbtnNext.Text = "下一页";//this._pagerSettings.NextPageText;
            this._lbtnNext.CommandName = "Next";
            this._lbtnNext.Click += new EventHandler(LinkButtion_Click);

            this._lbtnLast.Text = "尾页";//this._pagerSettings.LastPageText;
            this._lbtnLast.CommandName = "Last";
            this._lbtnLast.Click += new EventHandler(LinkButtion_Click);

            txtPageIndex.Width = 30;
            txtPageIndex.ToolTip = "按回车跳转";
            txtPageIndex.AutoPostBack = true;
            txtPageIndex.TextChanged += new EventHandler(PageIndex_TextChanged);

            this.Controls.Add(this._lbtnFirst);
            this.Controls.Add(this._lbtnPrev);
            this.Controls.Add(this._lbtnNext);
            this.Controls.Add(this._lbtnLast);
            this.Controls.Add(txtPageIndex);

            base.CreateChildControls();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.PageCount == 1)
                return;
            if (this.CssClass != "")
                writer.Write("<div class=\"{0}\">", this.CssClass);
            else
                writer.Write("<div>");
            if (this.RecordCount == 0)
            {
                writer.Write("对不起，没有查询到相关记录！</div>");
                return;
            }
            writer.Write("<table><tr><td>总记录：<span>{0}</span>条，每页<span>{1}</span>条，当前页：<span>{2}</span>/<span>{3}</span></td>", this.RecordCount, this.PageSize, this.PageIndex + 1, this.PageCount);

            #region 显示首页，上一页

            writer.Write("<td>");
            if (this.PageIndex == 0 || this.PageCount == 1)
            {
                _lbtnFirst.Enabled = false;
                _lbtnPrev.Enabled = false;
            }
            _lbtnFirst.RenderControl(writer);
            writer.Write("</td><td>");
            _lbtnPrev.RenderControl(writer);
            writer.Write("</td>");

            #endregion

            #region  显示下一页，末页

            writer.Write("<td>");
            if (this.PageIndex == this.PageCount - 1 || this.PageCount == 1)
            {
                _lbtnNext.Enabled = false;
                _lbtnLast.Enabled = false;
            }
            _lbtnNext.RenderControl(writer);
            writer.Write("</td><td>");
            _lbtnLast.RenderControl(writer);
            writer.Write("</td>");

            #endregion

            #region 跳转至

            writer.Write("<td>跳转至：</td><td style=\"width:45px\">");
            txtPageIndex.RenderControl(writer);
            writer.Write("</td></table></div>");

            #endregion
        }

        #endregion

        #region 子控件的方法

        private void LinkButtion_Click(object sender, EventArgs e)
        {
            string commandName = (sender as LinkButton).CommandName;
            switch (commandName)
            {
                case "First": this.PageIndex = 0; break;
                case "Prev": this.PageIndex--; break;
                case "Next": this.PageIndex++; break;
                case "Last": this.PageIndex = this.PageCount - 1; break;
            }
            OnPageChanged(e);
        }

        private void PageIndex_TextChanged(object sender, EventArgs e)
        {
            if (txtPageIndex.Text.Trim().Length == 0)
                return;
            int pageIndex = 0;
            if (int.TryParse(txtPageIndex.Text, out pageIndex))
            {
                if (pageIndex < 1)
                    this.PageIndex = 0;
                else if (pageIndex > this.PageCount)
                    this.PageIndex = this.PageCount - 1;
                else
                    this.PageIndex = pageIndex - 1;
            }
            OnPageChanged(e);
        }

        #endregion
    }
}
