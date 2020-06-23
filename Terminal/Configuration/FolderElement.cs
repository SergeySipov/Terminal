using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Terminal.Configuration
{
    internal sealed class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("key", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Key
        {
            get { return ((string)(base["key"])); }
            set { base["key"] = value; }
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Path
        {
            get { return ((string)(base["path"])); }
            set { base["path"] = value; }
        }
    }
}
