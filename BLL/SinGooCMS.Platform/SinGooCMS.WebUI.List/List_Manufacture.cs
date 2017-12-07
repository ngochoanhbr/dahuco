using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Text;
using System.Collections.Generic;
namespace SinGooCMS.WebUI.List
{
    public class List_Manufacture : SinGooCMS.BLL.Custom.UIPageBase
    {
        int intTotalCount = 0;
        int intTotalPage = 0;
		protected void Page_Load(object sender, System.EventArgs e)
        {
            base.NeedLogin = true;
            base.ReturnUrl = Request.RawUrl;
            if (base.IsPost && base.UserID != -1)
            {
                string text = WebUtils.GetFormString("_action", "add").ToLower();
                int formInt = WebUtils.GetFormInt("_idcom");

                ManufactureInfo com = new ManufactureInfo();
                if (text.Equals("modify"))
                {
                    com = Manufacture.GetDataById(formInt);
                }
                com.Name = WebUtils.GetFormString("name");
                com.Country = WebUtils.GetFormString("country");
                if (text == "del")
                {

                    if (Manufacture.Delete(formInt))
                    {
                        base.WriteJsonTip(true, base.GetCaption("Comp_Success"));
                    }
                    else
                    {
                        base.WriteJsonTip(base.GetCaption("Comp_DeleteFail"));
                    }
                }
                else if (string.IsNullOrEmpty(com.Name))
                {
                    base.WriteJsonTip(base.GetCaption("Comp_NameNotEmpty"));
                }
                else if (string.IsNullOrEmpty(com.Country))
                {
                    base.WriteJsonTip(base.GetCaption("Comp_PhoneNotEmpty"));
                }
                else if (text == "modify")
                {
                    if (Manufacture.Update(com))
                    {
                        base.WriteJsonTip(true, base.GetCaption("Comp_Success"));
                    }
                    else
                    {
                        base.WriteJsonTip(base.GetCaption("Comp_UpdateFail"));
                    }
                }
                else
                {
                    com.AutoTimeStamp = System.DateTime.Now;
                    int num = Manufacture.Add(com);
                    if (num > 0)
                    {
                        base.WriteJsonTip(true, base.GetCaption("Comp_Success"));
                    }
                    else
                    {
                        base.WriteJsonTip(base.GetCaption("Comp_AddFail"));
                    }
                }
            }


            else
            {
                int pageSize = 15;
                
                StringBuilder builderCondition = new StringBuilder(" 1=1 ");
                
                string strSort = " AutoID desc ";

                StringBuilder builderUrlPattern = new StringBuilder(base.ResolveUrl("~/List_Manufacture.aspx"));
                builderUrlPattern.Append("?");
                builderUrlPattern.Append("&page=$page");

                CMSPager pager = contents.GetPager(BLL.Manufacture.GetCount(builderCondition.ToString()), intCurrentPage, pageSize, builderUrlPattern.ToString());
                base.Put("pager", pager);
                IList<ManufactureInfo> Manufacture = BLL.Manufacture.GetPagerList(builderCondition.ToString(), strSort, pager.PageIndex, pager.PageSize, ref intTotalCount, ref intTotalPage);
                base.Put("man", Manufacture);
                base.UsingClient("temp/content_Manufacture.html");
            }
		}
	}
}
