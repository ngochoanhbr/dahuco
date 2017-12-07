using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.NodeMger
{
	public class NodeMove : H5ManagerPageBase
	{
		protected ListBox lbSourceNode;

		protected ListBox lbTargetNode;

		protected Button btnMove;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			this.lbSourceNode.Items.Clear();
			this.lbTargetNode.Items.Clear();
			System.Collections.Generic.List<NodeInfo> nodeTreeList = SinGooCMS.BLL.Node.GetNodeTreeList();
			this.lbTargetNode.Items.Add(new ListItem("作为根节点", "0"));
			foreach (NodeInfo current in nodeTreeList)
			{
				this.lbSourceNode.Items.Add(new ListItem(current.NodeName, current.AutoID.ToString()));
				this.lbTargetNode.Items.Add(new ListItem(current.NodeName, current.AutoID.ToString()));
			}
		}

		protected void btnMove_Click(object sender, System.EventArgs e)
		{
			int intPrimaryKeyIDValue = WebUtils.StringToInt(this.lbSourceNode.SelectedValue, 0);
			int num = WebUtils.StringToInt(this.lbTargetNode.SelectedValue, -1);
			NodeInfo dataById = SinGooCMS.BLL.Node.GetDataById(intPrimaryKeyIDValue);
			NodeInfo dataById2 = SinGooCMS.BLL.Node.GetDataById(num);
			if (dataById == null)
			{
				base.ShowMsg("请选择源栏目");
			}
			else if (num == -1)
			{
				base.ShowMsg("请选择目标栏目");
			}
			else if (dataById.AutoID == num)
			{
				base.ShowMsg("源栏目与目标栏目相同");
			}
			else if (StringUtils.Contain(dataById.ChildList.Split(new char[]
			{
				','
			}), num.ToString()))
			{
				base.ShowMsg("不能移动到子栏目下");
			}
			else
			{
				SinGooCMS.BLL.Node.NodeMove(dataById, num);
				PageBase.log.AddEvent(base.LoginAccount.AccountName, "移动栏目成功");
				base.ShowMsg("Thao tác thành công ");
				this.BindData();
			}
		}
	}
}
