using SinGooCMS.Common;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.h5.Controls.FieldControls
{
	public class FileType : BaseFieldControl
	{
		protected TextBox txtFile;

		private bool isLoad = false;

		public override string FieldValue
		{
			get
			{
				return this.txtFile.Text;
			}
			set
			{
				this.txtFile.Text = value;
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
				this.txtFile.Attributes.Add("FieldID", base.FieldName);
				if (!base.EnableNull)
				{
					this.txtFile.Attributes.Add("tip", "请输入内容");
					this.txtFile.Attributes.Add("datatype", "require");
					this.txtFile.Attributes.Add("require", "true");
					this.txtFile.Attributes.Add("msg", "*");
				}
				if (base.Settings != null)
				{
					this.txtFile.Width = Unit.Pixel(base.Settings.ControlWidth);
					this.txtFile.MaxLength = base.DataLength;
				}
			}
			this.isLoad = true;
		}
	}
}
