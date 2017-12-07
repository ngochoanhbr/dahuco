using SinGooCMS.Common;
using SinGooCMS.Config;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.h5.Controls.FieldControls
{
	public class PictureType : BaseFieldControl
	{
		public BaseConfigInfo config = ConfigProvider.Configs;

		private bool isLoad = false;

		protected TextBox txtImgUrl;

		protected Image Image1;

		public override string FieldValue
		{
			get
			{
				return this.txtImgUrl.Text;
			}
			set
			{
				this.txtImgUrl.Text = value;
				this.Image1.ImageUrl = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.InitSettings();
			}
		}

		private void InitSettings()
		{
			if (!this.isLoad)
			{
				this.txtImgUrl.Attributes.Add("FieldID", base.FieldName);
				if (!base.EnableNull)
				{
					this.txtImgUrl.Attributes.Add("tip", "请输入内容");
					this.txtImgUrl.Attributes.Add("datatype", "require");
					this.txtImgUrl.Attributes.Add("require", "true");
					this.txtImgUrl.Attributes.Add("msg", "*");
				}
				if (base.Settings != null)
				{
					this.txtImgUrl.Width = Unit.Pixel(base.Settings.ControlWidth);
					this.txtImgUrl.MaxLength = base.DataLength;
				}
			}
			this.isLoad = true;
		}

		public string GetImgUrl(string url)
		{
			url = "~/" + url;
			url = url.Replace("//", "/");
			return base.ResolveUrl(url);
		}
	}
}
