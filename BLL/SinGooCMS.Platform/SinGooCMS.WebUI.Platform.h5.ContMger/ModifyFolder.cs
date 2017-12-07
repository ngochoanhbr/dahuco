using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ContMger
{
	public class ModifyFolder : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox4;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			FolderInfo dataById = Folder.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.FolderName;
			this.TextBox4.Text = dataById.Remark;
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Add.ToString()) && !base.IsAuthorizedOp(ActionType.Add.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else if (base.Action.Equals(ActionType.Modify.ToString()) && !base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				string @string = WebUtils.GetString(this.TextBox1.Text);
				FolderInfo folderInfo = new FolderInfo();
				if (base.IsEdit)
				{
					folderInfo = Folder.GetDataById(base.OpID);
				}
				folderInfo.FolderName = @string;
				folderInfo.ParentID = 0;
				folderInfo.Remark = WebUtils.GetString(this.TextBox4.Text);
				folderInfo.Sort = Folder.MaxSort + 1;
				if (string.IsNullOrEmpty(@string))
				{
					base.ShowMsg("文件夹名称不能为空!");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						folderInfo.Lang = base.cultureLang;
						folderInfo.AutoTimeStamp = System.DateTime.Now;
						if (Folder.AddExt(folderInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加文件夹[" + folderInfo.FolderName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("添加失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (Folder.Update(folderInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改文件夹[" + folderInfo.FolderName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改失败");
						}
					}
				}
			}
		}
	}
}
