using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Text;
using System.Collections.Generic;
namespace SinGooCMS.WebUI.List
{
    public class List_Company : SinGooCMS.BLL.Custom.UIPageBase
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
                
                CompanyInfo com = new CompanyInfo();
                if (text.Equals("modify"))
                {
                    com = Company.GetDataById(formInt);
                }
                com.Name = WebUtils.GetFormString("name");
                com.Address = WebUtils.GetFormString("address");
                com.Phone = WebUtils.GetFormString("phone");
                if (text == "del")
                {

                    if (Company.Delete(formInt))
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
                else if (string.IsNullOrEmpty(com.Address))
                {
                    base.WriteJsonTip(base.GetCaption("Comp_AddressNotEmpty"));
                }
                else if (string.IsNullOrEmpty(com.Phone))
                {
                    base.WriteJsonTip(base.GetCaption("Comp_PhoneNotEmpty"));
                }
                else if (text == "modify")
                {
                    if (Company.Update(com))
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
                    int num = Company.Add(com);
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

                StringBuilder builderUrlPattern = new StringBuilder(base.ResolveUrl("~/List_Company.aspx"));
                builderUrlPattern.Append("?");
                builderUrlPattern.Append("&page=$page");

                CMSPager pager = contents.GetPager(BLL.Company.GetCount(builderCondition.ToString()), intCurrentPage, pageSize, builderUrlPattern.ToString());
                base.Put("pager", pager);
                IList<CompanyInfo> company = BLL.Company.GetPagerList(builderCondition.ToString(), strSort, pager.PageIndex, pager.PageSize, ref intTotalCount, ref intTotalPage);
                base.Put("com", company);
                base.UsingClient("temp/content_company.html");
            }
		}
	}
}
