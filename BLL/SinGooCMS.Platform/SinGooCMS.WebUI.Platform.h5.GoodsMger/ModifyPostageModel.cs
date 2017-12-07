using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class ModifyPostageModel : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected Button btnok;

		protected HtmlInputHidden hfOldIDs;

		protected HtmlInputHidden hfNewIDs;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			PostageModelInfo dataById = PostageModel.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.ModelName;
			this.TextBox2.Text = dataById.ShortDesc;
			this.hfOldIDs.Value = dataById.RuleSet;
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
				PostageModelInfo postageModelInfo = new PostageModelInfo();
				if (base.IsEdit)
				{
					postageModelInfo = PostageModel.GetDataById(base.OpID);
				}
				postageModelInfo.ModelName = this.TextBox1.Text.Trim();
				postageModelInfo.ShortDesc = this.TextBox2.Text.Trim();
				postageModelInfo.RuleSet = this.hfNewIDs.Value;
				postageModelInfo.Creator = base.LoginAccount.AccountName;
				postageModelInfo.AutoTimeStamp = System.DateTime.Now;
				if (string.IsNullOrEmpty(postageModelInfo.ModelName))
				{
					base.ShowMsg("模板名称不能为空");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						if (PostageModel.Add(postageModelInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加邮费模板[" + postageModelInfo.ModelName + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"PostageModelList.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("Thao tác thất bại");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (PostageModel.Update(postageModelInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改邮费模板[" + postageModelInfo.ModelName + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"PostageModelList.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
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
