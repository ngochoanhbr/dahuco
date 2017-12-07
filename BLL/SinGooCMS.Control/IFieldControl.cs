using System;
using System.Collections.ObjectModel;

namespace SinGooCMS.Control
{
    public interface IFieldControl
    {
        int ContentID { get; set; }
        FieldType ControlType { get; set; }
        string FieldName { get; set; }
        string FieldAlias { get; set; }
        string FieldValue { get; set; }
        string Description { get; set; }
        bool EnableNull { get; set; }
        string Tips { get; set; }
        FieldSetting Settings { get; set; }
        int DataLength { get; set; }
    }
}

