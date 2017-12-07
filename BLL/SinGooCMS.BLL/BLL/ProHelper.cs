using SinGooCMS.Config;
using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;

namespace SinGooCMS.BLL
{
	public class ProHelper
	{
		private DataRow mProductRow;

		private string mProductUrl;

		private static AbstrctFactory dbo = DataFactory.CreateDataFactory("mssql", null);

		public IList<SuitProductItemInfo> SuitProducts
		{
			get
			{
				return this.GetSuitPro();
			}
		}

		public string ProductUrl
		{
			get
			{
				string productUrl;
				if (string.IsNullOrEmpty(this.mProductUrl))
				{
					int intCategoryID = (int)this.Get("CateID");
					int intProductID = (int)this.Get("AutoID");
					DateTime dtCreate = (DateTime)this.Get("AutoTimeStamp");
					productUrl = ProHelper.GetProductUrl(intCategoryID, intProductID, dtCreate);
				}
				else
				{
					productUrl = this.mProductUrl;
				}
				return productUrl;
			}
		}

		public ProHelper(DataRow row)
		{
			this.mProductRow = row;
			this.mProductUrl = string.Empty;
		}

		public static string GetProductUrl(int intCategoryID, int intProductID, DateTime dtCreate)
		{
			string text = "/shop/goods.aspx?pid=" + intProductID.ToString();
			switch ((BrowseType)Enum.Parse(typeof(BrowseType), ConfigProvider.Configs.BrowseType))
			{
			case BrowseType.UrlRewriteAndAspx:
				text = "/shop/goods/" + intProductID.ToString() + ".aspx";
				break;
			case BrowseType.UrlRewriteNoAspx:
				text = "/shop/goods/" + intProductID.ToString();
				break;
			case BrowseType.HtmlRewrite:
				text = "/shop/goods/" + intProductID.ToString() + ".html";
				break;
			case BrowseType.Html:
				text = ("/html/goods/" + ConfigProvider.Configs.HtmlFileRule + ConfigProvider.Configs.HtmlFileExt).Replace("{$year}", dtCreate.ToString("yyyy")).Replace("{$month}", dtCreate.ToString("MM")).Replace("{$day}", dtCreate.ToString("dd")).Replace("{$id}", intProductID.ToString());
				break;
			}
			return text.Replace("//", "/");
		}

		public object Get(string name)
		{
			object result;
			if (this.mProductRow.Table.Columns.Contains(name))
			{
				result = this.mProductRow[name];
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		private IList<SuitProductItemInfo> GetSuitPro()
		{
			string text = this.Get("ProIDs").ToString();
			IList<SuitProductItemInfo> result;
			if (!string.IsNullOrEmpty(text))
			{
				IList<SuitProductItemInfo> list = new List<SuitProductItemInfo>();
				string[] array = text.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i];
					string[] array2 = text2.Split(new char[]
					{
						'|'
					});
					if (array2.Length == 3)
					{
						list.Add(new SuitProductItemInfo
						{
							ProductName = WebUtils.GetString(array2[0]),
							ProductID = WebUtils.GetInt(array2[1]),
							Product = ProHelper.dbo.GetModel<ProductInfo>(" SELECT TOP 1 * FROM shop_Product WHERE AutoID=" + array2[1]),
							Quantity = WebUtils.GetInt(array2[2])
						});
					}
				}
				result = list;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
