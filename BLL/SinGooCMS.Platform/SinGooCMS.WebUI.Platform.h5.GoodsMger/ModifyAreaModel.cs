using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class ModifyAreaModel : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected Button btnok;

		protected HtmlInputHidden hfIDs;

		public string[] OriginalCitys = new string[0];

		public string JsonForProvinceAndCity = string.Empty;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			AreaModelInfo dataById = AreaModel.GetDataById(base.OpID);
			if (dataById != null)
			{
				this.TextBox1.Text = dataById.ModelName;
				this.OriginalCitys = dataById.Citys.Split(new char[]
				{
					','
				});
			}
			System.Collections.Generic.IList<ZoneInfo> source = (from p in Zone.GetZoneList()
			orderby p.AutoID
			select p).ToList<ZoneInfo>();
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			System.Collections.Generic.List<ZoneInfo> list = (from p in source
			where p.Depth.Equals(1)
			select p).ToList<ZoneInfo>();
			if (list != null && list.Count > 0)
			{
				foreach (ZoneInfo itemProvince in list)
				{
					stringBuilder.Append("{\"Province\":\"" + itemProvince.ZoneName + "\",\"Citys\":[");
					System.Collections.Generic.List<ZoneInfo> list2 = (from p in source
					where p.Depth.Equals(2) && p.ParentID.Equals(itemProvince.AutoID)
					select p).ToList<ZoneInfo>();
					if (list2 != null && list2.Count > 0)
					{
						for (int i = 0; i < list2.Count; i++)
						{
							if (i == list2.Count - 1)
							{
								stringBuilder.Append(string.Concat(new object[]
								{
									"{\"AutoID\":",
									list2[i].AutoID,
									",\"City\":\"",
									list2[i].ZoneName,
									"\",\"IsChecked\":",
									(dataById != null && this.OriginalCitys.Contains(list2[i].AutoID.ToString())) ? 1 : 0,
									"}"
								}));
							}
							else
							{
								stringBuilder.Append(string.Concat(new object[]
								{
									"{\"AutoID\":",
									list2[i].AutoID,
									",\"City\":\"",
									list2[i].ZoneName,
									"\",\"IsChecked\":",
									(dataById != null && this.OriginalCitys.Contains(list2[i].AutoID.ToString())) ? 1 : 0,
									"},"
								}));
							}
						}
					}
					stringBuilder.Append("]},");
				}
			}
			this.JsonForProvinceAndCity = "[" + stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "]";
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
				AreaModelInfo areaModelInfo = new AreaModelInfo();
				if (base.IsEdit)
				{
					areaModelInfo = AreaModel.GetDataById(base.OpID);
				}
				areaModelInfo.ModelName = this.TextBox1.Text.Trim();
				areaModelInfo.Citys = this.hfIDs.Value.TrimEnd(new char[]
				{
					','
				});
				if (base.Action.Equals(ActionType.Add.ToString()))
				{
					areaModelInfo.Creator = base.LoginAccount.AccountName;
					areaModelInfo.AutoTimeStamp = System.DateTime.Now;
					if (AreaModel.Add(areaModelInfo) > 0)
					{
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加区域模板[" + areaModelInfo.ModelName + "] thành công");
						base.Response.Redirect(string.Concat(new object[]
						{
							"AreaModelList.aspx?CatalogID=",
							base.CurrentCatalogID,
							"&Module=",
							base.CurrentModuleCode,
							"&action=View"
						}));
					}
					else
					{
						base.ShowMsg("添加区域模板失败");
					}
				}
				if (base.Action.Equals(ActionType.Modify.ToString()))
				{
					if (AreaModel.Update(areaModelInfo))
					{
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改区域模板[" + areaModelInfo.ModelName + "] thành công");
						base.Response.Redirect(string.Concat(new object[]
						{
							"AreaModelList.aspx?CatalogID=",
							base.CurrentCatalogID,
							"&Module=",
							base.CurrentModuleCode,
							"&action=View"
						}));
					}
					else
					{
						base.ShowMsg("修改区域模板失败");
					}
				}
			}
		}
	}
}
