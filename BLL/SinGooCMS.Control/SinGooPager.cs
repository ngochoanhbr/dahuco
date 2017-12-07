using SinGooCMS.Utility;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.Control
{
	[DefaultProperty("Text"), Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), ToolboxData("<{0}:SinGooPager runat=server></{0}:SinGooPager>")]
	public class SinGooPager : CompositeControl, INamingContainer
	{
		private string _FirstPageText = "&laquo;";

		private string _PrevPageText = "&lsaquo;";

		private string _NextPageText = "&rsaquo;";

		private string _EndPageText = "&raquo;";

		private static readonly object EventPageIndexChanged = new object();

		private PagerSettings _pagerSettings = new PagerSettings();

		private LinkButton _lbtnFirst = new LinkButton();

		private LinkButton _lbtnPrev = new LinkButton();

		private LinkButton _lbtnNext = new LinkButton();

		private LinkButton _lbtnLast = new LinkButton();

		private LinkButton _lbtnNum = new LinkButton();

		private TextBox txtPageIndex = new TextBox();

		[Category("Action"), Description("Pager_OnPageIndexChanged")]
		public event EventHandler PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(SinGooPager.EventPageIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(SinGooPager.EventPageIndexChanged, value);
			}
		}

		[Browsable(true), Category("分页"), DefaultValue("0"), Description("当前页的索引"), Localizable(true)]
		public int PageIndex
		{
			get
			{
				return (this.ViewState["PageIndex"] == null) ? 1 : ((int)this.ViewState["PageIndex"]);
			}
			set
			{
				this.ViewState["PageIndex"] = value;
			}
		}

		[Browsable(true), Category("分页"), DefaultValue("10"), Description("数据源中每页要显示的行的数目"), Localizable(true)]
		public int PageSize
		{
			get
			{
				return (this.ViewState["PageSize"] == null) ? 10 : ((int)this.ViewState["PageSize"]);
			}
			set
			{
				this.ViewState["PageSize"] = value;
			}
		}

		[Browsable(true), Category("数据"), DefaultValue("0"), Description("数据源的总行数"), Localizable(true)]
		public int RecordCount
		{
			get
			{
				return (this.ViewState["RecordCount"] == null) ? 0 : ((int)this.ViewState["RecordCount"]);
			}
			set
			{
				this.ViewState["RecordCount"] = value;
			}
		}

		[Browsable(true), Category("分页模板"), Description("用于显示分页内容的模板")]
		public string TemplatePath
		{
			get;
			set;
		}

		public int PageCount
		{
			get
			{
				int result;
				if (this.RecordCount == 0 || this.PageSize == 0)
				{
					result = 0;
				}
				else
				{
					result = (int)Math.Ceiling((double)this.RecordCount / ((double)this.PageSize * 1.0));
				}
				return result;
			}
		}

		public string FirstPageText
		{
			get
			{
				return this._FirstPageText;
			}
			set
			{
				this._FirstPageText = value;
			}
		}

		public string PrevPageText
		{
			get
			{
				return this._PrevPageText;
			}
			set
			{
				this._PrevPageText = value;
			}
		}

		public string NextPageText
		{
			get
			{
				return this._NextPageText;
			}
			set
			{
				this._NextPageText = value;
			}
		}

		public string EndPageText
		{
			get
			{
				return this._EndPageText;
			}
			set
			{
				this._EndPageText = value;
			}
		}

		public string SplitTag
		{
			get;
			set;
		}

		protected void OnPageChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[SinGooPager.EventPageIndexChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this._lbtnFirst.Text = this.FirstPageText;
			this._lbtnFirst.CommandName = "First";
			this._lbtnFirst.Click += new EventHandler(this.LinkButtion_Click);
			this._lbtnPrev.Text = this.PrevPageText;
			this._lbtnPrev.CommandName = "Prev";
			this._lbtnPrev.Click += new EventHandler(this.LinkButtion_Click);
			this._lbtnNext.Text = this.NextPageText;
			this._lbtnNext.CommandName = "Next";
			this._lbtnNext.Click += new EventHandler(this.LinkButtion_Click);
			this._lbtnLast.Text = this.EndPageText;
			this._lbtnLast.CommandName = "Last";
			this._lbtnLast.Click += new EventHandler(this.LinkButtion_Click);
			this._lbtnNum.CommandName = "PageingToNum";
			this._lbtnNum.Click += new EventHandler(this.LinkButtion_Click);
			this.txtPageIndex.Width = 30;
			this.txtPageIndex.ToolTip = "按回车跳转";
			this.txtPageIndex.AutoPostBack = true;
			this.txtPageIndex.TextChanged += new EventHandler(this.PageIndex_TextChanged);
			this.Controls.Add(this._lbtnFirst);
			this.Controls.Add(this._lbtnPrev);
			this.Controls.Add(this._lbtnNext);
			this.Controls.Add(this._lbtnLast);
			this.Controls.Add(this._lbtnNum);
			this.Controls.Add(this.txtPageIndex);
			base.CreateChildControls();
		}

		protected override void Render(HtmlTextWriter writer)
		{
			if (this.RecordCount != 0)
			{
				if (this.PageCount != 1)
				{
					string text = "<div ${Class}><table><tr><td>总记录：<span>${TotalRecord}</span>条，每页<span>${PageSize}</span>条，当前页：<span>${PageIndex}</span>/<span>${TotalPage}</span></td><td>${FirstPageLink}</td><td>${PrevPageLink}</td><td>${PagerNums}</td><td>${NextPageLink}</td><td>${EndPageLink}</td><td>跳转至：</td><td style=\"width:45px\">${JumpLink}</td></table></div>";
					if (!string.IsNullOrEmpty(this.TemplatePath))
					{
						text = File.ReadAllText(HttpContext.Current.Server.MapPath(this.TemplatePath), Encoding.UTF8);
					}
					text = text.Replace("${TotalRecord}", this.RecordCount.ToString()).Replace("${PageSize}", this.PageSize.ToString()).Replace("${PageIndex}", this.PageIndex.ToString()).Replace("${TotalPage}", this.PageCount.ToString());
					text = text.Replace("${Class}", "class=" + this.CssClass);
					if (this.PageIndex == 1)
					{
						text = text.Replace("${FirstPageLink}", "<a disabled=\"disabled\">" + this.FirstPageText + "</a>");
						text = text.Replace("${PrevPageLink}", "<a disabled=\"disabled\">" + this.PrevPageText + "</a>");
					}
					else
					{
						text = text.Replace("${FirstPageLink}", string.Concat(new string[]
						{
							"<a title='首页' href=\"javascript:__doPostBack('",
							this._lbtnFirst.UniqueID,
							"','')\">",
							this.FirstPageText,
							"</a>"
						}));
						text = text.Replace("${PrevPageLink}", string.Concat(new string[]
						{
							"<a title='上一页' href=\"javascript:__doPostBack('",
							this._lbtnPrev.UniqueID,
							"','')\">",
							this.PrevPageText,
							"</a>"
						}));
					}
					StringBuilder stringBuilder = new StringBuilder();
					int[] pages = this.GetPages(10);
					for (int i = 0; i < pages.Length; i++)
					{
						int num = pages[i];
						if (!string.IsNullOrEmpty(this.SplitTag))
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"<",
								this.SplitTag,
								" ",
								(num == this.PageIndex) ? "class='active'" : "",
								"><a href=\"javascript:__doPostBack('",
								this._lbtnNum.UniqueID,
								"','",
								num,
								"')\">",
								num,
								"</a></",
								this.SplitTag,
								">"
							}));
						}
						else
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"<a ",
								(num == this.PageIndex) ? "class='active'" : "",
								" href=\"javascript:__doPostBack('",
								this._lbtnNum.UniqueID,
								"','",
								num,
								"')\">",
								num,
								"</a>"
							}));
						}
					}
					text = text.Replace("${PagerNums}", stringBuilder.ToString().Trim());
					if (this.PageIndex == this.PageCount)
					{
						text = text.Replace("${NextPageLink}", "<a disabled=\"disabled\">" + this.NextPageText + "</a>");
						text = text.Replace("${EndPageLink}", "<a disabled=\"disabled\">" + this.EndPageText + "</a>");
					}
					else
					{
						text = text.Replace("${NextPageLink}", string.Concat(new string[]
						{
							"<a title='下一页' href=\"javascript:__doPostBack('",
							this._lbtnNext.UniqueID,
							"','')\">",
							this.NextPageText,
							"</a>"
						}));
						text = text.Replace("${EndPageLink}", string.Concat(new string[]
						{
							"<a title='尾页' href=\"javascript:__doPostBack('",
							this._lbtnLast.UniqueID,
							"','')\">",
							this.EndPageText,
							"</a>"
						}));
					}
					text = text.Replace("${JumpLink}", string.Concat(new string[]
					{
						"<input name=\"",
						this.txtPageIndex.UniqueID,
						"\" type=\"text\" value=\"\" onchange=\"javascript:setTimeout('__doPostBack('",
						this.txtPageIndex.UniqueID,
						"','')', 0)\" onkeypress=\"if (WebForm_TextBoxKeyHandler(event) == false) return false;\" title=\"按回车跳转\" style=\"width:30px;\" />"
					}));
					writer.Write(text);
				}
			}
		}

		private void LinkButtion_Click(object sender, EventArgs e)
		{
			string commandName = (sender as LinkButton).CommandName;
			string text = commandName;
			if (text != null)
			{
				if (!(text == "First"))
				{
					if (!(text == "Prev"))
					{
						if (!(text == "Next"))
						{
							if (!(text == "Last"))
							{
								if (text == "PageingToNum")
								{
									this.PageIndex = WebUtils.GetInt(HttpContext.Current.Request.Params.Get("__EVENTARGUMENT"));
								}
							}
							else
							{
								this.PageIndex = this.PageCount;
							}
						}
						else
						{
							this.PageIndex++;
						}
					}
					else
					{
						this.PageIndex--;
					}
				}
				else
				{
					this.PageIndex = 1;
				}
			}
			this.OnPageChanged(e);
		}

		private void PageIndex_TextChanged(object sender, EventArgs e)
		{
			if (this.txtPageIndex.Text.Trim().Length != 0)
			{
				int @int = WebUtils.GetInt(this.txtPageIndex.Text);
				if (@int < 1)
				{
					this.PageIndex = 1;
				}
				else if (@int > this.PageCount)
				{
					this.PageIndex = this.PageCount;
				}
				else
				{
					this.PageIndex = @int;
				}
				this.OnPageChanged(e);
			}
		}

		public int[] GetPages(int count)
		{
			int num = this.PageIndex / count;
			if (this.PageIndex % count == 0)
			{
				num--;
			}
			int num2 = num * count + 1;
			int num3 = Math.Min(num * count + count, this.PageCount);
			int[] array = new int[num3 - num2 + 1];
			int num4 = 0;
			for (int i = num2; i <= num3; i++)
			{
				array[num4] = i;
				num4++;
			}
			return array;
		}
	}
}
