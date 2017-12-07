using SinGooCMS.BLL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class ModifyGuiGePic : H5ManagerPageBase
	{
		public int proID = WebUtils.GetQueryInt("pid");

		public int classID = WebUtils.GetQueryInt("classid");

		public string guigeName = WebUtils.GetQueryString("guigename");

		public string[] arrGuiGeItems = WebUtils.GetQueryString("guigeitems").Split(new char[]
		{
			','
		});

		protected UpdatePanel UpdatePanel1;

		protected Repeater Repeater1;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			System.Collections.Generic.List<GuiGeItemImage> list = new System.Collections.Generic.List<GuiGeItemImage>();
			string[] array = this.arrGuiGeItems;
			for (int i = 0; i < array.Length; i++)
			{
				string guiGeItem = array[i];
				list.Add(new GuiGeItemImage
				{
					GuiGeItem = guiGeItem,
					ImgPath = string.Empty
				});
			}
			if (this.proID > 0)
			{
				GuiGePicInfo byProID = GuiGePic.GetByProID(this.proID);
				if (byProID != null && byProID.ImagesCollections != null && byProID.ImagesCollections.Count > 0 && byProID.ClassID == this.classID && byProID.GuiGeName == this.guigeName)
				{
					foreach (GuiGeItemImage current in byProID.ImagesCollections)
					{
						for (int j = 0; j < list.Count; j++)
						{
							if (current.GuiGeItem == list[j].GuiGeItem)
							{
								list[j].ImgPath = current.ImgPath;
							}
						}
					}
				}
			}
			this.Repeater1.DataSource = list;
			this.Repeater1.DataBind();
		}

		private string GetCondition()
		{
			return " 1=1 ";
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string[] array = base.Request.Form["guigeitem"].Split(new char[]
			{
				','
			});
			string[] array2 = base.Request.Form["imgpath"].Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (!string.IsNullOrEmpty(array2[i]))
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						"{\"GuiGeItem\":\"",
						array[i],
						"\",\"ImgPath\":\"",
						array2[i],
						"\"},"
					}));
				}
			}
			string text = stringBuilder.ToString().Trim();
			if (text.Length > 0)
			{
				text = "[" + text.TrimEnd(new char[]
				{
					','
				}) + "]";
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "alertandredirect", string.Concat(new string[]
				{
					"<script>$.dialog.open.origin.document.getElementById('",
					WebUtils.GetQueryString("retid"),
					"').value='",
					text,
					"';$.dialog.close();</script>"
				}));
			}
		}
	}
}
