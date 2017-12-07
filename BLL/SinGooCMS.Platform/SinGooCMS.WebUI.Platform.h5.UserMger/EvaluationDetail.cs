using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class EvaluationDetail : H5ManagerPageBase
	{
		protected TextBox txtReply;

		protected Button btnok;

		public EvaluationInfo eva = new EvaluationInfo();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPost)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			this.eva = Evaluation.GetDataById(base.OpID);
			this.txtReply.Text = this.eva.ReplyContent;
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals("Reply") && !base.IsAuthorizedOp("Reply"))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				EvaluationInfo dataById = Evaluation.GetDataById(base.OpID);
				dataById.ReplyContent = base.Server.HtmlEncode(this.txtReply.Text);
				if (!string.IsNullOrEmpty(dataById.ReplyContent))
				{
					dataById.Replier = base.AccountName;
					dataById.ReplyTime = System.DateTime.Now;
				}
				else
				{
					dataById.Replier = string.Empty;
					dataById.ReplyTime = new System.DateTime(1900, 1, 1);
				}
				if (Evaluation.Update(dataById))
				{
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "回复评论[" + this.eva.Content + "] thành công");
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
