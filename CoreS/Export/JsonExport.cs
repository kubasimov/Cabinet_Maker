using Config;
using CoreS.Helpers;
using Newtonsoft.Json;
using System.IO;

namespace CoreS.Export
{
    public class JsonExport : MyLogger, IExport
    {
        IConfig _config;
        public JsonExport(IConfig config)
        {
            _config = config;
        }

        public void Export(Cabinet cabinet)
        {
            Logger.Debug("JsonExport");

            var path = Path.Combine(_config.CabinetFilesDirectory(), cabinet.Name() + ".json");

            File.WriteAllText(path, JsonConvert.SerializeObject(cabinet, Formatting.Indented));
        }
    }
}