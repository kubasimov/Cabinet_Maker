using Config.Model;
using Config.Sync;
using System;
using System.IO;

namespace Config
{
    public class Config : IConfig
    {
        readonly ISynchrontization synchrontization = new Synchronization();
        readonly BaseConfiguration _baseConfiguration;
        readonly CabinetModel _cabinetModel;
        readonly string _appName = "CabinetMaker";

        public Config()
        {
            FirstRun();
            _baseConfiguration = synchrontization.BaseRead();
            _cabinetModel = synchrontization.CabRead();
        }

        public string CabinetFilesDirectory()=>_baseConfiguration.cabinetFilesDirectory;
        
        public string CabinetName()=>_cabinetModel.name;

        public int CabinetHeight() => _cabinetModel.height;
        
        public int CabinetWidth() => _cabinetModel.width;

        public int CabinetDepth() => _cabinetModel.depth;

        public int CabinetSizeElement() => _cabinetModel.sizeElement;
        
        public int CabinetBack()=>_cabinetModel.back;

        public int SlotsTop() => _cabinetModel.Slots.Top;

        public int SlotsBottom() => _cabinetModel.Slots.Bottom;

        public int SlotsLeft() => _cabinetModel.Slots.Left;

        public int SlotsRight() => _cabinetModel.Slots.Right;

        public int SlotsBetweenVertically() => _cabinetModel.Slots.BetweenVertically;

        public int SlotsBetweenHorizontally() => _cabinetModel.Slots.BetweenHorizontally;

        public int SlotsBetweenCabinet() => _cabinetModel.Slots.BetweenCabinet;
        
        
        public void FirstRun()
        {
            string ApplicationDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _appName);
            string BaseConfigurationFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _appName, "BaseConfig.json");
            string CabinetConfigurationFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _appName, "CabinetConfig.json");
            string CabinetFilesDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _appName);
            string ExcelExportDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _appName);

            if (!Directory.Exists(ApplicationDataDirectory))
                Directory.CreateDirectory(ApplicationDataDirectory);

            if (!Directory.Exists(CabinetFilesDirectory))
                Directory.CreateDirectory(CabinetFilesDirectory);

            if (!Directory.Exists(ExcelExportDirectory))
                Directory.CreateDirectory(ExcelExportDirectory);

            if(!File.Exists(BaseConfigurationFilename))
            {
                var baseConf = new BaseConfiguration
                {
                    cabinetFilesDirectory = CabinetFilesDirectory,
                    excelExportDirectory = ExcelExportDirectory
                };
                synchrontization.BaseSave(baseConf);
            }

            if(!File.Exists(CabinetConfigurationFilename))
            {
                var cabConf = new CabinetModel
                {
                    name = "Default",
                    height = 720,
                    width = 600,
                    depth = 510,
                    sizeElement = 18,
                    back = 3,
                    enumFront=9,
                    Slots = new Slots
                    {
                        Left = 3,
                        Right = 3,
                        Bottom = 3,
                        Top = 3,
                        BetweenCabinet = 2,
                        BetweenHorizontally = 3,
                        BetweenVertically = 3
                    }
                };

                synchrontization.CabSave(cabConf);
            }
        }

        public string GetExcelExportDirectory() => _baseConfiguration.excelExportDirectory;
    }
}
