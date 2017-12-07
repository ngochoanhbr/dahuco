using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ADMger
{
	public class ModifyAd : H5ManagerPageBase
	{
		protected DropDownList ddlADPlace;

		protected TextBox TextBox1;

		protected TextBox TextBox4;

		protected FullImage Image1;

		protected TextBox TextBox6;

		protected TextBox TextBox2;

		protected TextBox timestart;

		protected TextBox timeend;

		protected HtmlInputCheckBox isaudit;

		protected Button btnok;

		public AdPlaceInfo adPlace = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.adPlace = AdPlace.GetCacheAdPlaceById(WebUtils.GetQueryInt("PlaceID"));
			if (!base.IsEdit && this.adPlace == null)
			{
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "alertandredirect", "<script>alert('没有找到广告位信息');$.dialog.close();</script>");
			}
			else if (!base.IsPostBack)
			{
				this.BindADPlace();
				this.timestart.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
				this.timeend.Text = System.DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
				if (base.IsEdit)
				{
					this.InitForModify();
				}
			}
		}

		private void BindADPlace()
		{
			this.ddlADPlace.DataSource = AdPlace.GetCacheAdPlaces();
			this.ddlADPlace.DataTextField = "PlaceName";
			this.ddlADPlace.DataValueField = "AutoID";
			this.ddlADPlace.DataBind();
			if (!base.IsEdit)
			{
				ListItem listItem = this.ddlADPlace.Items.FindByValue(this.adPlace.AutoID.ToString());
				if (listItem != null)
				{
					listItem.Selected = true;
				}
			}
		}

		private void InitForModify()
		{
			AdsInfo dataById = Ads.GetDataById(base.OpID);
			if (dataById != null)
			{
				this.adPlace = AdPlace.GetCacheAdPlaceById(dataById.PlaceID);
				ListItem listItem = this.ddlADPlace.Items.FindByValue(dataById.PlaceID.ToString());
				if (listItem != null)
				{
					listItem.Selected = true;
				}
				this.TextBox1.Text = dataById.AdName;
				this.Image1.ImageUrl = dataById.AdMediaPath;
				this.Image1.Attributes.Add("data-original", dataById.AdMediaPath);
				this.TextBox6.Text = dataById.AdMediaPath;
				this.TextBox2.Text = dataById.AdLink;
				this.timestart.Text = dataById.BeginDate.ToString("yyyy-MM-dd");
				this.timeend.Text = dataById.EndDate.ToString("yyyy-MM-dd");
				this.TextBox4.Text = dataById.AdText;
				this.isaudit.Checked = dataById.IsAudit;
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
				AdsInfo adsInfo = new AdsInfo();
				if (base.IsEdit)
				{
					adsInfo = Ads.GetDataById(base.OpID);
				}
				adsInfo.PlaceID = WebUtils.GetInt(this.ddlADPlace.SelectedValue);
				adsInfo.AdType = 1;
				adsInfo.AdName = WebUtils.GetString(this.TextBox1.Text);
				adsInfo.AdText = this.TextBox4.Text;
				adsInfo.AdMediaPath = WebUtils.GetString(this.TextBox6.Text);
				adsInfo.AdLink = WebUtils.GetString(this.TextBox2.Text);
				adsInfo.BeginDate = WebUtils.GetDateTime(this.timestart.Text);
				adsInfo.EndDate = WebUtils.GetDateTime(this.timeend.Text);
				adsInfo.IsAudit = this.isaudit.Checked;
				adsInfo.Lang = base.cultureLang;
				if (string.IsNullOrEmpty(adsInfo.AdName))
				{
					base.ShowMsg("请输入广告名称");
				}
				else if (adsInfo.EndDate < adsInfo.BeginDate)
				{
					base.ShowMsg("广告截止日期不能小于起始日期");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						adsInfo.Sort = Ads.MaxSort + 1;
						adsInfo.AutoTimeStamp = System.DateTime.Now;
						if (Ads.Add(adsInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加广告[" + adsInfo.AdName + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"AdsList.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&PlaceID=",
								WebUtils.GetQueryInt("PlaceID"),
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("添加广告失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (Ads.Update(adsInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改广告[" + adsInfo.AdName + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"AdsList.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&PlaceID=",
								adsInfo.PlaceID,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("修改广告失败");
						}
					}
				}
			}
		}
	}
}
