using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class ModifyProModel : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			ProductModelInfo dataById = ProductModel.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.ModelName;
			this.TextBox2.Text = dataById.TableName.Split(new char[]
			{
				'_'
			})[2];
			this.TextBox3.Text = dataById.ModelDesc;
			this.TextBox2.Enabled = false;
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
				ProductModelInfo productModelInfo = new ProductModelInfo();
				if (base.IsEdit)
				{
					productModelInfo = ProductModel.GetCacheModelById(base.OpID);
				}
				productModelInfo.ModelName = WebUtils.GetString(this.TextBox1.Text);
				productModelInfo.TableName = "shop_P_" + WebUtils.GetString(this.TextBox2.Text);
				productModelInfo.ModelDesc = WebUtils.GetString(this.TextBox3.Text);
				productModelInfo.Creator = base.LoginAccount.AccountName;
				if (string.IsNullOrEmpty(productModelInfo.ModelName))
				{
					base.ShowMsg("模型名称不能为空");
				}
				else if (string.IsNullOrEmpty(this.TextBox2.Text))
				{
					base.ShowMsg("数据表名不能为空");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						productModelInfo.Sort = ProductModel.MaxSort + 1;
						productModelInfo.AutoTimeStamp = System.DateTime.Now;
						ModelAddState modelAddState = ProductModel.Add(productModelInfo);
						ModelAddState modelAddState2 = modelAddState;
						switch (modelAddState2)
						{
						case ModelAddState.Error:
							base.ShowMsg("添加产品模型失败");
							break;
						case ModelAddState.ModelNameExists:
							base.ShowMsg("产品模型名称已经存在");
							break;
						case ModelAddState.TableNameIsUsing:
							base.ShowMsg("已经存在相同的自定义表名称");
							break;
						case ModelAddState.TableExists:
							base.ShowMsg("自定义表已经存在");
							break;
						case ModelAddState.CreateTableError:
							base.ShowMsg("创建自定义表失败");
							break;
						default:
							if (modelAddState2 == ModelAddState.Success)
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加产品模型[" + productModelInfo.ModelName + "] thành công");
								MessageUtils.DialogCloseAndParentReload(this);
							}
							break;
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (ProductModel.Update(productModelInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改产品模型[" + productModelInfo.ModelName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改产品模型失败");
						}
					}
				}
			}
		}
	}
}
