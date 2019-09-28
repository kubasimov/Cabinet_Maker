using System.IO;
using Core.Export;
using Newtonsoft.Json;

namespace CoreS.Export
{
    class JsonImport:IImport
    {
        public Cabinet Import(string path)
        {
            var cabinet2 = File.Exists(path) ? JsonConvert.DeserializeObject<Cabinet>(File.ReadAllText(path)) : new Cabinet();
            return cabinet2;
        }
    }
}
