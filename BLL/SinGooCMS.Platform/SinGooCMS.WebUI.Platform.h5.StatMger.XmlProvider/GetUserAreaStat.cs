using SinGooCMS.BLL;
using SinGooCMS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace SinGooCMS.WebUI.Platform.h5.StatMger.XmlProvider
{
	public class GetUserAreaStat : IHttpHandler
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
            //GetUserAreaStat.<>c__DisplayClass4 <>c__DisplayClass = new GetUserAreaStat.<>c__DisplayClass4();
            //context.Response.ContentType = "text/plain";
            //string[] array = new string[]
            //{
            //    "CD5C5C",
            //    "CD853F",
            //    "FF7F50",
            //    "5F9EA0",
            //    "DA70D6",
            //    "FFB6C1",
            //    "D2B48C",
            //    "B8860B",
            //    "00FF00",
            //    "ADFF2F"
            //};
            //string[] array2 = new string[]
            //{
            //    "CN.AH",
            //    "CN.BJ",
            //    "CN.CQ",
            //    "CN.FJ",
            //    "CN.GS",
            //    "CN.GD",
            //    "CN.GX",
            //    "CN.GZ",
            //    "CN.HA",
            //    "CN.HB",
            //    "CN.HE",
            //    "CN.HU",
            //    "CN.HL",
            //    "CN.HN",
            //    "CN.JS",
            //    "CN.JX",
            //    "CN.JL",
            //    "CN.LN",
            //    "CN.NM",
            //    "CN.NX",
            //    "CN.QH",
            //    "CN.SA",
            //    "CN.SD",
            //    "CN.SH",
            //    "CN.SX",
            //    "CN.SC",
            //    "CN.TJ",
            //    "CN.XJ",
            //    "CN.XZ",
            //    "CN.YN",
            //    "CN.ZJ",
            //    "CN.MA",
            //    "CN.HK",
            //    "CN.TA"
            //};
            //<>c__DisplayClass.arrDisplayValue = new string[]
            //{
            //    "安徽",
            //    "北京",
            //    "重庆",
            //    "福建",
            //    "甘肃",
            //    "广东",
            //    "广西",
            //    "贵州",
            //    "海南",
            //    "河北",
            //    "河南",
            //    "湖北",
            //    "黑龙江",
            //    "湖南",
            //    "江苏",
            //    "江西",
            //    "吉林",
            //    "辽宁",
            //    "内蒙古",
            //    "宁夏",
            //    "青海",
            //    "山西",
            //    "山东",
            //    "上海",
            //    "陕西",
            //    "四川",
            //    "天津",
            //    "新疆",
            //    "西藏",
            //    "云南",
            //    "浙江",
            //    "澳门",
            //    "香港",
            //    "台湾"
            //};
            //System.Collections.Generic.List<UserAreaStatInfo> list = new System.Collections.Generic.List<UserAreaStatInfo>();
            //DataTable userAreaStat = Stat.GetUserAreaStat();
            //int i;
            //for (i = 0; i < array2.Length; i++)
            //{
            //    DataRow dataRow = (from p in userAreaStat.AsEnumerable()
            //    where p.Field("Province").Contains(<>c__DisplayClass.arrDisplayValue[i])
            //    select p).FirstOrDefault<DataRow>();
            //    int userNum = (dataRow == null) ? 0 : dataRow.Field("TotalUser");
            //    list.Add(new UserAreaStatInfo
            //    {
            //        AreaID = array2[i],
            //        DisplayName = <>c__DisplayClass.arrDisplayValue[i],
            //        UserNum = userNum,
            //        ShowColor = "FFFFFF"
            //    });
            //}
            //list = (from p in list
            //orderby p.UserNum descending
            //select p).ToList<UserAreaStatInfo>();
            //int num = 0;
            //foreach (UserAreaStatInfo current in list)
            //{
            //    if (current.UserNum > 0)
            //    {
            //        current.ShowColor = array[num];
            //        num++;
            //    }
            //}
            //System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            //stringBuilder.Append("<map lang='ZH' showShadow='0' showBevel='0' showMarkerLabels='1' fillColor='F0FAFF' borderColor='330000' baseFont='宋体' baseFontSize='12' markerBorderColor='000000' markerBgColor='FF5904' markerRadius='6' legendPosition='bottom' useHoverColor='1' showMarkerToolTip='1' showCanvasBorder='0' canvasBorderColor='f1f1f1' canvasBorderThickness='2' borderColor='00324A'  hoverColor='C0D2F8'><data>");
            //foreach (UserAreaStatInfo current in list)
            //{
            //    stringBuilder.Append(string.Concat(new object[]
            //    {
            //        "<entity id='",
            //        current.AreaID,
            //        "' displayValue='",
            //        current.DisplayName,
            //        "' toolText='",
            //        current.DisplayName,
            //        "( ",
            //        current.UserNum,
            //        " 个会员)' color='",
            //        current.ShowColor,
            //        "' />"
            //    }));
            //}
            //stringBuilder.Append("</data></map>");
            //context.Response.Write(stringBuilder.ToString());
		}
	}
}
