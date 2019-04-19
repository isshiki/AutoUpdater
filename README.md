
# AutoUpdater

サーバーに配置されている「アプリ.exe」ファイルのバージョンと、ローカル実行環境の「アプリ.exe」ファイルのバージョンを比較して、サーバーに新しい「アプリ.exe」があれば、同じフォルダー内に存在する関連対象ファイル（".exe", ".config", ".manifest", ".dll", ".pdf", ".cer"）を含めて、フォルダーごとローカルにダウンロードし、ローカルのアプリケーションを最新に保つ。

プログラム「アプリ.exe」の実行終了時に、呼び出す目的で作成している。以下はその呼び出しコード例。

```C#
// 自動アップデータを実行する
try
{
    var psInfo = new ProcessStartInfo();
    psInfo.FileName = "AutoUpdater.exe"; // 実行するファイル
    psInfo.CreateNoWindow = true;        // コンソール・ウィンドウを開かない
    psInfo.UseShellExecute = false;      // シェル機能を使用しない
    Process.Start(psInfo);
}
catch (Exception)
{
}
```