using SinGooCMS.BLL;
using SinGooCMS.DAL;
using SinGooCMS.Utility;
using System;
using System.Data;
using System.Text;
using System.Web;

namespace SinGooCMS.WebUI.Platform.h5.StatMger.XmlProvider
{
	public class GetSellStat : IHttpHandler
	{
		private static AbstrctFactory dbo = DataFactory.CreateDataFactory("mssql", null);

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
			string[] array = new string[]
			{
				"#FF7F50",
				"#ADFF2F",
				"#00FF00",
				"#CD853F",
				"#5F9EA0",
				"#DA70D6",
				"#FFB6C1",
				"#D2B48C",
				"#B8860B",
				"#CD5C5C"
			};
			if (queryString.Equals("getchart"))
			{
				DataTable dataTable = Stat.GetSellStat();
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append("<chart palette='4' decimals='2' enableSmartLabels='1' enableRotation='0' bgColor='FFFFFF' bgAlpha='100' bgRatio='0,100' bgAngle='360' showBorder='0' startingAngle='70' caption='商品销售份额' subCaption='' baseFontSize ='13' showValues='0' captionPadding='3' chartTopMargin='5' chartBottomMargin='5' chartLeftMargin='5' chartRightMargin='5'>");
				if (dataTable != null && dataTable.Rows.Count > 0)
				{
					for (int i = 1; i < dataTable.Rows.Count; i++)
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							"<set label='",
							dataTable.Rows[i - 1]["ProName"],
							"' value='",
							dataTable.Rows[i - 1]["bfb"],
							"' isSliced='1' color='",
							array[i - 1],
							"' />"
						}));
					}
				}
				stringBuilder.Append("</chart>");
				context.Response.Write(stringBuilder.ToString());
			}
			else if (queryString.Equals("getjson"))
			{
				DataTable dataTable = GetSellStat.dbo.GetDataTable(" SELECT TOP 50 ProID,ProName,isnull(SUM(Quantity),0) AS SellNum,isnull(SUM(Price),0) AS SellAmount\r\n                                                FROM shop_OrderItem GROUP BY ProID,ProName ORDER BY isnull(SUM(Quantity),0) DESC ");
				if (dataTable != null && dataTable.Rows.Count > 0)
				{
					System.Text.StringBuilder stringBuilder2 = new System.Text.StringBuilder();
					stringBuilder2.Append("[");
					int num = 1;
					foreach (DataRow dataRow in dataTable.Rows)
					{
						if (dataTable.Rows.Count == num)
						{
							stringBuilder2.Append(string.Concat(new object[]
							{
								"{pro:'",
								dataRow["ProName"].ToString(),
								"',sellnum:'",
								dataRow["SellNum"],
								"',sellamount:",
								System.Convert.ToDecimal(dataRow["SellAmount"]).ToString("f2"),
								"}"
							}));
						}
						else
						{
							stringBuilder2.Append(string.Concat(new object[]
							{
								"{pro:'",
								dataRow["ProName"].ToString(),
								"',sellnum:'",
								dataRow["SellNum"],
								"',sellamount:",
								System.Convert.ToDecimal(dataRow["SellAmount"]).ToString("f2"),
								"},"
							}));
						}
						num++;
					}
					stringBuilder2.Append("]");
					HttpContext.Current.Response.Write(stringBuilder2.ToString());
				}
			}
		}
	}
}
