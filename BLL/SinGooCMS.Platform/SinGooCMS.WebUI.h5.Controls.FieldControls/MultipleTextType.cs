using SinGooCMS.Common;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.h5.Controls.FieldControls
{
	public class MultipleTextType : BaseFieldControl
	{
		protected TextBox txtField;

		private bool isLoad = false;

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
					this.txtField.Height = Unit.Pixel(base.Settings.ControlHeight);
					this.txtField.Attributes.Add("lenlimit", base.DataLength.ToString());
				}
			}
			this.isLoad = true;
		}
	}
}
