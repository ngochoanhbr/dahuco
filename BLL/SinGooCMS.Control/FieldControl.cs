using System;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace SinGooCMS.Control
{
    [ToolboxData("<{0}:FieldControl runat=\"server\"></{0}:FieldControl>"), Themeable(true)]
    public class FieldControl : System.Web.UI.Control, INamingContainer, IFieldControl
    {
        private IFieldControl fieldControl;
        protected override void CreateChildControls()
        {
            string virtualPath = this.ControlPath + this.ControlType.ToString() + ".ascx";
            if (!string.IsNullOrEmpty(this.LoadControlId))
                virtualPath = this.ControlPath + this.LoadControlId + ".ascx";

            System.Web.UI.Control control = this.Page.LoadControl(virtualPath);
            control.ID = "singoocmsctr";
            this.fieldControl = (IFieldControl)control;
            this.fieldControl.ControlType = this.ControlType;
            this.fieldControl.Description = this.Description;
            this.fieldControl.EnableNull = this.EnableNull;
            this.fieldControl.FieldAlias = this.FieldAlias;
            this.fieldControl.FieldName = this.FieldName;
            this.fieldControl.Settings = this.Settings;
            this.fieldControl.Tips = this.Tips;
            this.fieldControl.ContentID = this.ContentID;
            this.fieldControl.DataLength = this.DataLength;
            this.Controls.Add(control);
        }

        public int ContentID
        {
            get
            {
                object obj = this.ViewState["ContentID"];
                if (obj != null)
                    return (int)obj;

                return 0;
            }
            set
            {
                this.ViewState["ContentID"] = value;
            }
        }

        public string ControlPath
        {
            get
            {
                object obj = this.ViewState["ControlPath"];
                if (obj != null)
                    return (string)obj;

                return "~/Platform/FieldControls/";
            }
            set
            {
                this.ViewState["ControlPath"] = value;
            }
        }

        public FieldType ControlType
        {
            get
            {
                object obj = this.ViewState["ControlType"];
                if (obj != null)
                    return (FieldType)obj;

                return FieldType.SingleTextType;
            }
            set
            {
                this.ViewState["ControlType"] = value;
            }
        }

        public string Description
        {
            get
            {
                object obj = this.ViewState["Description"];
                if (obj != null)
                    return (string)obj;

                return string.Empty;
            }
            set
            {
                this.ViewState["Description"] = value;
            }
        }

        public bool EnableNull
        {
            get
            {
                object obj = this.ViewState["EnableNull"];
                return ((obj != null) && ((bool)obj));
            }
            set
            {
                this.ViewState["EnableNull"] = value;
            }
        }

        public string FieldAlias
        {
            get
            {
                object obj = this.ViewState["FieldAlias"];
                if (obj != null)
                    return (string)obj;

                return string.Empty;
            }
            set
            {
                this.ViewState["FieldAlias"] = value;
            }
        }

        public int FieldId
        {
            get
            {
                object obj = this.ViewState["FieldId"];
                if (obj != null)
                    return (int)obj;

                return 0;
            }
            set
            {
                this.ViewState["FieldId"] = value;
            }
        }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength
        {
            get
            {
                object obj = this.ViewState["DataLength"];
                if (obj != null)
                    return (int)obj;

                return 50;
            }
            set
            {
                this.ViewState["DataLength"] = value;
            }
        }
        public string FieldName
        {
            get
            {
                object obj = this.ViewState["FieldName"];
                if (obj != null)
                    return (string)obj;

                return string.Empty;
            }
            set
            {
                this.ViewState["FieldName"] = value;
            }
        }

        public string LoadControlId
        {
            get
            {
                object obj = this.ViewState["LoadControlId"];
                if (obj != null)
                    return (string)obj;

                return string.Empty;
            }
            set
            {
                this.ViewState["LoadControlId"] = value;
            }
        }

        public FieldSetting Settings
        {
            get
            {
                object obj = this.ViewState["Settings"];
                if (obj != null)
                    return (FieldSetting)obj;

                return null;
            }
            set
            {
                this.ViewState["Settings"] = value;
            }
        }

        public string Tips
        {
            get
            {
                object obj = this.ViewState["Tips"];
                if (obj != null)
                    return (string)obj;

                return string.Empty;
            }
            set
            {
                this.ViewState["Tips"] = value;
            }
        }
        public string FieldValue
        {
            get
            {
                object obj = this.ViewState["FieldValue"];
                if (obj != null)
                    return (string)obj;

                return string.Empty;
            }
            set
            {
                this.ViewState["FieldValue"] = value;
            }
        }
        public string Value
        {
            get
            {
                this.EnsureChildControls();
                if (this.fieldControl != null)
                    return this.fieldControl.FieldValue;

                return string.Empty;
            }
            set
            {
                this.EnsureChildControls();
                if (this.fieldControl != null)
                    this.fieldControl.FieldValue = value;
            }
        }
    }
}

