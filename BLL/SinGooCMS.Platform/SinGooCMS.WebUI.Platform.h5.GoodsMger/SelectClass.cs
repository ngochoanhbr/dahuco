using SinGooCMS.BLL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class SelectClass : H5ManagerPageBase
	{
		public CategoryInfo cate = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			base.NeedAuthorized = true;
			this.cate = SinGooCMS.BLL.Category.GetCacheCategoryByID(WebUtils.GetQueryInt("CateID"));
			if (this.cate == null)
			{
				base.ShowMsgAndRdirect("商品分类不存在或者已删除", string.Concat(new object[]
				{
					"GoodsPulish.aspx?Catalog=",
					base.CurrentCatalogID,
					"&Module=",
					base.CurrentModuleCode,
					"&action=Add"
				}));
			}
		}
	}
}
