using SinGooCMS.Utility;
using System;
using System.Data;

namespace SinGooCMS
{
	public class DeskSource
	{
		public static decimal dayOrderAmount;

		public static int dayOrderNum;

		public static int dayUserNum;

		public static int waitSendGoodsNum;

		public static int noReadMsgNum;

		public static int proQANum;

		public static int stockAlertNum;

		public static int stockOutNum;

		public static decimal sellTotal;

		public static int orderTotal;

		public static int goodsTotal;

		public static int userTotal;

		static DeskSource()
		{
			DeskSource.dayOrderAmount = 0m;
			DeskSource.dayOrderNum = 0;
			DeskSource.dayUserNum = 0;
			DeskSource.waitSendGoodsNum = 0;
			DeskSource.noReadMsgNum = 0;
			DeskSource.proQANum = 0;
			DeskSource.stockAlertNum = 0;
			DeskSource.stockOutNum = 0;
			DeskSource.sellTotal = 0m;
			DeskSource.orderTotal = 0;
			DeskSource.goodsTotal = 0;
			DeskSource.userTotal = 0;
		}

		public static void init(DataRow dr)
		{
			DeskSource.dayOrderAmount = WebUtils.GetDecimal(dr["dayOrderAmount"]);
			DeskSource.dayOrderNum = WebUtils.GetInt(dr["dayOrderNum"]);
			DeskSource.dayUserNum = WebUtils.GetInt(dr["dayUserNum"]);
			DeskSource.waitSendGoodsNum = WebUtils.GetInt(dr["waitSendGoodsNum"]);
			DeskSource.noReadMsgNum = WebUtils.GetInt(dr["noReadMsgNum"]);
			DeskSource.proQANum = WebUtils.GetInt(dr["proQANum"]);
			DeskSource.stockAlertNum = WebUtils.GetInt(dr["stockAlertNum"]);
			DeskSource.stockOutNum = WebUtils.GetInt(dr["stockOutNum"]);
			DeskSource.sellTotal = WebUtils.GetDecimal(dr["sellTotal"]);
			DeskSource.orderTotal = WebUtils.GetInt(dr["orderTotal"]);
			DeskSource.goodsTotal = WebUtils.GetInt(dr["goodsTotal"]);
			DeskSource.userTotal = WebUtils.GetInt(dr["userTotal"]);
		}
	}
}
