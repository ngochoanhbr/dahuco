using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Payments;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.WebUI.Platform.h5.Ajax
{
	public class AjaxMethod : ManagerPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = false;
			base.NeedAuthorized = false;
			if (base.Request.RequestType.ToUpper() == "GET")
			{
				string text = WebUtils.GetQueryString("type");
				string text2 = text;
				switch (text2)
				{
				case "GetCustomSettingCateXML":
					this.GetCustomSettingCateXML();
					break;
				case "GetNodeListXML":
					this.GetNodeListXML();
					break;
				case "GetContModelXML":
					this.GetContModelXML();
					break;
				case "GetUserGroupXML":
					this.GetUserGroupXML();
					break;
				case "GetDictListXML":
					this.GetDictListXML();
					break;
				case "GetProModelXML":
					this.GetProModelXML();
					break;
				case "GetProCateListForProXML":
					this.GetProCateListForProXML();
					break;
				case "GetDictItemsByName":
					this.GetDictItemsByName();
					break;
				case "GetChinaProvince":
					this.GetChinaProvince();
					break;
				case "GetChinaCity":
					this.GetChinaCity();
					break;
				case "GetChinaCounty":
					this.GetChinaCounty();
					break;
				case "GetFolderTreeXML":
					this.GetFolderTreeXML();
					break;
				case "GetGoodsClassListXML":
					this.GetGoodsClassListXML();
					break;
				case "GetPayTemplate":
					this.GetPayTemplate();
					break;
				case "GetUENews":
					this.GetUENews();
					break;
				case "getpro":
					this.GetPro();
					break;
				}
			}
			else if (base.Request.RequestType.ToUpper() == "POST")
			{
				string text = WebUtils.GetFormString("type");
				string text2 = text;
				if (text2 != null)
				{
					if (!(text2 == "AdminLogin"))
					{
						if (text2 == "ChangeSeo")
						{
							this.ChangeSeo();
						}
					}
					else
					{
						this.AdminLogin();
					}
				}
			}
		}

		private void GetCustomSettingCateXML()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			System.Collections.Generic.IList<SettingCategoryInfo> cacheSettingCategoryList = SettingCategory.GetCacheSettingCategoryList();
			if (cacheSettingCategoryList != null && cacheSettingCategoryList.Count > 0)
			{
				foreach (SettingCategoryInfo current in cacheSettingCategoryList)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"{id:'",
						current.AutoID.ToString(),
						"',name:'",
						current.CateDesc,
						"',isParent:false,'iconSkin':'leaf','click':\"AppendVal(",
						current.AutoID,
						",'",
						current.CateName,
						"')\"},"
					}));
				}
			}
			base.Response.Write("[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]");
		}

		private void GetNodeListXML()
		{
			int queryInt = WebUtils.GetQueryInt("id");
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			System.Collections.Generic.IList<NodeInfo> cacheChildNode = SinGooCMS.BLL.Node.GetCacheChildNode(queryInt);
			if (cacheChildNode != null && cacheChildNode.Count > 0)
			{
				foreach (NodeInfo current in cacheChildNode)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"{id:'",
						current.AutoID.ToString(),
						"',name:'",
						current.NodeName,
						"',namewithcount:'",
						current.NodeName,
						"(",
						current.ContCount,
						")',isParent:",
						(current.ChildCount > 0) ? "true" : "false",
						(current.ChildCount > 0) ? "" : ",'iconSkin':'leaf'",
						",'click':\"AppendVal(",
						current.AutoID,
						",'",
						current.NodeName,
						"')\"},"
					}));
				}
			}
			base.Response.Write("[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]");
		}

		private void GetContModelXML()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			System.Collections.Generic.IList<ContModelInfo> cacheModelList = ContModel.GetCacheModelList();
			if (cacheModelList != null && cacheModelList.Count > 0)
			{
				foreach (ContModelInfo current in cacheModelList)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"{id:'",
						current.AutoID.ToString(),
						"',name:'",
						current.ModelName,
						"',isParent:false,'iconSkin':'leaf','click':\"AppendVal(",
						current.AutoID,
						",'",
						current.ModelName,
						"')\"},"
					}));
				}
			}
			base.Response.Write("[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]");
		}

		private void GetUserGroupXML()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			System.Collections.Generic.IList<UserGroupInfo> cacheUserGroupList = UserGroup.GetCacheUserGroupList();
			if (cacheUserGroupList != null && cacheUserGroupList.Count > 0)
			{
				foreach (UserGroupInfo current in cacheUserGroupList)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"{id:'",
						current.AutoID.ToString(),
						"',name:'",
						current.GroupName,
						"',isParent:false,'iconSkin':'leaf','click':\"AppendVal(",
						current.AutoID,
						",'",
						current.GroupName,
						"')\"},"
					}));
				}
			}
			base.Response.Write("[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]");
		}

		private void GetDictListXML()
		{
			int queryInt = WebUtils.GetQueryInt("id");
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			System.Collections.Generic.List<DictsInfo> list = (System.Collections.Generic.List<DictsInfo>)Dicts.GetCacheDictsList();
			if (list != null && list.Count > 0)
			{
				foreach (DictsInfo current in list)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"{id:'",
						current.AutoID.ToString(),
						"',name:'",
						current.DisplayName,
						"',isParent:false,'iconSkin':'leaf','click':\"AppendVal(",
						current.AutoID,
						",'",
						current.DisplayName,
						"')\"},"
					}));
				}
			}
			base.Response.Write("[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]");
		}

		private void GetProModelXML()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			System.Collections.Generic.IList<ProductModelInfo> cacheModelList = ProductModel.GetCacheModelList();
			if (cacheModelList != null && cacheModelList.Count > 0)
			{
				foreach (ProductModelInfo current in cacheModelList)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"{id:'",
						current.AutoID.ToString(),
						"',name:'",
						current.ModelName,
						"',isParent:false,'iconSkin':'leaf','click':\"AppendVal(",
						current.AutoID,
						",'",
						current.ModelName,
						"')\"},"
					}));
				}
			}
			base.Response.Write("[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]");
		}

		private void GetProCateListForProXML()
		{
			int queryInt = WebUtils.GetQueryInt("id", 0);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			System.Collections.Generic.IList<CategoryInfo> cacheChildCate = Category.GetCacheChildCate(queryInt);
			if (cacheChildCate != null && cacheChildCate.Count > 0)
			{
				foreach (CategoryInfo current in cacheChildCate)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"{id:'",
						current.AutoID.ToString(),
						"',name:'",
						current.CategoryName,
						"',namewithcount:'",
						current.CategoryName,
						"(",
						current.GoodsCount,
						")',count:",
						current.GoodsCount,
						",isParent:",
						(current.ChildCount > 0) ? "true" : "false",
						(current.ChildCount > 0) ? "" : ",'iconSkin':'leaf'",
						",'click':\"AppendVal(",
						current.AutoID,
						",'",
						current.CategoryName,
						"')\"},"
					}));
				}
			}
			base.Response.Write("[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]");
		}

		private void GetDictItemsByName()
		{
			DictsInfo cacheDictsByName = Dicts.GetCacheDictsByName(WebUtils.GetQueryString("dictname"));
			if (cacheDictsByName != null && cacheDictsByName.Items.Count > 0)
			{
				base.Response.Write(JsonUtils.ObjectToJson<System.Collections.Generic.IList<DictItemInfo>>(cacheDictsByName.Items));
			}
			else
			{
				base.Response.Write(string.Empty);
			}
		}

		private void GetChinaProvince()
		{
			DataTable dataTable = PageBase.dbo.GetDataTable("SELECT ZoneEnName AS KeyName,ZoneName AS KeyValue FROM sys_Zone WHERE Depth=1 AND IsUsing=1");
			base.Response.Write(JsonUtils.DataTableToJson(dataTable));
		}

		private void GetChinaCity()
		{
			string strSQL = "SELECT ZoneEnName AS KeyName,ZoneName AS KeyValue FROM sys_Zone WHERE Depth=2 AND ParentID=" + WebUtils.GetQueryInt("ParentID", 1) + " AND IsUsing=1";
			DataTable dataTable = PageBase.dbo.GetDataTable(strSQL);
			base.Response.Write(JsonUtils.DataTableToJson(dataTable));
		}

		private void GetChinaCounty()
		{
			string strSQL = "SELECT ZoneEnName AS KeyName,ZoneName AS KeyValue FROM sys_Zone WHERE Depth=3 AND ParentID=" + WebUtils.GetQueryInt("ParentID", 369) + " AND IsUsing=1";
			DataTable dataTable = PageBase.dbo.GetDataTable(strSQL);
			base.Response.Write(JsonUtils.DataTableToJson(dataTable));
		}

		private void GetFolderTreeXML()
		{
			int queryInt = WebUtils.GetQueryInt("id", 0);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			System.Collections.Generic.IList<FolderInfo> list = Folder.GetList(1000, "ParentID=" + queryInt.ToString());
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
			base.Response.Write("[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]");
		}

		private void GetGoodsClassListXML()
		{
			int queryInt = WebUtils.GetQueryInt("id");
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			System.Collections.Generic.IList<GoodsClassInfo> list = GoodsClass.GetList(1000, "ParentID=" + queryInt);
			if (list != null && list.Count > 0)
			{
				foreach (GoodsClassInfo current in list)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						"{id:'",
						current.AutoID.ToString(),
						"',name:'",
						current.ClassName,
						"',isParent:",
						(current.ChildCount > 0) ? "true" : "false",
						(current.ChildCount > 0) ? "" : ",'iconSkin':'leaf'",
						",'click':\"AppendVal(",
						current.AutoID,
						",'",
						current.ClassName,
						"')\"},"
					}));
				}
			}
			base.Response.Write("[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]");
		}

		private void GetPayTemplate()
		{
			base.Response.Write(JsonUtils.ObjectToJson<PayTemplate>(PayTemplate.FindByPayCode(WebUtils.GetQueryString("paycode"))));
		}

		private void GetUENews()
		{
			base.Response.Write(UENews.GetJSON());
		}

		private void GetPro()
		{
			ProductInfo dataById = Product.GetDataById(WebUtils.GetQueryInt("pid"));
			base.Response.Write(JsonUtils.ObjectToJson<ProductInfo>(dataById));
		}

		private void AdminLogin()
		{
			string formString = WebUtils.GetFormString("_accountname");
			string formString2 = WebUtils.GetFormString("_accountpwd");
			string formString3 = WebUtils.GetFormString("_checkcode");
			this.Session["skin"] = ManagerSkin.Get(WebUtils.GetFormString("_skin"));
			if (base.CurrSkin.Name.Equals("blue") && string.Compare(base.ValidateCode, formString3, true) != 0)
			{
                base.WriteJsonTip(false, "Mã xác minh sai", "");
			}
			else if (string.IsNullOrEmpty(formString))
			{
                base.WriteJsonTip(false, "Nhập tên tài khoản", "");
			}
			else if (string.IsNullOrEmpty(formString2))
			{
                base.WriteJsonTip(false, "Vui lòng nhập mật khẩu tài khoản", "");
			}
			else if (Account.Login(formString, formString2))
			{
				PageBase.log.AddEvent(formString, "Đăng nhập quản lý platform成功", 1);
                base.WriteJsonTip(true, "Đăng nhập quản lý platform成功", base.CurrSkin.GlobalPath + "Main.aspx");
			}
			else
			{
                PageBase.log.AddEvent(formString, "Đăng nhập thất bại tài khoản! Hoặc mật khẩu không đúng", 1);
                base.WriteJsonTip(false, "Đăng nhập thất bại tài khoản! Hoặc mật khẩu không đúng", "");
			}
		}

		private void ChangeSeo()
		{
			int formInt = WebUtils.GetFormInt("_id");
			string formString = WebUtils.GetFormString("_key");
			string formString2 = WebUtils.GetFormString("_description");
			string strSQL = " update shop_Product set SEOKey='" + formString + "' where AutoID=" + formInt.ToString();
			if (!string.IsNullOrEmpty(formString2))
			{
				strSQL = " update shop_Product set SEODescription='" + formString2 + "' where AutoID=" + formInt.ToString();
			}
			if (PageBase.dbo.UpdateTable(strSQL))
			{
				base.WriteJsonTip(true, "Thao tác thành công", "");
			}
			else
			{
				base.WriteJsonTip(true, "Thao tác thất bại", "");
			}
		}
	}
}
