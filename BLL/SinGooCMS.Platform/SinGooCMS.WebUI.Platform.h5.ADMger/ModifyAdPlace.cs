using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ADMger
{
	public class ModifyAdPlace : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox5;

		protected H5TextBox TextBox2;

		protected H5TextBox TextBox3;

		protected TextBox TextBox6;

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
			AdPlaceInfo dataById = AdPlace.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.PlaceName;
			this.TextBox2.Text = dataById.Width.ToString();
			this.TextBox3.Text = dataById.Height.ToString();
			this.TextBox5.Text = dataById.TemplateFile;
			this.TextBox6.Text = dataById.PlaceDesc;
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
				AdPlaceInfo adPlaceInfo = new AdPlaceInfo();
				if (base.IsEdit)
				{
					adPlaceInfo = AdPlace.GetDataById(base.OpID);
				}
				adPlaceInfo.PlaceName = WebUtils.GetString(this.TextBox1.Text);
				adPlaceInfo.Width = WebUtils.GetInt(this.TextBox2.Text);
				adPlaceInfo.Height = WebUtils.GetInt(this.TextBox3.Text);
				adPlaceInfo.TemplateFile = WebUtils.GetString(this.TextBox5.Text);
				adPlaceInfo.PlaceDesc = WebUtils.GetString(this.TextBox6.Text);
				adPlaceInfo.Lang = base.cultureLang;
				if (string.IsNullOrEmpty(adPlaceInfo.PlaceName))
				{
					base.ShowMsg("请输入广告位名称");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						adPlaceInfo.Price = 0.0m;
						adPlaceInfo.Sort = AdPlace.MaxSort + 1;
						adPlaceInfo.AutoTimeStamp = System.DateTime.Now;
						if (AdPlace.Add(adPlaceInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加广告位[" + adPlaceInfo.PlaceName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("添加广告位失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (AdPlace.Update(adPlaceInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改广告位[" + adPlaceInfo.PlaceName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改广告位失败");
						}
					}
				}
			}
		}
	}
}
