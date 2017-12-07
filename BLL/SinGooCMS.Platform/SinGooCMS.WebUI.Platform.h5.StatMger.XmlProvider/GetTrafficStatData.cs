using SinGooCMS.BLL;
using SinGooCMS.Utility;
using System;
using System.Data;
using System.Text;
using System.Web;

namespace SinGooCMS.WebUI.Platform.h5.StatMger.XmlProvider
{
	public class GetTrafficStatData : IHttpHandler
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
				DataTable trafficStat = Stat.GetTrafficStat(queryString2, dateTime, dateTime2);
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append(string.Concat(new string[]
				{
					"<chart caption='",
					queryString2.Equals("year") ? "Thống kê lưu lượng hàng năm" : "Thống kê lưu lượng hàng tháng",
					"' subCaption='(",
					dateTime.ToShortDateString(),
					" - ",
					dateTime2.ToShortDateString(),
					")' lineThickness='2' showValues='0' formatNumberScale='0' anchorRadius='2' divLineIsDashed='1' showAlternateHGridColor='1' shadowAlpha='40' labelStep='1' numvdivlines='5' bgColor='FFFFFF' bgAngle='270' bgAlpha='10,10' alternateHGridAlpha='5' alternateHGridColor='CC3300' divLineColor='CC3300' divLineAlpha='20'  captionPadding='3' chartTopMargin='5' chartBottomMargin='10' chartLeftMargin='10' chartRightMargin='10' canvasBorderColor='FFCEBD' baseFontColor='000000' showBorder='0'>"
				}));
				if (queryString2.Equals("年"))
				{
					stringBuilder.Append("<categories>");
					for (int i = dateTime.Year; i <= dateTime2.Year; i++)
					{
                        stringBuilder.Append("<category label='năm " + i.ToString() + "' />");
					}
					stringBuilder.Append("</categories>");
					stringBuilder.Append("<dataset seriesName='P V' color='1D8BD1' anchorBorderColor='1D8BD1' anchorBgColor='1D8BD1'>");
					for (int i = dateTime.Year; i <= dateTime2.Year; i++)
					{
						DataRow[] array = trafficStat.Select("Title='" + i.ToString() + "'");
						string text = (array != null && array.Length > 0) ? array[0]["PV"].ToString() : "0";
						stringBuilder.Append(string.Concat(new string[]
						{
							"<set value='",
							text,
							"' toolText='",
							i.ToString(),
							"Tổng số hàng năm ",
							text,
							" PV' />"
						}));
					}
					stringBuilder.Append("</dataset>");
					stringBuilder.Append("<dataset seriesName='I P' color='F1683C' anchorBorderColor='F1683C' anchorBgColor='F1683C'>");
					for (int i = dateTime.Year; i <= dateTime2.Year; i++)
					{
						DataRow[] array = trafficStat.Select("Title='" + i.ToString() + "'");
						string text2 = (array != null && array.Length > 0) ? array[0]["IP"].ToString() : "0";
						stringBuilder.Append(string.Concat(new string[]
						{
							"<set value='",
							text2,
							"' toolText='",
							i.ToString(),
							"Year total ",
							text2,
							" IP' />"
						}));
					}
					stringBuilder.Append("</dataset>");
				}
				else if (queryString2.Equals("月"))
				{
					stringBuilder.Append("<categories>");
					for (int i = 1; i <= 12; i++)
					{
                        stringBuilder.Append("<category label='tháng " + i.ToString() + "' />");
					}
					stringBuilder.Append("</categories>");
					stringBuilder.Append("<dataset seriesName='P V' color='1D8BD1' anchorBorderColor='1D8BD1' anchorBgColor='1D8BD1'>");
					for (int i = 1; i <= 12; i++)
					{
						DataRow[] array = trafficStat.Select("Title='" + i.ToString() + "'");
						string text = (array != null && array.Length > 0) ? array[0]["PV"].ToString() : "0";
						stringBuilder.Append(string.Concat(new string[]
						{
							"<set value='",
							text,
							"' toolText='",
							i.ToString(),
							"Tổng số hàng tháng ",
							text,
							" PV' />"
						}));
					}
					stringBuilder.Append("</dataset>");
					stringBuilder.Append("<dataset seriesName='I P' color='F1683C' anchorBorderColor='F1683C' anchorBgColor='F1683C'>");
					for (int i = 1; i <= 12; i++)
					{
						DataRow[] array = trafficStat.Select("Title='" + i.ToString() + "'");
						string text2 = (array != null && array.Length > 0) ? array[0]["IP"].ToString() : "0";
						stringBuilder.Append(string.Concat(new string[]
						{
							"<set value='",
							text2,
							"' toolText='",
							i.ToString(),
							"Tổng số hàng tháng ",
							text2,
							" IP' />"
						}));
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
