using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class ModifyTag : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected H5TextBox TextBox2;

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
			TagsInfo dataById = SinGooCMS.BLL.Tags.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.TagName;
			this.TextBox2.Text = dataById.TagUrl.ToString();
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
				TagsInfo tagsInfo = new TagsInfo();
				if (base.IsEdit)
				{
					tagsInfo = SinGooCMS.BLL.Tags.GetDataById(base.OpID);
				}
				tagsInfo.TagName = WebUtils.GetString(this.TextBox1.Text);
				tagsInfo.TagUrl = WebUtils.GetString(this.TextBox2.Text);
				tagsInfo.Sort = SinGooCMS.BLL.Tags.MaxSort + 1;
				tagsInfo.IsTop = false;
				tagsInfo.IsRecommend = false;
				if (string.IsNullOrEmpty(tagsInfo.TagName))
				{
                    base.ShowMsg("Vui lòng nhập một thẻ tag");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						tagsInfo.AutoTimeStamp = System.DateTime.Now;
						tagsInfo.Lang = base.cultureLang;
						if (SinGooCMS.BLL.Tags.Add(tagsInfo) > 0)
						{
                            PageBase.log.AddEvent(base.LoginAccount.AccountName, "Thêm thẻ tag [" + tagsInfo.TagName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
                            base.ShowMsg("Thêm thẻ thất bại");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.Tags.Update(tagsInfo))
						{
                            PageBase.log.AddEvent(base.LoginAccount.AccountName, "Sửa đổi thẻ tag [" + tagsInfo.TagName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
                            base.ShowMsg("Sửa đổi thẻ tag thất bại");
						}
					}
				}
			}
		}
	}
}
