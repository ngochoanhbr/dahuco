using SinGooCMS.BLL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Text;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class MemberPrice : H5ManagerPageBase
	{
		protected Repeater Repeater1;

		protected Button btn_Save;

		public string strType = "goods";

		public ProductInfo proInit = null;

		public GoodsSpecifyInfo guige = null;

		public decimal decBasePrice = 0.0m;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.strType = WebUtils.GetQueryString("type");
			this.proInit = Product.GetDataById(base.OpID);
			this.guige = GoodsSpecify.GetDataById(base.OpID);
			this.decBasePrice = WebUtils.GetQueryDecimal("price");
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			string memberPriceSet = string.Empty;
			string text = this.strType;
			if (text != null)
			{
				if (!(text == "goods"))
				{
					if (text == "guige")
					{
						if (this.guige != null)
						{
							memberPriceSet = this.guige.MemberPriceSet;
						}
						this.Repeater1.DataSource = MemberPriceSet.GetList(memberPriceSet, this.decBasePrice);
					}
				}
				else
				{
					if (this.proInit != null)
					{
						memberPriceSet = this.proInit.MemberPriceSet;
					}
					this.Repeater1.DataSource = MemberPriceSet.GetList(memberPriceSet, this.decBasePrice);
				}
			}
			this.Repeater1.DataBind();
		}

		protected void btn_Save_Click(object sender, System.EventArgs e)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string formString = WebUtils.GetFormString("userlevelid");
			string formString2 = WebUtils.GetFormString("userlevelname");
			string formString3 = WebUtils.GetFormString("userprice");
			string formString4 = WebUtils.GetFormString("discountprice");
			string[] array = formString.Split(new char[]
			{
				','
			});
			string[] array2 = formString2.Split(new char[]
			{
				','
			});
			string[] array3 = formString3.Split(new char[]
			{
				','
			});
			string[] array4 = formString4.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(string.Concat(new object[]
				{
					"{\"UserLevelID\":",
					array[i],
					",\"UserLevelName\":\"",
					array2[i],
					"\",\"Price\":",
					WebUtils.GetDecimal(array3[i]),
					",\"DiscoutPrice\":",
					WebUtils.GetDecimal(array4[i]),
					"},"
				}));
			}
			string text = stringBuilder.ToString().Trim();
			if (text.Length > 0)
			{
				text = "[" + text.TrimEnd(new char[]
				{
					','
				}) + "]";
				text = text.Replace(",", "逗号");
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "alertandredirect", string.Concat(new string[]
				{
					"<script>$.dialog.open.origin.document.getElementById('",
					WebUtils.GetQueryString("retid"),
					"').value='",
					text,
					"';$.dialog.close();</script>"
				}));
			}
		}
	}
}
