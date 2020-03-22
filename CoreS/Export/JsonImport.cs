using Newtonsoft.Json;
using System.IO;

namespace CoreS.Export
{
    public class JsonImport:IImport
    {
        public Cabinet Import(string path)
        {
            var cabinet2 = File.Exists(path) ? JsonConvert.DeserializeObject<Cabinet>(File.ReadAllText(path)) : new Cabinet();
            return cabinet2;
        }
    }
}
