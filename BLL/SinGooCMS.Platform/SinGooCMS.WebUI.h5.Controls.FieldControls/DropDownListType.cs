using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Extensions;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.h5.Controls.FieldControls
{
	public class DropDownListType : BaseFieldControl
	{
		private bool isLoad = false;

		private string _Value = string.Empty;

		protected DropDownList dropField;

		public override string FieldValue
		{
			get
			{
				string text = this.dropField.SelectedValue;
				if (string.IsNullOrEmpty(text))
				{
					text = WebUtils.GetFormString((this.ClientID + "_dropField").Replace("_", "$"));
				}
				return text;
			}
			set
			{
				this.InitSettings();
				ListControl arg_18_0 = this.dropField;
				this._Value = value;
				arg_18_0.SelectedValue = value;
			}
		}

		public string Value
		{
			get
			{
				return this._Value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.InitSettings();
			}
		}

		private void InitSettings()
		{
			if (!this.isLoad)
			{
				this.dropField.Attributes.Add("FieldID", base.FieldName);
				this.dropField.Items.Clear();
				if (base.Settings.DataSource.IsNullOrEmpty())
				{
					this.dropField.Items.Add(new ListItem("未设置", "-1"));
				}
				else
				{
					string dataFrom = base.Settings.DataFrom;
					if (dataFrom != null)
					{
						if (!(dataFrom == "Text"))
						{
							if (!(dataFrom == "DataDictionary"))
							{
								if (dataFrom == "SQLQuery")
								{
									this.dropField.DataSource = BaseFieldControl.dbo.GetDataTable(base.Settings.DataSource);
									this.dropField.DataTextField = "KeyValue";
									this.dropField.DataValueField = "KeyName";
									this.dropField.DataBind();
								}
							}
							else
							{
								this.dropField.DataSource = Dicts.GetCacheDictsByName(base.Settings.DataSource).Items;
								this.dropField.DataTextField = "KeyValue";
								this.dropField.DataValueField = "KeyName";
								this.dropField.DataBind();
							}
						}
						else
						{
							string[] array = base.Settings.DataSource.Split(new char[]
							{
								','
							});
							for (int i = 0; i < array.Length; i++)
							{
								string text = array[i];
								string[] array2 = text.Split(new char[]
								{
									'|'
								});
								if (array2.Length == 2)
								{
									this.dropField.Items.Add(new ListItem(array2[0], array2[1]));
								}
								else
								{
									this.dropField.Items.Add(new ListItem(text, text));
								}
							}
						}
					}
				}
			}
			this.isLoad = true;
		}
	}
}
