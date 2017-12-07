using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class ModifyDict : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

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
			DictsInfo dataById = Dicts.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.DictName;
			if (dataById.IsSystem)
			{
				this.TextBox1.Enabled = false;
			}
			this.TextBox2.Text = dataById.DisplayName;
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
				DictsInfo dictsInfo = new DictsInfo();
				if (base.IsEdit)
				{
					dictsInfo = Dicts.GetDataById(base.OpID);
				}
				dictsInfo.DictName = WebUtils.GetString(this.TextBox1.Text);
				dictsInfo.DisplayName = WebUtils.GetString(this.TextBox2.Text);
				if (string.IsNullOrEmpty(dictsInfo.DictName))
				{
					base.ShowMsg("Tên từ điển không thể để trống");
				}
				else if (string.IsNullOrEmpty(dictsInfo.DisplayName))
				{
                    base.ShowMsg("Tên hiển thị không thể để trống");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						if (Dicts.ExistsForName(dictsInfo.DictName))
						{
							base.ShowMsg("Khóa từ điển đã tồn tại!");
						}
						else
						{
							dictsInfo.IsSystem = false;
							if (Dicts.Add(dictsInfo) > 0)
							{
								CacheUtils.Del("JsonLeeCMS_CacheForGetDicts");
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "Thêm từ điển [" + dictsInfo.DictName + "] thành công");
								MessageUtils.DialogCloseAndParentReload(this);
							}
							else
							{
								base.ShowMsg("Thao tác thất bại");
							}
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (Dicts.Update(dictsInfo))
						{
							CacheUtils.Del("JsonLeeCMS_CacheForGetDicts");
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "Sửa đổi từ điển [" + dictsInfo.DictName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("Thao tác thất bại");
						}
					}
				}
			}
		}
	}
}
