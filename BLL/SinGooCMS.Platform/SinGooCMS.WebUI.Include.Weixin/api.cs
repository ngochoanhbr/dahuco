using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using SinGooCMS.BLL;
using SinGooCMS.Weixin;
using System;
using System.IO;

namespace SinGooCMS.WebUI.Include.Weixin
{
    public class api : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string text = base.Request["signature"];
			string timestamp = base.Request["timestamp"];
			string nonce = base.Request["nonce"];
			string str = base.Request["echostr"];
			if (base.IsPost)
			{
                if (!CheckSignature.Check(text, timestamp, nonce, SinGooCMS.Weixin.Config.Token))
				{
					this.WriteContent("参数错误！");
				}
				else
				{
					PostModel postModel = new PostModel
					{
						Signature = base.Request.QueryString["signature"],
						Msg_Signature = base.Request.QueryString["msg_signature"],
						Timestamp = base.Request.QueryString["timestamp"],
						Nonce = base.Request.QueryString["nonce"],
						Token = SinGooCMS.Weixin.Config.Token,
                        EncodingAESKey = SinGooCMS.Weixin.Config.EncodingAESKey,
                        AppId = SinGooCMS.Weixin.Config.AppID
					};
					int maxRecordCount = 10;
					CustomMessageHandler customMessageHandler = new CustomMessageHandler(base.Request.InputStream, postModel, maxRecordCount);
					try
					{
						customMessageHandler.Execute();
						this.WriteContent(customMessageHandler.ResponseDocument.ToString());
					}
					catch (System.Exception ex)
					{
						using (System.IO.TextWriter textWriter = new System.IO.StreamWriter(base.Server.MapPath("~/log/Error_" + System.DateTime.Now.Ticks + ".txt")))
						{
							textWriter.WriteLine(ex.Message);
							textWriter.WriteLine(ex.InnerException.Message);
							if (customMessageHandler.ResponseDocument != null)
							{
								textWriter.WriteLine(customMessageHandler.ResponseDocument.ToString());
							}
							textWriter.Flush();
							textWriter.Close();
						}
						this.WriteContent("");
					}
					finally
					{
						base.Response.End();
					}
				}
			}
			else
			{
                if (CheckSignature.Check(text, timestamp, nonce, SinGooCMS.Weixin.Config.Token))
				{
					this.WriteContent(str);
				}
				else
				{
					this.WriteContent(string.Concat(new string[]
					{
						"failed:",
						text,
						",token:",
						SinGooCMS.Weixin.Config.Token,
						" ",
						CheckSignature.GetSignature(timestamp, nonce, SinGooCMS.Weixin.Config.Token),
						"。如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。"
					}));
				}
				base.Response.End();
			}
		}

		private void WriteContent(string str)
		{
			base.Response.Output.Write(str);
		}
	}
}
