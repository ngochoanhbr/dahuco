using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ContMger
{
	public class Index : H5ManagerPageBase
	{
		public string strListType = "Normal";

		public ContStatus contStatus = ContStatus.AuditSuccess;

		private ContentPublish publish = new ContentPublish();

		protected UpdatePanel UpdatePanel1;

		protected DropDownList ddlNode;

		protected TextBox search_text;

		protected Button searchbtn;

		protected LinkButton btn_DelBat;

		protected Button btn_Clear;

		protected LinkButton btn_SaveSort;

		protected Button btn_AuditOK;

		protected Button btn_AuditCancel;

		protected DropDownList ddlMoveTo;

		protected Button btn_MoveTo;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.strListType = WebUtils.GetQueryString("ListType", "Normal");
			string text = this.strListType;
			if (text != null)
			{
				if (!(text == "Normal"))
				{
					if (!(text == "Recycle"))
					{
						if (text == "DraftBox")
						{
							this.contStatus = ContStatus.WaittingAudit;
							this.btn_AuditOK.Visible = true;
							this.btn_AuditCancel.Visible = true;
							this.ddlMoveTo.Visible = true;
							this.btn_MoveTo.Visible = true;
							this.btn_SaveSort.Visible = true;
							this.btn_DelBat.Visible = true;
						}
					}
					else
					{
						this.contStatus = ContStatus.Recycle;
						this.btn_Clear.Visible = true;
					}
				}
				else
				{
					this.contStatus = ContStatus.AuditSuccess;
					this.btn_AuditOK.Visible = true;
					this.btn_AuditCancel.Visible = true;
					this.ddlMoveTo.Visible = true;
					this.btn_MoveTo.Visible = true;
					this.btn_SaveSort.Visible = true;
					this.btn_DelBat.Visible = true;
				}
			}
			if (!base.IsPostBack)
			{
				this.BindNodeTree();
				this.BindData();
			}
		}

		private void BindNodeTree()
		{
			System.Collections.Generic.List<NodeInfo> nodeTreeList = SinGooCMS.BLL.Node.GetNodeTreeList();
			this.ddlMoveTo.DataSource = nodeTreeList;
			this.ddlMoveTo.DataTextField = "NodeName";
			this.ddlMoveTo.DataValueField = "AutoID";
			this.ddlMoveTo.DataBind();
			this.ddlNode.DataSource = nodeTreeList;
			this.ddlNode.DataTextField = "NodeName";
			this.ddlNode.DataValueField = "AutoID";
			this.ddlNode.DataBind();
			this.ddlNode.Items.Insert(0, new ListItem("所有栏目", "-1"));
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " Sort ASC,AutoID desc ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = SinGooCMS.BLL.Content.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			string text = " 1=1 ";
			int @int = WebUtils.GetInt(this.ddlNode.SelectedValue);
			NodeInfo nodeInfo = (@int > 0) ? SinGooCMS.BLL.Node.GetCacheNodeById(@int) : null;
			if (nodeInfo != null)
			{
				text = text + " AND NodeID in (" + nodeInfo.ChildList + ") ";
			}
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				" AND Status=",
				(int)this.contStatus,
				" "
			});
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND Title like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
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
				ContentInfo contentById = SinGooCMS.BLL.Content.GetContentById(@int);
				if (contentById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Không có dữ liệu được tìm thấy, các dữ liệu không tồn tại hoặc bị xóa");
				}
				else if (contentById.Status == -1)
				{
					if (SinGooCMS.BLL.Content.DelFromRecycle(contentById.AutoID))
					{
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
						this.BindData();
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "彻底删除内容[" + contentById.Title + "] thành công");
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
					}
					else
					{
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
					}
				}
				else if (SinGooCMS.BLL.Content.DelToRecycle(contentById.AutoID.ToString()))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
					if (PageBase.config.BrowseType.Equals(BrowseType.Html.ToString()))
					{
						this.publish.CreateIndex();
						this.CreateDGNode(SinGooCMS.BLL.Node.GetCacheNodeById(contentById.NodeID));
					}
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "移除内容[" + contentById.Title + "]到回收站");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}

		protected void btn_DelBat_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
				if (!string.IsNullOrEmpty(repeaterCheckIDs))
				{
					if (this.strListType.Equals("Recycle"))
					{
						SinGooCMS.BLL.Content.DelFromRecycle(repeaterCheckIDs);
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "批量删除回收站里的内容");
					}
					else
					{
						SinGooCMS.BLL.Content.DelToRecycle(repeaterCheckIDs);
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
						if (PageBase.config.BrowseType.Equals(BrowseType.Html.ToString()))
						{
							this.publish.CreateIndex();
							System.Collections.Generic.List<NodeInfo> list = (System.Collections.Generic.List<NodeInfo>)PageBase.dbo.GetList<NodeInfo>(" SELECT DISTINCT(NodeID) FROM cms_Content WHERE AutoID IN (" + repeaterCheckIDs + ") AND Status=99 ");
							if (list != null && list.Count > 0)
							{
								foreach (NodeInfo current in list)
								{
									this.CreateDGNode(current);
								}
							}
						}
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "批量删除内容到回收站");
					}
					this.BindData();
				}
			}
		}

		protected void btn_Clear_Click(object sender, System.EventArgs e)
		{
			if (SinGooCMS.BLL.Content.Clear())
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
				this.BindData();
				PageBase.log.AddEvent(base.LoginAccount.AccountName, "清空回收站");
				base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
			}
			else
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
			}
		}

		public string GetStatus(int intStatus)
		{
			string result = "审核通过";
			switch (intStatus)
			{
			case -2:
				result = "<span style='color:red'>审核不通过</span>";
				break;
			case -1:
				result = "<span style='color:red'>回收站</span>";
				break;
			case 0:
				result = "待审核";
				break;
			default:
				if (intStatus == 99)
				{
					result = "<span class='ok'>审核通过</span>";
				}
				break;
			}
			return result;
		}

		protected void btn_SaveSort_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.Dictionary<int, int> repeaterSortDict = base.GetRepeaterSortDict(this.Repeater1, "txtsort", "autoid");
			if (SinGooCMS.BLL.Content.UpdateSort(repeaterSortDict))
			{
				if (PageBase.config.BrowseType.Equals(BrowseType.Html.ToString()))
				{
					string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
					System.Collections.Generic.List<NodeInfo> list = (System.Collections.Generic.List<NodeInfo>)PageBase.dbo.GetList<NodeInfo>(" SELECT DISTINCT(NodeID) FROM cms_Content WHERE AutoID IN (" + repeaterCheckIDs + ") AND Status=99 ");
					this.publish.CreateIndex();
					if (list != null && list.Count > 0)
					{
						foreach (NodeInfo current in list)
						{
							this.CreateDGNode(current);
						}
					}
				}
				this.BindData();
				PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新内容排序成功");
				base.ShowAjaxMsg(this.UpdatePanel1, "更新排序成功");
			}
			else
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "更新排序失败");
			}
		}

		protected void lnk_Restore_Click1(object sender, System.EventArgs e)
		{
			LinkButton linkButton = (LinkButton)sender;
			int @int = WebUtils.GetInt(linkButton.CommandArgument);
			ContentInfo contentById = SinGooCMS.BLL.Content.GetContentById(@int);
			if (contentById == null)
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "没有找到内容,不存在或者被删除");
			}
			else if (SinGooCMS.BLL.Content.UpdateStatus(contentById.AutoID.ToString(), ContStatus.AuditSuccess))
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
				CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
				CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
				if (PageBase.config.BrowseType.Equals(BrowseType.Html.ToString()))
				{
					this.publish.CreateIndex();
					this.CreateDGNode(SinGooCMS.BLL.Node.GetCacheNodeById(contentById.NodeID));
					this.publish.CreateContent(contentById.AutoID);
				}
				this.BindData();
				PageBase.log.AddEvent(base.LoginAccount.AccountName, "还原内容[" + contentById.Title + "] thành công");
				base.ShowAjaxMsg(this.UpdatePanel1, "还原内容成功");
			}
			else
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "还原内容失败");
			}
		}

		protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				LinkButton linkButton = (LinkButton)e.Row.FindControl("lnk_Copy");
				LinkButton linkButton2 = (LinkButton)e.Row.FindControl("lnk_Delete");
				LinkButton linkButton3 = (LinkButton)e.Row.FindControl("lnk_DelThorough");
				LinkButton linkButton4 = (LinkButton)e.Row.FindControl("lnk_Restore");
				if (this.contStatus == ContStatus.Recycle)
				{
					linkButton3.Visible = true;
					linkButton4.Visible = true;
				}
				else
				{
					linkButton2.Visible = true;
					linkButton.Visible = true;
				}
			}
		}

		protected void btn_MoveTo_Click(object sender, System.EventArgs e)
		{
			string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
			if (!string.IsNullOrEmpty(repeaterCheckIDs))
			{
				NodeInfo cacheNodeById = SinGooCMS.BLL.Node.GetCacheNodeById(WebUtils.GetInt(this.ddlMoveTo.SelectedValue));
				if (cacheNodeById != null)
				{
					System.Collections.Generic.List<NodeInfo> list = (System.Collections.Generic.List<NodeInfo>)PageBase.dbo.GetList<NodeInfo>(" SELECT DISTINCT(NodeID) FROM cms_Content WHERE AutoID IN (" + repeaterCheckIDs + ") AND Status=99 ");
					if (SinGooCMS.BLL.Content.MoveContent(repeaterCheckIDs, cacheNodeById.AutoID, cacheNodeById.NodeName))
					{
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
						this.BindData();
						if (PageBase.config.BrowseType.Equals(BrowseType.Html.ToString()))
						{
							this.publish.CreateIndex();
							if (list != null && list.Count > 0)
							{
								foreach (NodeInfo current in list)
								{
									this.CreateDGNode(current);
								}
							}
							System.Collections.Generic.List<ContentInfo> list2 = (System.Collections.Generic.List<ContentInfo>)SinGooCMS.BLL.Content.GetList(1000, " AutoID IN (" + repeaterCheckIDs + ") AND Status=99 ");
							if (list2 != null && list2.Count > 0)
							{
								int num = 0;
								foreach (ContentInfo current2 in list2)
								{
									if (num != current2.NodeID)
									{
										num = current2.NodeID;
										this.CreateDGNode(SinGooCMS.BLL.Node.GetCacheNodeById(num));
									}
									this.publish.CreateContent(current2.AutoID);
								}
							}
						}
						PageBase.log.AddEvent(base.LoginAccount.AccountName, string.Concat(new string[]
						{
							"移动内容(",
							repeaterCheckIDs,
							")到栏目[",
							cacheNodeById.NodeName,
							"] thành công"
						}));
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
					}
				}
			}
		}

		protected void lnk_Copy_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Add.ToString()) && !base.IsAuthorizedOp(ActionType.Add.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				int num = WebUtils.StringToInt(((LinkButton)sender).CommandArgument);
				ContentInfo dataById = SinGooCMS.BLL.Content.GetDataById(num);
				if (SinGooCMS.BLL.Content.CopyContent(num))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "复制内容[" + dataById.Title + "] thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "复制内容成功");
				}
			}
		}

		protected void btn_AuditOK_Click(object sender, System.EventArgs e)
		{
			if (base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
				if (!string.IsNullOrEmpty(repeaterCheckIDs))
				{
					string[] array = repeaterCheckIDs.Split(new char[]
					{
						','
					});
					SinGooCMS.BLL.Content.UpdateStatus(repeaterCheckIDs, ContStatus.AuditSuccess);
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
					if (PageBase.config.BrowseType.Equals(BrowseType.Html.ToString()))
					{
						this.publish.CreateIndex();
						System.Collections.Generic.List<ContentInfo> list = (System.Collections.Generic.List<ContentInfo>)SinGooCMS.BLL.Content.GetList(1000, " AutoID IN (" + repeaterCheckIDs + ") AND Status=99 ");
						if (list != null && list.Count > 0)
						{
							int num = 0;
							foreach (ContentInfo current in list)
							{
								if (num != current.NodeID)
								{
									num = current.NodeID;
									this.CreateDGNode(SinGooCMS.BLL.Node.GetCacheNodeById(num));
								}
								this.publish.CreateContent(current.AutoID);
							}
						}
					}
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "批量审核文章通过[" + repeaterCheckIDs + "] thành công");
					this.BindData();
				}
			}
			else
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
		}

		protected void btn_AuditCancel_Click(object sender, System.EventArgs e)
		{
			if (base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
				if (!string.IsNullOrEmpty(repeaterCheckIDs))
				{
					SinGooCMS.BLL.Content.UpdateStatus(repeaterCheckIDs, ContStatus.WaittingAudit);
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
					CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
					if (PageBase.config.BrowseType.Equals(BrowseType.Html.ToString()))
					{
						this.publish.CreateIndex();
						System.Collections.Generic.List<ContentInfo> list = (System.Collections.Generic.List<ContentInfo>)SinGooCMS.BLL.Content.GetList(1000, " AutoID IN (" + repeaterCheckIDs + ") AND Status=0 ");
						if (list != null && list.Count > 0)
						{
							int num = 0;
							foreach (ContentInfo current in list)
							{
								if (num != current.NodeID)
								{
									num = current.NodeID;
									this.CreateDGNode(SinGooCMS.BLL.Node.GetCacheNodeById(num));
								}
								this.publish.CreateContent(current.AutoID);
							}
						}
					}
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "批量取消文章审核[" + repeaterCheckIDs + "] thành công");
					this.BindData();
				}
			}
			else
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
		}

		private void CreateDGNode(NodeInfo node)
		{
			if (node.ParentID > 0)
			{
				this.publish.CreateNode(node.AutoID);
				this.CreateDGNode(SinGooCMS.BLL.Node.GetCacheNodeById(node.ParentID));
			}
			else
			{
				this.publish.CreateNode(node.AutoID);
			}
		}
	}
}
