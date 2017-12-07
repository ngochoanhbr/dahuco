using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Utility;
using System;
using System.Data;
using System.Web;

namespace SinGooCMS.WebUI.Ajax
{
    public class AjaxEvalution : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			HttpContext current = HttpContext.Current;
			current.Response.ContentType = "text/plain";
			string text = string.Empty;
			string empty = string.Empty;
			if (current.Request.RequestType == enumQueryType.GET.ToString())
			{
				text = current.Request.QueryString["t"];
				string text2 = text;
				if (text2 != null)
				{
					if (!(text2 == "getevalist"))
					{
						if (text2 == "getevapagerfun")
						{
							this.GetEvaPagerFun();
						}
					}
					else
					{
						this.GetEvaList();
					}
				}
			}
		}

		private void GetEvaPagerFun()
		{
			int queryInt = WebUtils.GetQueryInt("pageindex", 1);
			int queryInt2 = WebUtils.GetQueryInt("pagesize", 20);
			int count = Evaluation.GetCount(" ProID=" + WebUtils.GetQueryInt("pid", 0) + " AND IsAudit=1 ");
			CMSPager cMSPager = new CMSPager(count, queryInt2);
			cMSPager.UrlPattern = "geteva($page)";
			cMSPager.PageIndex = queryInt;
			cMSPager.Calculate();
			base.Put("evapager", cMSPager);
			base.UsingClient("inc/_evapagerfun.html");
		}

		private void GetEvaList()
		{
			int num = 0;
			int num2 = 0;
			DataSet pagerData = Evaluation.GetPagerData("*", "ProID=" + WebUtils.GetQueryInt("pid", 0) + " AND IsAudit=1", "AutoID desc", WebUtils.GetQueryInt("pagesize", 20), WebUtils.GetQueryInt("pageindex", 1), ref num, ref num2);
			DataTable dataTable = null;
			if (pagerData != null && pagerData.Tables.Count > 0)
			{
				dataTable = pagerData.Tables[0];
				DataColumn dataColumn = new DataColumn();
				dataColumn.ColumnName = "DateStr";
				dataColumn.DataType = System.Type.GetType("System.String");
				dataColumn.MaxLength = 50;
				dataTable.Columns.Add(dataColumn);
				foreach (DataRow dataRow in dataTable.Rows)
				{
					dataRow["DateStr"] = System.DateTime.Parse(dataRow["AutoTimeStamp"].ToString()).ToString();
					dataRow["UserName"] = StringUtils.GetAnonymous(dataRow["UserName"].ToString());
				}
				base.Response.Write(JsonUtils.DataTableToJson(dataTable));
			}
			else
			{
				base.Response.Write(string.Empty);
			}
		}
	}
}
