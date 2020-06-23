using System;
using System.IO;
using Console = Colorful.Console;
using Terminal.Configuration;
using System.Drawing;

namespace Terminal
{
    internal static class Terminal
    {
        private static TerminalController _controller;

        static Terminal() =>
            (_controller, Console.Title, Console.ForegroundColor) = 
                (new TerminalController(), "Terminal", ColorTranslator.FromHtml(Cfg.AppSettings["StandartColor"]));

        static void Main()
        {
            if (!Directory.Exists(Cfg.PluginFolder))
                Directory.CreateDirectory(Cfg.PluginFolder);

            while (_controller.IsTerminalWorking)
            {                
                Console.Write($"[{Environment.UserName}@{Environment.MachineName}]:", ColorTranslator.FromHtml(Cfg.AppSettings["UserColor"]));
                Console.Write($"{Cfg.CurrentFolder}$ ", ColorTranslator.FromHtml(Cfg.AppSettings["CurrentFolderColor"]));

                _controller.WaitForInput();
                var (result, color) = _controller.GetResult();
                Console.WriteLine(result, color);
            }
        }

        private static void button1_Click(object sender, EventArgs e)
        {
            //var cmdProcess = Process.Start(new ProcessStartInfo
            //{
            //    FileName = "cmd",
            //    Arguments = "/c chcp 65001 & dism /online /cleanup-image /startcomponentcleanup",
            //    UseShellExecute = false,
            //    RedirectStandardOutput = true,
            //    CreateNoWindow = true,
            //});
            //cmdProcess.BeginOutputReadLine();
            //cmdProcess.OutputDataReceived += (se, ea) => { textBox1.Text += ea.Data + "\n"; };

            //var powerShellProcess = Process.Start(new ProcessStartInfo
            //{
            //    FileName = "powershell",
            //    Arguments = "/command get-appxpackage | ft name",
            //    UseShellExecute = false,
            //    RedirectStandardOutput = true,
            //    CreateNoWindow = true,
            //});

            //textBox1.Text = powerShellProcess.StandardOutput.ReadToEnd();
        }
    }
}
