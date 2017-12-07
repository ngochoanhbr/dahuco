using SinGooCMS.Common;
using SinGooCMS.Payments;
using System;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.MobMger
{
	public class ModifyPaymentSet : H5ManagerPageBase
	{
		protected DropDownList DropDownList1;

		protected Button btnok;

		public string ParametersValue = "{}";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindPayTemplate();
				if (base.IsEdit)
				{
					this.InitForModify();
				}
			}
		}

		private void InitForModify()
		{
			PaymentInfo dataById = Payment.GetDataById(base.OpID);
			if (dataById != null)
			{
				ListItem listItem = this.DropDownList1.Items.FindByValue(dataById.PayCode);
				if (listItem != null)
				{
					listItem.Selected = true;
				}
				this.DropDownList1.Enabled = false;
				this.ParametersValue = dataById.ParametersValue;
			}
		}

		private void BindPayTemplate()
		{
			this.DropDownList1.DataSource = from p in PayTemplate.Load()
			where p.IsMobile
			select p;
			this.DropDownList1.DataTextField = "DisplayName";
			this.DropDownList1.DataValueField = "PayCode";
			this.DropDownList1.DataBind();
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
				PaymentInfo paymentInfo = new PaymentInfo();
				if (base.IsEdit)
				{
					paymentInfo = Payment.GetDataById(base.OpID);
				}
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				string[] array = base.Request.Form["p-key"].Split(new char[]
				{
					','
				});
				string[] array2 = base.Request.Form["p-value"].Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i]))
					{
						stringBuilder.Append(array[i] + ":\"" + array2[i] + "\",");
					}
				}
				paymentInfo.ParametersValue = "{" + stringBuilder.ToString().Trim().TrimEnd(new char[]
				{
					','
				}) + "}";
				if (base.Action.Equals(ActionType.Add.ToString()))
				{
					paymentInfo.PayCode = this.DropDownList1.SelectedValue;
					paymentInfo.AutoTimeStamp = System.DateTime.Now;
					if (Payment.Exists(paymentInfo.PayCode))
					{
						base.ShowMsg("支付方式已存在!");
					}
					else if (Payment.Add(paymentInfo) > 0)
					{
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加支付[" + paymentInfo.DisplayName + "] thành công");
						base.Response.Redirect(string.Concat(new object[]
						{
							"MobilePaySet.aspx?CatalogID=",
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
					if (Payment.Update(paymentInfo))
					{
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改支付[" + paymentInfo.DisplayName + "] thành công");
						base.Response.Redirect(string.Concat(new object[]
						{
							"MobilePaySet.aspx?CatalogID=",
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
