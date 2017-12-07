using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class SetPurview : H5ManagerPageBase
	{
		private System.Collections.Generic.IList<PurviewInfo> listCurrentRolePruview = null;

		public RoleInfo role = null;

		public string jsondata = string.Empty;

		protected Literal lbtext;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.role = SinGooCMS.BLL.Role.GetDataById(base.OpID);
			if (this.role == null)
			{
				base.ShowMsg("角色不存在");
			}
			else if (this.role.RoleName == "超级管理员")
			{
				base.ShowMsgAndRdirect("不能设置超级管理员", string.Concat(new string[]
				{
					"Role.aspx?CatalogID=",
					base.CurrentModuleCode,
					"&Module=",
					base.CurrentModuleCode,
					"&action=View"
				}));
			}
			else
			{
				this.listCurrentRolePruview = Purview.GetListByRoleID(this.role.AutoID);
				if (!base.IsPostBack)
				{
					this.BindPurview();
				}
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("SetPurview"))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else if (this.role != null && this.role.RoleName != "超级管理员")
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				string text = HttpContext.Current.Request.Form["purviewcollect"];
				if (!string.IsNullOrEmpty(text) && text.IndexOf(',') != -1)
				{
					stringBuilder.Append(" INSERT sys_Purview  (     RoleID,     ModuleID,     OperateCode ) ");
					string[] array = text.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i += 2)
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							" select ",
							this.role.AutoID,
							",",
							array[i],
							",'",
							array[i + 1],
							"' union all"
						}));
					}
					string text2 = stringBuilder.ToString();
					text2 = text2.Substring(0, text2.Length - "union all".Length);
					Purview.DeleteByRoleID(base.OpID);
					if (PageBase.dbo.ExecSQL(text2))
					{
						CacheUtils.Del("JsonLeeCMS_CacheForGetAccountMenuDT");
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新角色[" + this.role.RoleName + "]的权限设置成功");
						base.Response.Redirect(string.Concat(new object[]
						{
							"Role.aspx?CatalogID=",
							base.CurrentCatalogID,
							"&Module=",
							base.CurrentModuleCode,
							"&action=View"
						}));
					}
					else
					{
						base.ShowMsg("Thao tác thất bại");
					}
				}
			}
		}

		private void BindPurview()
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			System.Collections.Generic.IList<CatalogInfo> cacheCatalogList = Catalog.GetCacheCatalogList();
			if (cacheCatalogList != null && cacheCatalogList.Count > 0)
			{
				DataTable dt = Operate.GetOperateRelation();
				var source = from t in dt.AsEnumerable()
				group t by new
				{
                    //t1 = t.Field("ModuleName"),
                    //t2 = t.Field("ModuleCode")
				} into m
				select new
				{
                    //CatalogID = m.Max((DataRow p) => p.Field("CatalogID")),
                    //ModuleName = m.Key.t1,
                    //ModuleCode = m.Key.t2
				};
				foreach (CatalogInfo itemCatalog in cacheCatalogList)
				{
					builder.Append(string.Concat(new string[]
					{
						"{\"CatalogCode\":\"",
						itemCatalog.CatalogCode,
						"\",\"CatalogName\":\"",
						itemCatalog.CatalogName,
						"\",\"Modules\":["
					}));
                    //System.Collections.Generic.IEnumerable<<>f__AnonymousType1<int,string,string>> moduleForCatalog = from p in source
                    //where p.CatalogID.Equals(itemCatalog.AutoID)
                    //select p;
                    //if (moduleForCatalog != null && moduleForCatalog.ToList().Count > 0)
                    //{
                    //    int counter1 = 0;
                    //    moduleForCatalog.ToList().ForEach(delegate(p)
                    //    {
                    //        if (p.CatalogID == itemCatalog.AutoID)
                    //        {
                    //            if (counter1 == moduleForCatalog.ToList().Count - 1)
                    //            {
                    //                builder.Append(string.Concat(new string[]
                    //                {
                    //                    "{\"ModuleName\":\"",
                    //                    p.ModuleName,
                    //                    "\",\"ModuleCode\":\"",
                    //                    p.ModuleCode,
                    //                    "\",\"Operates\":[",
                    //                    this.GetOperators(p.ModuleCode, dt),
                    //                    "]}"
                    //                }));
                    //            }
                    //            else
                    //            {
                    //                builder.Append(string.Concat(new string[]
                    //                {
                    //                    "{\"ModuleName\":\"",
                    //                    p.ModuleName,
                    //                    "\",\"ModuleCode\":\"",
                    //                    p.ModuleCode,
                    //                    "\",\"Operates\":[",
                    //                    this.GetOperators(p.ModuleCode, dt),
                    //                    "]},"
                    //                }));
                    //            }
                    //        }
                    //        counter1++;
                    //    });
                    //}
					builder.Append("]},");
				}
			}
			this.lbtext.Text = (this.jsondata = "[" + builder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]");
		}

		private string GetOperators(string strModuleCode, DataTable dtSource)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			DataRow[] array = dtSource.Select("ModuleCode='" + strModuleCode + "'");
			if (array != null && array.Length > 0)
			{
				DataRow[] array2 = array;
				DataRow dr;
				for (int i = 0; i < array2.Length; i++)
				{
					dr = array2[i];
					PurviewInfo purviewInfo = (from p in this.listCurrentRolePruview
					where p.ModuleID.Equals(WebUtils.GetInt(dr["AutoID"])) && p.OperateCode == dr["OperateCode"].ToString()
					select p).FirstOrDefault<PurviewInfo>();
					stringBuilder.Append(string.Concat(new object[]
					{
						"{\"ModuleID\":",
						dr["AutoID"].ToString(),
						",\"OperateName\":\"",
						dr["OperateName"].ToString(),
						"\",\"OperateCode\":\"",
						dr["OperateCode"].ToString(),
						"\",\"HasPurview\":",
						(purviewInfo != null) ? 1 : 0,
						"},"
					}));
				}
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			});
		}
	}
}
