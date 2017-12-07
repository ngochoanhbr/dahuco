using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class UrlRewrite : H5ManagerPageBase
	{
		protected TextBox search_text;

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
			System.Collections.Generic.IList<UrlRewritEntity> list = new System.Collections.Generic.List<UrlRewritEntity>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(HttpContext.Current.Server.MapPath("/Config/url.config"));
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("rewriter/rewrite");
			if (xmlNodeList.Count > 0)
			{
				foreach (XmlNode xmlNode in xmlNodeList)
				{
					list.Add(new UrlRewritEntity
					{
						ID = ((xmlNode.Attributes["id"] == null) ? string.Empty : xmlNode.Attributes["id"].Value),
						Url = xmlNode.Attributes["url"].Value,
						To = xmlNode.Attributes["to"].Value,
						IsSystem = WebUtils.GetBool(xmlNode.Attributes["issystem"].Value)
					});
				}
			}
			this.Repeater1.DataSource = list;
			this.Repeater1.DataBind();
		}

		protected void btn_SaveRule_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?><rewriter><!--以下是系统固有的url重写，请不要改动，否则会找不到地址，id表示模板中调用的key，如 ${register_url},程序中调用 SinGooCMS.Common.UrlRewrite.Get(\"register_url\")-->");
				string[] array = base.Request.Form["_keyname"].Split(new char[]
				{
					','
				});
				string[] array2 = base.Request.Form["_sourceurl"].Split(new char[]
				{
					','
				});
				string[] array3 = base.Request.Form["_tourl"].Split(new char[]
				{
					','
				});
				string[] array4 = base.Request.Form["_issystem"].Split(new char[]
				{
					','
				});
				if (array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!string.IsNullOrEmpty(array[i]) && !string.IsNullOrEmpty(array2[i]) && !string.IsNullOrEmpty(array3[i]))
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"<rewrite id=\"",
								array[i],
								"\" url=\"",
								array2[i].Replace("&", "&amp;"),
								"\" to=\"",
								array3[i].Replace("&", "&amp;"),
								"\" issystem=\"",
								array4[i],
								"\" processing=\"stop\"/>"
							}));
						}
					}
				}
				stringBuilder.Append("</rewriter>");
				System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("/Config/url.config"), stringBuilder.ToString().Trim(), System.Text.Encoding.UTF8);
				this.BindData();
				base.ShowMsg("Thao tác thành công");
			}
			catch (System.Exception ex)
			{
				base.ShowMsg(ex.Message);
			}
		}
	}
}
