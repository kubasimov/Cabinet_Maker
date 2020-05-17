using CoreS.Helpers;
using Newtonsoft.Json;
using System.IO;

namespace CoreS.Export
{
    public class JsonImport:MyLogger,IImport
    {
        public Cabinet Import(string path)
        {
            Logger.Debug("JsonImport");
            if (File.Exists(path))
                return JsonConvert.DeserializeObject<Cabinet>(File.ReadAllText(path));
            return null;
        }
    }
}
