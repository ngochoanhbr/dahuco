using System;
using System.Collections.ObjectModel;

namespace SinGooCMS.Entity
{
    public partial class UserFieldInfo
    {
        private string _Value = string.Empty;
        private Collection<string> _ItemValues;

        /// <summary>
        /// 字段的值
        /// </summary>
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
        public string GetSetting(int index)
        {
            if (index < this.ItemValues.Count)
            {
                return this.ItemValues[index];
            }
            return string.Empty;
        }
        public FieldSetting Settings
        {
            get
            {
                if (this.Setting != null && !string.IsNullOrEmpty(this.Setting))
                    return SinGooCMS.Utility.XmlSerializerUtils.Deserialize<FieldSetting>(this.Setting);

                return new FieldSetting();
            }
        }
        /// <summary>
        /// 获取多项 \n为分段
        /// </summary>
        public Collection<string> ItemValues
        {
            get
            {
                if (this._ItemValues == null)
                {
                    this._ItemValues = new Collection<string>();
                    if (!string.IsNullOrEmpty(this._Setting) && (this._ItemValues.Count == 0))
                    {
                        foreach (string str in this._Setting.Split(new char[] { '\n' }))
                        {
                            this._ItemValues.Add(str.Trim());
                        }
                    }
                }
                return this._ItemValues;
            }
        }
    }
}
