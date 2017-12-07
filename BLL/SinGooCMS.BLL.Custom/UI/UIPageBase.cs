using SinGooCMS.Common;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;

namespace SinGooCMS.BLL.Custom
{
	public class UIPageBase : CMSPageBase
	{
		protected int PageIndex = 0;

		protected int PageSize = 10;

		protected int TotalCount = 0;

		protected int TotalPage = 0;

		protected string Condition = string.Empty;

		protected string Sort = "AutoID DESC";

		protected string UrlPattern = string.Empty;

		protected string Action = "view";

		public UIPageBase()
		{
			this.PageIndex = WebUtils.GetQueryInt("page", 1);
			this.Action = WebUtils.GetQueryString("action");
		}

		protected void AutoPageing<T>(T model) where T : IEntity
		{
			IList<T> lst = PageBase.dbo.GetPager2005<T>(model.DBTableName, this.Condition, this.Sort, this.PageSize, this.PageIndex, ref this.TotalCount, ref this.TotalPage);
			CMSPager pager = this.contents.GetPager(this.TotalCount, this.PageIndex, this.PageSize, this.UrlPattern);
			base.Put("pager", pager);
			base.Put("jcdatas", lst);
			base.Put("condition", this.Condition);
		}

		protected void UsingClientSpecial(string strTemplateFile)
		{
			switch (base.ClientType)
			{
			case enumClient.Mobile:
				this.UsingLang("mobile/" + strTemplateFile);
				return;
			case enumClient.Weixin:
				this.UsingLang("weixin/" + strTemplateFile);
				return;
			}
			base.Using(strTemplateFile);
		}

		protected void UsingClient(string strTemplateFile)
		{
			switch (base.ClientType)
			{
			case enumClient.Mobile:
				this.UsingLang("mobile/" + strTemplateFile);
				return;
			case enumClient.Weixin:
				this.UsingLang("weixin/" + strTemplateFile);
				return;
			}
			this.UsingLang(strTemplateFile);
		}

		protected void UsingLang(string strTemplateFile)
		{
			string cultureLang = base.cultureLang;
			if (cultureLang != null)
			{
				if (cultureLang == "zh-cn")
				{
					base.Using(strTemplateFile);
					return;
				}
			}
			base.Using(base.cultureLang + "/" + strTemplateFile);
		}
	}
}
