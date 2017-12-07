using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Text;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.MarketMger
{
	public class ModifyGroupon : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected HiddenField hdf_ProID;

		protected TextBox TextBox2;

		protected TextBox TextBox4;

		protected TextBox TextBox5;

		protected FullImage Image1;

		protected TextBox TextBox3;

		protected H5TextBox TextBox6;

		protected Repeater rpt_img;

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
			GrouponInfo dataById = SinGooCMS.BLL.Groupon.GetDataById(base.OpID);
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
				this.TextBox6.Text = dataById.JoinNum.ToString();
				this.TextBox7.Text = dataById.ActDesc;
				this.rpt_img.DataSource = dataById.PriceLadders;
				this.rpt_img.DataBind();
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
					GrouponInfo grouponInfo = new GrouponInfo();
					if (base.IsEdit)
					{
						grouponInfo = SinGooCMS.BLL.Groupon.GetDataById(base.OpID);
					}
					grouponInfo.ProID = dataById.AutoID;
					grouponInfo.ActName = WebUtils.GetString(this.TextBox2.Text);
					grouponInfo.ActImage = WebUtils.GetString(this.TextBox3.Text);
					grouponInfo.StartTime = WebUtils.GetDateTime(this.TextBox4.Text);
					grouponInfo.EndTime = WebUtils.GetDateTime(this.TextBox5.Text);
					grouponInfo.JoinNum = WebUtils.GetInt(this.TextBox6.Text);
					grouponInfo.ActDesc = WebUtils.GetString(this.TextBox7.Text);
					grouponInfo.PriceLadder = this.GetPriceLadder();
					if (string.IsNullOrEmpty(grouponInfo.ActName))
					{
						base.ShowMsg("请输入活动显示名称");
					}
					else if (grouponInfo.StartTime > grouponInfo.EndTime)
					{
						base.ShowMsg("开始时间不能超过截止时间");
					}
					else
					{
						if (base.Action.Equals(ActionType.Add.ToString()))
						{
							grouponInfo.Sort = SinGooCMS.BLL.Promotion.MaxSort + 1;
							grouponInfo.AutoTimeStamp = System.DateTime.Now;
							if (SinGooCMS.BLL.Groupon.Add(grouponInfo) > 0)
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加团购活动[" + grouponInfo.ActName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"Groupon.aspx?CatalogID=",
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
							if (SinGooCMS.BLL.Groupon.Update(grouponInfo))
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改团购活动[" + grouponInfo.ActName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"Groupon.aspx?CatalogID=",
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

		private string GetPriceLadder()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string formString = WebUtils.GetFormString("numdd");
			string formString2 = WebUtils.GetFormString("xsprice");
			string[] array = formString.Split(new char[]
			{
				','
			});
			string[] array2 = formString2.Split(new char[]
			{
				','
			});
			if (formString.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					int @int = WebUtils.GetInt(array[i]);
					decimal @decimal = WebUtils.GetDecimal(array2[i]);
					if (@int > 0 || !(@decimal <= 0m))
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"{\"JoinNum\":",
							@int.ToString(),
							",\"Price\":",
							@decimal.ToString(),
							"},"
						}));
					}
				}
			}
			return "[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]";
		}
	}
}
