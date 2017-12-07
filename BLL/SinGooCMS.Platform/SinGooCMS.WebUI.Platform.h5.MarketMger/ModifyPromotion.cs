using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.MarketMger
{
	public class ModifyPromotion : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected HiddenField hdf_ProID;

		protected TextBox TextBox2;

		protected TextBox TextBox4;

		protected TextBox TextBox5;

		protected FullImage Image1;

		protected TextBox TextBox3;

		protected H5TextBox TextBox6;

		protected TextBox TextBox7;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.TextBox4.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				this.TextBox5.Text = System.DateTime.Now.AddDays(7.0).ToString("yyyy-MM-dd HH:mm:ss");
				if (base.IsEdit)
				{
					this.InitForModify();
				}
			}
		}

		private void InitForModify()
		{
			PromotionInfo dataById = SinGooCMS.BLL.Promotion.GetDataById(base.OpID);
			ProductInfo dataById2 = Product.GetDataById((dataById != null) ? dataById.ProID : 0);
			if (dataById != null && dataById2 != null)
			{
				this.TextBox1.Text = dataById2.ProductName;
				this.hdf_ProID.Value = dataById2.AutoID.ToString();
				this.TextBox2.Text = dataById.ActName;
				this.Image1.ImageUrl = dataById.ActImage;
				this.TextBox3.Text = dataById.ActImage;
				this.TextBox4.Text = dataById.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
				this.TextBox5.Text = dataById.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
				this.TextBox6.Text = dataById.PromotePrice.ToString("f2");
				this.TextBox7.Text = dataById.ActDesc;
			}
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
				ProductInfo dataById = Product.GetDataById(WebUtils.GetInt(this.hdf_ProID.Value));
				if (dataById == null)
				{
					base.ShowMsg("商品不存在或者已被删除！");
				}
				else
				{
					PromotionInfo promotionInfo = new PromotionInfo();
					if (base.IsEdit)
					{
						promotionInfo = SinGooCMS.BLL.Promotion.GetDataById(base.OpID);
					}
					promotionInfo.ProID = dataById.AutoID;
					promotionInfo.ActName = WebUtils.GetString(this.TextBox2.Text);
					promotionInfo.ActImage = WebUtils.GetString(this.TextBox3.Text);
					promotionInfo.StartTime = WebUtils.GetDateTime(this.TextBox4.Text);
					promotionInfo.EndTime = WebUtils.GetDateTime(this.TextBox5.Text);
					promotionInfo.PromotePrice = WebUtils.GetDecimal(this.TextBox6.Text);
					promotionInfo.ActDesc = WebUtils.GetString(this.TextBox7.Text);
					if (string.IsNullOrEmpty(promotionInfo.ActName))
					{
						base.ShowMsg("请输入活动显示名称");
					}
					else if (promotionInfo.StartTime > promotionInfo.EndTime)
					{
						base.ShowMsg("开始时间不能超过截止时间");
					}
					else
					{
						if (base.Action.Equals(ActionType.Add.ToString()))
						{
							promotionInfo.Sort = SinGooCMS.BLL.Promotion.MaxSort + 1;
							promotionInfo.AutoTimeStamp = System.DateTime.Now;
							if (SinGooCMS.BLL.Promotion.Add(promotionInfo) > 0)
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加限时抢购活动[" + promotionInfo.ActName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"Promotion.aspx?CatalogID=",
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
							if (SinGooCMS.BLL.Promotion.Update(promotionInfo))
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改限时抢购活动[" + promotionInfo.ActName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"Promotion.aspx?CatalogID=",
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
	}
}
