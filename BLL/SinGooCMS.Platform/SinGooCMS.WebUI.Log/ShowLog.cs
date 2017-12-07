using SinGooCMS.BLL;
using SinGooCMS.Control;
using SinGooCMS.Utility;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Log
{
	public class ShowLog : Page
	{
		protected HtmlHead Head1;

		protected HtmlForm form1;

		protected ScriptManager scriptManager1;

		protected TextBox timestart;

		protected TextBox timeend;

		protected TextBox search_text;

		protected Button searchbtn;

		protected UpdatePanel UpdatePanel1;

		protected GridView GridView1;

		protected SinGooPager SinGooPager1;

		protected Button btn_DelBat;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " AutoID DESC ";
			this.GridView1.DataSource = Visitor.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.GridView1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			string text = " 1=1 AND (ErrMessage is not null AND ErrMessage<>'') ";
			System.DateTime t = WebUtils.StringToDateTime(this.timestart.Text, new System.DateTime(1900, 1, 1));
			System.DateTime t2 = WebUtils.StringToDateTime(this.timeend.Text, new System.DateTime(1900, 1, 1));
			if (t > new System.DateTime(1900, 1, 1))
			{
				text = text + " AND AutoTimeStamp>='" + t.ToString("yyyy-MM-dd") + " 00:00:00' ";
			}
			if (t2 > new System.DateTime(1900, 1, 1))
			{
				text = text + " AND AutoTimeStamp<='" + t2.ToString("yyyy-MM-dd") + " 23:59:59' ";
			}
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" AND ErrMessage like '%",
					WebUtils.GetString(this.search_text.Text),
					"%' AND StackTrace like '%",
					WebUtils.GetString(this.search_text.Text),
					"%' "
				});
			}
			return text;
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}
	}
}
