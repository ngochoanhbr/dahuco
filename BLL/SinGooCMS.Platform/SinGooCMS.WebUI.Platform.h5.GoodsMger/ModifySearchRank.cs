using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class ModifySearchRank : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected H5TextBox TextBox2;

		protected CheckBox CheckBox3;

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
			SearchRankInfo dataById = SinGooCMS.BLL.SearchRank.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.SearchKey;
			this.TextBox2.Text = dataById.Times.ToString();
			this.CheckBox3.Checked = dataById.IsRecommend;
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
				SearchRankInfo searchRankInfo = new SearchRankInfo();
				if (base.IsEdit)
				{
					searchRankInfo = SinGooCMS.BLL.SearchRank.GetDataById(base.OpID);
				}
				searchRankInfo.SearchKey = WebUtils.GetString(this.TextBox1.Text);
				searchRankInfo.Times = WebUtils.GetInt(this.TextBox2.Text);
				searchRankInfo.IsRecommend = this.CheckBox3.Checked;
				if (string.IsNullOrEmpty(searchRankInfo.SearchKey))
				{
					base.ShowMsg("请输入搜索关键字");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						if (SinGooCMS.BLL.SearchRank.Add(searchRankInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加搜索关键字[" + searchRankInfo.SearchKey + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("Thao tác thất bại");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.SearchRank.Update(searchRankInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改搜索关键字[" + searchRankInfo.SearchKey + "] thành công");
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
