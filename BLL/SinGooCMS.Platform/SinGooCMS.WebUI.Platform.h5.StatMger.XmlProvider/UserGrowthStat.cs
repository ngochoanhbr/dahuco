using SinGooCMS.BLL;
using SinGooCMS.Utility;
using System;
using System.Data;
using System.Text;
using System.Web;

namespace SinGooCMS.WebUI.Platform.h5.StatMger.XmlProvider
{
	public class UserGrowthStat : IHttpHandler
	{
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			string queryString = WebUtils.GetQueryString("t");
			string value = WebUtils.GetQueryString("st") + " 00:00:00";
			string value2 = WebUtils.GetQueryString("dt") + " 23:59:59";
			string queryString2 = WebUtils.GetQueryString("sy");
			System.DateTime dateTime = WebUtils.GetDateTime(value, System.DateTime.Now.AddMonths(-1));
			System.DateTime dateTime2 = WebUtils.GetDateTime(value2, System.DateTime.Now);
			if (queryString.Equals("getchart"))
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				DataTable userGrowthStat = Stat.GetUserGrowthStat(queryString2, dateTime, dateTime2);
				string text = string.Empty;
				string text2 = queryString2;
				if (text2 != null)
				{
					if (!(text2 == "年"))
					{
						if (!(text2 == "月"))
						{
							if (text2 == "周")
							{
								text = "一周内会员增长统计";
							}
						}
						else
						{
							text = "按月会员增长统计";
						}
					}
					else
					{
						text = "按年会员增长统计";
					}
				}
				stringBuilder.Append(string.Concat(new string[]
				{
					"<chart caption='",
					text,
					"' subCaption='(",
					dateTime.ToShortDateString(),
					" - ",
					dateTime2.ToShortDateString(),
					")' lineThickness='2' showValues='0' formatNumberScale='0' anchorRadius='2' divLineIsDashed='1' showAlternateHGridColor='1' shadowAlpha='40' labelStep='1' numvdivlines='5' bgColor='FFFFFF' bgAngle='270' bgAlpha='10,10' alternateHGridAlpha='5' alternateHGridColor='CC3300' divLineColor='CC3300' divLineAlpha='20'  captionPadding='3' chartTopMargin='5' chartBottomMargin='10' chartLeftMargin='10' chartRightMargin='15' canvasBorderColor='FFCEBD' baseFontColor='000000' showBorder='0'>"
				}));
				if (queryString2.Equals("年"))
				{
					stringBuilder.Append("<categories>");
					for (int i = dateTime.Year; i <= dateTime2.Year; i++)
					{
						stringBuilder.Append("<category label='" + i.ToString() + "年' />");
					}
					stringBuilder.Append("</categories>");
					stringBuilder.Append("<dataset seriesName='注册会员数量' color='1D8BD1' anchorBorderColor='1D8BD1' anchorBgColor='1D8BD1'>");
					for (int i = dateTime.Year; i <= dateTime2.Year; i++)
					{
						DataRow[] array = userGrowthStat.Select("Title='" + i.ToString() + "'");
						string text3 = (array != null && array.Length > 0) ? array[0]["RegUserNum"].ToString() : "0";
						stringBuilder.Append(string.Concat(new string[]
						{
							"<set value='",
							text3,
							"' toolText='",
							i.ToString(),
							"年 共 ",
							text3,
							" 个新增会员' />"
						}));
					}
					stringBuilder.Append("</dataset>");
				}
				else if (queryString2.Equals("月"))
				{
					stringBuilder.Append("<categories>");
					for (int i = 1; i <= 12; i++)
					{
						stringBuilder.Append("<category label='" + i.ToString() + "月' />");
					}
					stringBuilder.Append("</categories>");
					stringBuilder.Append("<dataset seriesName='注册会员数量' color='1D8BD1' anchorBorderColor='1D8BD1' anchorBgColor='1D8BD1'>");
					for (int i = 1; i <= 12; i++)
					{
						DataRow[] array = userGrowthStat.Select("Title='" + i.ToString() + "'");
						string text3 = (array != null && array.Length > 0) ? array[0]["RegUserNum"].ToString() : "0";
						stringBuilder.Append(string.Concat(new string[]
						{
							"<set value='",
							text3,
							"' toolText='",
							i.ToString(),
							"月共 ",
							text3,
							" 个新增会员' />"
						}));
					}
					stringBuilder.Append("</dataset>");
				}
				else if (queryString2.Equals("周"))
				{
					stringBuilder.Append("<categories>");
					System.DateTime t = System.DateTime.Now.AddDays(-6.0);
					while (t <= System.DateTime.Now)
					{
						stringBuilder.Append("<category label='" + t.ToString("MM-dd") + "' />");
						t = t.AddDays(1.0);
					}
					stringBuilder.Append("</categories>");
					stringBuilder.Append("<dataset seriesName='注册会员数量' color='1D8BD1' anchorBorderColor='1D8BD1' anchorBgColor='1D8BD1'>");
					t = System.DateTime.Now.AddDays(-6.0);
					while (t <= System.DateTime.Now)
					{
						DataRow[] array = userGrowthStat.Select("Title='" + t.ToString("MM-dd") + "'");
						string text3 = (array != null && array.Length > 0) ? array[0]["RegUserNum"].ToString() : "0";
						stringBuilder.Append(string.Concat(new string[]
						{
							"<set value='",
							text3,
							"' toolText='",
							t.ToString("MM-dd"),
							"共 ",
							text3,
							" 个新增会员' />"
						}));
						t = t.AddDays(1.0);
					}
					stringBuilder.Append("</dataset>");
				}
				stringBuilder.Append(" <styles><definition><style name='CaptionFont' type='font' size='14'/></definition><application><apply toObject='CAPTION' styles='CaptionFont' /><apply toObject='SUBCAPTION' styles='CaptionFont' /></application> </styles>");
				stringBuilder.Append("</chart>");
				context.Response.Write(stringBuilder.ToString());
			}
		}
	}
}
