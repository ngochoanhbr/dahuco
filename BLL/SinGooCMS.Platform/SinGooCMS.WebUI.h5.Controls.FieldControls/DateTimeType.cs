using SinGooCMS.Common;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.h5.Controls.FieldControls
{
	public class DateTimeType : BaseFieldControl
	{
		protected TextBox txtField;

		private bool isLoad = false;

		public override string FieldValue
		{
			get
			{
				return WebUtils.GetString(this.txtField.Text);
			}
			set
			{
				if (base.Settings.IsDataType && base.Settings.DataFormat.Trim().Equals("yyyy-MM-dd"))
				{
					this.txtField.Text = WebUtils.GetDateTime(value).ToString("yyyy-MM-dd");
				}
				else
				{
					this.txtField.Text = value;
				}
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
				this.txtField.Attributes.Add("FieldID", base.FieldName);
				if (!base.EnableNull)
				{
					this.txtField.Attributes.Add("required", "required");
				}
				if (base.Settings != null)
				{
					this.txtField.Width = Unit.Pixel(base.Settings.ControlWidth);
					this.txtField.MaxLength = base.DataLength;
					this.txtField.Attributes.Add("data-date-format", base.Settings.DataFormat);
				}
			}
			this.isLoad = true;
		}
	}
}
