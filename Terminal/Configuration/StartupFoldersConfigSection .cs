using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Terminal.Configuration
{
    internal sealed class StartupFoldersConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Folders")]
        public FoldersCollection FolderItems
        {
            get { return ((FoldersCollection)(base["Folders"])); }
        }
    }
}
