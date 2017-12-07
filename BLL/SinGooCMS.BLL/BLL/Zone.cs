using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SinGooCMS.BLL
{
	public class Zone : BizBase
	{
		public static ZoneInfo GetDataById(int intID)
		{
			return (from p in Zone.GetZoneList()
			where p.AutoID.Equals(intID)
			select p).FirstOrDefault<ZoneInfo>();
		}

		public static IList<ZoneInfo> GetList(string strIDs)
		{
			string[] arrIDs = strIDs.Split(new char[]
			{
				','
			});
			return (from p in Zone.GetZoneList()
			where arrIDs.Contains(p.AutoID.ToString())
			select p).ToList<ZoneInfo>();
		}

		public static IList<ZoneInfo> GetProvinceList()
		{
			List<ZoneInfo> zoneList = Zone.GetZoneList();
			IList<ZoneInfo> result;
			if (zoneList != null)
			{
				result = (from p in zoneList
				where p.Depth.Equals(1)
				select p).ToList<ZoneInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static IList<ZoneInfo> GetCityList(int intProvinceID)
		{
			List<ZoneInfo> zoneList = Zone.GetZoneList();
			IList<ZoneInfo> result;
			if (zoneList != null)
			{
				result = (from p in zoneList
				where p.Depth.Equals(2) && p.ParentID == intProvinceID
				select p).ToList<ZoneInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static IList<ZoneInfo> GetCityList(string strProvinceName)
		{
			List<ZoneInfo> zoneList = Zone.GetZoneList();
			ZoneInfo zone = (from p in zoneList
			where p.ZoneName.Equals(strProvinceName) && p.Depth.Equals(1)
			select p).Take(1).FirstOrDefault<ZoneInfo>();
			IList<ZoneInfo> result;
			if (zoneList != null)
			{
				result = zoneList.Where(delegate(ZoneInfo p)
				{
					int num = p.Depth;
					bool arg_38_0;
					if (num.Equals(2))
					{
						num = p.ParentID;
						arg_38_0 = num.Equals((zone == null) ? 0 : zone.AutoID);
					}
					else
					{
						arg_38_0 = false;
					}
					return arg_38_0;
				}).ToList<ZoneInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static IList<ZoneInfo> GetCountyList(int intCityID)
		{
			List<ZoneInfo> zoneList = Zone.GetZoneList();
			IList<ZoneInfo> result;
			if (zoneList != null)
			{
				result = (from p in zoneList
				where p.Depth.Equals(3) && p.ParentID == intCityID
				select p).ToList<ZoneInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static IList<ZoneInfo> GetCountyList(string strCityName)
		{
			List<ZoneInfo> zoneList = Zone.GetZoneList();
			ZoneInfo zone = (from p in zoneList
			where p.ZoneName.Equals(strCityName) && p.Depth.Equals(2)
			select p).Take(1).FirstOrDefault<ZoneInfo>();
			IList<ZoneInfo> result;
			if (zoneList != null)
			{
				result = zoneList.Where(delegate(ZoneInfo p)
				{
					int num = p.Depth;
					bool arg_38_0;
					if (num.Equals(3))
					{
						num = p.ParentID;
						arg_38_0 = num.Equals((zone == null) ? 0 : zone.AutoID);
					}
					else
					{
						arg_38_0 = false;
					}
					return arg_38_0;
				}).ToList<ZoneInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static ZoneInfo GetArea(string strProvince, string strCity, string strCounty)
		{
			List<ZoneInfo> zoneList = Zone.GetZoneList();
			ZoneInfo result;
			if (!string.IsNullOrEmpty(strProvince) && string.IsNullOrEmpty(strCity) && string.IsNullOrEmpty(strCounty))
			{
				result = (from p in zoneList
				where p.ZoneName.Equals(strProvince) && p.Depth.Equals(1)
				select p).FirstOrDefault<ZoneInfo>();
			}
			else if (!string.IsNullOrEmpty(strProvince) && !string.IsNullOrEmpty(strCity) && string.IsNullOrEmpty(strCounty))
			{
				ZoneInfo province = (from p in zoneList
				where p.ZoneName.Equals(strProvince) && p.Depth.Equals(1)
				select p).Take(1).FirstOrDefault<ZoneInfo>();
				result = zoneList.Where(delegate(ZoneInfo p)
				{
					int arg_50_0;
					if (p.ZoneName.Equals(strCity))
					{
						int num = p.Depth;
						if (num.Equals(2))
						{
							num = p.ParentID;
							arg_50_0 = (num.Equals((province == null) ? 0 : province.AutoID) ? 1 : 0);
							return arg_50_0 != 0;
						}
					}
					arg_50_0 = 0;
					return arg_50_0 != 0;
				}).FirstOrDefault<ZoneInfo>();
			}
			else if (!string.IsNullOrEmpty(strProvince) && !string.IsNullOrEmpty(strCity) && !string.IsNullOrEmpty(strCounty))
			{
				ZoneInfo city = (from p in zoneList
				where p.ZoneName.Equals(strCity) && p.Depth.Equals(2)
				select p).Take(1).FirstOrDefault<ZoneInfo>();
				result = zoneList.Where(delegate(ZoneInfo p)
				{
					int arg_50_0;
					if (p.ZoneName.Equals(strCounty))
					{
						int num = p.Depth;
						if (num.Equals(3))
						{
							num = p.ParentID;
							arg_50_0 = (num.Equals((city == null) ? 0 : city.AutoID) ? 1 : 0);
							return arg_50_0 != 0;
						}
					}
					arg_50_0 = 0;
					return arg_50_0 != 0;
				}).FirstOrDefault<ZoneInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static IList<ZoneInfo> GetPagerList(int intParentID, string strZoneName, int intCurrentPageIndex, int intPageSize, ref int intTotalCount)
		{
			List<ZoneInfo> list = Zone.GetZoneList();
			if (list != null && intParentID > -1)
			{
				list = (from p in list
				where p.ParentID.Equals(intParentID)
				select p).ToList<ZoneInfo>();
			}
			if (list != null && !string.IsNullOrEmpty(strZoneName))
			{
				list = (from p in list
				where p.ZoneName.Equals(strZoneName)
				select p).ToList<ZoneInfo>();
			}
			IList<ZoneInfo> result;
			if (list != null)
			{
				intTotalCount = list.Count;
				List<ZoneInfo> list2 = list.Take(intPageSize * intCurrentPageIndex).Skip(intPageSize * (intCurrentPageIndex - 1)).ToList<ZoneInfo>();
				result = list2;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static int GetCount(int intParentID, string strZoneName)
		{
			List<ZoneInfo> list = Zone.GetZoneList();
			if (list != null && intParentID > -1)
			{
				list = (from p in list
				where p.ParentID.Equals(intParentID)
				select p).ToList<ZoneInfo>();
			}
			if (list != null && !string.IsNullOrEmpty(strZoneName))
			{
				list = (from p in list
				where p.ZoneName.Equals(strZoneName)
				select p).ToList<ZoneInfo>();
			}
			int result;
			if (list != null)
			{
				result = list.Count<ZoneInfo>();
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public static List<ZoneInfo> GetZoneList()
		{
			return JsonUtils.JsonToObject<List<ZoneInfo>>(File.ReadAllText(HttpContext.Current.Server.MapPath("/Config/zone.json")));
		}
	}
}
