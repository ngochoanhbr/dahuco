using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.MarketMger
{
	public class ModifyAuction : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected HiddenField hdf_ProID;

		protected TextBox TextBox2;

		protected TextBox TextBox4;

		protected TextBox TextBox5;

		protected FullImage Image1;

		protected TextBox TextBox3;

		protected H5TextBox TextBox8;

		protected H5TextBox TextBox9;

		protected H5TextBox TextBox10;

		protected H5TextBox TextBox11;

		protected H5TextBox TextBox12;

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
			AuctionInfo dataById = SinGooCMS.BLL.Auction.GetDataById(base.OpID);
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
				this.TextBox7.Text = dataById.ActDesc;
				this.TextBox8.Text = dataById.BasePrice.ToString("f2");
				this.TextBox9.Text = dataById.StepPrice.ToString("f2");
				this.TextBox10.Text = dataById.ProtectPrice.ToString("f2");
				this.TextBox11.Text = dataById.TopPrice.ToString("f2");
				this.TextBox12.Text = dataById.CashDeposit.ToString("f2");
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
					AuctionInfo auctionInfo = new AuctionInfo();
					if (base.IsEdit)
					{
						auctionInfo = SinGooCMS.BLL.Auction.GetDataById(base.OpID);
					}
					auctionInfo.ProID = dataById.AutoID;
					auctionInfo.ActName = WebUtils.GetString(this.TextBox2.Text);
					auctionInfo.ActImage = WebUtils.GetString(this.TextBox3.Text);
					auctionInfo.StartTime = WebUtils.GetDateTime(this.TextBox4.Text);
					auctionInfo.EndTime = WebUtils.GetDateTime(this.TextBox5.Text);
					auctionInfo.ActDesc = WebUtils.GetString(this.TextBox7.Text);
					auctionInfo.BasePrice = WebUtils.GetDecimal(this.TextBox8.Text);
					auctionInfo.StepPrice = WebUtils.GetDecimal(this.TextBox9.Text);
					auctionInfo.ProtectPrice = WebUtils.GetDecimal(this.TextBox10.Text);
					auctionInfo.TopPrice = WebUtils.GetDecimal(this.TextBox11.Text);
					auctionInfo.CashDeposit = WebUtils.GetDecimal(this.TextBox12.Text);
					if (string.IsNullOrEmpty(auctionInfo.ActName))
					{
						base.ShowMsg("请输入活动显示名称");
					}
					else if (auctionInfo.StartTime > auctionInfo.EndTime)
					{
						base.ShowMsg("开始时间不能超过截止时间");
					}
					else
					{
						if (base.Action.Equals(ActionType.Add.ToString()))
						{
							auctionInfo.Sort = SinGooCMS.BLL.Auction.MaxSort + 1;
							auctionInfo.AutoTimeStamp = System.DateTime.Now;
							if (SinGooCMS.BLL.Auction.Add(auctionInfo) > 0)
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加拍卖活动[" + auctionInfo.ActName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"Auction.aspx?CatalogID=",
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
							if (SinGooCMS.BLL.Auction.Update(auctionInfo))
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改拍卖活动[" + auctionInfo.ActName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"Auction.aspx?CatalogID=",
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
