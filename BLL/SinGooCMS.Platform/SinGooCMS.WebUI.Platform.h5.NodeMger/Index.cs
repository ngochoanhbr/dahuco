using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SinGooCMS.WebUI.Platform.h5.NodeMger
{
	public class Index : H5ManagerPageBase
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

		public int nodeParentID = 0;

		private NodeInfo nodeParent = null;

		private ContentPublish publish = new ContentPublish();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.nodeParentID = WebUtils.GetQueryInt("ParentID");
			this.nodeParent = SinGooCMS.BLL.Node.GetCacheNodeById(this.nodeParentID);
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " Sort asc,Depth asc,RootID asc ";
			if (this.nodeParent != null)
			{
				strSort = " Depth asc,RootID asc,Sort asc ";
			}
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = this.GetWithChilds(SinGooCMS.BLL.Node.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num));
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		private System.Collections.Generic.IList<NodeInfo> GetWithChilds(System.Collections.Generic.IList<NodeInfo> lstParents)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			foreach (NodeInfo current in lstParents)
			{
				stringBuilder.Append(current.AutoID.ToString() + ",");
			}
			string strIds = stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			});
			System.Collections.Generic.List<NodeInfo> list = (from p in SinGooCMS.BLL.Node.GetCacheAllNodes()
			where strIds.Contains(p.RootID.ToString())
			orderby p.RootID
			orderby p.Depth
			orderby p.Sort
			select p).ToList<NodeInfo>();
			System.Collections.Generic.IList<NodeInfo> result;
			if (list != null && list.Count > 0)
			{
				System.Collections.Generic.List<NodeInfo> list2 = new System.Collections.Generic.List<NodeInfo>();
				foreach (NodeInfo current in lstParents)
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

		private System.Collections.Generic.List<NodeInfo> GetAllChilds(int parentID, System.Collections.Generic.List<NodeInfo> lstChilds)
		{
			System.Collections.Generic.List<NodeInfo> list = new System.Collections.Generic.List<NodeInfo>();
			foreach (NodeInfo current in lstChilds)
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
			string text = " 1=1 ";
			if (this.nodeParent != null)
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					" AND (AutoID=",
					this.nodeParent.AutoID,
					" OR ParentID=",
					this.nodeParent.AutoID,
					") "
				});
			}
			else
			{
				text += " And ParentID=0 ";
			}
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND NodeName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
			}
			return text;
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
				NodeInfo cacheNodeById = SinGooCMS.BLL.Node.GetCacheNodeById(@int);
				NodeDeleteStatus nodeDeleteStatus = SinGooCMS.BLL.Node.Delete(cacheNodeById.AutoID);
				NodeDeleteStatus nodeDeleteStatus2 = nodeDeleteStatus;
				switch (nodeDeleteStatus2)
				{
				case NodeDeleteStatus.Error:
					base.ShowAjaxMsg(this.UpdatePanel1, "删除失败");
					break;
				case NodeDeleteStatus.NotExists:
					base.ShowAjaxMsg(this.UpdatePanel1, "没有找到栏目,栏目不存在或已被删除");
					break;
				case NodeDeleteStatus.HasChildNode:
					base.ShowAjaxMsg(this.UpdatePanel1, "删除失败,存在下级栏目");
					break;
				case NodeDeleteStatus.HasContent:
					base.ShowAjaxMsg(this.UpdatePanel1, "删除失败,存在内容");
					break;
				default:
					if (nodeDeleteStatus2 == NodeDeleteStatus.Success)
					{
						this.BindData();
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除栏目[" + cacheNodeById.NodeName + "] thành công");
						base.ShowAjaxMsg(this.UpdatePanel1, "删除栏目成功");
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
			return ContModel.GetCacheModelByID(intModelID).ModelName;
		}

		protected void btn_SaveSort_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.Dictionary<int, int> repeaterSortDict = base.GetRepeaterSortDict(this.Repeater1, "txtsort", "autoid");
			if (repeaterSortDict.Count > 0)
			{
				if (SinGooCMS.BLL.Node.UpdateSort(repeaterSortDict))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "设置栏目排序成功");
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
				string text = base.ImportFolder + "NodeData.xml";
				text = base.Server.MapPath(text);
				postedFile.SaveAs(text);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(text);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("NodeTemplate");
				if (elementsByTagName == null || (elementsByTagName != null && elementsByTagName.Count == 0))
				{
					base.ShowMsg("没有找到根目录 NodeTemplate");
				}
				else
				{
					foreach (XmlNode xmlNode in elementsByTagName[0].ChildNodes)
					{
						if (xmlNode.Name != "NodeTemplate")
						{
							this.AddNode(xmlNode, 0);
						}
					}
				}
			}
			this.BindData();
		}

		private void AddNode(XmlNode node, int intParentID)
		{
			NodeInfo nodeInfo = (from p in SinGooCMS.BLL.Node.GetCacheAllNodes()
			where p.AutoID.Equals(intParentID)
			select p).FirstOrDefault<NodeInfo>();
			string strModelName = "普通文章";
			if (node.Attributes["model"] != null)
			{
				strModelName = node.Attributes["model"].Value.Trim();
			}
			ContModelInfo modelByName = ContModel.GetModelByName(strModelName);
			string templateOfNodeIndex = "栏目首页.html";
			if (node.Attributes["tpindex"] != null)
			{
				templateOfNodeIndex = node.Attributes["tpindex"].Value.Trim();
			}
			else if (nodeInfo != null)
			{
				templateOfNodeIndex = nodeInfo.NodeSetting.TemplateOfNodeIndex;
			}
			string templateOfNodeList = "栏目列表页.html";
			if (node.Attributes["tplist"] != null)
			{
				templateOfNodeList = node.Attributes["tplist"].Value.Trim();
			}
			else if (nodeInfo != null)
			{
				templateOfNodeList = nodeInfo.NodeSetting.TemplateOfNodeList;
			}
			string templateOfNodeContent = "栏目详情页.html";
			if (node.Attributes["tpdetail"] != null)
			{
				templateOfNodeContent = node.Attributes["tpdetail"].Value.Trim();
			}
			else if (nodeInfo != null)
			{
				templateOfNodeContent = nodeInfo.NodeSetting.TemplateOfNodeContent;
			}
			if (node.Attributes["name"] != null && modelByName != null)
			{
				string nodeName = node.Attributes["name"].Value.Trim();
				string urlRewriteName = StringUtils.GetNewFileName();
				if (node.Attributes["id"] != null)
				{
					urlRewriteName = node.Attributes["id"].Value.Trim();
				}
				bool isShowOnMenu = false;
				if (node.Attributes["ismenu"] != null)
				{
					isShowOnMenu = (node.Attributes["ismenu"].Value.Trim() == "1");
				}
				NodeInfo nodeInfo2 = new NodeInfo();
				nodeInfo2.NodeName = nodeName;
				nodeInfo2.UrlRewriteName = urlRewriteName;
				nodeInfo2.ParentID = intParentID;
				nodeInfo2.ModelID = modelByName.AutoID;
				nodeInfo2.NodeBanner = string.Empty;
				nodeInfo2.SeoKey = nodeInfo2.NodeName;
				nodeInfo2.SeoDescription = nodeInfo2.NodeName;
				nodeInfo2.Remark = string.Empty;
				nodeInfo2.ItemPageSize = 10;
				nodeInfo2.IsShowOnMenu = isShowOnMenu;
				nodeInfo2.IsShowOnNav = false;
				nodeInfo2.IsTop = false;
				nodeInfo2.IsRecommend = false;
				nodeInfo2.CustomLink = string.Empty;
				nodeInfo2.Lang = base.cultureLang;
				nodeInfo2.AutoTimeStamp = System.DateTime.Now;
				nodeInfo2.NodeSetting.EnableAddInParent = true;
				nodeInfo2.NodeSetting.AllowComment = true;
				nodeInfo2.NodeSetting.NeedLogin = false;
				string empty = string.Empty;
				nodeInfo2.NodeSetting.EnableViewUGroups = empty;
				string empty2 = string.Empty;
				nodeInfo2.NodeSetting.EnableViewULevel = empty2;
				nodeInfo2.NodeSetting.TemplateOfNodeIndex = templateOfNodeIndex;
				nodeInfo2.NodeSetting.TemplateOfNodeList = templateOfNodeList;
				nodeInfo2.NodeSetting.TemplateOfNodeContent = templateOfNodeContent;
				int num = 0;
				if (SinGooCMS.BLL.Node.Add(nodeInfo2, out num) == NodeAddStatus.Success)
				{
					ContentInfo entity = new ContentInfo
					{
						NodeID = num,
						NodeName = nodeInfo2.NodeName,
						ModelID = modelByName.AutoID,
						TableName = modelByName.TableName,
						Title = "栏目(" + nodeInfo2.NodeName + ")的测试标题",
						Summary = "栏目(" + nodeInfo2.NodeName + ")的测试摘要",
						Content = "栏目(" + nodeInfo2.NodeName + ")的测试内容",
						Sort = SinGooCMS.BLL.Content.MaxSort + 1,
						Lang = base.cultureLang,
						Inputer = base.LoginAccount.AccountName,
						Status = 99,
						AutoTimeStamp = System.DateTime.Now
					};
					int num2 = SinGooCMS.BLL.Content.Add(entity);
					if (num2 > 0)
					{
						System.Collections.Generic.IList<ContFieldInfo> customFieldListByModelID = ContField.GetCustomFieldListByModelID(modelByName.AutoID);
						foreach (ContFieldInfo current in customFieldListByModelID)
						{
							if (current.FieldName == "ContID")
							{
								current.Value = num2.ToString();
							}
							else
							{
								current.Value = string.Empty;
							}
						}
						SinGooCMS.BLL.Content.AddCustomContent(modelByName, customFieldListByModelID);
					}
					if (node.HasChildNodes)
					{
						foreach (XmlNode node2 in node.ChildNodes)
						{
							this.AddNode(node2, num);
						}
					}
				}
			}
		}

		protected void btn_ExportXML_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.List<NodeInfo> list = (System.Collections.Generic.List<NodeInfo>)SinGooCMS.BLL.Node.GetCacheChildNode(0);
			if (list != null && list.Count > 0)
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?><NodeTemplate>");
				foreach (NodeInfo current in list)
				{
					stringBuilder.Append(this.NodeToXml(current));
				}
				stringBuilder.Append("</NodeTemplate>");
				string path = base.Server.MapPath(base.ExportFolder + "NodeData.xml");
				System.IO.File.WriteAllText(path, stringBuilder.ToString(), System.Text.Encoding.UTF8);
				base.Response.Redirect("/include/download?file=" + DEncryptUtils.DESEncode(base.ExportFolder + "NodeData.xml"));
			}
			else
			{
				base.ShowMsg("没有找到任何记录");
			}
		}

		private string NodeToXml(NodeInfo node)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			ContModelInfo cacheModelByID = ContModel.GetCacheModelByID(node.ModelID);
			stringBuilder.Append(string.Concat(new object[]
			{
				"<Node name=\"",
				node.NodeName,
				"\" id=\"",
				node.UrlRewriteName,
				"\" model=\"",
				cacheModelByID.ModelName,
				"\" ismenu=\"",
				node.IsShowOnMenu ? 1 : 0,
				"\" tpindex=\"",
				node.NodeSetting.TemplateOfNodeIndex,
				"\" tplist=\"",
				node.NodeSetting.TemplateOfNodeList,
				"\" tpdetail=\"",
				node.NodeSetting.TemplateOfNodeContent,
				"\">"
			}));
			if (node.ChildCount > 0)
			{
				foreach (NodeInfo current in SinGooCMS.BLL.Node.GetCacheChildNode(node.AutoID))
				{
					stringBuilder.Append(this.NodeToXml(current));
				}
			}
			stringBuilder.Append("</Node>");
			return stringBuilder.ToString();
		}
	}
}
