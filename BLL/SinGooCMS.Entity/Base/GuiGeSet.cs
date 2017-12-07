using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class GuiGeSet
	{
		public string GuiGeName
		{
			get;
			set;
		}

		public string GuiGeValue
		{
			get;
			set;
		}

		public bool IsImageShow
		{
			get;
			set;
		}

		public List<string> GuiGeValus
		{
			get
			{
				List<string> result;
				if (!string.IsNullOrEmpty(this.GuiGeValue))
				{
					result = new List<string>(this.GuiGeValue.Split(new char[]
					{
						','
					}));
				}
				else
				{
					result = null;
				}
				return result;
			}
		}
	}
}
