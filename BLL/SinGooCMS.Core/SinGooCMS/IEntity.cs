using System;
using System.Collections.Generic;

namespace SinGooCMS
{
	public interface IEntity
	{
		string DBTableName
		{
			get;
		}

		string PKName
		{
			get;
		}

		string Fields
		{
			get;
		}

		List<string> FieldList
		{
			get;
		}
	}
}
