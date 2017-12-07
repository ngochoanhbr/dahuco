using System;
using System.Resources;

namespace JsonLeeCMS.Control
{
    internal sealed class SR
    {
        private static JsonLeeCMS.Control.SR _loader = null;
        private static object _lock = new object();
        private ResourceManager _rm;

        private SR()
        {
            this._rm = new ResourceManager("JsonLeeCMS.Control.Resources.Chinese", base.GetType().Assembly);
        }

        private static JsonLeeCMS.Control.SR GetLoader()
        {
            if (_loader == null)
            {
                lock (_lock)
                {
                    if (_loader == null)
                    {
                        _loader = new JsonLeeCMS.Control.SR();
                    }
                }
            }
            return _loader;
        }

        public static string GetString(string name)
        {
            JsonLeeCMS.Control.SR loader = GetLoader();
            string str = null;
            if (loader != null)
            {
                str = loader.Resources.GetString(name, null);
            }
            return str;
        }

        private ResourceManager Resources
        {
            get
            {
                return this._rm;
            }
        }
    }
}

