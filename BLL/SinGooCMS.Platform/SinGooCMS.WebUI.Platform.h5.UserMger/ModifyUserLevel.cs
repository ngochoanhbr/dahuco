using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class ModifyUserLevel : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected H5TextBox TextBox2;

		protected H5TextBox TextBox3;

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
			UserLevelInfo dataById = SinGooCMS.BLL.UserLevel.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.LevelName;
			this.TextBox2.Text = dataById.Integral.ToString();
			this.TextBox3.Text = dataById.Discount.ToString("f2");
			this.TextBox4.Text = dataById.LevelDesc;
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
				UserLevelInfo userLevelInfo = new UserLevelInfo();
				if (base.IsEdit)
				{
					userLevelInfo = SinGooCMS.BLL.UserLevel.GetCacheUserLevelById(base.OpID);
				}
				userLevelInfo.LevelName = WebUtils.GetString(this.TextBox1.Text);
				userLevelInfo.Integral = WebUtils.StringToInt(this.TextBox2.Text);
				userLevelInfo.Discount = (double)WebUtils.StringToDecimal(this.TextBox3.Text);
				userLevelInfo.LevelDesc = WebUtils.GetString(this.TextBox4.Text);
				userLevelInfo.Sort = SinGooCMS.BLL.UserLevel.MaxSort + 1;
				if (string.IsNullOrEmpty(userLevelInfo.LevelName))
				{
					base.ShowMsg("用户等级名称不为空!");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						if (SinGooCMS.BLL.UserLevel.Add(userLevelInfo) > 0)
						{
							CacheUtils.Del("JsonLeeCMS_CacheForGetUserLevel");
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加用户等级[" + userLevelInfo.LevelName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("添加用户等级失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.UserLevel.Update(userLevelInfo))
						{
							CacheUtils.Del("JsonLeeCMS_CacheForGetUserLevel");
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改用户等级[" + userLevelInfo.LevelName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改用户等级失败");
						}
					}
				}
			}
		}
	}
}
