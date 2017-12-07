using SinGooCMS.Config;
using SinGooCMS.DAL;
using SinGooCMS.DAL.Utils;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SinGooCMS.BLL
{
	public class User : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_User ");
			}
		}

		public static UserInfo GetUserById(int intUserID)
		{
			UserInfo model = BizBase.dbo.GetModel<UserInfo>("cms_User", "AutoID=" + intUserID.ToString());
			if (model != null)
			{
				model.UserGroup = UserGroup.GetCacheUserGroupById(model.GroupID);
				model.UserLevel = UserLevel.GetCacheUserLevelById(model.LevelID);
				model.CustomTable = User.GetCustomContentInfo(intUserID, model.UserGroup.TableName);
			}
			return model;
		}

		public static DataTable GetCustomContentInfo(int intUserID, string strTableName)
		{
			return BizBase.dbo.GetDataTable(string.Concat(new object[]
			{
				" SELECT TOP 1 * FROM ",
				strTableName,
				" WHERE UserID=",
				intUserID
			}));
		}

		public static bool Delete(int intPrimaryKey)
		{
			bool result;
			if (intPrimaryKey <= 0)
			{
				result = false;
			}
			else
			{
				UserInfo dataById = User.GetDataById(intPrimaryKey);
				UserGroupInfo cacheUserGroupById = UserGroup.GetCacheUserGroupById(dataById.GroupID);
				if (BizBase.dbo.DeleteTable("cms_User", "AutoID=" + intPrimaryKey.ToString()))
				{
					User.Logout();
					result = BizBase.dbo.DeleteTable(string.Concat(new object[]
					{
						"DELETE FROM ",
						cacheUserGroupById.TableName,
						" WHERE UserID=",
						intPrimaryKey
					}));
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public static bool DelUserByID(int intUserID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@UserID", intUserID)
			};
			BizBase.dbo.ExecProc("p_System_UserDel", arrParam);
			User.Logout();
			return true;
		}

		public static bool Delete(string strIDs)
		{
			bool result;
			if (string.IsNullOrEmpty(strIDs))
			{
				result = false;
			}
			else
			{
				string[] array = strIDs.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string value = array[i];
					User.DelUserByID(WebUtils.GetInt(value));
				}
				result = true;
			}
			return result;
		}

		public static bool IsExistsByName(string strUserName)
		{
			return User.IsExistsByName(strUserName, 0);
		}

		public static bool IsExistsByEmail(string strEmail)
		{
			return User.IsExistsByEmail(strEmail, 0);
		}

		public static bool IsExistsByMobile(string strMobile)
		{
			return User.IsExistsByMobile(strMobile, 0);
		}

		public static bool IsExistsByName(string strUserName, int intUserID)
		{
			return !string.IsNullOrEmpty(strUserName) && BizBase.dbo.GetValue<int>(" SELECT COUNT(*) FROM cms_User WHERE UserName='" + strUserName + "'" + ((intUserID > 0) ? (" AND AutoID<>" + intUserID.ToString()) : "")) > 0;
		}

		public static bool IsExistsByEmail(string strEmail, int intUserID)
		{
			return !string.IsNullOrEmpty(strEmail) && BizBase.dbo.GetValue<int>(" SELECT COUNT(*) FROM cms_User WHERE Email='" + strEmail + "'" + ((intUserID > 0) ? (" AND AutoID<>" + intUserID.ToString()) : "")) > 0;
		}

		public static bool IsExistsByMobile(string strMobile, int intUserID)
		{
			return !string.IsNullOrEmpty(strMobile) && BizBase.dbo.GetValue<int>(" SELECT COUNT(*) FROM cms_User WHERE Mobile='" + strMobile + "'" + ((intUserID > 0) ? (" AND AutoID<>" + intUserID.ToString()) : "")) > 0;
		}

		public static UserStatus Reg(UserInfo userMain)
		{
			Dictionary<string, UserFieldInfo> dicField = new Dictionary<string, UserFieldInfo>();
			return User.Reg(userMain, dicField);
		}

		public static UserStatus Reg(UserInfo userMain, Dictionary<string, UserFieldInfo> dicField)
		{
			int num = 0;
			return User.Reg(userMain, dicField, ref num);
		}

		public static UserStatus Reg(UserInfo userMain, Dictionary<string, UserFieldInfo> dicField, ref int intUserID)
		{
			userMain.RealName = User.GetStringFromDic("RealName", dicField);
			userMain.Gender = User.GetStringFromDic("Gender", dicField);
			userMain.Birthday = User.GetDateTimeFromDic("Birthday", dicField, new DateTime(1900, 1, 1));
			userMain.NickName = User.GetStringFromDic("NickName", dicField);
			userMain.HeaderPhoto = User.GetStringFromDic("HeaderPhoto", dicField);
			userMain.CreditLine = User.GetDecimalFromDic("CreditLine", dicField, 0.0m);
			userMain.Area = User.GetStringFromDic("Area", dicField);
			userMain.Country = User.GetStringFromDic("Country", dicField);
			userMain.Province = User.GetStringFromDic("Province", dicField);
			userMain.City = User.GetStringFromDic("City", dicField);
			userMain.County = User.GetStringFromDic("County", dicField);
			userMain.Address = User.GetStringFromDic("Address", dicField);
			userMain.PostCode = User.GetStringFromDic("PostCode", dicField);
			userMain.TelePhone = User.GetStringFromDic("TelePhone", dicField);
			userMain.Remark = User.GetStringFromDic("Remark", dicField);
			string[] array = userMain.Area.Split(new char[]
			{
				','
			});
			if (array.Length > 0)
			{
				userMain.Province = array[0];
			}
			if (array.Length > 1)
			{
				userMain.City = array[1];
			}
			if (array.Length > 2)
			{
				userMain.County = array[2];
			}
			userMain.Sort = User.MaxSort + 1;
			userMain.AutoTimeStamp = DateTime.Now;
			userMain.LastLoginTime = DateTime.Now;
			if (userMain.FileSpace.Equals(0))
			{
				userMain.FileSpace = ConfigUtils.GetAppSetting<int>("FileCapacity");
			}
			UserStatus result;
			if (User.IsExistsByName(userMain.UserName))
			{
				result = UserStatus.ExistsUserName;
			}
			else if (User.IsExistsByEmail(userMain.Email))
			{
				result = UserStatus.ExistsEmail;
			}
			else if (User.IsExistsByMobile(userMain.Mobile))
			{
				result = UserStatus.ExistsMobile;
			}
			else
			{
				int num = User.Add(userMain);
				intUserID = num;
				if (num > 0)
				{
					UserGroupInfo cacheUserGroupById = UserGroup.GetCacheUserGroupById(userMain.GroupID);
					IList<UserFieldInfo> customFieldListByModelID = UserField.GetCustomFieldListByModelID(userMain.GroupID);
					foreach (UserFieldInfo current in customFieldListByModelID)
					{
						if (current.FieldName == "UserID")
						{
							current.Value = num.ToString();
						}
						else
						{
							current.Value = User.GetStringFromDic(current.FieldName, dicField);
						}
					}
					User.AddCustomContent(cacheUserGroupById, customFieldListByModelID);
					result = UserStatus.Success;
				}
				else
				{
					result = UserStatus.Error;
				}
			}
			return result;
		}

		public static bool AddCustomContent(UserGroupInfo model, IList<UserFieldInfo> fieldList)
		{
			string commandText = User.GenerateSqlOfInsert(model.TableName, fieldList);
			SqlParameter[] parameters = User.PrepareSqlParameters(fieldList);
			return DBHelper.ExecuteNonQuery(commandText, parameters) > 0;
		}

		public static string GenerateSqlOfInsert(string tableName, IList<UserFieldInfo> fieldList)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("INSERT INTO [");
			stringBuilder.Append(tableName);
			stringBuilder.Append("] (");
			foreach (UserFieldInfo current in fieldList)
			{
				num++;
				stringBuilder.Append(current.FieldName);
				if (num < fieldList.Count)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(" ) Values ( ");
			num = 0;
			foreach (UserFieldInfo current in fieldList)
			{
				num++;
				stringBuilder.Append("@");
				stringBuilder.Append(current.FieldName);
				if (num < fieldList.Count)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		public static SqlParameter[] PrepareSqlParameters(IList<UserFieldInfo> fieldList)
		{
			List<SqlParameter> list = new List<SqlParameter>();
			foreach (UserFieldInfo current in fieldList)
			{
				SqlParameter item = new SqlParameter("@" + current.FieldName, current.Value);
				list.Add(item);
			}
			return list.ToArray();
		}

		public static string GetEncodePwd(string strOrigialPwd)
		{
			return DEncryptUtils.MD5EnCode(DEncryptUtils.MD5EnCode(strOrigialPwd));
		}

		public static UserStatus Update(UserInfo userMain, Dictionary<string, UserFieldInfo> dicField)
		{
			userMain.RealName = User.GetStringFromDic("RealName", dicField);
			userMain.Gender = User.GetStringFromDic("Gender", dicField);
			userMain.Birthday = User.GetDateTimeFromDic("Birthday", dicField, new DateTime(1900, 1, 1));
			userMain.NickName = User.GetStringFromDic("NickName", dicField);
			userMain.HeaderPhoto = User.GetStringFromDic("HeaderPhoto", dicField);
			userMain.CreditLine = User.GetDecimalFromDic("CreditLine", dicField, 0.0m);
			userMain.Area = User.GetStringFromDic("Area", dicField);
			userMain.Country = User.GetStringFromDic("Country", dicField);
			userMain.Province = User.GetStringFromDic("Province", dicField);
			userMain.City = User.GetStringFromDic("City", dicField);
			userMain.County = User.GetStringFromDic("County", dicField);
			string[] array = userMain.Area.Split(new char[]
			{
				','
			});
			if (array.Length > 0)
			{
				userMain.Province = array[0];
			}
			if (array.Length > 1)
			{
				userMain.City = array[1];
			}
			if (array.Length > 2)
			{
				userMain.County = array[2];
			}
			userMain.Address = User.GetStringFromDic("Address", dicField);
			userMain.PostCode = User.GetStringFromDic("PostCode", dicField);
			userMain.TelePhone = User.GetStringFromDic("TelePhone", dicField);
			userMain.Remark = User.GetStringFromDic("Remark", dicField);
			UserStatus result;
			if (User.IsExistsByName(userMain.UserName, userMain.AutoID))
			{
				result = UserStatus.ExistsUserName;
			}
			else if (User.IsExistsByEmail(userMain.Email, userMain.AutoID))
			{
				result = UserStatus.ExistsEmail;
			}
			else if (User.IsExistsByMobile(userMain.Mobile, userMain.AutoID))
			{
				result = UserStatus.ExistsMobile;
			}
			else if (User.Update(userMain))
			{
				UserGroupInfo cacheUserGroupById = UserGroup.GetCacheUserGroupById(userMain.GroupID);
				IList<UserFieldInfo> customFieldListByModelID = UserField.GetCustomFieldListByModelID(userMain.GroupID);
				foreach (UserFieldInfo current in customFieldListByModelID)
				{
					if (current.FieldName == "UserID")
					{
						current.Value = userMain.AutoID.ToString();
					}
					else
					{
						current.Value = User.GetStringFromDic(current.FieldName, dicField);
					}
				}
				User.UpdateCustomUser(userMain.AutoID, cacheUserGroupById.TableName, customFieldListByModelID);
				result = UserStatus.Success;
			}
			else
			{
				result = UserStatus.Error;
			}
			return result;
		}

		public static bool UpdateCustomUser(int intUserID, string strTableName, IList<UserFieldInfo> fieldList)
		{
			string commandText = User.GenerateSqlOfUpdate(intUserID, strTableName, fieldList);
			SqlParameter[] parameters = User.PrepareSqlParameters(fieldList);
			return DBHelper.ExecuteNonQuery(commandText, parameters) > 0;
		}

		public static string GenerateSqlOfUpdate(int intUserID, string strTableName, IList<UserFieldInfo> fieldList)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder("UPDATE " + strTableName + " SET ");
			foreach (UserFieldInfo current in fieldList)
			{
				num++;
				stringBuilder.Append(current.FieldName);
				stringBuilder.Append("=@");
				stringBuilder.Append(current.FieldName);
				if (num < fieldList.Count)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(" WHERE UserID=");
			stringBuilder.Append(intUserID);
			return stringBuilder.ToString();
		}

		public static UserInfo GetUserByName(string strUserName)
		{
			UserInfo result;
			if (string.IsNullOrEmpty(strUserName))
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<UserInfo>("SELECT TOP 1 * FROM cms_User WHERE UserName='" + StringUtils.ChkSQL(strUserName) + "'");
			}
			return result;
		}

		public static UserInfo GetUserByEmail(string strEmail)
		{
			UserInfo result;
			if (string.IsNullOrEmpty(strEmail))
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<UserInfo>("SELECT TOP 1 * FROM cms_User WHERE Email='" + StringUtils.ChkSQL(strEmail) + "'");
			}
			return result;
		}

		public static UserInfo GetUserByMobile(string strMobile)
		{
			UserInfo result;
			if (string.IsNullOrEmpty(strMobile))
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<UserInfo>("SELECT TOP 1 * FROM cms_User WHERE Mobile='" + StringUtils.ChkSQL(strMobile) + "'");
			}
			return result;
		}

		public static LoginStatus UserLogin(string strLoginName, string strEncryPassword)
		{
			UserInfo userInfo = new UserInfo();
			return User.UserLogin(strLoginName, strEncryPassword, ref userInfo);
		}

		public static LoginStatus UserLogin(string strLoginName, string strEncryPassword, ref UserInfo userRef)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@loginname", strLoginName)
			};
			userRef = BizBase.dbo.GetModel<UserInfo>(BizBase.dbo.ExecProcReReader("p_System_UserLogin", arrParam));
			LoginLogInfo last = LoginLog.GetLast(UserType.User, strLoginName);
			LoginStatus result;
			if (userRef == null)
			{
				result = LoginStatus.UserNameIncorrect;
			}
			else if (last != null && last.LoginFailCount >= ConfigProvider.Configs.TryLoginTimes && (DateTime.Now - last.AutoTimeStamp).TotalMinutes < 5.0)
			{
				result = LoginStatus.MutilLoginFail;
			}
			else if (strEncryPassword != userRef.Password)
			{
				new LogManager().AddLoginLog(UserType.User, strLoginName, false);
				result = LoginStatus.PasswordIncorrect;
			}
			else if (userRef.Status != 99)
			{
				result = LoginStatus.StatusNotAllow;
			}
			else
			{
				HttpCookie httpCookie = new HttpCookie("singoouser");
				httpCookie.Values["uid"] = userRef.AutoID.ToString();
				httpCookie.Values["uname"] = HttpUtility.UrlEncode(userRef.UserName);
				httpCookie.Values["nickname"] = HttpUtility.UrlEncode(userRef.NickName);
				httpCookie.Values["pwd"] = HttpUtility.UrlEncode(DEncryptUtils.DESEncode(userRef.Password));
				string cookieTime = ConfigProvider.Configs.CookieTime;
				if (cookieTime != null)
				{
					if (!(cookieTime == "一周"))
					{
						if (cookieTime == "一年")
						{
							httpCookie.Expires = DateTime.Now.AddYears(1);
						}
					}
					else
					{
						httpCookie.Expires = DateTime.Now.AddDays(7.0);
					}
				}
				HttpContext.Current.Response.Cookies.Add(httpCookie);
				userRef.LoginCount++;
				userRef.LastLoginIP = IPUtils.GetIP();
				userRef.LastLoginTime = DateTime.Now;
				if (string.IsNullOrEmpty(userRef.Province))
				{
					TaoBaoAreaInfo iPAreaFromTaoBao = IPUtils.GetIPAreaFromTaoBao(IPUtils.GetIP());
					if (iPAreaFromTaoBao != null)
					{
						userRef.Country = iPAreaFromTaoBao.data.country;
						userRef.Province = iPAreaFromTaoBao.data.region;
						userRef.City = iPAreaFromTaoBao.data.city;
						userRef.County = iPAreaFromTaoBao.data.county;
					}
				}
				User.Update(userRef);
				new LogManager().AddLoginLog(UserType.User, strLoginName, true);
				result = LoginStatus.Success;
			}
			return result;
		}

		public static void Logout()
		{
			HttpCookie httpCookie = new HttpCookie("singoouser");
			httpCookie.Expires = DateTime.Now.AddDays(-1.0);
			HttpContext.Current.Response.SetCookie(httpCookie);
		}

		public static UserInfo GetLoginUser()
		{
			int intUserID = -1;
			string text = string.Empty;
			if (HttpContext.Current.Request.Cookies["singoouser"] != null && HttpContext.Current.Request.Cookies["singoouser"]["uid"] != null)
			{
				intUserID = WebUtils.GetInt(HttpContext.Current.Request.Cookies["singoouser"]["uid"], -1);
			}
			if (HttpContext.Current.Request.Cookies["singoouser"] != null && HttpContext.Current.Request.Cookies["singoouser"]["pwd"] != null)
			{
				text = HttpContext.Current.Request.Cookies["singoouser"]["pwd"].ToString();
				text = HttpUtility.UrlDecode(text);
			}
			UserInfo userById = User.GetUserById(intUserID);
			UserInfo result;
			if (userById != null && DEncryptUtils.DESEncode(userById.Password) == text)
			{
				result = userById;
			}
			else
			{
				result = null;
			}
			return result;
		}

		internal static string GetStringFromDic(string fieldName, Dictionary<string, UserFieldInfo> dicField)
		{
			string result;
			if (dicField.ContainsKey(fieldName))
			{
				result = dicField[fieldName].Value;
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		internal static DateTime GetDateTimeFromDic(string fieldName, Dictionary<string, UserFieldInfo> dicField, DateTime defaultValue)
		{
			DateTime dateTime = new DateTime(1900, 1, 1);
			DateTime result;
			if (dicField.ContainsKey(fieldName) && DateTime.TryParse(dicField[fieldName].Value, out dateTime))
			{
				result = dateTime;
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		internal static int GetIntFromDic(string fieldName, Dictionary<string, UserFieldInfo> dicField, int defaultValue)
		{
			int result;
			if (dicField.ContainsKey(fieldName))
			{
				result = WebUtils.StringToInt(dicField[fieldName].Value, defaultValue);
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		internal static decimal GetDecimalFromDic(string fieldName, Dictionary<string, UserFieldInfo> dicField, decimal defaultValue)
		{
			decimal result;
			if (dicField.ContainsKey(fieldName))
			{
				result = WebUtils.StringToDecimal(dicField[fieldName].Value, defaultValue);
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		internal static bool GetBoolFromDic(string fieldName, Dictionary<string, UserFieldInfo> dicField, bool defaultValue)
		{
			bool result;
			if (dicField.ContainsKey(fieldName))
			{
				result = WebUtils.StringToBool(dicField[fieldName].Value, defaultValue);
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		public static IList<UserFieldInfo> GetFieldListWithValue(int intUserID, int intGroupID)
		{
			IList<UserFieldInfo> fieldListByModelID = UserField.GetFieldListByModelID(intGroupID, true);
			IList<UserFieldInfo> usingFieldList = UserField.GetUsingFieldList(intGroupID);
			UserGroupInfo cacheUserGroupById = UserGroup.GetCacheUserGroupById(intGroupID);
			if (cacheUserGroupById != null)
			{
				DataTable dataTable = BizBase.dbo.GetDataTable(string.Concat(new object[]
				{
					"SELECT * FROM cms_User AS A,",
					cacheUserGroupById.TableName,
					" AS B WHERE A.AutoID=",
					intUserID,
					" AND B.UserID=",
					intUserID
				}));
				if (dataTable.Rows.Count == 1)
				{
					foreach (UserFieldInfo current in fieldListByModelID)
					{
						if (dataTable.Columns.Contains(current.FieldName))
						{
							current.Value = dataTable.Rows[0][current.FieldName].ToString();
						}
						else
						{
							current.Value = current.DefaultValue;
						}
					}
				}
			}
			return fieldListByModelID;
		}

		public static bool UpdatePassword(int intUserID, string strEncryPassword)
		{
			return BizBase.dbo.ExecSQL("UPDATE cms_User SET\tPassword='" + strEncryPassword + "' WHERE AutoID=" + intUserID.ToString());
		}

		public static bool UpdatePassword(string stUserName, string strEncryPassword)
		{
			return BizBase.dbo.ExecSQL(string.Concat(new string[]
			{
				"UPDATE cms_User SET\tPassword='",
				strEncryPassword,
				"' WHERE UserName='",
				stUserName,
				"'"
			}));
		}

		public static void UpdateLoginNum(int intUserID)
		{
			BizBase.dbo.ExecSQL("UPDATE cms_User SET LoginCount=LoginCount+1 WHERE AutoID=" + intUserID.ToString());
		}

		public static int GetFileCapacity(string userName)
		{
			return BizBase.dbo.GetValue<int>(" select ISNULL(sum(FileSize),0) from sys_FileUpload where UserType='User' and UserName='" + userName + "' ");
		}

		public static bool IsValidUserName(string strUserName)
		{
			return new Regex(ConfigProvider.Configs.UserNameRule).Match(strUserName).Success;
		}

		public static int Add(UserInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<UserInfo>(entity);
			}
			return result;
		}

		public static bool Update(UserInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<UserInfo>(entity);
		}

		public static UserInfo GetDataById(int intPrimaryKeyIDValue)
		{
			UserInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<UserInfo>(" SELECT TOP 1 AutoID,UserName,GroupID,LevelID,Password,Email,Mobile,Amount,Integral,FileSpace,CertType,CertNo,NickName,RealName,Gender,Birthday,HeaderPhoto,CreditLine,Area,Country,Province,City,County,Address,PostCode,TelePhone,Remark,IsThirdLoginReg,Status,Sort,LoginCount,LastLoginTime,LastLoginIP,AutoTimeStamp FROM cms_User WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static UserInfo GetTopData()
		{
			return User.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static UserInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,UserName,GroupID,LevelID,Password,Email,Mobile,Amount,Integral,FileSpace,CertType,CertNo,NickName,RealName,Gender,Birthday,HeaderPhoto,CreditLine,Area,Country,Province,City,County,Address,PostCode,TelePhone,Remark,IsThirdLoginReg,Status,Sort,LoginCount,LastLoginTime,LastLoginIP,AutoTimeStamp FROM cms_User ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<UserInfo>(text);
		}

		public static IList<UserInfo> GetAllList()
		{
			return User.GetList(0, string.Empty);
		}

		public static IList<UserInfo> GetTopNList(int intTopCount)
		{
			return User.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<UserInfo> GetTopNList(int intTopCount, string strSort)
		{
			return User.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<UserInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return User.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<UserInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,UserName,GroupID,LevelID,Password,Email,Mobile,Amount,Integral,FileSpace,CertType,CertNo,NickName,RealName,Gender,Birthday,HeaderPhoto,CreditLine,Area,Country,Province,City,County,Address,PostCode,TelePhone,Remark,IsThirdLoginReg,Status,Sort,LoginCount,LastLoginTime,LastLoginIP,AutoTimeStamp from cms_User ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<UserInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_User", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_User", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return User.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return User.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return User.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return User.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,UserName,GroupID,LevelID,Password,Email,Mobile,Amount,Integral,FileSpace,CertType,CertNo,NickName,RealName,Gender,Birthday,HeaderPhoto,CreditLine,Area,Country,Province,City,County,Address,PostCode,TelePhone,Remark,IsThirdLoginReg,Status,Sort,LoginCount,LastLoginTime,LastLoginIP,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_User";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<UserInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return User.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<UserInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<UserInfo> result = new List<UserInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,UserName,GroupID,LevelID,Password,Email,Mobile,Amount,Integral,FileSpace,CertType,CertNo,NickName,RealName,Gender,Birthday,HeaderPhoto,CreditLine,Area,Country,Province,City,County,Address,PostCode,TelePhone,Remark,IsThirdLoginReg,Status,Sort,LoginCount,LastLoginTime,LastLoginIP,AutoTimeStamp";
			pager.PagerTable = "cms_User";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<UserInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_User SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
		}

		public static bool UpdateSort(Dictionary<int, int> dicIDAndSort)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (dicIDAndSort.Count > 0)
			{
				foreach (KeyValuePair<int, int> current in dicIDAndSort)
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						" UPDATE cms_User SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}
	}
}
