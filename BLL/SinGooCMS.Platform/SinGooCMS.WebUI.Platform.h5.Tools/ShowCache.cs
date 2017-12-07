using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.Tools
{
	public class ShowCache : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Button btn_Clear;

		protected Repeater Repeater1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedAuthorized = false;
			if (!base.IsPostBack)
			{
				this.BindCacheAll();
			}
		}

		private void BindCacheAll()
		{
			System.Collections.Generic.List<CacheSet> list = new System.Collections.Generic.List<CacheSet>();
			System.Collections.Generic.List<string> keyList = CacheUtils.GetKeyList();
			foreach (string current in keyList)
			{
				list.Add(new CacheSet
				{
					CacheKey = current
				});
			}
			this.Repeater1.DataSource = list;
			this.Repeater1.DataBind();
		}

		protected void lnkdel_Click(object sender, System.EventArgs e)
		{
			string commandArgument = ((LinkButton)sender).CommandArgument;
			CacheUtils.Del(commandArgument);
			this.BindCacheAll();
		}

		protected void btn_Clear_Click(object sender, System.EventArgs e)
		{
			if (CacheUtils.ClearAll() > 0)
			{
				base.Response.Redirect(base.Request.RawUrl);
			}
		}
	}
}
