using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.MarketMger
{
	public class ModifyCoupons : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected H5TextBox TextBox2;

		protected H5TextBox TextBox3;

		protected TextBox TextBox4;

		protected TextBox TextBox5;

		protected TextBox TextBox6;

		protected HtmlInputCheckBox chkhasused;

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
			CouponsInfo dataById = SinGooCMS.BLL.Coupons.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.Title;
			this.TextBox2.Text = dataById.Notes.ToString("f2");
			this.TextBox3.Text = dataById.Touch.ToString("f2");
			this.TextBox4.Text = dataById.StartTime.ToString("yyyy-MM-dd");
			this.TextBox5.Text = dataById.EndTime.ToString("yyyy-MM-dd");
			this.TextBox6.Text = dataById.UserName;
			this.chkhasused.Checked = dataById.IsUsed;
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
				CouponsInfo couponsInfo = new CouponsInfo();
				if (base.IsEdit)
				{
					couponsInfo = SinGooCMS.BLL.Coupons.GetDataById(base.OpID);
				}
				couponsInfo.Title = WebUtils.GetString(this.TextBox1.Text);
				couponsInfo.Notes = WebUtils.GetDecimal(this.TextBox2.Text);
				couponsInfo.Touch = WebUtils.GetDecimal(this.TextBox3.Text);
				couponsInfo.StartTime = WebUtils.GetDateTime(this.TextBox4.Text);
				couponsInfo.EndTime = WebUtils.GetDateTime(this.TextBox5.Text);
				couponsInfo.UserName = WebUtils.GetString(this.TextBox6.Text);
				couponsInfo.IsUsed = this.chkhasused.Checked;
				if (string.IsNullOrEmpty(couponsInfo.Title))
				{
					base.ShowMsg("优惠券标题不能为空！");
				}
				else if (couponsInfo.Notes <= 0m)
				{
					base.ShowMsg("优惠券面值不正确！");
				}
				else if (couponsInfo.EndTime < couponsInfo.StartTime)
				{
					base.ShowMsg("截止日期不应该在开始日期前面！");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						couponsInfo.SN = SinGooCMS.BLL.Coupons.CreateSN();
						couponsInfo.Sort = 999;
						couponsInfo.AutoTimeStamp = System.DateTime.Now;
						if (SinGooCMS.BLL.Coupons.Add(couponsInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加优惠券[" + couponsInfo.Title + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"Coupons.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("添加优惠券失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.Coupons.Update(couponsInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改优惠券[" + couponsInfo.Title + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"Coupons.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("修改优惠券失败");
						}
					}
				}
			}
		}
	}
}
