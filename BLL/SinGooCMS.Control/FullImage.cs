using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.Control
{
	[ToolboxData("<{0}:ExtImage runat=server></{0}:ExtImage>")]
	public class FullImage : Image
	{
		private string _EmptyImage = "/platform/h5/images/empty.png";

		public string EmptyImage
		{
			get
			{
				return this._EmptyImage;
			}
			set
			{
				this._EmptyImage = value;
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(this.ImageUrl))
			{
				this.ImageUrl = this.EmptyImage;
			}
			base.Render(writer);
		}
	}
}
