using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Config.Model
{
    internal class BaseConfiguration
    {
        [JsonProperty]
        internal string cabinetFilesDirectory;
        [JsonProperty]
        internal string excelExportDirectory;

    }
}

    
