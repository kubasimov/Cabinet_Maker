using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Export
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
