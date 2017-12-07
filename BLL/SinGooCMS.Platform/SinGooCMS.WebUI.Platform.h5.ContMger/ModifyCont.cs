using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ContMger
{
	public class ModifyCont : H5ManagerPageBase
	{
		public string strType = "Normal";

		public NodeInfo contNode = null;

		public ContModelInfo contModel = null;

		public ContentInfo contInit = null;

		private ContentPublish publish = new ContentPublish();

		protected Literal nodepath;

		protected Repeater Repeater1;

		protected HtmlInputCheckBox isaudit;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.strType = WebUtils.GetQueryString("ListType", "Normal");
			this.contNode = SinGooCMS.BLL.Node.GetCacheNodeById(WebUtils.GetQueryInt("NodeID"));
			if (base.IsEdit)
			{
				this.contInit = SinGooCMS.BLL.Content.GetContentById(base.OpID);
				this.contNode = SinGooCMS.BLL.Node.GetCacheNodeById((this.contInit == null) ? 0 : this.contInit.NodeID);
			}
			if (this.contNode == null)
			{
				base.ShowMsgAndRdirect("没有找到栏目信息", string.Concat(new object[]
				{
					"ContentList.aspx?CatalogID=",
					base.CurrentCatalogID,
					"&Module=",
					base.CurrentModuleCode,
					"&action=View&ListType=",
					this.strType
				}));
			}
			else if (base.IsEdit && this.contInit == null)
			{
				base.ShowMsgAndRdirect("没有找到内容信息", string.Concat(new object[]
				{
					"ContentList.aspx?CatalogID=",
					base.CurrentCatalogID,
					"&Module=",
					base.CurrentModuleCode,
					"&action=View&ListType=",
					this.strType
				}));
			}
			else
			{
				this.contModel = (base.IsEdit ? ContModel.GetCacheModelByID(this.contInit.ModelID) : ContModel.GetCacheModelByID(this.contNode.ModelID));
				if (!base.IsPostBack)
				{
					this.BindData();
				}
			}
		}

		private void BindData()
		{
			System.Collections.Generic.IList<NodeInfo> nodeNav = new CMSContents().GetNodeNav(this.contNode.AutoID);
			string str = string.Empty;
			if (nodeNav != null && nodeNav.Count > 0)
			{
				foreach (NodeInfo current in nodeNav)
				{
					str = str + current.NodeName + " » ";
				}
			}
			this.nodepath.Text = str + this.contNode.NodeName;
			System.Collections.Generic.List<ContFieldInfo> list = (System.Collections.Generic.List<ContFieldInfo>)SinGooCMS.BLL.Content.GetFieldListWithValue(base.OpID, this.contModel.AutoID, this.contModel.TableName);
			list.Sort((ContFieldInfo parameterA, ContFieldInfo parameterB) => parameterA.Sort.CompareTo(parameterB.Sort));
			this.Repeater1.DataSource = list;
			this.Repeater1.DataBind();
			if (base.IsEdit)
			{
				this.isaudit.Checked = (this.contInit.Status == 99);
			}
		}

		protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				ContFieldInfo contFieldInfo = e.Item.DataItem as ContFieldInfo;
				FieldControl fieldControl = e.Item.FindControl("field") as FieldControl;
				if (fieldControl != null)
				{
					fieldControl.ControlType = (FieldType)contFieldInfo.FieldType;
					fieldControl.ControlPath = "~/Platform/h5/FieldControls/";
					fieldControl.LoadControlId = ((FieldType)contFieldInfo.FieldType).ToString();
					fieldControl.FieldName = contFieldInfo.FieldName;
					fieldControl.FieldAlias = contFieldInfo.Alias;
					fieldControl.Tips = contFieldInfo.Tip;
					fieldControl.FieldId = contFieldInfo.AutoID;
					fieldControl.Settings = XmlSerializerUtils.Deserialize<SinGooCMS.Control.FieldSetting>(contFieldInfo.Setting);
					fieldControl.DataLength = contFieldInfo.DataLength;
					fieldControl.EnableNull = contFieldInfo.EnableNull;
					if (!string.IsNullOrEmpty(contFieldInfo.Value))
					{
						fieldControl.Value = contFieldInfo.Value;
					}
					else
					{
						fieldControl.Value = (contFieldInfo.DefaultValue ?? string.Empty);
					}
					if (!base.IsEdit && fieldControl.FieldName == "Sort")
					{
						fieldControl.Value = (SinGooCMS.BLL.Content.MaxSort + 1).ToString();
					}
				}
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Add.ToString()) && !base.IsAuthorizedOp(ActionType.Add.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else if (base.Action.Equals(ActionType.Modify.ToString()) && !base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				if (base.Action.Equals(ActionType.Add.ToString()))
				{
					ContentInfo contentInfo = new ContentInfo();
					if (this.Add(ref contentInfo))
					{
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加内容[" + contentInfo.Title + "] thành công");
						base.Response.Redirect(string.Concat(new object[]
						{
							"Index.aspx?CatalogID=",
							base.CurrentCatalogID,
							"&Module=",
							base.CurrentModuleCode,
							"&action=View&ListType=",
							this.strType
						}));
					}
					else
					{
						base.ShowMsg("添加内容失败");
					}
				}
				if (base.Action.Equals(ActionType.Modify.ToString()))
				{
					ContentInfo contentById = SinGooCMS.BLL.Content.GetContentById(base.OpID);
					if (this.Modify(ref contentById))
					{
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
						CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改内容[" + contentById.Title + "] thành công");
						base.Response.Redirect(string.Concat(new object[]
						{
							"Index.aspx?CatalogID=",
							base.CurrentCatalogID,
							"&Module=",
							base.CurrentModuleCode,
							"&action=View&ListType=",
							this.strType
						}));
					}
					else
					{
						base.ShowMsg("修改内容失败");
					}
				}
			}
		}

		private bool Add(ref ContentInfo contentAdd)
		{
			bool result;
			if (SinGooCMS.BLL.Content.GetContCount() >= base.ver.ContentLimit)
			{
				base.ShowMsg("添加失败,超出最大内容数量");
				result = false;
			}
			else
			{
				System.Collections.Generic.Dictionary<string, ContFieldInfo> fieldDicWithValues = this.GetFieldDicWithValues();
				contentAdd.NodeID = this.contNode.AutoID;
				contentAdd.Status = (this.isaudit.Checked ? 99 : 0);
				contentAdd.Sort = SinGooCMS.BLL.Content.MaxSort + 1;
				int num = SinGooCMS.BLL.Content.Add(contentAdd, fieldDicWithValues);
				contentAdd.AutoID = num;
				result = (num > 0);
			}
			return result;
		}

		private System.Collections.Generic.Dictionary<string, ContFieldInfo> GetFieldDicWithValues()
		{
			System.Collections.Generic.Dictionary<string, ContFieldInfo> dictionary = new System.Collections.Generic.Dictionary<string, ContFieldInfo>();
			foreach (RepeaterItem repeaterItem in this.Repeater1.Items)
			{
				FieldControl fieldControl = repeaterItem.FindControl("field") as FieldControl;
				if (fieldControl != null)
				{
					ContFieldInfo dataById = SinGooCMS.BLL.ContField.GetDataById(fieldControl.FieldId);
					if (dataById != null)
					{
						dataById.Value = fieldControl.Value;
						dictionary.Add(dataById.FieldName, dataById);
					}
				}
			}
			return dictionary;
		}

		private bool Modify(ref ContentInfo contModify)
		{
			System.Collections.Generic.Dictionary<string, ContFieldInfo> fieldDicWithValues = this.GetFieldDicWithValues();
			contModify.Status = (this.isaudit.Checked ? 99 : 0);
			return SinGooCMS.BLL.Content.UpdateContent(contModify, fieldDicWithValues);
		}
	}
}
