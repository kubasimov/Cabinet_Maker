using Config;
using CoreS.Helpers;
using CoreS.Model;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace CoreS.Export
{
    public class JsonExport : MyLogger, IExport
    {
        IConfig _config;
        public JsonExport(IConfig config)
        {
            _config = config;
        }

        public async Task ExportAsync(CabinetModelDTO cabinet)
        {
            Logger.Debug("JsonExport");

            var path = Path.Combine(_config.CabinetFilesDirectory(), cabinet._name + ".json");

            using (StreamWriter writer = File.CreateText(path))     
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(cabinet, Formatting.Indented));
            }
            //File.WriteAllText(path, JsonConvert.SerializeObject(cabinet, Formatting.Indented));
        }
    }
}