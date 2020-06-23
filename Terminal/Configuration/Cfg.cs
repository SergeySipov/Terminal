using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using Conf = System.Configuration.Configuration;
using ConfManager = System.Configuration.ConfigurationManager;

namespace Terminal.Configuration
{
    internal static class Cfg
    {
        private static Conf _conf;
        private static StartupFoldersConfigSection _section;

        static Cfg()
        {
            _conf = ConfManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _section = (StartupFoldersConfigSection)_conf.GetSection("StartupFolders");
        }

        public static NameValueCollection AppSettings
        {
            get => ConfManager.AppSettings;
        }

        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get => ConfManager.ConnectionStrings;
        }

        public static string PluginFolder
        {
            get => _section.FolderItems[0].Path;
            set => SetValue(value, 0);
        }

        public static string CurrentFolder
        {
            get => _section.FolderItems [1].Path;
            set => SetValue(value, 1);
        }

        public static string PreviousFolder
        {
            get => _section.FolderItems[2].Path;
            set => SetValue(value, 2);
        }

        private static void SetValue(string value, int index)
        {
            if (IsFileNameValid(value))
            {
                _section.FolderItems[index].Path = value;
                _conf.Save();
            }
        }

        private static bool IsFileNameValid(string fileName)
        {
            if ((fileName == null) || (fileName.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                return false;
            try
            {
                var tempFileInfo = new FileInfo(fileName);
                return true;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }
    }
}
