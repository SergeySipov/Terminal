using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Terminal.Code;
using Terminal.Configuration;
using Terminal.Models;

namespace Terminal
{
    internal class TerminalController
    {
        public bool IsTerminalWorking { get; private set; }
        private Command _command;

        public TerminalController()
        {
            Cfg.CurrentFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            Cfg.PluginFolder = @$"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\.terminalPlugins";
            IsTerminalWorking = true;
        }

        public void WaitForInput()  
        {
            _command = new Command(Console.ReadLine(), Cfg.CurrentFolder);
            SearchCommandDll();
        }

        public (string, Color) GetResult()
        {//TODO зарефакторить
            if (_command.IsCmdExecuteSucceed.HasValue)
            {
                if (_command.IsCmdExecuteSucceed.Value)
                {
                    Cfg.PreviousFolder = _command.PreviousFolder;
                    Cfg.CurrentFolder = _command.CurrentFolder;
                    return (_command.Result, ColorTranslator.FromHtml(Cfg.AppSettings["SuccessColor"]));
                }
                else
                {
                    return (_command.Result, ColorTranslator.FromHtml(Cfg.AppSettings["ErrorColor"]));
                }
            }
            else
            {
                return (_command.Result, ColorTranslator.FromHtml(Cfg.AppSettings["StandartColor"]));
            }
        }

        private void SearchCommandDll()
        {
            if (_command.CmdName == "exit")
            {
                IsTerminalWorking = false;
                return;
            }

            string pathToDll = $@"{Cfg.PluginFolder}\{_command.CmdName}.dll";

            if (File.Exists(pathToDll))
            {
                UploadDll(pathToDll);
            }
            else
            {
                _command.Result = "Команда не обнаружена";
                _command.IsCmdExecuteSucceed = false;
            }
        }

        private void UploadDll(string pathToDll)
        { 
            var assembly = Assembly.LoadFrom(pathToDll);
            var cmdClass = Activator.CreateInstance(assembly.GetTypes().First());

            try
            {
                _command = CmdJsonConvert.Deserialize(cmdClass.GetType()
                        .GetMethod(Cfg.AppSettings["DllMethodName"])
                        .Invoke(cmdClass, new object[] { CmdJsonConvert.Serialize(_command) }).ToString());
            }
            catch
            {
                _command.Result = "Ошибка выполнения команды";
                _command.IsCmdExecuteSucceed = false;
            }
        }
    }
}
