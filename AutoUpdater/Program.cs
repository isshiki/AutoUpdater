
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AutoUpdater
{
    class Program
    {
        private const string prefix = "@";
        private static string[] targetExtensions = { ".exe", ".config", ".manifest", ".dll", ".pdf", ".cer" };
        private const string thisExeName = "AutoUpdater.exe";

        private static string localFolder;
        private static string localExePath;
        private static string serverExePath;
        private static string copyExePath;


        static void Main(string[] args)
        {
            var serverFolder = AutoUpdater.AppSettings.ServerFolder;
            if (String.IsNullOrEmpty(serverFolder))
            {
                //Console.WriteLine("AutoUpdater.exe.configファイルでキー「serverFolder」に対する値を設定してください。");
                Environment.Exit(1);
                return;
            }

            var exeName = AutoUpdater.AppSettings.ExeName;
            if (String.IsNullOrEmpty(exeName))
            {
                //Console.WriteLine("AutoUpdater.exe.configファイルでキー「exeName」に対する値を設定してください。");
                Environment.Exit(1);
                return;
            }

            Assembly entryAsm = Assembly.GetEntryAssembly();
            localFolder = Path.GetDirectoryName(entryAsm.Location);

            // サーバーフォルダーとローカルフォルダーが同じとは、自分自身ということ。自動更新する必要がない
            if (localFolder.StartsWith(serverFolder, StringComparison.OrdinalIgnoreCase))
            {
                Environment.Exit(0);
                return;
            }

            localExePath = Path.Combine(localFolder, exeName);
            serverExePath = Path.Combine(serverFolder, exeName);
            copyExePath = Path.Combine(localFolder, prefix + prefix + exeName);

            // ローカルにコピーしたうえで処理する
            try
            {
                if (File.Exists(serverExePath) == false) return;    // サーバーにアクセスできるかチェック

                if (File.Exists(copyExePath))
                {
                    File.Delete(copyExePath);
                }

                File.Copy(serverExePath, copyExePath);
            }
            catch (Exception)
            {
                return;
            }

            try
            {
                // サーバ側のアセンブリ情報を取得
                Assembly serverAsm = Assembly.LoadFile(copyExePath);
                AssemblyName serverAsmName = serverAsm.GetName();
                //Console.WriteLine(serverAsmName.Version.ToString());

                // アプリケーションオブジェクトからアセンブリ情報を取得
                Assembly localAsm = Assembly.LoadFile(localExePath);
                AssemblyName localAsmName = localAsm.GetName();
                //Console.WriteLine(localAsmName.Version.ToString());

                CheckAndUpdate(serverAsmName, localAsmName, serverFolder);
            }
            catch (Exception)
            {
            }

            // 後片付け
            try
            {
                if (File.Exists(copyExePath))
                {
                    File.Delete(copyExePath);
                }
            }
            catch (Exception)
            {
            }

            Environment.Exit(0);
        }

        private static void CheckAndUpdate(AssemblyName serverAsmName, AssemblyName localAsmName, string serverFolder)
        {
            // バージョンを比較する
            if (serverAsmName.Version <= localAsmName.Version)
            {
                return; // 変わらないか、サーバ側の方が古いので何もしない
            }

            // 新バージョンがサーバ側に存在するときの処理
            string[] files = Directory.GetFiles(serverFolder);

            List<string> extensions = new List<string>();
            extensions.AddRange(targetExtensions);

            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                if (fileName.Equals(thisExeName, StringComparison.OrdinalIgnoreCase)) continue;

                string checkExtention = Path.GetExtension(filePath).ToLower();
                if (extensions.Contains(checkExtention) == false)
                {
                    continue;
                }

                string sourceFile = filePath;
                string backupFile = Path.Combine(localFolder, prefix + fileName);
                string destinationFile = Path.Combine(localFolder, fileName);

                try
                {
                    // 取りあえず「@」を先頭に付けたファイル名でバックアップ
                    if (File.Exists(destinationFile))
                    {
                        if (File.Exists(backupFile))
                        {
                            File.Delete(backupFile);
                        }
                        File.Move(destinationFile, backupFile);
                    }

                    //　ファイルをローカルにダウンロード
                    File.Copy(sourceFile, destinationFile);
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
    }
}
