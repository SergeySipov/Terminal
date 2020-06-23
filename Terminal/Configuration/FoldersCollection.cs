using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Terminal.Configuration
{
    [ConfigurationCollection(typeof(FolderElement))]
    internal sealed class FoldersCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() =>
            new FolderElement();

        protected override object GetElementKey(ConfigurationElement element) =>
            ((FolderElement)(element)).Key;

        public void Add(string key, string path) => 
            BaseAdd(new FolderElement { Key = key, Path = path });

        public FolderElement this[int index]
        {
            get { return (FolderElement)BaseGet(index); }
        }
    }
}
