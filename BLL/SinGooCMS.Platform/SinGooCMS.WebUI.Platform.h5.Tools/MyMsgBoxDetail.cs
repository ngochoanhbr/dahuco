using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.Tools
{
	public class MyMsgBoxDetail : ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

		protected TextBox TextBox4;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedAuthorized = false;
			this.TextBox1.Text = base.LoginAccount.AccountName;
			this.TextBox1.Enabled = false;
			if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			MessageInfo dataById = Message.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.Sender;
			this.TextBox1.Enabled = false;
			this.TextBox2.Text = dataById.Receiver;
			this.TextBox3.Text = dataById.MsgTitle;
			this.TextBox4.Text = dataById.MsgBody;
			DataRow[] array = base.GetAccountMenu().Select(" ModuleCode='MessageMger' ");
			if ((dataById.ReceiverType == UserType.Manager.ToString() && base.LoginAccount.AccountName.Equals(dataById.Receiver)) || (dataById.ReceiverType == UserType.System.ToString() && dataById.Receiver == "系统" && array != null && array.Length > 0 && base.IsAuthorizedOp(WebUtils.GetInt(array[0]["ModuleID"]), "ReadSystemMsg")))
			{
				dataById.IsRead = true;
				Message.Update(dataById);
			}
		}
	}
}
