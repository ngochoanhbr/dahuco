using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class ModifyUserGroup : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

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
			UserGroupInfo dataById = SinGooCMS.BLL.UserGroup.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.GroupName;
			this.TextBox2.Text = dataById.TableName.Split(new char[]
			{
				'_'
			})[2];
			this.TextBox3.Text = dataById.GroupDesc;
			this.TextBox2.Enabled = false;
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
				UserGroupInfo userGroupInfo = new UserGroupInfo();
				if (base.IsEdit)
				{
					userGroupInfo = SinGooCMS.BLL.UserGroup.GetCacheUserGroupById(base.OpID);
				}
				userGroupInfo.GroupName = WebUtils.GetString(this.TextBox1.Text);
				userGroupInfo.TableName = "cms_U_" + WebUtils.GetString(this.TextBox2.Text);
				userGroupInfo.GroupDesc = WebUtils.GetString(this.TextBox3.Text);
				userGroupInfo.Sort = SinGooCMS.BLL.UserGroup.MaxSort + 1;
				userGroupInfo.Creator = base.LoginAccount.AccountName;
				if (string.IsNullOrEmpty(userGroupInfo.GroupName) || string.IsNullOrEmpty(this.TextBox2.Text.Trim()))
				{
					base.ShowMsg("用户组名称和数据表名不能为空");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						userGroupInfo.AutoTimeStamp = System.DateTime.Now;
						ModelAddState modelAddState = SinGooCMS.BLL.UserGroup.Add(userGroupInfo);
						ModelAddState modelAddState2 = modelAddState;
						switch (modelAddState2)
						{
						case ModelAddState.Error:
							base.ShowMsg("添加用户组失败");
							break;
						case ModelAddState.ModelNameExists:
							base.ShowMsg("用户组名称已经存在");
							break;
						case ModelAddState.TableNameIsUsing:
							base.ShowMsg("已经存在相同的自定义表名称");
							break;
						case ModelAddState.TableExists:
							base.ShowMsg("自定义表已经存在");
							break;
						case ModelAddState.CreateTableError:
							base.ShowMsg("创建自定义表失败");
							break;
						default:
							if (modelAddState2 == ModelAddState.Success)
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加用户组[" + userGroupInfo.GroupName + "] thành công");
								MessageUtils.DialogCloseAndParentReload(this);
							}
							break;
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.UserGroup.Update(userGroupInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改用户组[" + userGroupInfo.GroupName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改用户组失败");
						}
					}
				}
			}
		}
	}
}
