using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5
{
	public class Main : ManagerPageBase
	{
		protected Repeater rpt_NewOrder;

		protected Repeater rpt_NewUser;

		protected Repeater rpt_UeNews;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			base.NeedAuthorized = false;
			string action = base.Action;
			if (action != null)
			{
				if (action == "logout")
				{
					this.Logout();
				}
			}
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@managername", base.AccountName)
			};
			DataTable dataTable = PageBase.dbo.ExecProcReDT("p_System_DeskSTAT", arrParam);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				DeskSource.init(dataTable.Rows[0]);
			}
			this.BindNewOrder();
			this.BindNewUser();
			this.BindUeNews();
		}

		public System.Collections.Generic.IList<MessageInfo> GetMessages()
		{
			string text = " (ReceiverType='manager' AND Receiver='" + base.LoginAccount.AccountName + "') ";
			DataRow[] array = base.GetAccountMenu().Select(" ModuleCode='MessageMger' ");
			if (array != null && array.Length > 0 && base.IsAuthorizedOp(WebUtils.GetInt(array[0]["ModuleID"]), "ReadSystemMsg"))
			{
				text = " (" + text + " OR (ReceiverType='system' AND Receiver='系统')) ";
			}
			text = " select * from sys_Message where " + text + " AND IsRead=0 order by SendTime desc ";
			return PageBase.dbo.GetList<MessageInfo>(text);
		}

		private void BindNewOrder()
		{
			this.rpt_NewOrder.DataSource = PageBase.dbo.GetDataTable(" select top 100 AutoID,OrderNo,Consignee,Country,Province,City,County,[Address],Mobile,OrderPayTime,UserName,(select HeaderPhoto from cms_User where AutoID=shop_Orders.UserID) as HeaderPhoto,( case when DATEDIFF(DAY,OrderPayTime,GETDATE())=0 then '今天' when DATEDIFF(DAY,OrderPayTime,GETDATE())=1 then '昨天' when DATEDIFF(DAY,OrderPayTime,GETDATE())=2 then '前天' else CONVERT(nvarchar(5),OrderPayTime,10) end )+' '+CONVERT(nvarchar(5),OrderPayTime,108) as daystr from shop_Orders where OrderStatus=" + 10 + " order by OrderPayTime desc ");
			this.rpt_NewOrder.DataBind();
		}

		private void BindNewUser()
		{
			this.rpt_NewUser.DataSource = PageBase.dbo.GetDataTable(" select top 10 AutoID,GroupID,UserName,Email,Mobile,HeaderPhoto,AutoTimeStamp,( case when DATEDIFF(DAY,AutoTimeStamp,GETDATE())=0 then '今天' when DATEDIFF(DAY,AutoTimeStamp,GETDATE())=1 then '昨天' when DATEDIFF(DAY,AutoTimeStamp,GETDATE())=2 then '前天' else CONVERT(nvarchar(5),AutoTimeStamp,10) end )+' '+CONVERT(nvarchar(5),AutoTimeStamp,108) as daystr from cms_User order by AutoTimeStamp desc ");
			this.rpt_NewUser.DataBind();
		}

		private void BindUeNews()
		{
			System.Collections.Generic.List<UENewsInfo> list = UENews.Get();
			this.rpt_UeNews.DataSource = ((list != null) ? list.Take(6) : new System.Collections.Generic.List<UENewsInfo>());
			this.rpt_UeNews.DataBind();
		}

		private void Logout()
		{
            PageBase.log.AddEvent(base.LoginAccount.AccountName, "Đăng xuất", 1);
			HttpContext.Current.Session["Account"] = null;
			HttpContext.Current.Session.Remove("Account");
			FormsAuthentication.SignOut();
			base.Response.Redirect("/platform/login");
		}
	}
}
