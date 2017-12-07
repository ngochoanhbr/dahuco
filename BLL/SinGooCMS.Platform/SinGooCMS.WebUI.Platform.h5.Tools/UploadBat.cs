using SinGooCMS.BLL;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.Tools
{
	public class UploadBat : H5ManagerPageBase
	{
		protected DropDownList ddlFolder;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindFolder();
			}
		}

		private void BindFolder()
		{
			this.ddlFolder.DataSource = Folder.GetAllList();
			this.ddlFolder.DataTextField = "FolderName";
			this.ddlFolder.DataValueField = "AutoID";
			this.ddlFolder.DataBind();
			this.ddlFolder.Items.Insert(0, new ListItem("未归档", "-1"));
			this.ddlFolder.Items[0].Selected = true;
		}
	}
}
