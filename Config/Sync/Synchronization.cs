using Config.Model;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Config.Sync
{
    class Synchronization : ISynchrontization
    {
        string BaseConfigurationFilename=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"CabinetMaker", "BaseConfig.json");
        string CabinetConfigurationFilename= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CabinetMaker", "CabinetConfig.json");

        public BaseConfiguration BaseRead()
        {
            return JsonConvert.DeserializeObject<BaseConfiguration>(File.ReadAllText(BaseConfigurationFilename));
        }

        public void BaseSave(BaseConfiguration baseConfiguration)
        {
            File.WriteAllText(BaseConfigurationFilename, JsonConvert.SerializeObject(baseConfiguration, Formatting.Indented));
        }

        public CabinetModel CabRead()
        {
            return JsonConvert.DeserializeObject<CabinetModel>(File.ReadAllText(CabinetConfigurationFilename));
        }

        public void CabSave(CabinetModel cabConf)
        {
            File.WriteAllText(CabinetConfigurationFilename, JsonConvert.SerializeObject(cabConf, Formatting.Indented));
        }
    }
}
