using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class ModifyShippingAddr : H5ManagerPageBase
	{
		protected TextBox TextBox4;

		protected TextBox theusername;

		protected TextBox txtArea;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

		protected TextBox TextBox5;

		protected HtmlInputCheckBox CheckBox7;

		protected Button btnok;

		public ShippingAddressInfo addrEdit = new ShippingAddressInfo();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			ShippingAddressInfo dataById = ShippingAddress.GetDataById(base.OpID);
			this.addrEdit = dataById;
			this.TextBox2.Text = dataById.Address;
			this.TextBox3.Text = dataById.PostCode;
			this.TextBox4.Text = dataById.Consignee;
			this.TextBox5.Text = dataById.ContactPhone;
			this.CheckBox7.Checked = dataById.IsDefault;
			this.txtArea.Text = string.Concat(new string[]
			{
				dataById.Province,
				",",
				dataById.City,
				",",
				dataById.County
			});
			this.theusername.Text = dataById.UserName;
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
				ShippingAddressInfo shippingAddressInfo = new ShippingAddressInfo();
				if (base.IsEdit)
				{
					shippingAddressInfo = ShippingAddress.GetDataById(base.OpID);
				}
				string @string = WebUtils.GetString(this.txtArea.Text);
				shippingAddressInfo.Country = WebUtils.GetFormString("_country");
				string[] array = @string.Split(new char[]
				{
					','
				});
				if (array.Length > 0)
				{
					shippingAddressInfo.Province = array[0];
				}
				if (array.Length > 1)
				{
					shippingAddressInfo.City = array[1];
				}
				if (array.Length > 2)
				{
					shippingAddressInfo.County = array[2];
				}
				shippingAddressInfo.Address = WebUtils.GetString(this.TextBox2.Text);
				shippingAddressInfo.PostCode = WebUtils.GetString(this.TextBox3.Text);
				shippingAddressInfo.Consignee = WebUtils.GetString(this.TextBox4.Text);
				shippingAddressInfo.ContactPhone = WebUtils.GetString(this.TextBox5.Text);
				shippingAddressInfo.IsDefault = this.CheckBox7.Checked;
				UserInfo userByName = SinGooCMS.BLL.User.GetUserByName(this.theusername.Text);
				if (userByName == null)
				{
					base.ShowMsg("会员不存在!");
				}
				else if (string.IsNullOrEmpty(shippingAddressInfo.Consignee))
				{
					base.ShowMsg("收件人不为空!");
				}
				else if (string.IsNullOrEmpty(shippingAddressInfo.Address))
				{
					base.ShowMsg("地址信息不为空!");
				}
				else
				{
					shippingAddressInfo.UserID = userByName.AutoID;
					shippingAddressInfo.UserName = userByName.UserName;
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						shippingAddressInfo.AutoTimeStamp = System.DateTime.Now;
						int num = ShippingAddress.Add(shippingAddressInfo);
						if (num > 0)
						{
							if (shippingAddressInfo.IsDefault)
							{
								ShippingAddress.SetDefShippingAddr(num, shippingAddressInfo.UserID);
							}
							PageBase.log.AddEvent(base.LoginAccount.AccountName, string.Concat(new string[]
							{
								"添加收货地址[",
								shippingAddressInfo.Consignee,
								"：",
								shippingAddressInfo.Country,
								"，",
								shippingAddressInfo.Province,
								"，",
								shippingAddressInfo.City,
								"，",
								shippingAddressInfo.County,
								"，",
								shippingAddressInfo.Address,
								"] thành công"
							}));
							base.Response.Redirect(string.Concat(new object[]
							{
								"ShippingAddr.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("添加收货地址失败");
						}
					}
					else if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (ShippingAddress.Update(shippingAddressInfo))
						{
							if (shippingAddressInfo.IsDefault)
							{
								ShippingAddress.SetDefShippingAddr(shippingAddressInfo.AutoID, shippingAddressInfo.UserID);
							}
							PageBase.log.AddEvent(base.LoginAccount.AccountName, string.Concat(new string[]
							{
								"修改收货地址[",
								shippingAddressInfo.Consignee,
								"：",
								shippingAddressInfo.Country,
								"，",
								shippingAddressInfo.Province,
								"，",
								shippingAddressInfo.City,
								"，",
								shippingAddressInfo.County,
								"，",
								shippingAddressInfo.Address,
								"] thành công"
							}));
							base.Response.Redirect(string.Concat(new object[]
							{
								"ShippingAddr.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("修改收货地址失败");
						}
					}
				}
			}
		}
	}
}
