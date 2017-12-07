using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Threading;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.MarketMger
{
	public class CopyCoupons : H5ManagerPageBase
	{
		protected H5TextBox TextBox1;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("Copy"))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				CouponsInfo dataById = SinGooCMS.BLL.Coupons.GetDataById(base.OpID);
				int @int = WebUtils.GetInt(this.TextBox1.Text);
				if (dataById == null)
				{
					base.ShowMsg("找不到原始优惠券信息！");
				}
				else if (@int <= 0)
				{
					base.ShowMsg("无效的复制张数！");
				}
				else if (@int > 10000)
				{
					base.ShowMsg("最大不能超过10000张！");
				}
				else
				{
					string text = string.Empty;
					for (int i = 0; i < @int; i++)
					{
						if (!SinGooCMS.BLL.Coupons.Copy(dataById))
						{
							text = text + "复制第" + i.ToString() + "条优惠券时发生错误";
							break;
						}
						System.Threading.Thread.Sleep(50);
					}
					if (string.IsNullOrEmpty(text))
					{
						PageBase.log.AddEvent(base.LoginAccount.AccountName, string.Concat(new string[]
						{
							"批量复制",
							@int.ToString(),
							"张优惠券[",
							dataById.Title,
							"] thành công"
						}));
						MessageUtils.DialogCloseAndParentReload(this);
					}
					else
					{
						base.ShowMsg(text);
					}
				}
			}
		}
	}
}
