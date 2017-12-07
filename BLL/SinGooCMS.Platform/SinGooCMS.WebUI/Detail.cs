using SinGooCMS.BLL;

using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web;

namespace SinGooCMS.WebUI
{
    public class Detail : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ContentInfo currentContent = base.GetCurrentContent();
			NodeInfo cacheNodeById = SinGooCMS.BLL.Node.GetCacheNodeById((currentContent == null) ? 0 : currentContent.NodeID);
			ContModelInfo cacheModelByID = ContModel.GetCacheModelByID((currentContent == null) ? 0 : currentContent.ModelID);
			if (currentContent == null)
			{
				base.Response.Redirect("/Include/Error/ErrorMsg_404.htm");
			}
			else if (cacheNodeById == null)
			{
				base.Alert(base.GetCaption("CMS_NodeNotExist"), base.ResolveUrl("~/"));
			}
			else if (cacheNodeById.NodeSetting.NeedLogin)
			{
				if (base.UserID.Equals(-1))
				{
					base.Alert(base.GetCaption("CMS_NodeNeedLoginToView"), "/User/LoginExpire.html?returnurl=" + HttpUtility.UrlEncode(base.Request.RawUrl));
				}
				else
				{
					bool flag = !string.IsNullOrEmpty(cacheNodeById.NodeSetting.EnableViewUGroups);
					bool flag2 = !string.IsNullOrEmpty(cacheNodeById.NodeSetting.EnableViewULevel);
					string text = StringUtils.AppendStr(cacheNodeById.NodeSetting.EnableViewUGroups, ',');
					string text2 = StringUtils.AppendStr(cacheNodeById.NodeSetting.EnableViewULevel, ',');
					if (ConfigUtils.GetAppSetting<string>("ViewPurviewType") == ViewPurviewType.Superior.ToString())
					{
						ViewPurview viewPurview = new ViewPurview();
						new ViewPurview().GetAccessPurview(cacheNodeById, ref viewPurview);
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
						this.ShowTemplate(cacheNodeById, currentContent);
					}
				}
			}
			else
			{
				this.ShowTemplate(cacheNodeById, currentContent);
			}
		}

		private void ShowTemplate(NodeInfo currNode, ContentInfo currContent)
		{
			Content.UpdateHit(this.intContID);
			base.Put("currnode", currNode);
			base.Put("prevcont", this.contents.GetPrevContent(currContent.AutoID, currNode.AutoID));
			base.Put("nextcont", this.contents.GetNextContent(currContent.AutoID, currNode.AutoID));
			base.Put("prevcont_all", this.contents.GetPrevContent(currContent.AutoID));
			base.Put("nextcont_all", this.contents.GetNextContent(currContent.AutoID));
			if (!string.IsNullOrEmpty(currContent.TemplateFile))
			{
				base.UsingClientSpecial(currContent.TemplateFile);
			}
			else
			{
				base.UsingClientSpecial(currNode.NodeSetting.TemplateOfNodeContent);
			}
		}
	}
}
