using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SinGooCMS.Utility;

namespace SinGooCMS.Entity
{
    public partial class AreaModelInfo
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                return ToCityString();
            }
        }
        /// <summary>
        /// 城市的地区信息
        /// </summary>
        public IList<ZoneInfo> CityList
        {
            get
            {
                List<ZoneInfo> lstZone = SinGooCMS.Utility.JsonUtils.JsonToObject<List<ZoneInfo>>(System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/Config/zone.json")));
                List<ZoneInfo> lstTheZone = new List<ZoneInfo>();
                foreach (string item in Citys.Split(','))
                {
                    var temp = lstZone.Where(p => p.AutoID.Equals(WebUtils.GetInt(item))).FirstOrDefault();
                    if (temp != null)
                        lstTheZone.Add(temp);
                }

                return lstTheZone;
            }
        }
        /// <summary>
        /// 城市信息
        /// </summary>
        /// <returns></returns>
        private string ToCityString()
        {
            if (CityList != null && CityList.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (ZoneInfo item in CityList)
                {
                    builder.Append(item.ZoneName + ",");
                }

                return builder.ToString().TrimEnd(',');
            }

            return "城市未定义";
        }
    }
}
