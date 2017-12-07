using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.Utility
{
	public static class MessageUtils
	{
		public static void Show(Page page, string msg)
		{
			page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript'>alert('" + msg.ToString() + "');</script>");
		}

		public static void ShowAndRedirect(Page page, string msg, string url)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("<script language='javascript' defer>");
			stringBuilder.AppendFormat("alert('{0}');", msg);
			stringBuilder.AppendFormat("self.location.href='{0}'", url);
			stringBuilder.Append("</script>");
			page.ClientScript.RegisterStartupScript(page.GetType(), "message", stringBuilder.ToString());
		}

		public static void ResponseScript(Page page, string script)
		{
			page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer='defer'>" + script + "</script>");
		}

		public static void CloseThisWindow(Page _Page)
		{
			_Page.ClientScript.RegisterClientScriptBlock(_Page.GetType(), "closewin", "<script language='javascript'>window.close()</script>");
		}

		public static void WinBack(Page _Page)
		{
			_Page.ClientScript.RegisterClientScriptBlock(_Page.GetType(), "winback", "<script language='javascript'>history.go(-1);</script>");
		}

		public static void GoToPage(Page _Page, string RedirectURL)
		{
			_Page.ClientScript.RegisterClientScriptBlock(_Page.GetType(), "gotopage", "<script language='javascript'>location='" + RedirectURL + "'</script>");
		}

		public static void GoToPage(Page _Page, string RedirectURL, string target)
		{
			_Page.ClientScript.RegisterClientScriptBlock(_Page.GetType(), "gotopage", string.Concat(new string[]
			{
				"<script language='javascript'>",
				target,
				".location='",
				RedirectURL,
				"'</script>"
			}));
		}

		public static void JumpPage(string _DefaultPage, string Target, Page _Page)
		{
			_Page.ClientScript.RegisterClientScriptBlock(_Page.GetType(), "jumppage", string.Concat(new string[]
			{
				"<script language='javascript'>window.open('",
				_DefaultPage,
				"','",
				Target,
				"','')</script>"
			}));
		}

		public static void H5Tip(Page page, string msgContent)
		{
			MessageUtils.H5Tip(page, msgContent, false, null);
		}

		public static void H5Tip(UpdatePanel UpdatePanel1, string msgContent)
		{
			MessageUtils.H5Tip(null, msgContent, true, UpdatePanel1);
		}

		public static void H5Tip(Page page, string msgContent, bool isAjax, UpdatePanel UpdatePanel1)
		{
			Literal literal = new Literal();
			literal.Text = "<script type=\"text/javascript\">h5.tip('" + msgContent + "');</script>";
			if (isAjax)
			{
				ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "tips", literal.Text, false);
			}
			else
			{
				page.Header.Controls.Add(literal);
			}
		}

		public static void DialogClode(Page page)
		{
			page.ClientScript.RegisterClientScriptBlock(page.GetType(), "close", "<script>$.dialog.close();</script>");
		}

		public static void DialogCloseAndParentReload(Page page)
		{
			page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alertandredirect", "<script>$.dialog.close();$.dialog.open.origin.location.reload();</script>");
		}

		public static void DivTip(Page page, string msgContent)
		{
			MessageUtils.DivTip(page, msgContent, false, null);
		}

		public static void DivTip(UpdatePanel UpdatePanel1, string msgContent)
		{
			MessageUtils.DivTip(null, msgContent, true, UpdatePanel1);
		}

		public static void DivTip(Page page, string msgContent, bool isAjax, UpdatePanel UpdatePanel1)
		{
			Literal literal = new Literal();
			literal.Text = "<script type=\"text/javascript\">$(function(){$.dialog.tips('" + msgContent + "',3)});</script>";
			if (isAjax)
			{
				ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "tips", literal.Text, false);
			}
			else
			{
				page.Header.Controls.Add(literal);
			}
		}

		public static void DivShow(UpdatePanel UpdatePanel1, string msgTitle, string msgContent)
		{
			MessageUtils.DivShow(UpdatePanel1, msgTitle, msgContent, false);
		}

		public static void DivShow(UpdatePanel UpdatePanel1, string msgTitle, string msgContent, bool boolModal)
		{
			MessageUtils.DivShow(null, 260, 60, 20, msgTitle, msgContent, boolModal, true, UpdatePanel1);
		}

		public static void DivShow(Page page, string msgTitle, string msgContent)
		{
			MessageUtils.DivShow(page, msgTitle, msgContent, false);
		}

		public static void DivShow(Page page, string msgTitle, string msgContent, bool boolModal)
		{
			MessageUtils.DivShow(page, 260, 60, 20, msgTitle, msgContent, boolModal, false, null);
		}

		public static void DivShow(Page page, int dialogWidth, int dialogHeight, int dialogLineHeight, string msgTitle, string msgContent, bool boolModal, bool boolIsAjax, UpdatePanel UpdatePanel1)
		{
			Literal literal = new Literal();
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string randomNumber = StringUtils.GetRandomNumber();
			stringBuilder.Append(string.Concat(new object[]
			{
				"<script type=\"text/javascript\">$(function(){$.dialog({id:'jsonleecmsdialog',title:'",
				msgTitle,
				"',content:'",
				msgContent,
				"',width:",
				dialogWidth,
				",height:",
				dialogHeight
			}));
			if (boolModal)
			{
				stringBuilder.Append(",lock:true");
			}
			else
			{
				stringBuilder.Append(",lock:false");
			}
			stringBuilder.Append("});});</script>");
			if (boolIsAjax)
			{
				ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), randomNumber, stringBuilder.ToString(), false);
			}
			else
			{
				literal.Text = stringBuilder.ToString();
				page.Header.Controls.Add(literal);
			}
		}

		public static void DivShowAndRedirect(UpdatePanel UpdatePanel1, string msgTitle, string msgContent, string strURL)
		{
			MessageUtils.DivShowAndRedirect(null, 260, 60, 50, msgTitle, msgContent, 3000, strURL, null, true, UpdatePanel1);
		}

		public static void DivShowAndRedirect(Page page, string msgTitle, string msgContent, string strURL)
		{
			MessageUtils.DivShowAndRedirect(page, msgTitle, msgContent, 3000, strURL);
		}

		public static void DivShowAndRedirect(Page page, string msgTitle, string msgContent, int intOutTime, string strURL)
		{
			MessageUtils.DivShowAndRedirect(page, 260, 60, 50, msgTitle, msgContent, intOutTime, strURL, null, false, null);
		}

		public static void DivShowAndRedirect(Page page, int intDialogWidth, int intDialogHeight, int intDialogLineHeight, string msgTitle, string msgContent, int intOutTime, string strURL, string strTarget, bool boolIsAjax, UpdatePanel UpdatePanel1)
		{
			Literal literal = new Literal();
			string randomNumber = StringUtils.GetRandomNumber();
			literal.Text = string.Concat(new object[]
			{
				"<script type=\"text/javascript\">$(function(){$.dialog({title:'",
				msgTitle,
				"',content:'",
				msgContent,
				"',width:",
				intDialogWidth,
				",height:",
				intDialogLineHeight
			});
			Literal expr_5E = literal;
			expr_5E.Text = expr_5E.Text + ",lock:true,time:3,close:function(){location='" + strURL + "'}});});</script>";
			if (boolIsAjax)
			{
				ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), randomNumber, literal.Text, false);
			}
			else
			{
				page.Header.Controls.Add(literal);
			}
		}
	}
}
