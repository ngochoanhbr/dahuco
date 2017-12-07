using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class Category : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Button searchbtn;

		protected LinkButton btn_DelBat;

		protected LinkButton btn_SaveSort;

		protected Button btn_ExportXML;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected System.Web.UI.WebControls.FileUpload fu_Import;

		protected Button btn_Import;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " Sort asc,Depth asc,RootID asc,AutoID DESC ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = this.GetWithChilds(SinGooCMS.BLL.Category.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num));
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		private System.Collections.Generic.IList<CategoryInfo> GetWithChilds(System.Collections.Generic.IList<CategoryInfo> lstParents)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			foreach (CategoryInfo current in lstParents)
			{
				stringBuilder.Append(current.AutoID.ToString() + ",");
			}
			string strIds = stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			});
			System.Collections.Generic.List<CategoryInfo> list = (from p in SinGooCMS.BLL.Category.GetCacheCategoryList()
			where strIds.Contains(p.RootID.ToString())
			orderby p.RootID
			orderby p.Depth
			orderby p.Sort
			select p).ToList<CategoryInfo>();
			System.Collections.Generic.IList<CategoryInfo> result;
			if (list != null && list.Count > 0)
			{
				System.Collections.Generic.List<CategoryInfo> list2 = new System.Collections.Generic.List<CategoryInfo>();
				foreach (CategoryInfo current in lstParents)
				{
					list2.Add(current);
					if (current.ChildCount > 0)
					{
						list2.AddRange(this.GetAllChilds(current.AutoID, list));
					}
				}
				result = list2;
			}
			else
			{
				result = lstParents;
			}
			return result;
		}

		private System.Collections.Generic.List<CategoryInfo> GetAllChilds(int parentID, System.Collections.Generic.List<CategoryInfo> lstChilds)
		{
			System.Collections.Generic.List<CategoryInfo> list = new System.Collections.Generic.List<CategoryInfo>();
			foreach (CategoryInfo current in lstChilds)
			{
				if (current.ParentID.Equals(parentID))
				{
					list.Add(current);
					if (current.ChildCount > 0)
					{
						list.AddRange(this.GetAllChilds(current.AutoID, lstChilds));
					}
				}
			}
			return list;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			string result = " 1=1 And ParentID=0 ";
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				result = " CategoryName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
			}
			return result;
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SinGooPager1.PageIndex = 1;
			this.BindData();
		}

		protected void lnk_Delete_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				int @int = WebUtils.GetInt((sender as LinkButton).CommandArgument);
				CategoryInfo cacheCategoryByID = SinGooCMS.BLL.Category.GetCacheCategoryByID(@int);
				NodeDeleteStatus nodeDeleteStatus = SinGooCMS.BLL.Category.Delete(cacheCategoryByID.AutoID);
				NodeDeleteStatus nodeDeleteStatus2 = nodeDeleteStatus;
				switch (nodeDeleteStatus2)
				{
				case NodeDeleteStatus.Error:
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
					break;
				case NodeDeleteStatus.NotExists:
					base.ShowAjaxMsg(this.UpdatePanel1, "没有找到分类,分类不存在或已被删除");
					break;
				case NodeDeleteStatus.HasChildNode:
					base.ShowAjaxMsg(this.UpdatePanel1, "删除失败,存在下级分类");
					break;
				case NodeDeleteStatus.HasContent:
					base.ShowAjaxMsg(this.UpdatePanel1, "删除失败,存在所属产品");
					break;
				default:
					if (nodeDeleteStatus2 == NodeDeleteStatus.Success)
					{
						this.BindData();
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除产品分类[" + cacheCategoryByID.CategoryName + "] thành công");
						base.ShowAjaxMsg(this.UpdatePanel1, "删除分类成功");
					}
					break;
				}
			}
		}

		protected void btn_DelBat_Click(object sender, System.EventArgs e)
		{
		}

		public string GetModelName(int intModelID)
		{
			return ProductModel.GetCacheModelById(intModelID).ModelName;
		}

		protected void btn_SaveSort_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.Dictionary<int, int> repeaterSortDict = base.GetRepeaterSortDict(this.Repeater1, "txtsort", "autoid");
			if (repeaterSortDict.Count > 0)
			{
				if (SinGooCMS.BLL.Category.UpdateSort(repeaterSortDict))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "设置广告位排序成功");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}

		protected void btn_Import_Click(object sender, System.EventArgs e)
		{
			HttpPostedFile postedFile = this.fu_Import.PostedFile;
			if (postedFile == null || postedFile.ContentLength == 0)
			{
				base.ShowMsg("请选择文件");
			}
			else if (System.IO.Path.GetExtension(postedFile.FileName).ToLower() != ".xml")
			{
				base.ShowMsg("文件格式不正确，请上传xml文件");
			}
			else
			{
				string text = base.ImportFolder + "CategoryData.xml";
				text = base.Server.MapPath(text);
				postedFile.SaveAs(text);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(text);
				foreach (XmlNode xmlNode in xmlDocument.GetElementsByTagName("CategoryTemplate")[0].ChildNodes)
				{
					if (xmlNode.Name != "CategoryTemplate")
					{
						this.AddCategory(xmlNode, 0);
					}
				}
			}
			this.BindData();
		}

		private void AddCategory(XmlNode node, int intParentID)
		{
			CategoryInfo categoryInfo = (from p in SinGooCMS.BLL.Category.GetCacheCategoryList()
			where p.AutoID.Equals(intParentID)
			select p).FirstOrDefault<CategoryInfo>();
			string strModelName = "普通商品模型";
			if (node.Attributes["model"] != null)
			{
				strModelName = node.Attributes["model"].Value.Trim();
			}
			ProductModelInfo modelByName = ProductModel.GetModelByName(strModelName);
			if (node.Attributes["name"] != null && modelByName != null)
			{
				string text = node.Attributes["name"].Value.Trim();
				string text2 = StringUtils.GetNewFileName();
				if (node.Attributes["id"] != null)
				{
					text2 = node.Attributes["id"].Value.Trim();
				}
				CategoryInfo cate = new CategoryInfo
				{
					CategoryName = text,
					ParentID = intParentID,
					ModelID = modelByName.AutoID,
					SeoKey = text,
					SeoDescription = text,
					Remark = string.Empty,
					ItemPageSize = 10,
					IsTop = false,
					IsRecommend = false,
					Lang = base.cultureLang,
					AutoTimeStamp = System.DateTime.Now
				};
				int intParentID2 = 0;
				if (SinGooCMS.BLL.Category.Add(cate, out intParentID2) == NodeAddStatus.Success)
				{
					if (node.HasChildNodes)
					{
						foreach (XmlNode node2 in node.ChildNodes)
						{
							this.AddCategory(node2, intParentID2);
						}
					}
				}
			}
		}

		protected void btn_ExportXML_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.List<CategoryInfo> list = (System.Collections.Generic.List<CategoryInfo>)SinGooCMS.BLL.Category.GetCacheChildCate(0);
			if (list != null && list.Count > 0)
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?><CategoryTemplate>");
				foreach (CategoryInfo current in list)
				{
					stringBuilder.Append(this.NodeToXml(current));
				}
				stringBuilder.Append("</CategoryTemplate>");
				string path = base.Server.MapPath(base.ExportFolder + "CategoryData.xml");
				System.IO.File.WriteAllText(path, stringBuilder.ToString(), System.Text.Encoding.UTF8);
				base.Response.Redirect("/Include/Download?file=" + DEncryptUtils.DESEncode(base.ExportFolder + "CategoryData.xml"));
			}
			else
			{
				base.ShowMsg("没有找到任何记录");
			}
		}

		private string NodeToXml(CategoryInfo cate)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			ProductModelInfo cacheModelById = ProductModel.GetCacheModelById(cate.ModelID);
			stringBuilder.Append(string.Concat(new string[]
			{
				"<Cate name=\"",
				cate.CategoryName,
				"\" id=\"",
				cate.UrlRewriteName,
				"\" model=\"",
				(cacheModelById == null) ? "0" : cacheModelById.ModelName,
				"\" >"
			}));
			if (cate.ChildCount > 0)
			{
				foreach (CategoryInfo current in SinGooCMS.BLL.Category.GetCacheChildCate(cate.AutoID))
				{
					stringBuilder.Append(this.NodeToXml(current));
				}
			}
			stringBuilder.Append("</Cate>");
			return stringBuilder.ToString();
		}

		protected void btn_Export_Click(object sender, System.EventArgs e)
		{
			DataTable dataTable = PageBase.dbo.GetDataTable("SELECT AutoID AS 自动编号,CategoryName AS 分类名称,Identifier AS 分类标识,ParentID AS 上级分类,Depth AS 层级, ChildCount AS 子分类个数,ChildList AS 子分类,CategoryImage AS 分类图片,SeoKey AS 搜索关键字,SeoDescription AS 搜索描述, CustomLink AS 自定义链接,Creator AS 创建者,Sort AS 排序,Lang AS 当前语言,AutoTimeStamp AS 创建时间  FROM shop_Category WHERE " + this.GetCondition());
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				string text = base.ExportFolder + StringUtils.GetRandomNumber() + ".xls";
				text = base.Server.MapPath(text);
				DataToXSL.CreateXLS(dataTable, text, true);
				ResponseUtils.ResponseFile(text);
			}
			else
			{
				base.ShowMsg("没有找到任何记录");
			}
		}
	}
}
