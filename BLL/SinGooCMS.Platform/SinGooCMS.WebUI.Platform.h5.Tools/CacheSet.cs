using System;

namespace SinGooCMS.WebUI.Platform.h5.Tools
{
	internal class CacheSet
	{
		private string _cachekey;

		public string CacheKey
		{
			get
			{
				return this._cachekey;
			}
			set
			{
				this._cachekey = value;
			}
		}
	}
}
