using SinGooCMS.Common;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform
{
	public class H5ManagerPageBase : ManagerPageBase
	{
		public string ShowNavigate()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("<li><a href='/Platform/h5/Main.aspx'><i class='iconfont mr5'>&#xe610;</i>Home</a></li>");
			DataRow[] array = base.GetAccountMenu().Select(" CatalogID=" + base.CurrentCatalogID + " AND IsDefault=1 ");
			if (array != null && array.Length == 1)
			{
				stringBuilder.Append(string.Concat(new object[]
				{
					"<li><a href='/Platform/h5/",
					array[0]["FilePath"],
					"?CatalogID=",
					base.CurrentCatalogID,
					"&Module=",
					base.CurrentModuleCode,
					"&action=View'>",
					array[0]["CatalogName"],
					"</a></li>"
				}));
			}
			DataRow[] array2 = base.GetAccountMenu().Select(" ModuleID=" + base.CurrentModuleID);
			if (array2 != null && array2.Length == 1)
			{
				stringBuilder.Append("<li class='active'>" + array2[0]["ModuleName"] + "</li>");
			}
			return stringBuilder.ToString().Trim();
		}

		public string GetRepeaterCheckIDs(Repeater Repeater1, string strCheckBoxID, string strHiddenAutoID)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			foreach (RepeaterItem repeaterItem in Repeater1.Items)
			{
				CheckBox checkBox = repeaterItem.FindControl(strCheckBoxID) as CheckBox;
				HiddenField hiddenField = repeaterItem.FindControl(strHiddenAutoID) as HiddenField;
				if (checkBox.Checked)
				{
					stringBuilder.Append(hiddenField.Value + ",");
				}
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			});
		}

		public System.Collections.Generic.Dictionary<int, int> GetRepeaterSortDict(Repeater Repeater1, string strSortBoxID, string strHiddenAutoID)
		{
			System.Collections.Generic.Dictionary<int, int> dictionary = new System.Collections.Generic.Dictionary<int, int>();
			foreach (RepeaterItem repeaterItem in Repeater1.Items)
			{
				TextBox textBox = repeaterItem.FindControl(strSortBoxID) as TextBox;
				HiddenField hiddenField = repeaterItem.FindControl(strHiddenAutoID) as HiddenField;
				dictionary.Add(WebUtils.GetInt(hiddenField.Value), WebUtils.GetInt(textBox.Text));
			}
			return dictionary;
		}
	}
}
