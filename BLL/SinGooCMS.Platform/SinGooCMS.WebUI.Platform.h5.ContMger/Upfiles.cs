using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ContMger
{
	public class Upfiles : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected DropDownList ddlFolder;

		protected TextBox selectdate;

		protected TextBox search_text;

		protected CheckBox showimgonly;

		protected Button searchbtn;

		protected LinkButton btn_DelBat;

		protected DropDownList ddlMoveTo;

		protected Button btn_MoveTo;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindFolder();
				this.BindData();
			}
		}

		private void BindFolder()
		{
			System.Collections.Generic.List<FolderInfo> dataSource = (System.Collections.Generic.List<FolderInfo>)Folder.GetAllList();
			this.ddlMoveTo.DataSource = dataSource;
			this.ddlMoveTo.DataTextField = "FolderName";
			this.ddlMoveTo.DataValueField = "AutoID";
			this.ddlMoveTo.DataBind();
			this.ddlMoveTo.Items.Insert(0, new ListItem("未归档", "-1"));
			this.ddlFolder.DataSource = dataSource;
			this.ddlFolder.DataTextField = "FolderName";
			this.ddlFolder.DataValueField = "AutoID";
			this.ddlFolder.DataBind();
			this.ddlFolder.Items.Insert(0, new ListItem("未归档", "-1"));
			this.ddlFolder.Items.Insert(0, new ListItem("所有文件夹", "0"));
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
			int @int = WebUtils.GetInt(this.ddlFolder.SelectedValue);
			if (@int != 0)
			{
				text = text + "AND FolderID=" + @int;
			}
			if (!string.IsNullOrEmpty(this.selectdate.Text))
			{
				text = text + " AND CONVERT(nvarchar(7),AutoTimeStamp,120)='" + WebUtils.GetString(this.selectdate.Text) + "'";
			}
			if (this.showimgonly.Checked)
			{
				text += " AND Thumb<> '' ";
			}
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND FileName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
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

		protected void btn_MoveTo_Click(object sender, System.EventArgs e)
		{
			string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
			if (!string.IsNullOrEmpty(repeaterCheckIDs))
			{
				SinGooCMS.BLL.FileUpload.MoveToFolder(repeaterCheckIDs, WebUtils.GetInt(this.ddlMoveTo.SelectedValue));
			}
			this.BindData();
		}

		protected void lnk_Delete_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				int @int = WebUtils.GetInt((sender as LinkButton).CommandArgument);
				FileUploadInfo dataById = SinGooCMS.BLL.FileUpload.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "没有找到文件,文件不存在或者已删除");
				}
				else if (SinGooCMS.BLL.FileUpload.DelBatAndFile(@int.ToString()))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除上传文件[" + dataById.VirtualPath + "] thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "删除文件成功");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "删除文件失败");
				}
			}
		}

		protected void btn_DelBat_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
				if (!string.IsNullOrEmpty(repeaterCheckIDs))
				{
					SinGooCMS.BLL.FileUpload.DelBatAndFile(repeaterCheckIDs);
					this.BindData();
				}
			}
		}

		public string ShowPreview(string strFileName, string strThumb)
		{
			string result;
			if (ValidateUtils.IsImage(strFileName))
			{
				result = string.Concat(new string[]
				{
					"<img class='thumb' data-original='",
					strFileName,
					"' src='",
					strThumb,
					"' alt='' />"
				});
			}
			else
			{
				string text = System.IO.Path.GetExtension(strFileName).ToLower();
				string text2 = text;
				if (text2 != null)
				{
					if (text2 == ".doc")
					{
						result = "<img class='thumb' data-original='../images/imgico/doc.png' src='../images/imgico/doc.png' alt='' />";
						return result;
					}
					if (text2 == ".pdf")
					{
						result = "<img class='thumb' data-original='../images/imgico/pdf.png' src='../images/imgico/pdf.png' alt='' />";
						return result;
					}
					if (text2 == ".rar")
					{
						result = "<img class='thumb' data-original='../images/imgico/rar.png' src='../images/imgico/rar.png' alt='' />";
						return result;
					}
					if (text2 == ".zip")
					{
						result = "<img class='thumb' data-original='../images/imgico/zip.png' src='../images/imgico/zip.png' alt='' />";
						return result;
					}
					if (text2 == ".txt")
					{
						result = "<img class='thumb' data-original='../images/imgico/txt.png' src='../images/imgico/txt.png' alt='' />";
						return result;
					}
				}
				result = "<img class='thumb' data-original='../images/imgico/file.png' src='../images/imgico/file.png' alt='' />";
			}
			return result;
		}
	}
}
