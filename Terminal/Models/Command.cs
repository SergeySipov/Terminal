using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace Terminal.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Command
    {
        [JsonProperty]
        public string CmdName { get; set; }
        [JsonProperty]
        public char[] Params { get; set; }
        [JsonProperty]
        public string Argument { get; set; }
        [JsonProperty]
        public DateTime DateTime { get; set; }
        [JsonProperty]
        public string CurrentFolder { get; set; }
        [JsonProperty]
        public string PreviousFolder { get; set; }
        [JsonProperty]
        public bool? IsCmdExecuteSucceed { get; set; }
        [JsonProperty]
        public string Result { get; set; }

        private static Regex _regex;
        static Command() => _regex = new Regex(@"^([a-z]+)(?:\s+-([a-z]+))?(?:\s+([a-zA-Z\.\/]+))?$", RegexOptions.Compiled);

        [JsonConstructor]
        public Command(string cmdName, char[] @params, string argument, DateTime dateTime, string currentFolder, string previousFolder, bool? isCmdExecuteSucceed, string result)
        {
            CmdName = cmdName;
            Params = @params;
            Argument = argument;
            DateTime = dateTime;
            CurrentFolder = currentFolder;
            PreviousFolder = previousFolder;
            IsCmdExecuteSucceed = isCmdExecuteSucceed;
            Result = result;
        }

        public Command(string command, string currentFolder)
        {
            var match = _regex.Match(command);
            
            CmdName = match.Groups[1].Value;
            Params = match.Groups[2].Value.ToCharArray();
            Argument = match.Groups[3].Value;
            DateTime = DateTime.Now;
            CurrentFolder = currentFolder;
        }
    }
}
