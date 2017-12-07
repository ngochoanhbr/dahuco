using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Control;
using SinGooCMS.Utility;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ContMger
{
	public class FileConfig : H5ManagerPageBase
	{
		protected HtmlInputCheckBox CheckBox1;

		protected TextBox TextBox2;

		protected H5TextBox TextBox3;

		protected TextBox TextBox4;

		protected DropDownList ddl_cutmode;

		protected H5TextBox TextBox5;

		protected H5TextBox TextBox6;

		protected Literal position;

		protected RadioButtonList sytype;

		protected TextBox TextBox10;

		protected DropDownList watermarkfontname;

		protected H5TextBox TextBox11;

		protected H5TextBox TextBox12;

		protected TextBox TextBox13;

		protected H5TextBox TextBox14;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.LoadSystemFont();
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			BaseConfigInfo cacheBaseConfig = ConfigProvider.GetCacheBaseConfig();
			if (cacheBaseConfig != null)
			{
				this.CheckBox1.Checked = cacheBaseConfig.EnableFileUpload;
				this.TextBox2.Text = cacheBaseConfig.EnableUploadExt;
				this.TextBox3.Text = cacheBaseConfig.UploadSizeLimit.ToString();
				this.TextBox4.Text = cacheBaseConfig.UploadSaveRule;
				if (cacheBaseConfig.ThumbSize.IndexOf("X") == -1)
				{
					this.TextBox11.Text = "0";
					this.TextBox5.Text = "0";
				}
				else
				{
					this.TextBox5.Text = cacheBaseConfig.ThumbSize.Split(new char[]
					{
						'X'
					})[0];
					this.TextBox6.Text = cacheBaseConfig.ThumbSize.Split(new char[]
					{
						'X'
					})[1];
				}
				ListItem listItem = this.ddl_cutmode.Items.FindByValue(cacheBaseConfig.ThumbModel);
				if (listItem != null)
				{
					listItem.Selected = true;
				}
				this.LoadPosition(cacheBaseConfig.WaterMarkPosition);
				ListItem listItem2 = this.sytype.Items.FindByText(cacheBaseConfig.WaterMarkType);
				if (listItem2 != null)
				{
					listItem2.Selected = true;
				}
				this.TextBox10.Text = cacheBaseConfig.WaterMarkText;
				this.TextBox11.Text = cacheBaseConfig.WaterMarkTextSize.ToString();
				ListItem listItem3 = this.watermarkfontname.Items.FindByText(cacheBaseConfig.WaterMarkTextFont);
				if (listItem3 != null)
				{
					listItem3.Selected = true;
				}
				this.TextBox12.Text = cacheBaseConfig.WaterMarkTextColor;
				this.TextBox13.Text = cacheBaseConfig.WaterMarkImage;
				this.TextBox14.Text = cacheBaseConfig.WaterMarkAlpha.ToString();
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				BaseConfigInfo baseConfigInfo = ConfigProvider.GetCacheBaseConfig();
				if (baseConfigInfo == null)
				{
					baseConfigInfo = new BaseConfigInfo();
				}
				baseConfigInfo.EnableFileUpload = this.CheckBox1.Checked;
				baseConfigInfo.EnableUploadExt = WebUtils.GetString(this.TextBox2.Text);
				baseConfigInfo.UploadSizeLimit = WebUtils.GetInt(this.TextBox3.Text, 0);
				baseConfigInfo.UploadSaveRule = WebUtils.GetString(this.TextBox4.Text);
				int @int = WebUtils.GetInt(this.TextBox5.Text);
				int int2 = WebUtils.GetInt(this.TextBox6.Text);
				baseConfigInfo.ThumbSize = @int.ToString() + "X" + int2.ToString();
				baseConfigInfo.ThumbModel = this.ddl_cutmode.SelectedValue;
				baseConfigInfo.WaterMarkText = WebUtils.GetString(this.TextBox10.Text);
				baseConfigInfo.WaterMarkImage = WebUtils.GetString(this.TextBox13.Text);
				baseConfigInfo.WaterMarkPosition = WebUtils.GetFormInt("watermarkstatus");
				baseConfigInfo.WaterMarkType = this.sytype.SelectedValue;
				baseConfigInfo.WaterMarkTextSize = WebUtils.GetInt(this.TextBox11.Text, 12);
				baseConfigInfo.WaterMarkAlpha = WebUtils.GetDouble(this.TextBox14.Text, 0.6);
				baseConfigInfo.WaterMarkTextFont = this.watermarkfontname.SelectedValue;
				baseConfigInfo.WaterMarkTextColor = WebUtils.GetString(this.TextBox12.Text);
				if (ConfigProvider.Update(baseConfigInfo))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
					CacheUtils.Del("JsonLeeCMS_CacheForVER");
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新文件参数成功");
					base.ShowMsg("Cập nhật thành công");
				}
				else
				{
					base.ShowMsg("Cập nhật thất bại");
				}
			}
		}

		public void LoadPosition(int selectid)
		{
			this.position.Text = "<table width=\"256\" height=\"207\" border=\"0\" background=\"/Include/Images/flower.jpg\">";
			for (int i = 1; i < 10; i++)
			{
				if (i % 3 == 1)
				{
					Literal expr_2D = this.position;
					expr_2D.Text += "<tr>";
				}
				Literal expr_49 = this.position;
				expr_49.Text += ((selectid == i) ? string.Concat(new object[]
				{
					"<td width=\"33%\" align=\"center\" style=\"vertical-align:middle;\"><input class='checkbox_radio' type=\"radio\" id=\"watermarkstatus\" name=\"watermarkstatus\" value=\"",
					i,
					"\" checked><b>#",
					i,
					"</b></td>"
				}) : string.Concat(new object[]
				{
					"<td width=\"33%\" align=\"center\" style=\"vertical-align:middle;\"><input class='checkbox_radio' type=\"radio\" id=\"watermarkstatus\" name=\"watermarkstatus\" value=\"",
					i,
					"\" ><b>#",
					i,
					"</b></td>"
				}));
				if (i % 3 == 0)
				{
					Literal expr_E1 = this.position;
					expr_E1.Text += "</tr>";
				}
			}
			Literal expr_10E = this.position;
			expr_10E.Text += "</table><input class='checkbox_radio' type=\"radio\" id=\"watermarkstatus\" name=\"watermarkstatus\" value=\"0\" ";
			if (selectid == 0)
			{
				Literal expr_135 = this.position;
				expr_135.Text += " checked";
			}
			Literal expr_151 = this.position;
			expr_151.Text += ">不启用水印功能";
		}

		private void LoadSystemFont()
		{
			this.watermarkfontname.Items.Clear();
			InstalledFontCollection installedFontCollection = new InstalledFontCollection();
			FontFamily[] families = installedFontCollection.Families;
			for (int i = 0; i < families.Length; i++)
			{
				FontFamily fontFamily = families[i];
				this.watermarkfontname.Items.Add(new ListItem(fontFamily.Name, fontFamily.Name));
			}
		}
	}
}
