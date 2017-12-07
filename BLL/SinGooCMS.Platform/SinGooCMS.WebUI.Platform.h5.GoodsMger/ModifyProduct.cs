using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class ModifyProduct : H5ManagerPageBase
	{
		protected Literal catepath;

		protected TextBox TextBox1;

		protected DropDownList DropDownList3;

		protected TextBox TextBox4;

		protected FullImage Image1;

		protected TextBox TextBox5;

		protected RadioButtonList RadioButtonList8;

		protected HtmlGenericControl suitpros;

		protected Repeater rpt_suitprolist;

		protected TextBox metaKey;

		protected TextBox metaDescription;

		protected CheckBox chkHot;

		protected CheckBox chkRecommand;

		protected CheckBox chkNew;

		protected CheckBox isvirtual;

		protected CheckBox isbooking;

		protected DropDownList ddlAreaModel;

		protected DropDownList ddlPostageModel;

		protected HtmlInputCheckBox auditstatus;

		protected TextBox TextBox12;

		protected H5TextBox TextBox10;

		protected H5TextBox TextBox11;

		protected Literal splm;

		protected Panel panelswitch;

		protected Panel panelgg;

		protected H5TextBox TextBox13;

		protected CheckBox chkExchange;

		protected H5TextBox TextBox14;

		protected H5TextBox TextBox15;

		protected H5TextBox TextBox16;

		protected H5TextBox alartnum;

		protected Repeater rpt_Tags;

		protected Repeater rptDetail;

		protected Repeater rpt_img;

		protected Button btnok;

		protected Button btnok_andcontinue;

		public CategoryInfo currCate = null;

		public GoodsClassInfo goodsClass = null;

		public ProductModelInfo proModel = null;

		public ProductInfo proInit = null;

		public string JsonForGuiGeInitValue = string.Empty;

		public string IsOnSale = "On";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.IsOnSale = WebUtils.GetQueryString("Status", "On");
			this.currCate = SinGooCMS.BLL.Category.GetCacheCategoryByID(WebUtils.GetQueryInt("CateID"));
			this.goodsClass = SinGooCMS.BLL.GoodsClass.GetDataById(WebUtils.GetQueryInt("ClassID"));
			if (base.IsEdit)
			{
				this.proInit = Product.GetDataById(base.OpID);
				if (this.currCate == null)
				{
					this.currCate = SinGooCMS.BLL.Category.GetDataById((this.proInit == null) ? 0 : this.proInit.CateID);
				}
				if (this.goodsClass == null)
				{
					this.goodsClass = SinGooCMS.BLL.GoodsClass.GetDataById((this.proInit == null) ? 0 : this.proInit.ClassID);
				}
			}
			this.panelgg.Visible = (this.panelswitch.Visible = ((this.goodsClass == null) ? false : true));
			if (this.currCate == null)
			{
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "alertandredirect", string.Concat(new object[]
				{
					"<script>alert('Không tìm thấy thông tin');location='Products.aspx?CatalogID=",
					base.CurrentCatalogID,
					"&Module=",
					base.CurrentModuleCode,
					"&action=View'</script>"
				}));
			}
			else if (base.IsEdit && this.proInit == null)
			{
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "alertandredirect", string.Concat(new object[]
				{
					"<script>alert('Thông tin sản phẩm không tìm thấy');location='Products.aspx?CatalogID=",
					base.CurrentCatalogID,
					"&Module=",
					base.CurrentModuleCode,
					"&action=View'</script>"
				}));
			}
			else
			{
				this.proModel = (base.IsEdit ? ProductModel.GetCacheModelById(this.proInit.ModelID) : ProductModel.GetCacheModelById(this.currCate.ModelID));
				if (!base.IsPostBack)
				{
					this.BindBrand();
					this.BindAreaModel();
					this.BindPostageModel();
					this.BindTags();
					this.BindData();
				}
			}
		}

		private void BindBrand()
		{
			this.DropDownList3.DataSource = Brand.GetBrands();
			this.DropDownList3.DataTextField = "BrandName";
			this.DropDownList3.DataValueField = "AutoID";
			this.DropDownList3.DataBind();
		}

		private void BindPhoto()
		{
			this.rpt_img.DataSource = PhotoAlbum.GetPhotoAlbumByPID(base.OpID);
			this.rpt_img.DataBind();
		}

		private void BindAreaModel()
		{
			this.ddlAreaModel.DataSource = AreaModel.GetList(1000, "", "AutoID desc");
			this.ddlAreaModel.DataTextField = "ModelName";
			this.ddlAreaModel.DataValueField = "AutoID";
			this.ddlAreaModel.DataBind();
		}

		private void BindPostageModel()
		{
			this.ddlPostageModel.DataSource = PostageModel.GetList(1000, "", "AutoID desc");
			this.ddlPostageModel.DataTextField = "ModelName";
			this.ddlPostageModel.DataValueField = "AutoID";
			this.ddlPostageModel.DataBind();
		}

		private void BindTags()
		{
			this.rpt_Tags.DataSource = SinGooCMS.BLL.Tags.GetAllList();
			this.rpt_Tags.DataBind();
		}

		private void BindData()
		{
			System.Collections.Generic.IList<CategoryInfo> categoryNav = new ShopFunction().GetCategoryNav(this.currCate.AutoID);
			string text = string.Empty;
			if (categoryNav != null && categoryNav.Count > 0)
			{
				foreach (CategoryInfo current in categoryNav)
				{
					text = text + current.CategoryName + " » ";
				}
			}
			this.catepath.Text = text.Trim().TrimEnd(new char[]
			{
				'»'
			});
            this.splm.Text = ((this.goodsClass == null) ? "Không chọn các loại sản phẩm" : this.goodsClass.ClassName);
			System.Collections.Generic.List<ProductFieldInfo> list = (System.Collections.Generic.List<ProductFieldInfo>)Product.GetFieldListWithValue(base.OpID, this.proModel.AutoID);
			list.Sort((ProductFieldInfo parameterA, ProductFieldInfo parameterB) => parameterA.Sort.CompareTo(parameterB.Sort));
			this.rptDetail.DataSource = list;
			this.rptDetail.DataBind();
			if (base.IsEdit)
			{
				this.TextBox1.Text = this.proInit.ProductName;
				ListItem listItem = this.DropDownList3.Items.FindByValue(this.proInit.BrandID.ToString());
				if (listItem != null)
				{
					listItem.Selected = true;
				}
				this.TextBox4.Text = this.proInit.ProductSN;
				this.TextBox5.Text = this.proInit.ProImg;
				this.Image1.ImageUrl = this.proInit.ProImg;
				ListItem listItem2 = this.RadioButtonList8.Items.FindByText(this.proInit.SellType);
				if (listItem2 != null)
				{
					listItem2.Selected = true;
					if (listItem2.Text == "Thiết lập bán hàng")
					{
						this.suitpros.Attributes.Remove("class");
						this.rpt_suitprolist.DataSource = this.proInit.SuitProductList;
						this.rpt_suitprolist.DataBind();
					}
				}
				this.metaKey.Text = this.proInit.SEOKey;
				this.metaDescription.Text = this.proInit.SEODescription;
				this.TextBox10.Text = this.proInit.MarketPrice.ToString("f2");
				this.TextBox11.Text = this.proInit.SellPrice.ToString("f2");
				this.TextBox12.Text = this.proInit.Unit;
				this.TextBox13.Text = this.proInit.BuyIntegral.ToString();
				this.TextBox14.Text = this.proInit.GiveIntegral.ToString();
				this.TextBox15.Text = this.proInit.Stock.ToString();
				this.TextBox16.Text = this.proInit.BuyLimit.ToString();
				this.chkExchange.Checked = this.proInit.IsExchange;
				this.isvirtual.Checked = this.proInit.IsVirtual;
				this.isbooking.Checked = this.proInit.IsBooking;
				ListItem listItem3 = this.ddlAreaModel.Items.FindByValue(this.proInit.AreaModelID.ToString());
				if (listItem3 != null)
				{
					listItem3.Selected = true;
				}
				ListItem listItem4 = this.ddlPostageModel.Items.FindByValue(this.proInit.PostageModelID.ToString());
				if (listItem4 != null)
				{
					listItem4.Selected = true;
				}
				this.alartnum.Text = this.proInit.AlarmNum.ToString();
				this.chkHot.Checked = this.proInit.IsTop;
				this.chkRecommand.Checked = this.proInit.IsRecommend;
				this.chkNew.Checked = this.proInit.IsNew;
				this.auditstatus.Checked = (this.proInit.Status == 99);
				this.BindPhoto();
				if (this.goodsClass != null)
				{
					this.InitGuiGe(this.goodsClass, this.proInit);
				}
			}
			else
			{
				this.TextBox4.Text = Product.GetProductSN();
				this.btnok_andcontinue.Visible = true;
			}
		}

		private void InitGuiGe(GoodsClassInfo goodsClass, ProductInfo pro)
		{
			if (goodsClass.AutoID == pro.ClassID)
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				System.Collections.Generic.IList<GoodsSpecifyInfo> list = GoodsSpecify.GetList(1000, " ProID=" + pro.AutoID, "AutoID asc");
				foreach (GoodsSpecifyInfo current in list)
				{
					stringBuilder.Append("<tr>");
					int num = 0;
					string[] array = current.Specification.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i];
						stringBuilder.Append(string.Concat(new string[]
						{
							"<td class='ggname' style='background:#ffa;position:relative;' onclick=\\\"createMenu(this,'",
							goodsClass.GeiGeSets[num].GuiGeValue,
							"')\\\"><span>",
							text,
							"</span><input type='hidden' name='gg_name' value='",
							text,
							"' /></td>"
						}));
						num++;
					}
					string gUID = StringUtils.GetGUID();
					stringBuilder.Append(string.Concat(new object[]
					{
						"<td><input type='text' value='",
						current.ProductSN,
						"' name='gg_hh' class='input-txt' style='width:120px;' /></td><td><input type='text' value='",
						current.SellPrice.ToString("f2"),
						"' name='gg_xsj' class='input-txt' style='width:60px;' /><input type='hidden' name='gg_memberprice' id='",
						gUID,
						"'/> <a href='javascript:;' onclick=\\\"$.dialog.open('MemberPrice.aspx?Module=",
						base.CurrentModuleCode,
						"&type=guige&opid=",
						current.AutoID,
						"&price=",
						current.SellPrice.ToString("f2"),
						"&retid=",
						gUID,
						"&action=Modify',{title:'Chỉnh sửa giá thành viên',width:580,height:430},false);\\\" >giá thành viên</a></td><td><input type='text' value='",
						current.Stock,
						"' name='gg_kc' class='input-txt' style='width:60px;' /></td><td><input type='text' value='",
						current.AlarmNum,
						"' name='gg_alarmnum' class='input-txt' style='width:60px;' /></td><td><a href='javascript:;' onclick='$(this).parent().parent().remove();'>Remove</a></td>"
					}));
					stringBuilder.Append("</tr>");
				}
				this.JsonForGuiGeInitValue = stringBuilder.ToString().Trim();
			}
		}

		protected void rptDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				ProductFieldInfo productFieldInfo = e.Item.DataItem as ProductFieldInfo;
				FieldControl fieldControl = e.Item.FindControl("field") as FieldControl;
				if (fieldControl != null)
				{
					fieldControl.ControlType = (FieldType)productFieldInfo.FieldType;
					fieldControl.ControlPath = "~/Platform/h5/FieldControls/";
					fieldControl.LoadControlId = ((FieldType)productFieldInfo.FieldType).ToString();
					fieldControl.FieldName = productFieldInfo.FieldName;
					fieldControl.FieldAlias = productFieldInfo.Alias;
					fieldControl.FieldId = productFieldInfo.AutoID;
					fieldControl.Settings = XmlSerializerUtils.Deserialize<SinGooCMS.Control.FieldSetting>(productFieldInfo.Setting);
					fieldControl.DataLength = productFieldInfo.DataLength;
					fieldControl.EnableNull = productFieldInfo.EnableNull;
					if (!string.IsNullOrEmpty(productFieldInfo.Value))
					{
						fieldControl.Value = productFieldInfo.Value;
					}
					else
					{
						fieldControl.Value = (productFieldInfo.DefaultValue ?? string.Empty);
					}
				}
			}
		}

		protected void btnok_andcontinue_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Add.ToString()) && !base.IsAuthorizedOp(ActionType.Add.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else if (base.Action.Equals(ActionType.Add.ToString()))
			{
				ProductInfo productInfo = new ProductInfo();
				string text = this.Add(ref productInfo);
				if (text == "success")
				{
					this.CreateGuiGe(productInfo.AutoID);
					this.CreatePro2Json();
                    PageBase.log.AddEvent(base.LoginAccount.AccountName, "Thêm sản phẩm [" + productInfo.ProductName + "] thành công");
					base.ShowMsg("Thao tác thành công");
				}
				else
				{
					base.ShowMsg(text);
				}
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Add.ToString()) && !base.IsAuthorizedOp(ActionType.Add.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else if (base.Action.Equals(ActionType.Modify.ToString()) && !base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				if (base.Action.Equals(ActionType.Add.ToString()))
				{
					ProductInfo productInfo = new ProductInfo();
                    string text = this.Add(ref productInfo);
                    //string text = "fail";
					if (text == "success")
					{
						this.CreateGuiGe(productInfo.AutoID);
						this.CreatePro2Json();
						this.SaveGuiGePic(productInfo);
                        PageBase.log.AddEvent(base.LoginAccount.AccountName, "Thêm sản phẩm [" + productInfo.ProductName + "] thành công");
						base.Response.Redirect(string.Concat(new object[]
						{
							"Products.aspx?CatalogID=",
							base.CurrentCatalogID,
							"&Module=",
							base.CurrentModuleCode,
							"&Status=",
							(productInfo.Status == 99) ? "On" : "Off",
							"&action=View"
						}));
					}
					else
					{
						base.ShowMsg(text);
					}
				}
				if (base.Action.Equals(ActionType.Modify.ToString()))
				{
					ProductInfo dataById = Product.GetDataById(base.OpID);
					string text = this.Modify(ref dataById);
					if (text == "success")
					{
						this.CreateGuiGe(dataById.AutoID);
						this.CreatePro2Json();
						this.SaveGuiGePic(dataById);
                        PageBase.log.AddEvent(base.LoginAccount.AccountName, "Sửa sản phẩm [" + dataById.ProductName + "] thành công");
						base.Response.Redirect(string.Concat(new object[]
						{
							"Products.aspx?CatalogID=",
							base.CurrentCatalogID,
							"&Module=",
							base.CurrentModuleCode,
							"&Status=",
							(dataById.Status == 99) ? "On" : "Off",
							"&action=View"
						}));
					}
					else
					{
						base.ShowMsg(text);
					}
				}
			}
		}

		private string Add(ref ProductInfo proAdd)
		{
			string result = "success";
			string @string = WebUtils.GetString(this.TextBox1.Text);
            int autoID = this.currCate.AutoID;
            int @int = WebUtils.GetInt(this.DropDownList3.SelectedValue, 0);
            string string2 = WebUtils.GetString(this.TextBox4.Text);
            int int2 = WebUtils.GetInt(this.TextBox13.Text);
            bool @checked = this.chkExchange.Checked;
            if (string.IsNullOrEmpty(@string))
            {
                result = "Vui lòng chọn tên sản phẩm";
            }
            if (autoID.Equals(0))
            {
                result = "Vui lòng chọn";
            }
            else if (@int.Equals(0))
            {
                result = "Vui lòng chọn thương hiệu";
            }
            else if (Product.ExistsProductSN(string2, 0))
            {
                result = "ID sản phẩm đã tồn tại";
            }
            else if (@checked && int2 <= 0)
            {
                result = "The required points for convertible goods are not less than 0";
            }
            else
			{
				proAdd.ProductName = @string;
                proAdd.CateID = autoID;
				System.Collections.Generic.List<PhotoAlbumInfo> list = new System.Collections.Generic.List<PhotoAlbumInfo>();
                this.SetEntity(proAdd, list);
				System.Collections.Generic.Dictionary<string, ProductFieldInfo> fieldDicWithValues = this.GetFieldDicWithValues();
				if (!Product.Add(proAdd, fieldDicWithValues, list))
				{
                    result = "Thêm hàng hóa thất bại";
				}
			}
			return result;
		}

		private System.Collections.Generic.Dictionary<string, ProductFieldInfo> GetFieldDicWithValues()
		{
			System.Collections.Generic.Dictionary<string, ProductFieldInfo> dictionary = new System.Collections.Generic.Dictionary<string, ProductFieldInfo>();
			foreach (RepeaterItem repeaterItem in this.rptDetail.Items)
			{
				FieldControl fieldControl = repeaterItem.FindControl("field") as FieldControl;
				if (fieldControl != null)
				{
					ProductFieldInfo dataById = ProductField.GetDataById(fieldControl.FieldId);
					if (dataById != null)
					{
						dataById.Value = fieldControl.Value;
						dictionary.Add(dataById.FieldName, dataById);
					}
				}
			}
			return dictionary;
		}

		private string Modify(ref ProductInfo proModify)
		{
			string result = "success";
			string @string = WebUtils.GetString(this.TextBox1.Text);
			string string2 = WebUtils.GetString(this.TextBox4.Text);
			int @int = WebUtils.GetInt(this.TextBox13.Text);
			bool @checked = this.chkExchange.Checked;
			if (string.IsNullOrEmpty(@string))
			{
                result = "Vui lòng chọn tên sản phẩm";
			}
			else if (Product.ExistsProductSN(string2, proModify.AutoID))
			{
                result = "ID sản phẩm đã tồn tại";
			}
			else if (@checked && @int <= 0)
			{
                result = "The required points for convertible goods are not less than 0";
			}
			else
			{
				System.Collections.Generic.List<PhotoAlbumInfo> list = new System.Collections.Generic.List<PhotoAlbumInfo>();
				proModify.ProductName = @string;
				this.SetEntity(proModify, list);
				System.Collections.Generic.Dictionary<string, ProductFieldInfo> fieldDicWithValues = this.GetFieldDicWithValues();
				if (!Product.Update(proModify, fieldDicWithValues, list))
				{
					result = "Sửa sản phẩm thất bại";
				}
			}
			return result;
		}

		private void SetEntity(ProductInfo pro, System.Collections.Generic.List<PhotoAlbumInfo> listProPhoto)
		{
            int brandID = WebUtils.StringToInt(this.DropDownList3.SelectedValue, 0);
            int areaModelID = WebUtils.StringToInt(this.ddlAreaModel.SelectedValue, 0);
            int postageModelID = WebUtils.StringToInt(this.ddlPostageModel.SelectedValue, 0);
            pro.BrandID = brandID;
            pro.ModelID = this.currCate.ModelID;
            pro.SellType = this.RadioButtonList8.SelectedItem.Text;
            pro.ProductSN = WebUtils.GetString(this.TextBox4.Text);
            pro.ProImg = this.TextBox5.Text;
            pro.Stock = WebUtils.StringToInt(this.TextBox15.Text, 0);
            pro.AlarmNum = WebUtils.GetInt(this.alartnum.Text);
            pro.MarketPrice = WebUtils.StringToDecimal(this.TextBox10.Text, 0m);
            pro.SellPrice = WebUtils.StringToDecimal(this.TextBox11.Text, 0m);
            pro.Unit = WebUtils.GetString(this.TextBox12.Text);
            pro.BuyIntegral = WebUtils.StringToInt(this.TextBox13.Text, 0);
            pro.GiveIntegral = WebUtils.StringToInt(this.TextBox14.Text, 0);
            pro.BuyLimit = WebUtils.StringToInt(this.TextBox16.Text, 0);
            pro.SEOKey = WebUtils.GetString(this.metaKey.Text);
            pro.SEODescription = WebUtils.GetString(this.metaDescription.Text);
            pro.IsTop = this.chkHot.Checked;
            pro.IsRecommend = this.chkRecommand.Checked;
            pro.IsNew = this.chkNew.Checked;
            pro.Status = (this.auditstatus.Checked ? 99 : 0);
            pro.IsExchange = this.chkExchange.Checked;
            pro.IsVirtual = this.isvirtual.Checked;
            pro.IsBooking = this.isbooking.Checked;
            pro.ProIDs = this.SetSuitPros();
            pro.AreaModelID = areaModelID;
            pro.PostageModelID = postageModelID;
            pro.ClassID = ((this.goodsClass == null) ? 0 : this.goodsClass.AutoID);
            pro.Tags = WebUtils.GetFormString("chk_tag");
            string formString = WebUtils.GetFormString("hdf_memberprice");
            if (!string.IsNullOrEmpty(formString))
            {
                pro.MemberPriceSet = formString.Replace("逗号", ",");
            }
            string formString2 = WebUtils.GetFormString("proimg");
            string formString3 = WebUtils.GetFormString("imgdesc");
            string[] array = formString2.Split(new char[]
            {
                ','
            });
            string[] array2 = formString3.Split(new char[]
            {
                ','
            });
            if (array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    string text = array[i].Trim();
                    if (!string.IsNullOrEmpty(text))
                    {
                        PhotoAlbumInfo photoAlbumInfo = new PhotoAlbumInfo();
                        photoAlbumInfo.ImgSrc = text;
                        photoAlbumInfo.ImgThumbSrc = photoAlbumInfo.ImgSrc.Replace(System.IO.Path.GetExtension(text), "_thumb" + System.IO.Path.GetExtension(text));
                        photoAlbumInfo.Remark = array2[i];
                        listProPhoto.Add(photoAlbumInfo);
                    }
                }
            }
		}

		private string SetSuitPros()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string formString = WebUtils.GetFormString("suit_pro");
			string formString2 = WebUtils.GetFormString("suit_pronum");
			string[] array = formString.Split(new char[]
			{
				','
			});
			string[] array2 = formString2.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (!string.IsNullOrEmpty(array[i]) && array[i].IndexOf("|") != -1)
				{
					stringBuilder.Append(array[i] + "|" + array2[i] + ",");
				}
			}
			string text = stringBuilder.ToString();
			if (text.EndsWith(","))
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text;
		}

		private void CreateGuiGe(int proID)
		{
			GoodsSpecify.DelByProID(proID);
			if (base.Request.Form.AllKeys.Contains("gg_name") && base.Request.Form.AllKeys.Contains("gg_hh") && base.Request.Form.AllKeys.Contains("gg_xsj") && base.Request.Form.AllKeys.Contains("gg_memberprice") && base.Request.Form.AllKeys.Contains("gg_kc") && base.Request.Form.AllKeys.Contains("gg_alarmnum"))
			{
				string[] array = base.Request.Form["gg_name"].Split(new char[]
				{
					','
				});
				string[] array2 = base.Request.Form["gg_hh"].Split(new char[]
				{
					','
				});
				string[] array3 = base.Request.Form["gg_xsj"].Split(new char[]
				{
					','
				});
				string[] array4 = base.Request.Form["gg_memberprice"].Split(new char[]
				{
					','
				});
				string[] array5 = base.Request.Form["gg_kc"].Split(new char[]
				{
					','
				});
				string[] array6 = base.Request.Form["gg_alarmnum"].Split(new char[]
				{
					','
				});
				if (array2.Length > 0)
				{
					for (int i = 0; i < array2.Length; i++)
					{
						string text = string.Empty;
						for (int j = 0; j < this.goodsClass.GeiGeSets.Count; j++)
						{
							text = text + array[i * this.goodsClass.GeiGeSets.Count + j] + ",";
						}
						GoodsSpecify.Add(new GoodsSpecifyInfo
						{
							Specification = text.TrimEnd(new char[]
							{
								','
							}),
							ProID = proID,
							ProductSN = array2[i],
							PreviewImg = string.Empty,
							SellPrice = WebUtils.GetDecimal(array3[i]),
							Stock = WebUtils.GetInt(array5[i]),
							MemberPriceSet = array4[i].Replace("逗号", ","),
							PromotePrice = 0.0m,
							AlarmNum = WebUtils.GetInt(array6[i]),
							Sort = 99,
							Lang = base.cultureLang,
							AutoTimeStamp = System.DateTime.Now
						});
					}
				}
			}
		}

		private void CreatePro2Json()
		{
			System.Collections.Generic.List<ShortProduct> list = new System.Collections.Generic.List<ShortProduct>();
			System.Collections.Generic.List<ProductInfo> list2 = (System.Collections.Generic.List<ProductInfo>)Product.GetList(100000, " Status=99 ", "IsTop desc,IsRecommend desc,AutoID desc");
			foreach (ProductInfo current in list2)
			{
				list.Add(new ShortProduct
				{
					ProID = current.AutoID,
					ProductName = current.ProductName
				});
			}
			System.IO.File.WriteAllText(base.Server.MapPath("/Config/pro.js"), "prodata=" + JsonUtils.ObjectToJson<System.Collections.Generic.List<ShortProduct>>(list));
		}

		private void SaveGuiGePic(ProductInfo pro)
		{
			if (pro != null && this.goodsClass != null)
			{
				GuiGeSet guiGeSet = (from p in this.goodsClass.GeiGeSets
				where p.IsImageShow
				select p).FirstOrDefault<GuiGeSet>();
				string formString = WebUtils.GetFormString("hdf_backguigepic");
				if (guiGeSet != null && !string.IsNullOrEmpty(formString))
				{
					GuiGePic.DelByProID(pro.AutoID);
					GuiGePicInfo entity = new GuiGePicInfo
					{
						ProID = pro.AutoID,
						ClassID = pro.ClassID,
						GuiGeName = guiGeSet.GuiGeName,
						ImagesCollection = formString,
						AutoTimeStamp = System.DateTime.Now
					};
					GuiGePic.Add(entity);
				}
			}
		}
	}
}
