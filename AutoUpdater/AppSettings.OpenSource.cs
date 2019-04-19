using System;
using System.Diagnostics;

namespace AutoUpdater
{
    public static class AppSettings
    {
        public static string ServerFolder
        {
            get
            {
                var serverFolder = System.Configuration.ConfigurationManager.AppSettings["serverFolder"];
                if (String.IsNullOrEmpty(serverFolder))
                {
                    Debug.Assert(false, "AutoUpdater.exe.configファイルでキー「serverFolder」に対する値を設定してください。");
                }
                return serverFolder;
            }
        }

        public static string ExeName
        {
            get
            {
                var exeName = System.Configuration.ConfigurationManager.AppSettings["exeName"];
                if (String.IsNullOrEmpty(exeName))
                {
                    Debug.Assert(false, "AutoUpdater.exe.configファイルでキー「exeName」に対する値を設定してください。");
                }
                return exeName;
            }
        }
    }
}
