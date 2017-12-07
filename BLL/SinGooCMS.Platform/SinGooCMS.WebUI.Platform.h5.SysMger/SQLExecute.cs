using SinGooCMS.Common;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class SQLExecute : H5ManagerPageBase
	{
		protected DropDownList drpPageSize;

		protected TextBox txtscript;

		protected Button btn_ExecuteSQL;

		protected Literal lblmsg;

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected void btn_ExecuteSQL_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("Query"))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				this.lblmsg.Text = string.Empty;
				string text = this.txtscript.Text;
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						if (PageBase.dbo.ExecSQL(text))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "执行SQL语句[" + text + "] thành công");
							base.ShowMsg("执行成功");
						}
						else
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "执行SQL语句[" + text + "]失败");
							base.ShowMsg("执行失败");
						}
					}
					catch (System.Exception ex)
					{
						PageBase.log.AddErrLog(ex.Message, ex.StackTrace);
						this.lblmsg.Text = ex.Message;
					}
				}
			}
		}

		protected void lnk_Delete_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.H5Tip("Không có thẩm quyền");
			}
		}

		protected void btn_SaveSort_Click(object sender, System.EventArgs e)
		{
		}
	}
}
