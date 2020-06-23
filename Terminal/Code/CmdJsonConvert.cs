using Newtonsoft.Json;
using Terminal.Models;

namespace Terminal.Code
{
    public static class CmdJsonConvert
    {
        public static string Serialize(Command cmd) => JsonConvert.SerializeObject(cmd, Formatting.Indented);

        public static Command Deserialize(string json) => JsonConvert.DeserializeObject<Command>(json);

    }
}
