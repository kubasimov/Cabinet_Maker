using Newtonsoft.Json;
using System;
using System.IO;

namespace CoreS.Export
{
    public class JsonExport : IExport
    {
        public void Export(Cabinet cabinet)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cabinet_Maker", cabinet.Name() + ".json");

            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Cabinet_Maker")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cabinet_Maker"));
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(cabinet, Formatting.Indented));
        }
    }
}