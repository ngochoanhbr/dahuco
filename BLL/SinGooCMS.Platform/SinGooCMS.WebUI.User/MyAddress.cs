using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI.User
{
    public class MyAddress : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			if (base.IsPost && base.UserID != -1)
			{
				string text = WebUtils.GetFormString("_action", "add").ToLower();
				int formInt = WebUtils.GetFormInt("_addrid");
				ShippingAddressInfo shippingAddressInfo = new ShippingAddressInfo();
				if (text.Equals("modify"))
				{
					shippingAddressInfo = ShippingAddress.GetDataById(formInt);
				}
				shippingAddressInfo.UserID = base.UserID;
				shippingAddressInfo.Consignee = WebUtils.GetFormString("_consignee");
				shippingAddressInfo.Country = "中国";
				string formString = WebUtils.GetFormString("_area");
				string[] array = formString.Split(new char[]
				{
					','
				});
				shippingAddressInfo.Province = ((array.Length > 0) ? array[0] : string.Empty);
				shippingAddressInfo.City = ((array.Length > 1) ? array[1] : string.Empty);
				shippingAddressInfo.County = ((array.Length > 2) ? array[2] : string.Empty);
				shippingAddressInfo.Address = WebUtils.GetFormString("_address");
				shippingAddressInfo.PostCode = WebUtils.GetFormString("_postcode");
				shippingAddressInfo.ContactPhone = WebUtils.GetFormString("_contactphone");
				shippingAddressInfo.IsDefault = (WebUtils.GetFormString("_isdefault").ToUpper() == "ON");
				if (string.IsNullOrEmpty(shippingAddressInfo.Consignee))
				{
					base.WriteJsonTip(base.GetCaption("Addr_ConsigneeNotEmpty"));
				}
				else if (string.IsNullOrEmpty(shippingAddressInfo.Address))
				{
					base.WriteJsonTip(base.GetCaption("Addr_DetailAddrNotEmpty"));
				}
				else if (string.IsNullOrEmpty(shippingAddressInfo.ContactPhone))
				{
					base.WriteJsonTip(base.GetCaption("Addr_MobileOrPhoneMustOne"));
				}
				else if (text == "modify")
				{
					if (ShippingAddress.Update(shippingAddressInfo))
					{
						if (shippingAddressInfo.IsDefault)
						{
							ShippingAddress.SetDefShippingAddr(shippingAddressInfo.AutoID, base.UserID);
						}
						base.WriteJsonTip(true, base.GetCaption("Addr_Success"), UrlRewrite.Get("myaddress_url"));
					}
					else
					{
						base.WriteJsonTip(base.GetCaption("Addr_UpdateFail"));
					}
				}
				else
				{
					shippingAddressInfo.AutoTimeStamp = System.DateTime.Now;
					int num = ShippingAddress.Add(shippingAddressInfo);
					if (num > 0)
					{
						if (shippingAddressInfo.IsDefault)
						{
							ShippingAddress.SetDefShippingAddr(num, base.UserID);
						}
						base.WriteJsonTip(true, base.GetCaption("Addr_Success"), UrlRewrite.Get("myaddress_url"));
					}
					else
					{
						base.WriteJsonTip(base.GetCaption("Addr_AddFail"));
					}
				}
			}
			else
			{
				this.Action = WebUtils.GetQueryString("action", "add");
				int queryInt = WebUtils.GetQueryInt("id");
				if (this.Action == "delete" && queryInt > 0 && ShippingAddress.Delete(queryInt))
				{
					base.Response.Redirect(UrlRewrite.Get("myaddress_url"));
				}
				base.Put("action", this.Action);
				base.Put("curraddr", ShippingAddress.GetDataById(queryInt));
				base.Put("addrlist", ShippingAddress.GetShippingAddrByUID(base.UserID));
				base.UsingClient("user/收货地址.html");
			}
		}
	}
}
