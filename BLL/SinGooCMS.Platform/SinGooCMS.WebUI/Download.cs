using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.IO;

namespace SinGooCMS.WebUI
{
	public class Download : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string text = WebUtils.GetQueryString("file");
			try
			{
				text = DEncryptUtils.DESDecode(text);
			}
			catch
			{
				text = string.Empty;
			}
			string text2 = base.Server.MapPath(text);
			if (System.IO.File.Exists(text2))
			{
				FileUploadInfo model = PageBase.dbo.GetModel<FileUploadInfo>(" select top 1 * from sys_FileUpload where VirtualPath='" + text + "' ");
				if (model != null)
				{
					model.DownloadCount++;
					FileUpload.Update(model);
				}
				ResponseUtils.ResponseFile(text2);
			}
			else
			{
				base.Response.Write(base.GetCaption("CMS_FileNotExist"));
				base.Response.End();
			}
		}
	}
}
