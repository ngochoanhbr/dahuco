using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using SinGooCMS.WebIM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SinGooCMS.WebUI.Ajax
{
	public class AjaxMethod : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			HttpContext current = HttpContext.Current;
			current.Response.ContentType = "text/plain";
			string text = string.Empty;
			string text2 = string.Empty;
			if (current.Request.RequestType.ToUpper() == "GET")
			{
				text = current.Request.QueryString["t"];
				string text3 = text;
				switch (text3)
				{
                    case "getlist":
                        this.GetbyID();
                        break;
                    case "getsupplier":
                        this.GetSupplierbyID();
                        break;
                    case "getmanufacture":
                        this.GetManufacturebyID();
                        break;
                    case "getcustomer":
                        this.GetCustomerbyID();
                        break;
				case "isuserexists":
					this.IsUserExists();
					break;
				case "getregcode":
					this.GetRegCode();
					break;
				case "getfindpwdcode":
					this.GetFindPwdCode();
					break;
				case "getbindcode":
					this.GetBindCode();
					break;
				case "userbind":
					this.UserBind();
					break;
				case "addprofavorite":
					this.AddProFavorite();
					break;
				case "dingyue":
					this.DingYue();
					break;
				case "getimuserlist":
					this.GetIMUserList();
					break;
				case "checknewmsg":
					this.CheckNewMsg();
					break;
				case "getgoodsattr":
					this.GetGoodsAttr();
					break;
				case "getshippingaddr":
					this.GetShippingAddr();
					break;
				case "getshippingfee":
					this.GetShippingFee();
					break;
				case "getsearchpro":
					this.GetSearchPro();
					break;
				}
			}
			else if (current.Request.RequestType.ToUpper() == "POST")
			{
				text2 = current.Request.Form["t"];
				string text3 = text2;
				if (text3 != null)
				{
					if (!(text3 == "feedback"))
					{
						if (!(text3 == "addhyq"))
						{
							if (!(text3 == "addstockout"))
							{
								if (text3 == "sendgoodsQA")
								{
									this.SendGoodsQA();
								}
							}
							else
							{
								this.AddStockout();
							}
						}
						else
						{
							this.AddCoupons();
						}
					}
					else
					{
						this.SaveFeedback();
					}
				}
			}
		}

		private void IsUserExists()
		{
			string queryString = WebUtils.GetQueryString("_checkusername");
			if (string.IsNullOrEmpty(queryString))
			{
				base.Response.Write("{\"ret\":-1,\"msg\":\"会员名称不为空\"}");
			}
			else if (SinGooCMS.BLL.User.IsExistsByName(queryString))
			{
				base.Response.Write("{\"ret\":1,\"msg\":\"会员名称已存在\"}");
			}
			else
			{
				base.Response.Write("{\"ret\":0,\"msg\":\"会员名称不存在\"}");
			}
		}

		private void GetRegCode()
		{
			string queryString = WebUtils.GetQueryString("type");
			if (string.Compare(queryString, "byemail", true) == 0)
			{
				this.GetMailCode("reg");
			}
			else if (string.Compare(queryString, "bymobile", true) == 0)
			{
				this.GetMobildCode("reg");
			}
		}

		private void GetFindPwdCode()
		{
			string queryString = WebUtils.GetQueryString("type");
			if (string.Compare(queryString, "byemail", true) == 0)
			{
				this.GetMailCode("findpwd");
			}
			else if (string.Compare(queryString, "bymobile", true) == 0)
			{
				this.GetMobildCode("findpwd");
			}
		}

		private void GetBindCode()
		{
			string queryString = WebUtils.GetQueryString("type");
			if (string.Compare(queryString, "byemail", true) == 0)
			{
				this.GetMailCode("bind");
			}
			else if (string.Compare(queryString, "bymobile", true) == 0)
			{
				this.GetMobildCode("bind");
			}
		}

		private void GetMobildCode(string strSMSType)
		{
			string randomNumber = StringUtils.GetRandomNumber(5, true);
			string strContent = string.Empty;
			if (strSMSType == "reg")
			{
				strContent = base.GetConfigValue("RegSMSCheckCode").Replace("${checkcode}", randomNumber);
			}
			else if (strSMSType == "findpwd")
			{
				strContent = base.GetConfigValue("GetPwdSMSCheckCode").Replace("${username}", WebUtils.GetQueryString("username")).Replace("${checkcode}", randomNumber);
			}
			else if (strSMSType == "bind")
			{
				strContent = base.GetConfigValue("BindSMSCheckCode").Replace("${username}", WebUtils.GetQueryString("username")).Replace("${checkcode}", randomNumber);
			}
			string queryString = WebUtils.GetQueryString("paramval");
			string s = "{\"ret\":\"fail\",\"timeout\":0,\"msg\":\"短信验证码发送失败\"}";
			bool flag = true;
			if (!string.IsNullOrEmpty(queryString))
			{
				if ((strSMSType == "reg" || strSMSType == "bind") && SinGooCMS.BLL.User.IsExistsByMobile(queryString))
				{
					s = "{\"ret\":\"exists\",\"timeout\":0,\"msg\":\"手机号码已经存在\"}";
				}
				else
				{
					SMSInfo lastCheckCode = SMS.GetLastCheckCode(queryString);
					if (lastCheckCode != null)
					{
						System.TimeSpan timeSpan = System.DateTime.Now - lastCheckCode.AutoTimeStamp;
						if (timeSpan.TotalSeconds < 60.0)
						{
							flag = false;
							s = "{\"ret\":\"fail\",\"timeout\":" + (60.0 - timeSpan.TotalSeconds).ToString("f0") + ",\"msg\":\"发送间隔太短\"}";
						}
					}
					if (flag)
					{
						if (MsgService.SendSMSCheckCode(queryString, strContent, randomNumber) > 0)
						{
							s = "{\"ret\":\"success\",\"timeout\":60,\"msg\":\"短信验证码发送成功，请注意查收！\"}";
						}
					}
				}
			}
			base.Response.Write(s);
		}

		private void GetMailCode(string strMailType)
		{
			string randomNumber = StringUtils.GetRandomNumber(5, true);
			string text = string.Empty;
			string strSubject = string.Empty;
			if (strMailType == "reg")
			{
				strSubject = "注册验证码";
				text = base.GetConfigValue("RegMailCheckCode").Replace("${checkcode}", randomNumber);
			}
			else if (strMailType == "findpwd")
			{
				strSubject = "找回密码验证码";
				text = base.GetConfigValue("GetPwdMailCheckCode").Replace("${username}", WebUtils.GetQueryString("username")).Replace("${checkcode}", randomNumber);
			}
			else if (strMailType == "bind")
			{
				strSubject = "会员绑定验证码";
				text = base.GetConfigValue("BindMailCheckCode").Replace("${username}", WebUtils.GetQueryString("username")).Replace("${checkcode}", randomNumber);
			}
			string queryString = WebUtils.GetQueryString("paramval");
			string s = "{\"ret\":\"fail\",\"timeout\":0,\"msg\":\"邮箱验证码发送失败\"}";
			bool flag = true;
			if (!string.IsNullOrEmpty(queryString))
			{
				if ((strMailType == "reg" || strMailType == "bind") && SinGooCMS.BLL.User.IsExistsByEmail(queryString))
				{
					s = "{\"ret\":\"exists\",\"timeout\":0,\"msg\":\"邮箱地址已存在\"}";
				}
				else
				{
					SMSInfo lastCheckCode = SMS.GetLastCheckCode(queryString);
					if (lastCheckCode != null)
					{
						System.TimeSpan timeSpan = System.DateTime.Now - lastCheckCode.AutoTimeStamp;
						if (timeSpan.TotalSeconds < 60.0)
						{
							flag = false;
							s = "{\"ret\":\"fail\",\"timeout\":" + (60.0 - timeSpan.TotalSeconds).ToString("f0") + ",\"msg\":\"发送间隔太短\"}";
						}
					}
					if (flag)
					{
						string empty = string.Empty;
						if (MsgService.SendMail(queryString, strSubject, text, out empty))
						{
							SMSInfo entity = new SMSInfo
							{
								SMSMob = queryString,
								SMSText = text,
								SMSType = "CheckCode",
								ValidateCode = randomNumber,
								ReturnMsg = empty,
								Status = 1,
								AutoTimeStamp = System.DateTime.Now
							};
							SMS.Add(entity);
							s = "{\"ret\":\"success\",\"timeout\":60,\"msg\":\"邮箱验证码发送成功，请登录邮箱查收！\"}";
						}
						else
						{
							s = "{\"ret\":\"fail\",\"timeout\":0,\"msg\":\"" + empty + "\"}";
						}
					}
				}
			}
			base.Response.Write(s);
		}

		private void UserBind()
		{
			string queryString = WebUtils.GetQueryString("type");
			string queryString2 = WebUtils.GetQueryString("paramval");
			string text = string.Empty;
			SMSInfo lastCheckCode = SMS.GetLastCheckCode(queryString2);
			if (lastCheckCode != null)
			{
				text = lastCheckCode.ValidateCode;
			}
			if (base.UserID == -1)
			{
				base.Response.Write("{\"ret\":\"fail\",\"msg\":\"会员未登录或者登录超时\"}");
			}
			else if (string.IsNullOrEmpty(text))
			{
				base.Response.Write("{\"ret\":\"fail\",\"msg\":\"验证码不能为空\"}");
			}
			else if (queryString != "byemail" && queryString != "bymobile")
			{
				base.Response.Write("{\"ret\":\"fail\",\"msg\":\"类型不正确，必须是邮箱或者手机\"}");
			}
			else if (string.IsNullOrEmpty(queryString2))
			{
				base.Response.Write("{\"ret\":\"fail\",\"msg\":\"参数值不为空\"}");
			}
			else if (string.Compare(WebUtils.GetQueryString("checkcode"), text, true) != 0)
			{
				base.Response.Write("{\"ret\":\"fail\",\"msg\":\"验证码不正确\"}");
			}
			else
			{
				string text2 = queryString;
				if (text2 != null)
				{
					if (text2 == "byemail")
					{
						if (SinGooCMS.BLL.User.IsExistsByEmail(queryString2, base.UserID))
						{
							base.Response.Write("{\"ret\":\"fail\",\"msg\":\"邮箱地址已存在\"}");
						}
						else if (PageBase.dbo.UpdateTable(string.Concat(new object[]
						{
							" update cms_User set Email='",
							queryString2,
							"' where AutoID=",
							base.UserID
						})))
						{
							new MsgService(base.LoginUser).SendBindEmail();
							base.Response.Write("{\"ret\":\"success\",\"msg\":\"恭喜您，电子邮箱[" + queryString2 + "]绑定成功\"}");
						}
						goto IL_29C;
					}
					if (text2 == "bymobile")
					{
						if (SinGooCMS.BLL.User.IsExistsByMobile(queryString2, base.UserID))
						{
							base.Response.Write("{\"ret\":\"fail\",\"msg\":\"手机号码已存在\"}");
						}
						else if (PageBase.dbo.UpdateTable(string.Concat(new object[]
						{
							" update cms_User set Mobile='",
							queryString2,
							"' where AutoID=",
							base.UserID
						})))
						{
							new MsgService(base.LoginUser).SendBindMobile();
							base.Response.Write("{\"ret\":\"success\",\"msg\":\"恭喜您，手机[" + queryString2 + "]绑定成功\"}");
						}
						goto IL_29C;
					}
				}
				base.Response.Write("{\"ret\":\"fail\",\"msg\":\"Thao tác thất bại，请联系管理员\"}");
				IL_29C:;
			}
		}

		private void AddProFavorite()
		{
			FavoritesInfo favorite = Favorites.GetFavorite(WebUtils.GetQueryInt("pid"), base.UserID);
			ProductInfo dataById = Product.GetDataById(WebUtils.GetQueryInt("pid"));
			if (dataById != null)
			{
				if (favorite != null)
				{
					Favorites.Delete(favorite.AutoID);
					base.Response.Write("{\"ret\":\"success\",\"status\":2,\"msg\":\"已取消收藏\"}");
				}
				else
				{
					FavoritesInfo entity = new FavoritesInfo
					{
						UserID = base.UserID,
						ProductID = dataById.AutoID,
						ProductName = dataById.ProductName,
						ProductImage = dataById.ProImg,
						Price = dataById.SellPrice,
						Lang = base.cultureLang,
						AutoTimeStamp = System.DateTime.Now
					};
					if (Favorites.Add(entity) > 0)
					{
						base.Response.Write("{\"ret\":\"success\",\"status\":1,\"msg\":\"收藏成功\"}");
					}
					else
					{
						base.Response.Write("{\"ret\":\"fail\",\"status\":0,\"msg\":\"收藏失败\"}");
					}
				}
			}
			else
			{
				base.Response.Write("{\"ret\":\"fail\",\"status\":-1,\"msg\":\"商品不存在\"}");
			}
		}

		private void DingYue()
		{
			string queryString = WebUtils.GetQueryString("_mailaddr");
			if (string.IsNullOrEmpty(queryString))
			{
				base.Response.Write("{\"msg\":\"" + base.GetCaption("DingYue_EmailNotEmpty") + "\"}");
			}
			else if (!ValidateUtils.IsEmail(queryString))
			{
				base.Response.Write("{\"msg\":\"" + base.GetCaption("DingYue_EmailIncorrect") + "\"}");
			}
			else
			{
				int num = SinGooCMS.BLL.DingYue.Add(queryString);
				if (num == -1)
				{
					base.Response.Write("{\"msg\":\"" + base.GetCaption("DingYue_EmailExists") + "\"}");
				}
				else if (num > 0)
				{
					base.Response.Write("{\"msg\":\"" + base.GetCaption("DingYue_Success") + "\"}");
				}
			}
		}

		private void GetIMUserList()
		{
			int num = 0;
			int num2 = 0;
			int queryInt = WebUtils.GetQueryInt("pagesize", 10);
			int queryInt2 = WebUtils.GetQueryInt("page", 1);
			string queryString = WebUtils.GetQueryString("sendertype");
			DataSet pagerData = OnlineUser.GetPagerData("*", string.Concat(new string[]
			{
				" Not (UserType='",
				queryString,
				"' AND UserID=",
				WebUtils.GetQueryInt("senderid", 0).ToString(),
				")"
			}), " IsOnline desc,UserType asc ", queryInt, queryInt2, ref num, ref num2);
			if (pagerData != null && pagerData.Tables.Count > 0)
			{
				DataTable dt = pagerData.Tables[0];
				base.Response.Write(JsonUtils.DataTableToJson(dt));
			}
			else
			{
				base.Response.Write(string.Empty);
			}
		}

		private void CheckNewMsg()
		{
			string queryString = WebUtils.GetQueryString("chattype");
			int num = 0;
			if (queryString == "manager")
			{
				AccountInfo loginAccount = Account.GetLoginAccount();
				if (loginAccount != null)
				{
					num = loginAccount.AutoID;
				}
			}
			else if (queryString == "user")
			{
				num = base.UserID;
			}
			base.Response.Write(IMMessage.HasNewMessage(queryString, num));
		}

		private void GetGoodsAttr()
		{
			GoodsSpecifyInfo goodsSpecifyInfo = GoodsSpecify.Get(WebUtils.GetQueryInt("proid"), WebUtils.GetQueryString("attrpath"));
			if (goodsSpecifyInfo != null)
			{
				base.Response.Write(JsonUtils.ObjectToJson<GoodsSpecifyInfo>(goodsSpecifyInfo));
			}
			else
			{
				base.Response.Write("{\"ret\":\"fail\",\"status\":0,\"msg\":\"Chúng tôi không tìm thấy bất kỳ dữ liệu\"}");
			}
		}

		private void GetShippingAddr()
		{
			ShippingAddressInfo dataById = ShippingAddress.GetDataById(WebUtils.GetQueryInt("id"));
			System.Collections.Generic.List<ShippingAddressInfo> list = (System.Collections.Generic.List<ShippingAddressInfo>)ShippingAddress.GetShippingAddrByUID(WebUtils.GetQueryInt("uid"));
			if (dataById != null)
			{
				base.Response.Write(JsonUtils.ObjectToJson<ShippingAddressInfo>(dataById));
			}
			else if (list != null && list.Count > 0)
			{
				base.Response.Write(JsonUtils.ObjectToJson<System.Collections.Generic.List<ShippingAddressInfo>>(list));
			}
			else
			{
				base.Response.Write("{\"ret\":\"fail\",\"status\":0,\"msg\":\"Chúng tôi không tìm thấy bất kỳ dữ liệu\"}");
			}
		}
        private void GetbyID()
        {
            CompanyInfo dataById = Company.GetDataById(WebUtils.GetQueryInt("id"));
            if (dataById != null)
            {
                base.Response.Write(JsonUtils.ObjectToJson<CompanyInfo>(dataById));
            }
            else
            {
                base.Response.Write("{\"ret\":\"fail\",\"status\":0,\"msg\":\"Chúng tôi không tìm thấy bất kỳ dữ liệu\"}");
            }
        }
        private void GetSupplierbyID()
        {
            SupplierInfo dataById = Supplier.GetDataById(WebUtils.GetQueryInt("id"));
            if (dataById != null)
            {
                base.Response.Write(JsonUtils.ObjectToJson<SupplierInfo>(dataById));
            }
            else
            {
                base.Response.Write("{\"ret\":\"fail\",\"status\":0,\"msg\":\"Chúng tôi không tìm thấy bất kỳ dữ liệu\"}");
            }
        }
        private void GetManufacturebyID()
        {
            ManufactureInfo dataById = Manufacture.GetDataById(WebUtils.GetQueryInt("id"));
            if (dataById != null)
            {
                base.Response.Write(JsonUtils.ObjectToJson<ManufactureInfo>(dataById));
            }
            else
            {
                base.Response.Write("{\"ret\":\"fail\",\"status\":0,\"msg\":\"Chúng tôi không tìm thấy bất kỳ dữ liệu\"}");
            }
        }
        private void GetCustomerbyID()
        {
            CustomerInfo dataById = Customer.GetDataById(WebUtils.GetQueryInt("id"));
            if (dataById != null)
            {
                base.Response.Write(JsonUtils.ObjectToJson<CustomerInfo>(dataById));
            }
            else
            {
                base.Response.Write("{\"ret\":\"fail\",\"status\":0,\"msg\":\"Chúng tôi không tìm thấy bất kỳ dữ liệu\"}");
            }
        }
		private void GetShippingFee()
		{
			ShippingParam shippingParam = JsonUtils.JsonToObject<ShippingParam>(WebUtils.GetQueryString("querystr"));
			ShippingAddressInfo addr = ShippingAddress.GetDataById((shippingParam == null) ? 0 : shippingParam.addrid);
			if (addr != null && shippingParam != null && shippingParam.pros.Count > 0)
			{
				foreach (ProAndNum current in shippingParam.pros)
				{
					ProductInfo dataById = Product.GetDataById(current.pid);
					AreaModelInfo dataById2 = AreaModel.GetDataById((dataById == null) ? 0 : dataById.AreaModelID);
					PostageModelInfo dataById3 = PostageModel.GetDataById((dataById == null) ? 0 : dataById.PostageModelID);
					bool arg_DD_0;
					if (dataById2 != null)
					{
						arg_DD_0 = ((from p in dataById2.CityList
						where p.ZoneName.Equals(addr.City)
						select p).FirstOrDefault<ZoneInfo>() == null);
					}
					else
					{
						arg_DD_0 = true;
					}
					if (!arg_DD_0)
					{
						current.isshipping = true;
					}
					if (dataById3 != null && dataById3.PostageItems.Count > 0)
					{
						PostageItem postageItem = (from p in dataById3.PostageItems
						where p.AreaNames.Contains(addr.Province)
						select p).FirstOrDefault<PostageItem>();
						if (postageItem != null && current.isshipping)
						{
							current.shippingfee = postageItem.ExpFee + (current.num - 1) * postageItem.ExpAddoneFee;
						}
					}
				}
				shippingParam.totalfee = shippingParam.pros.Sum((ProAndNum p) => p.shippingfee);
			}
			base.Response.Write(JsonUtils.ObjectToJson<ShippingParam>(shippingParam));
		}

		private void GetSearchPro()
		{
			System.Collections.Generic.List<ShortProduct> list = new System.Collections.Generic.List<ShortProduct>();
			string queryString = WebUtils.GetQueryString("key");
			System.Collections.Generic.List<ProductInfo> list2 = (System.Collections.Generic.List<ProductInfo>)Product.GetList(10, " Status=99 AND ProductName like '%" + queryString + "%' ", "IsTop desc,IsRecommend desc,AutoID desc");
			foreach (ProductInfo current in list2)
			{
				list.Add(new ShortProduct
				{
					ProID = current.AutoID,
					ProductName = current.ProductName
				});
			}
			base.Response.Write(JsonUtils.ObjectToJson<System.Collections.Generic.List<ShortProduct>>(list));
		}

		private void SaveFeedback()
		{
			string formString = WebUtils.GetFormString("_uname");
			string formString2 = WebUtils.GetFormString("_email");
			string formString3 = WebUtils.GetFormString("_mobile");
			string formString4 = WebUtils.GetFormString("_phone");
			string formString5 = WebUtils.GetFormString("_title");
			string formString6 = WebUtils.GetFormString("_content");
			if (string.Compare(base.ValidateCode, WebUtils.GetFormString("_yzm"), true) != 0)
			{
				base.Response.Write("{\"msg\":\"" + base.GetCaption("ValidateCodeIncorrect") + "\",\"status\":\"fail\"}");
			}
			else if (string.IsNullOrEmpty(formString))
			{
				base.Response.Write("{\"msg\":\"" + base.GetCaption("Feedback_UserNameEmpty") + "\",\"status\":\"fail\"}");
			}
			else if (string.IsNullOrEmpty(formString6))
			{
				base.Response.Write("{\"msg\":\"" + base.GetCaption("Feedback_ContentEmpty") + "\",\"status\":\"fail\"}");
			}
			else
			{
				FeedbackInfo feedbackInfo = new FeedbackInfo
				{
					UserName = formString,
					Title = (string.IsNullOrEmpty(formString5) ? base.GetCaption("Feedback_Title").Replace("${username}", formString) : formString5),
					Content = formString6,
					Email = formString2,
					Mobile = formString3,
					Telephone = formString4,
					IPaddress = IPUtils.GetIP(),
					Replier = string.Empty,
					ReplyContent = string.Empty,
					ReplyDate = new System.DateTime(1900, 1, 1),
					IsAudit = true,
					Lang = base.cultureLang,
					AutoTimeStamp = System.DateTime.Now
				};
				int num = Feedback.Add(feedbackInfo);
				if (num > 0)
				{
					string empty = string.Empty;
					if (WebUtils.GetBool(base.GetConfigValue("IsSendMailForLY")) && !string.IsNullOrEmpty(base.GetConfigValue("ReciverEMail")))
					{
						if (MsgService.SendMail(base.GetConfigValue("ReciverEMail"), formString5, feedbackInfo.Content, out empty))
						{
							base.Response.Write("{\"msg\":\"" + base.GetCaption("Feedback_Success") + "\",\"status\":\"success\"}");
						}
						else
						{
							base.Response.Write("{\"msg\":\"" + base.GetCaption("Feedback_SuccessButMailFail") + "\",\"status\":\"fail\"}");
						}
					}
					else
					{
						base.Response.Write("{\"msg\":\"" + base.GetCaption("Feedback_Success") + "\",\"status\":\"success\"}");
					}
				}
				else
				{
					base.Response.Write("{\"msg\":\"" + base.GetCaption("Feedback_Error") + "\",\"status\":\"fail\"}");
				}
			}
		}

		private void AddCoupons()
		{
			string formString = WebUtils.GetFormString("code");
			CouponsInfo dataBySN = Coupons.GetDataBySN(formString);
			if (dataBySN == null)
			{
				base.WriteJsonTip(false, "优惠券编码无效", "");
			}
			else if (base.UserID == -1)
			{
				base.WriteJsonTip(false, "会员没有登录", "");
			}
			else if (dataBySN.EndTime > System.DateTime.Now)
			{
				base.WriteJsonTip(false, "优惠券已过期", "");
			}
			else if (!string.IsNullOrEmpty(dataBySN.UserName))
			{
				base.WriteJsonTip(false, "优惠券已经绑定了会员", "");
			}
			else if (dataBySN.IsUsed)
			{
				base.WriteJsonTip(false, "优惠券已经使用过了", "");
			}
			else if (dataBySN.UserName == base.UserName)
			{
				base.WriteJsonTip(false, "已经绑定了这张优惠券", "");
			}
			else
			{
				dataBySN.UserName = base.UserName;
				if (Coupons.Update(dataBySN))
				{
					base.WriteJsonTip(true, "优惠券绑定成功！", "");
				}
				else
				{
					base.WriteJsonTip(false, "Thao tác thất bại！", "");
				}
			}
		}

		private void AddStockout()
		{
			ProductInfo dataById = Product.GetDataById(WebUtils.GetFormInt("pid"));
			StockoutInfo last = Stockout.GetLast(WebUtils.GetFormInt("pid"));
			if (dataById == null)
			{
				base.WriteJsonTip(false, "商品不存在或者已删除", "");
			}
			else if (base.UserID == -1)
			{
				base.WriteJsonTip(false, "会员没有登录", "");
			}
			else if (last != null && System.DateTime.Now < last.AutoTimeStamp.AddDays(3.0))
			{
				base.WriteJsonTip(false, "3天内已经登记过了", "");
			}
			else
			{
				StockoutInfo entity = new StockoutInfo
				{
					ProID = dataById.AutoID,
					ProName = dataById.ProductName,
					UserName = base.UserName,
					CurrStock = dataById.Stock,
					AutoTimeStamp = System.DateTime.Now
				};
				if (Stockout.Add(entity) > 0)
				{
					base.WriteJsonTip(true, "登记成功，我们将尽快处理！", "");
				}
				else
				{
					base.WriteJsonTip(false, "Thao tác thất bại！", "");
				}
			}
		}

		private void SendGoodsQA()
		{
			ProductInfo dataById = Product.GetDataById(WebUtils.GetFormInt("proid"));
			string formString = WebUtils.GetFormString("question");
			if (dataById == null)
			{
				base.WriteJsonTip(false, "商品不存在或者已删除", "");
			}
			else if (base.UserID == -1)
			{
				base.WriteJsonTip(false, "会员没有登录", "");
			}
			else if (string.IsNullOrEmpty(formString))
			{
				base.WriteJsonTip(false, "请输入问题内容", "");
			}
			else
			{
				GoodsQAInfo entity = new GoodsQAInfo
				{
					ProductID = dataById.AutoID,
					ProductName = dataById.ProductName,
					UserName = base.UserName,
					Question = formString,
					Answer = string.Empty,
					IsShow = true,
					AutoTimeStamp = System.DateTime.Now
				};
				if (GoodsQA.Add(entity) > 0)
				{
					base.WriteJsonTip(true, "问题提交成功！我们将尽快回复你。", "");
				}
				else
				{
					base.WriteJsonTip(false, "Thao tác thất bại！", "");
				}
			}
		}
	}
}
