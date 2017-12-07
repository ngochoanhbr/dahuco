using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Data;
using System.Web;

namespace SinGooCMS.WebUI.Ajax
{
	public class AjaxComment : CMSPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			HttpContext current = HttpContext.Current;
			current.Response.ContentType = "text/plain";
			string text = string.Empty;
			string text2 = string.Empty;
			if (current.Request.RequestType == enumQueryType.GET.ToString())
			{
				text = current.Request.QueryString["t"];
				string text3 = text;
				if (text3 != null)
				{
					if (!(text3 == "getcommentlist"))
					{
						if (text3 == "getcommentpagerfun")
						{
							this.GetCommentPagerFun();
						}
					}
					else
					{
						this.GetCommentList();
					}
				}
			}
			else if (current.Request.RequestType == enumQueryType.POST.ToString())
			{
				text2 = current.Request.Form["t"];
				string text3 = text2;
				if (text3 != null)
				{
					if (text3 == "addcomment")
					{
						this.AddComment();
					}
				}
			}
		}

		private void AddComment()
		{
			int formInt = WebUtils.GetFormInt("cmtcontid", 0);
			ContentInfo contentById = Content.GetContentById(formInt);
			string formString = WebUtils.GetFormString("cmtusername");
			string formString2 = WebUtils.GetFormString("cmtemail");
			string formString3 = WebUtils.GetFormString("cmttitle");
			string formString4 = WebUtils.GetFormString("cmtcontent");
			string formString5 = WebUtils.GetFormString("ccode");
			if (base.ValidateCode != formString5)
			{
				base.Response.Write("验证码不正确");
			}
			else if (contentById == null)
			{
				base.Response.Write("没有找到相关内容");
			}
			else if (string.IsNullOrEmpty(formString))
			{
				base.Response.Write("用户名称不为空");
			}
			else if (string.IsNullOrEmpty(formString3))
			{
				base.Response.Write("标题不为空");
			}
			else if (string.IsNullOrEmpty(formString4))
			{
				base.Response.Write("评论内容不为空");
			}
			else if (Comment.Add(new CommentInfo
			{
				Content = formString4,
				ContID = contentById.AutoID,
				ContName = contentById.Title,
				EnableAnonymous = true,
				IPAddress = IPUtils.GetIP(),
				IPArea = string.Empty,
				IsAudit = true,
				IsReply = false,
				Lang = base.cultureLang,
				ReplyID = 0,
				Title = formString3,
				UserID = 0,
				UserName = formString,
				AutoTimeStamp = System.DateTime.Now
			}) > 0)
			{
				base.Response.Write("发表评论成功");
			}
			else
			{
				base.Response.Write("发表评论失败");
			}
		}

		private void GetCommentPagerFun()
		{
			base.Using("myblog/inc/cmtpagerfun.html");
			ContentInfo contentById = Content.GetContentById(WebUtils.GetQueryInt("contid", 0));
			int queryInt = WebUtils.GetQueryInt("cmtpageindex", 1);
			int queryInt2 = WebUtils.GetQueryInt("cmtpagesize", 10);
			base.SetCmtPage(contentById, queryInt2, queryInt);
		}

		private void GetCommentList()
		{
			int num = 0;
			int num2 = 0;
			DataSet pagerData = Comment.GetPagerData("*", "ContID=" + WebUtils.GetQueryInt("contid", 0) + " AND IsAudit=1", "AutoID desc", WebUtils.GetQueryInt("pagesize", 0), WebUtils.GetQueryInt("pageindex", 0), ref num, ref num2);
			if (pagerData != null && pagerData.Tables.Count > 0)
			{
				DataTable dt = pagerData.Tables[0];
				base.Response.Write(JsonUtils.DataTableToJson(dt));
			}
			else
			{
				base.Response.Write(string.Empty);
			}
		}
	}
}
