using SinGooCMS.BLL;

using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI
{
    public class GenerateAD : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AdPlaceInfo cacheAdPlaceById = AdPlace.GetCacheAdPlaceById(WebUtils.GetQueryInt("placeid"));
			if (cacheAdPlaceById != null)
			{
				base.Put("adplace", cacheAdPlaceById);
				base.Put("ads", Ads.GetCacheAdsByPlaceID(cacheAdPlaceById.AutoID));
				base.Using(cacheAdPlaceById.TemplateFile);
			}
		}
	}
}
