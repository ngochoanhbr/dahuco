using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;

namespace SinGooCMS.WebUI.Shop
{
    public class ShoppingCart : SinGooCMS.BLL.Custom.UIPageBase
	{
		private decimal decTotalMoney = 0m;

		private string strCartID;

		private string strAction;

		private string strIds;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.strCartID = this.shop.GetCartID();
			this.strAction = WebUtils.GetQueryString("action").ToLower();
			this.strIds = WebUtils.GetQueryString("ids");
			if (!string.IsNullOrEmpty(this.strAction))
			{
				string text = this.strAction;
				if (text != null)
				{
					if (!(text == "del"))
					{
						if (!(text == "clear"))
						{
							if (!(text == "plus"))
							{
								if (!(text == "minus"))
								{
									if (text == "quantity")
									{
										int queryInt = WebUtils.GetQueryInt("num", 1);
										CartInfo cart = Cart.GetCart(WebUtils.GetInt(this.strIds));
										if (cart != null)
										{
											GoodsSpecifyInfo dataById = GoodsSpecify.GetDataById(cart.GoodsAttr);
											int stock = cart.Product.Stock;
											if (dataById != null)
											{
												stock = dataById.Stock;
											}
											if (queryInt > stock)
											{
												cart.Quantity = stock;
											}
											else if (queryInt < 1)
											{
												cart.Quantity = 1;
											}
											else
											{
												cart.Quantity = queryInt;
											}
											cart.SubTotal = cart.Price * cart.Quantity;
											Cart.Update(cart);
										}
									}
								}
								else
								{
									CartInfo cart2 = Cart.GetCart(WebUtils.GetInt(this.strIds));
									if (cart2 != null)
									{
										if (cart2.Quantity > 1)
										{
											cart2.Quantity--;
										}
										cart2.SubTotal = cart2.Price * cart2.Quantity;
										Cart.Update(cart2);
									}
								}
							}
							else
							{
								CartInfo cart3 = Cart.GetCart(WebUtils.GetInt(this.strIds));
								if (cart3 != null)
								{
									GoodsSpecifyInfo dataById = GoodsSpecify.GetDataById(cart3.GoodsAttr);
									int stock = cart3.Product.Stock;
									if (dataById != null)
									{
										stock = dataById.Stock;
									}
									if (cart3.Quantity < stock)
									{
										cart3.Quantity++;
									}
									cart3.SubTotal = cart3.Price * cart3.Quantity;
									Cart.Update(cart3);
								}
							}
						}
						else
						{
							Cart.ClearByCartID(this.strCartID);
						}
					}
					else if (!string.IsNullOrEmpty(this.strIds))
					{
						Cart.Delete(this.strIds);
					}
				}
				base.Response.Redirect(UrlRewrite.Get("cart_url"));
			}
			else
			{
				System.Collections.Generic.IList<CartInfo> listByCartNo = Cart.GetListByCartNo(this.strCartID);
				foreach (CartInfo current in listByCartNo)
				{
					this.decTotalMoney += current.Quantity * current.Price;
				}
				base.Put("totalamount", this.decTotalMoney);
				base.UsingClient("购物车.html");
			}
		}
	}
}
