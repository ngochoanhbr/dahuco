using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class DictItem : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Repeater Repeater1;

		protected LinkButton btn_SaveSort;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			DictsInfo dataById = Dicts.GetDataById(base.OpID);
			if (dataById != null)
			{
				this.Repeater1.DataSource = dataById.Items;
				this.Repeater1.DataBind();
			}
		}

		protected void lnk_Delete_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				DictsInfo dataById = Dicts.GetDataById(base.OpID);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thông tin từ điển không được tìm thấy, từ điển không tồn tại hoặc đã bị xóa");
				}
				else
				{
					string commandArgument = ((LinkButton)sender).CommandArgument;
					System.Collections.Generic.List<DictItemInfo> list = new System.Collections.Generic.List<DictItemInfo>();
					foreach (DictItemInfo current in dataById.Items)
					{
						if (!(current.KeyName == commandArgument.Split(new char[]
						{
							'|'
						})[0]) || !(current.KeyValue == commandArgument.Split(new char[]
						{
							'|'
						})[1]))
						{
							list.Add(current);
						}
					}
					dataById.DictValue = JsonUtils.ObjectToJson<System.Collections.Generic.List<DictItemInfo>>(list);
					if (Dicts.Update(dataById))
					{
						CacheUtils.Del("JsonLeeCMS_CacheForGetDicts");
						this.BindData();
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "xóa từ điển[" + commandArgument + "] thành công");
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
					}
					else
					{
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
					}
				}
			}
		}

		protected void btn_DelBat_Click(object sender, System.EventArgs e)
		{
		}

		protected void btn_SaveSort_Click(object sender, System.EventArgs e)
		{
			DictsInfo dataById = Dicts.GetDataById(base.OpID);
			System.Collections.Generic.List<DictItemInfo> list = new System.Collections.Generic.List<DictItemInfo>();
			string[] array = WebUtils.GetFormString("key").Split(new char[]
			{
				','
			});
			string[] array2 = WebUtils.GetFormString("value").Split(new char[]
			{
				','
			});
			string[] array3 = WebUtils.GetFormString("sort").Split(new char[]
			{
				','
			});
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					list.Add(new DictItemInfo
					{
						KeyName = array[i],
						KeyValue = array2[i],
						Sort = WebUtils.GetInt(array3[i])
					});
				}
				list = (from p in list
				orderby p.Sort
				select p).ToList<DictItemInfo>();
				dataById.DictValue = JsonUtils.ObjectToJson<System.Collections.Generic.List<DictItemInfo>>(list);
				if (Dicts.Update(dataById))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "Đặt từ điển [" + dataById.DictName + "] sắp xếp thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}
	}
}
