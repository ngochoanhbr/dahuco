using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class UserList : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected DropDownList ddlGroup;

		protected DropDownList ddlLevel;

		protected TextBox search_text;

		protected CheckBox yousj;

		protected CheckBox youyx;

		protected Button searchbtn;

		protected LinkButton btn_DelBat;

		protected Button btn_Export;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindGroup();
				this.BindLevel();
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " LastLoginTime DESC,AutoID DESC ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = SinGooCMS.BLL.User.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private void BindGroup()
		{
			this.ddlGroup.DataSource = SinGooCMS.BLL.UserGroup.GetCacheUserGroupList();
			this.ddlGroup.DataTextField = "GroupName";
			this.ddlGroup.DataValueField = "AutoID";
			this.ddlGroup.DataBind();
            this.ddlGroup.Items.Insert(0, new ListItem("Tất cả nhóm", "-1"));
		}

		private void BindLevel()
		{
			this.ddlLevel.DataSource = SinGooCMS.BLL.UserLevel.GetCacheUserLevelList();
			this.ddlLevel.DataTextField = "LevelName";
			this.ddlLevel.DataValueField = "AutoID";
			this.ddlLevel.DataBind();
            this.ddlLevel.Items.Insert(0, new ListItem("Tất cả cấp bậc", "-1"));
		}

		private string GetCondition()
		{
			string text = " 1=1 ";
			string queryString = WebUtils.GetQueryString("type");
			string text2 = queryString;
			if (text2 != null)
			{
				if (!(text2 == "new"))
				{
					if (!(text2 == "active"))
					{
						if (text2 == "dormant")
						{
							text += " AND DATEDIFF(DAY,LastLoginTime,GETDATE())>30 ";
						}
					}
					else
					{
						text += " AND LastLoginTime BETWEEN dateadd(day,-30,getdate()) AND getdate() ";
					}
				}
				else
				{
					text += " AND CONVERT(nvarchar(10),AutoTimeStamp,120)=CONVERT(nvarchar(10),GETDATE(),120) ";
				}
			}
			int @int = WebUtils.GetInt(this.ddlGroup.SelectedValue);
			if (@int > 0)
			{
				text = text + " AND GroupID=" + @int;
			}
			int int2 = WebUtils.GetInt(this.ddlLevel.SelectedValue);
			if (int2 > 0)
			{
				text = text + " AND LevelID=" + int2;
			}
			if (this.yousj.Checked)
			{
				text += " AND (Mobile is not null AND Mobile<>'') ";
			}
			if (this.youyx.Checked)
			{
				text += " AND (Email is not null AND Email<>'') ";
			}
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND UserName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
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

		protected void lnk_Delete_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
                base.ShowAjaxMsg(this.UpdatePanel1, "Không có quyền");
			}
			else
			{
				int @int = WebUtils.GetInt((sender as LinkButton).CommandArgument);
				UserInfo dataById = SinGooCMS.BLL.User.GetDataById(@int);
				if (dataById == null)
				{
                    base.ShowAjaxMsg(this.UpdatePanel1, "Tôi không tìm thấy tài khoản này, tài khoản này không tồn tại hoặc đã bị xóa");
				}
				else if (SinGooCMS.BLL.User.DelUserByID(@int))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "Đã xóa thành viên [" + dataById.UserName + "] thành công");
                    base.ShowAjaxMsg(this.UpdatePanel1, "Hoạt động成功");
				}
				else
				{
                    base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}

		protected void btn_DelBat_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
                base.ShowAjaxMsg(this.UpdatePanel1, "Không có quyền");
			}
			else
			{
				string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
				if (!string.IsNullOrEmpty(repeaterCheckIDs))
				{
					string[] array = repeaterCheckIDs.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						string value = array[i];
						SinGooCMS.BLL.User.DelUserByID(WebUtils.GetInt(value, 0));
					}
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "Xóa nhiều thành viên cùng lúc [" + repeaterCheckIDs + "] thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
			}
		}

		protected void btn_Export_Click(object sender, System.EventArgs e)
		{
            DataTable dataTable = PageBase.dbo.GetDataTable("SELECT AutoID AS Ma,UserName AS TenDangNhap,Email,Mobile, RealName,Gender,Birthday, AutoTimeStamp,LoginCount,LastLoginTime FROM cms_User WHERE " + this.GetCondition());
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				string text = base.ExportFolder + StringUtils.GetRandomNumber() + ".xls";
				DataToXSL.CreateXLS(dataTable, base.Server.MapPath(text), true);
				ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(UpdatePanel), "download", "<script>location='/include/download?file=" + DEncryptUtils.DESEncode(text) + "'</script>", false);
			}
			else
			{
                base.ShowMsg("Không có dữ liệu tìm thấy");
			}
		}
	}
}
