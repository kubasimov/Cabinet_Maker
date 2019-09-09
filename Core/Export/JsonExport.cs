using System;
using System.IO;
using Newtonsoft.Json;

namespace Core.Export
{
    public class JsonExport : IExport
    {
        public void Export(Cabinet cabinet)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cabinet_Maker", cabinet.Name + ".json");

            File.WriteAllText(path, JsonConvert.SerializeObject(cabinet, Formatting.Indented));
        }
    }
}