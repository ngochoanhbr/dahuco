using SinGooCMS.BLL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SinGooCMS.WebUI.Platform.h5.Ajax
{
	public class getFolders : IHttpHandler
	{
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			int formInt = WebUtils.GetFormInt("id", 0);
			System.Collections.Generic.List<FolderInfo> list = (System.Collections.Generic.List<FolderInfo>)Folder.GetList(100, "ParentID=" + formInt);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			if (list != null && list.Count > 0)
			{
				foreach (FolderInfo current in list)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"{id:'",
						current.AutoID.ToString(),
						"',name:'",
						current.FolderName,
						"',isParent:",
						(current.ChildCount > 0) ? "true" : "false",
						(current.ChildCount > 0) ? "" : ",'iconSkin':'leaf'",
						",'click':\"AppendVal(",
						current.AutoID,
						",'",
						current.FolderName,
						"')\"},"
					}));
				}
			}
			context.Response.Write("[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]");
		}
	}
}
