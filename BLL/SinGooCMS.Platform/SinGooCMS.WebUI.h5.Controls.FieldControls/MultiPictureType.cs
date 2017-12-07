using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.IO;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.h5.Controls.FieldControls
{
	public class MultiPictureType : BaseFieldControl
	{
		protected Repeater rpt_img;

		private bool isLoad = false;

		public override string FieldValue
		{
			get
			{
				string text = string.Empty;
				string text2 = (this.ViewState["gallery"] != null) ? this.ViewState["gallery"].ToString() : string.Empty;
				if (!string.IsNullOrEmpty(text2))
				{
					Attachment.Delete(text2);
				}
				string formString = WebUtils.GetFormString("imgselect-" + this.ClientID);
				string formString2 = WebUtils.GetFormString("imgdesc-" + this.ClientID);
				string[] array = formString.Split(new char[]
				{
					','
				});
				string[] array2 = formString2.Split(new char[]
				{
					','
				});
				if (array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						string text3 = array[i].Trim();
						if (!string.IsNullOrEmpty(text3))
						{
							AttachmentInfo entity = new AttachmentInfo
							{
								ContID = base.ContentID,
								FilePath = text3,
								ImgThumb = text3.Replace(System.IO.Path.GetExtension(text3), "_thumb" + System.IO.Path.GetExtension(text3)),
								Remark = array2[i],
								Sort = Attachment.MaxSort + 1,
								AutoTimeStamp = System.DateTime.Now
							};
							text = text + Attachment.Add(entity) + ",";
						}
					}
				}
				if (text.EndsWith(","))
				{
					text = text.Substring(0, text.Length - 1);
				}
				return text;
			}
			set
			{
				this.ViewState["gallery"] = value;
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
			string text = (this.ViewState["gallery"] == null) ? string.Empty : this.ViewState["gallery"].ToString();
			if (!string.IsNullOrEmpty(text))
			{
				this.rpt_img.DataSource = Attachment.GetList(100, "AutoID in (" + text + ")", "AutoID desc");
				this.rpt_img.DataBind();
			}
		}

		public string GetImgUrl(string url)
		{
			url = "~/" + url;
			url = url.Replace("//", "/");
			return base.ResolveUrl(url);
		}
	}
}
