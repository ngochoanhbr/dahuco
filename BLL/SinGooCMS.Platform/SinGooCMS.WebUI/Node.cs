using SinGooCMS.BLL;

using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web;

namespace SinGooCMS.WebUI
{
    public class Node : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			NodeInfo currentNode = base.GetCurrentNode();
			if (currentNode == null)
			{
				base.Response.Redirect("/Include/Error/ErrorMsg_404.htm");
			}
			else if (currentNode.NodeSetting.NeedLogin)
			{
				if (base.UserID.Equals(-1))
				{
					base.Alert(base.GetCaption("CMS_NodeNeedLoginToView"), "/User/LoginExpire.html?returnurl=" + HttpUtility.UrlEncode(base.Request.RawUrl));
				}
				else
				{
					bool flag = !string.IsNullOrEmpty(currentNode.NodeSetting.EnableViewUGroups);
					bool flag2 = !string.IsNullOrEmpty(currentNode.NodeSetting.EnableViewULevel);
					string text = StringUtils.AppendStr(currentNode.NodeSetting.EnableViewUGroups, ',');
					string text2 = StringUtils.AppendStr(currentNode.NodeSetting.EnableViewULevel, ',');
					if (ConfigUtils.GetAppSetting<string>("ViewPurviewType") == ViewPurviewType.Superior.ToString())
					{
						ViewPurview viewPurview = new ViewPurview();
						new ViewPurview().GetAccessPurview(currentNode, ref viewPurview);
						flag = !string.IsNullOrEmpty(viewPurview.GroupPurview);
						flag2 = !string.IsNullOrEmpty(viewPurview.LevelPurview);
						text = StringUtils.AppendStr(viewPurview.GroupPurview, ',');
						text2 = StringUtils.AppendStr(viewPurview.LevelPurview, ',');
					}
					if (flag && !text.Contains("," + base.LoginUser.GroupID.ToString() + ","))
					{
						this.Alert(base.GetCaption("CMS_TheUserGroupAccessDenied"), "/");
					}
					else if (flag2 && !text2.Contains("," + base.LoginUser.LevelID.ToString() + ","))
					{
						this.Alert(base.GetCaption("CMS_TheUserLevelAccessDenied"), "/");
					}
					else
					{
						this.ShowTemplate(currentNode);
					}
				}
			}
			else
			{
				this.ShowTemplate(currentNode);
			}
		}

		private void ShowTemplate(NodeInfo currnode)
		{
			base.SetPage(currnode);
			base.UsingClientSpecial(currnode.NodeSetting.TemplateOfNodeIndex);
		}
	}
}
