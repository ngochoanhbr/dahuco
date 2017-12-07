using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class ModifyDictItem : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected H5TextBox TextBox3;

		protected Button btnok;

		public DictsInfo dictCurrent = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.dictCurrent = Dicts.GetDataById(WebUtils.GetQueryInt("dictid"));
			if (this.dictCurrent == null)
			{
                base.ShowMsgAndRdirect("Chúng tôi không tìm thấy thông tin từ điển, lỗi thông số", base.Request.UrlReferrer.ToString());
			}
			else if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			DictItemInfo dictItemInfo = (from p in Dicts.GetDataById(WebUtils.GetQueryInt("dictid")).Items
			where p.KeyName.Equals(WebUtils.GetQueryString("key"))
			select p).FirstOrDefault<DictItemInfo>();
			if (dictItemInfo != null)
			{
				this.TextBox1.Text = dictItemInfo.KeyName;
				this.TextBox2.Text = dictItemInfo.KeyValue;
				this.TextBox3.Text = dictItemInfo.Sort.ToString();
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
				DictsInfo dataById = Dicts.GetDataById(WebUtils.GetQueryInt("dictid"));
				string strKeyName = WebUtils.GetString(this.TextBox1.Text);
				string @string = WebUtils.GetString(this.TextBox2.Text);
				int @int = WebUtils.GetInt(this.TextBox3.Text);
				if (string.IsNullOrEmpty(strKeyName))
				{
                    base.ShowMsg("Nội dung Từ điển quan trọng không thể để trống");
				}
				else if (string.IsNullOrEmpty(@string))
				{
					base.ShowMsg("Giá trị từ điền không thể để trống");
				}
				else if (base.Action.Equals(ActionType.Add.ToString()) && dataById.Items.Exists((DictItemInfo p) => p.KeyName.Equals(strKeyName)))
				{
					base.ShowMsg("Nội dung từ điển đã tồn tại");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						string text = string.Empty;
						if (dataById.DictValue.IndexOf("[") == -1)
						{
							text = string.Concat(new object[]
							{
								"{\"KeyName\":\"",
								strKeyName,
								"\",\"KeyValue\":\"",
								@string,
								"\",\"Sort\":",
								@int,
								"}"
							});
						}
						else
						{
							text = string.Concat(new object[]
							{
								dataById.DictValue.TrimStart(new char[]
								{
									'['
								}).TrimEnd(new char[]
								{
									']'
								}),
								",{\"KeyName\":\"",
								strKeyName,
								"\",\"KeyValue\":\"",
								@string,
								"\",\"Sort\":",
								@int,
								"}"
							});
						}
						dataById.DictValue = "[" + text.TrimStart(new char[]
						{
							','
						}) + "]";
						if (Dicts.Update(dataById))
						{
							CacheUtils.Del("JsonLeeCMS_CacheForGetDicts");
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "Thêm nội dung từ điển [" + strKeyName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("Thao tác thất bại");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						DictItemInfo dictItemInfo = (from p in Dicts.GetDataById(WebUtils.GetQueryInt("dictid")).Items
						where p.KeyName.Equals(WebUtils.GetQueryString("key"))
						select p).FirstOrDefault<DictItemInfo>();
						string oldValue = string.Concat(new object[]
						{
							"{\"KeyName\":\"",
							dictItemInfo.KeyName,
							"\",\"KeyValue\":\"",
							dictItemInfo.KeyValue,
							"\",\"Sort\":",
							dictItemInfo.Sort,
							"}"
						});
						string newValue = string.Concat(new object[]
						{
							"{\"KeyName\":\"",
							strKeyName,
							"\",\"KeyValue\":\"",
							@string,
							"\",\"Sort\":",
							@int,
							"}"
						});
						dataById.DictValue = dataById.DictValue.Replace(oldValue, newValue);
						if (Dicts.Update(dataById))
						{
							CacheUtils.Del("JsonLeeCMS_CacheForGetDicts");
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "Sửa đổi nội dung từ điển [" + strKeyName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("Thao tác thất bại");
						}
					}
				}
			}
		}
	}
}
