using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace SinGooCMS.Utility
{
    /// <summary>
    /// 中国大陆身份证信息类
    /// </summary>
    public class ChineseIdCard
    {
        public enum enumArea { Country, Province, City, County };
        public ChineseIdCard()
        {
            //
        }
        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="strIdCardNo"></param>
        public ChineseIdCard(string strIdCardNo)
        {
            IdCardNo = strIdCardNo;
        }

        public string IdCardNo
        {
            get;
            set;
        }

        #region 公共属性
        //是否有效的身份证
        public bool IsIdCard
        {
            get
            {
                switch (IdCardNo.Length)
                {
                    case 18:
                        return CheckIDCard18(IdCardNo);
                    case 15:
                        return CheckIDCard15(IdCardNo);
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country
        {
            get
            {
                return "中国";
            }
        }
        /// <summary>
        /// 省份,自治区
        /// </summary>
        public string Province
        {
            get
            {
                return GetArea(enumArea.Province);
            }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string City
        {
            get
            {
                return GetArea(enumArea.City);
            }
        }
        /// <summary>
        /// 区县
        /// </summary>
        public string County
        {
            get
            {
                return GetArea(enumArea.County);
            }
        }
        /// <summary>
        /// 获取生日
        /// </summary>
        /// <param name="IdCardNo"></param>
        /// <returns></returns>
        public string Brithday
        {
            get
            {
                string rtn = "1900-01-01";
                if (IdCardNo.Length == 15)
                {
                    rtn = IdCardNo.Substring(6, 6).Insert(4, "-").Insert(2, "-");
                }
                else if (IdCardNo.Length == 18)
                {
                    rtn = IdCardNo.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                }
                return rtn;
            }
        }
        /// <summary>
        /// 获取性别
        /// </summary>
        /// <returns></returns>
        public string Sex
        {
            get
            {
                string rtn;
                string tmp = "";
                if (IdCardNo.Length == 15)
                {
                    tmp = IdCardNo.Substring(IdCardNo.Length - 3);
                }
                else if (IdCardNo.Length == 18)
                {
                    tmp = IdCardNo.Substring(IdCardNo.Length - 4);
                    tmp = tmp.Substring(0, 3);
                }
                int sx = int.Parse(tmp);
                int outNum;
                Math.DivRem(sx, 2, out outNum);
                if (outNum == 0)
                {
                    rtn = "女";
                }
                else
                {
                    rtn = "男";
                }
                return rtn;
            }
        }
        #endregion

        /// <summary>
        /// 获取身份证的地区
        /// </summary>
        /// <param name="enumarea">区域枚举</param>
        /// <returns></returns>
        private string GetArea(enumArea enumarea)
        {
            StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Config/idcardarea.txt"));
            string strTemp = string.Empty;
            bool isFound = false;
            string strArea = "没有找到符合的地区";

            while (!reader.EndOfStream)
            {
                strTemp = reader.ReadLine().Trim();
                switch (enumarea)
                {
                    case enumArea.Province:
                        //省级2位
                        if (strTemp.Length >= 2 && IdCardNo.Substring(0, 2) == strTemp.Substring(0, 2))
                        {
                            strArea = strTemp.Trim().Split('\t')[1];
                            isFound = true;
                        }
                        break;
                    case enumArea.City:
                        //市级4位
                        if (strTemp.Length >= 4 && IdCardNo.Substring(0, 4) == strTemp.Trim().Substring(0, 4))
                        {
                            strArea = strTemp.Trim().Split('\t')[1];
                            isFound = true;
                        }
                        break;
                    case enumArea.County:
                        //县级6位
                        if (strTemp.Length >= 6 && IdCardNo.Substring(0, 6) == strTemp.Trim().Substring(0, 6))
                        {
                            strArea = strTemp.Trim().Split('\t')[1];
                            isFound = true;
                        }
                        break;
                }

                if (isFound)
                    break;
            }

            return strArea;
        }

        #region 私有方法
        private bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }
        private bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }
        #endregion

    }
}
