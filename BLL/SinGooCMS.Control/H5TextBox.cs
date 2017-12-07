using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.Control
{
    public enum TextBoxMode
    {
        SingleLine,
        MultiLine,
        Password,
        Email,
        Url,
        Number,
        Range,
        Date,
        Search,
        Color
    }
	[ToolboxData("<{0}:H5TextBox runat=server></{0}:TextBox>")]
	public class H5TextBox : TextBox
	{
		[Browsable(false)]
		public override System.Web.UI.WebControls.TextBoxMode TextMode
		{
			get
			{
				return base.TextMode;
			}
			set
			{
				base.TextMode = value;
			}
		}

		[Browsable(true), Category("Behavior"), DefaultValue(0), Description("自定义文本框的行为模式")]
		public TextBoxMode Mode
		{
			get
			{
				object obj = this.ViewState["TextMode"];
				TextBoxMode result;
				if (obj != null)
				{
					result = (TextBoxMode)obj;
				}
				else
				{
					result = TextBoxMode.SingleLine;
				}
				return result;
			}
			set
			{
				if (value < TextBoxMode.SingleLine || value > TextBoxMode.Color)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["TextMode"] = value;
				if (value >= TextBoxMode.Email)
				{
					this.TextMode = System.Web.UI.WebControls.TextBoxMode.SingleLine;
				}
				else
				{
					this.TextMode = (System.Web.UI.WebControls.TextBoxMode)value;
				}
			}
		}

		[Browsable(true), Category("Appearance"), DefaultValue(""), Description("对Text修正后的值")]
		public string Value
		{
			get
			{
				return this.InputText(base.Text, base.MaxLength);
			}
		}

		private string InputText(string inputString, int maxLength)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (inputString != null && inputString != string.Empty)
			{
				inputString = inputString.Trim();
				if (maxLength != 0 && inputString.Length > maxLength)
				{
					inputString = inputString.Substring(0, maxLength);
				}
				for (int i = 0; i < inputString.Length; i++)
				{
					char c = inputString[i];
					if (c != '"')
					{
						switch (c)
						{
						case '<':
							stringBuilder.Append("<");
							goto IL_B1;
						case '>':
							stringBuilder.Append(">");
							goto IL_B1;
						}
						stringBuilder.Append(inputString[i]);
					}
					else
					{
						stringBuilder.Append("");
					}
					IL_B1:;
				}
				stringBuilder.Replace("'", "''");
			}
			return stringBuilder.ToString();
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Mode != TextBoxMode.SingleLine && this.Mode != TextBoxMode.MultiLine && this.Mode != TextBoxMode.Password)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Type, this.Mode.ToString().ToLower());
				if (this.Mode == TextBoxMode.Search)
				{
					writer.AddAttribute("results", "s");
				}
			}
			base.AddAttributesToRender(writer);
		}
	}
}
