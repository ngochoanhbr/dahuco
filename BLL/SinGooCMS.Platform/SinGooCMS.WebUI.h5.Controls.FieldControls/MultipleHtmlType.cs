using CKEditor.NET;
using SinGooCMS.Common;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.h5.Controls.FieldControls
{
	public class MultipleHtmlType : BaseFieldControl
	{
		private bool isLoad = false;

		protected CKEditorControl txtContent;

		public override string FieldValue
		{
			get
			{
				return this.txtContent.Text;
			}
			set
			{
				this.txtContent.Text = value;
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
				this.txtContent.Attributes.Add("FieldID", base.FieldName);
				if (base.Settings != null)
				{
					int controlWidth = base.Settings.ControlWidth;
					if (controlWidth <= 100)
					{
						this.txtContent.Width = Unit.Percentage((double)controlWidth);
					}
					else
					{
						this.txtContent.Width = Unit.Pixel(controlWidth);
					}
					this.txtContent.Height = Unit.Pixel(base.Settings.ControlHeight);
				}
			}
			this.isLoad = true;
		}
	}
}
