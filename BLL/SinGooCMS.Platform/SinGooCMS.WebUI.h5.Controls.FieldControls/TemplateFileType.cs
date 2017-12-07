using SinGooCMS.Common;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.h5.Controls.FieldControls
{
	public class TemplateFileType : BaseFieldControl
	{
		protected TextBox txtTemplate;

		private bool isLoad = false;

		public override string FieldValue
		{
			get
			{
				return this.txtTemplate.Text;
			}
			set
			{
				this.txtTemplate.Text = value;
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
				this.txtTemplate.Attributes.Add("FieldID", base.FieldName);
				if (!base.EnableNull)
				{
					this.txtTemplate.Attributes.Add("tip", "请输入内容");
					this.txtTemplate.Attributes.Add("datatype", "require");
					this.txtTemplate.Attributes.Add("require", "true");
					this.txtTemplate.Attributes.Add("msg", "*");
				}
				if (base.Settings != null)
				{
					this.txtTemplate.Width = Unit.Pixel(base.Settings.ControlWidth);
					this.txtTemplate.MaxLength = base.DataLength;
				}
			}
			this.isLoad = true;
		}
	}
}
