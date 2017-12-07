using SinGooCMS.BLL;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.Tools
{
	public class UploadTools : H5ManagerPageBase
	{
		protected DropDownList ddlFolder;

		protected UpdatePanel UpdatePanel1;

		protected DropDownList ddlFolder2;

		protected TextBox selectdate;

		protected TextBox search_text;

		protected CheckBox imgonly;

		protected Button searchbtn;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		public string SelectType = "single";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedAuthorized = false;
			this.SelectType = WebUtils.GetQueryString("type");
			if (!base.IsPostBack)
			{
				this.BindFolder();
				this.BindData();
			}
		}

		private void BindFolder()
		{
			System.Collections.Generic.IList<FolderInfo> allList = Folder.GetAllList();
			this.ddlFolder.DataSource = allList;
			this.ddlFolder.DataTextField = "FolderName";
			this.ddlFolder.DataValueField = "AutoID";
			this.ddlFolder.DataBind();
			this.ddlFolder.Items.Insert(0, new ListItem("未归档", "-1"));
			this.ddlFolder.Items[0].Selected = true;
			this.ddlFolder2.DataSource = allList;
			this.ddlFolder2.DataTextField = "FolderName";
			this.ddlFolder2.DataValueField = "AutoID";
			this.ddlFolder2.DataBind();
			this.ddlFolder2.Items.Insert(0, new ListItem("未归档", "-1"));
			this.ddlFolder2.Items.Insert(0, new ListItem("所有文件", "0"));
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " AutoID DESC ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = SinGooCMS.BLL.FileUpload.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			string text = " 1=1 ";
			if (this.ddlFolder2.SelectedValue != "0")
			{
				text = text + " AND FolderID=" + WebUtils.GetInt(this.ddlFolder2.SelectedValue);
			}
			if (!string.IsNullOrEmpty(this.selectdate.Text))
			{
				text = text + " AND CONVERT(nvarchar(7),AutoTimeStamp,120)='" + WebUtils.GetString(this.selectdate.Text) + "'";
			}
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND [FileName] like '%" + StringUtils.ChkSQL(this.search_text.Text) + "%' ";
			}
			if (this.imgonly.Checked)
			{
				text += " AND (Thumb is not null and Thumb<>'') ";
			}
			return text;
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SinGooPager1.PageIndex = 1;
			this.BindData();
		}
	}
}
