using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Extensions;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.h5.Controls.FieldControls
{
	public class CheckBoxType : BaseFieldControl
	{
		protected CheckBoxList cblField;

		private bool isLoad = false;

		private string _Value = string.Empty;

		public override string FieldValue
		{
			get
			{
				return base.GetSelectedValues(this.cblField.Items);
			}
			set
			{
				this.InitSettings();
				this._Value = value;
				base.SetValue(value, this.cblField.Items);
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
				this.cblField.Attributes.Add("FieldID", base.FieldName);
				if (base.Settings.DataSource.IsNullOrEmpty())
				{
					this.cblField.Items.Add(new ListItem("未设置", "-1"));
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
									this.cblField.DataSource = BaseFieldControl.dbo.GetDataTable(base.Settings.DataSource);
									this.cblField.DataTextField = "KeyValue";
									this.cblField.DataValueField = "KeyName";
									this.cblField.DataBind();
								}
							}
							else
							{
								this.cblField.DataSource = Dicts.GetCacheDictsByName(base.Settings.DataSource).Items;
								this.cblField.DataTextField = "KeyValue";
								this.cblField.DataValueField = "KeyName";
								this.cblField.DataBind();
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
									this.cblField.Items.Add(new ListItem(array2[0], array2[1]));
								}
								else
								{
									this.cblField.Items.Add(new ListItem(text, text));
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
