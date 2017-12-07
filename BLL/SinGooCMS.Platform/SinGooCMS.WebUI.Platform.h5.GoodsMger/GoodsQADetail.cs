using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class GoodsQADetail : H5ManagerPageBase
	{
		protected TextBox txtReply;

		protected Button btnok;

		public GoodsQAInfo qa = new GoodsQAInfo();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPost)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			this.qa = SinGooCMS.BLL.GoodsQA.GetDataById(base.OpID);
			this.txtReply.Text = this.qa.Answer;
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals("Reply") && !base.IsAuthorizedOp("Reply"))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				GoodsQAInfo dataById = SinGooCMS.BLL.GoodsQA.GetDataById(base.OpID);
				dataById.Answer = base.Server.HtmlEncode(this.txtReply.Text);
				if (SinGooCMS.BLL.GoodsQA.Update(dataById))
				{
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "回复商品咨询成功");
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
