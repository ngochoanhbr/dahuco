using SinGooCMS.Common;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.h5.Controls.FieldControls
{
	public class SingleTextType : BaseFieldControl
	{
		private bool isLoad = false;

		protected TextBox txtField;

		public override string FieldValue
		{
			get
			{
				return this.txtField.Text;
			}
			set
			{
				this.txtField.Text = value;
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
				this.txtField.Attributes.Add("placeholder", base.Tips);
				if (base.Settings != null)
				{
					this.txtField.Width = Unit.Pixel(base.Settings.ControlWidth);
					this.txtField.MaxLength = base.DataLength;
					if (base.Settings.TextMode.Equals("Password"))
					{
						this.txtField.TextMode = TextBoxMode.Password;
					}
				}
			}
			this.isLoad = true;
		}
	}
}
