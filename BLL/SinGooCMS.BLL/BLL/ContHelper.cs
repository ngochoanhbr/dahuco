using SinGooCMS.Config;
using SinGooCMS.Entity;
using System;
using System.Data;

namespace SinGooCMS.BLL
{
	public class ContHelper
	{
		private DataRow mContentRow;

		private string mContentUrl;

		public string ContentUrl
		{
			get
			{
				string contentUrl;
				if (string.IsNullOrEmpty(this.mContentUrl))
				{
					int intNodeID = (int)this.Get("NodeID");
					int intContID = (int)this.Get("AutoID");
					DateTime dtCreate = (DateTime)this.Get("AutoTimeStamp");
					contentUrl = ContHelper.GetContentUrl(intNodeID, intContID, dtCreate);
				}
				else
				{
					contentUrl = this.mContentUrl;
				}
				return contentUrl;
			}
		}

		public ContHelper(DataRow row)
		{
			this.mContentRow = row;
			this.mContentUrl = string.Empty;
		}

		public object Get(string name)
		{
			object result;
			if (this.mContentRow.Table.Columns.Contains(name))
			{
				result = this.mContentRow[name];
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		public static string GetContentUrl(int intNodeID, int intContID, DateTime dtCreate)
		{
			string text = "/article/detail.aspx?aid=" + intContID.ToString();
			NodeInfo cacheNodeById = Node.GetCacheNodeById(intNodeID);
			string newValue = string.IsNullOrEmpty(cacheNodeById.UrlRewriteName) ? ("node-" + cacheNodeById.AutoID.ToString()) : cacheNodeById.UrlRewriteName;
			switch ((BrowseType)Enum.Parse(typeof(BrowseType), ConfigProvider.Configs.BrowseType))
			{
			case BrowseType.UrlRewriteAndAspx:
				text = "/article/detail/" + intContID.ToString() + ".aspx";
				break;
			case BrowseType.UrlRewriteNoAspx:
				text = "/article/detail/" + intContID.ToString();
				break;
			case BrowseType.HtmlRewrite:
				text = "/article/detail/" + intContID.ToString() + ".html";
				break;
			case BrowseType.Html:
				text = ("/html/" + ConfigProvider.Configs.HtmlFileRule + ConfigProvider.Configs.HtmlFileExt).Replace("{$nodedir}", newValue).Replace("{$year}", dtCreate.ToString("yyyy")).Replace("{$month}", dtCreate.ToString("MM")).Replace("{$day}", dtCreate.ToString("dd")).Replace("{$id}", intContID.ToString()).Replace("///", "/").Replace("//", "/");
				break;
			}
			return text.Replace("//", "/");
		}
	}
}
