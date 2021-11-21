using Config;
using CoreS;
using CoreS.Model;
using GalaSoft.MvvmLight;
using NLog;
using System.Collections.ObjectModel;
using System.IO;
using WPF3.Interface;


namespace WPF3.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private IDataExchangeViewModel _dataExchangeViewModel;
        private IConfig _config;

        public MainViewModel(IDataExchangeViewModel dataExchangeViewModel, IConfig config)
        {
            _dataExchangeViewModel = dataExchangeViewModel;

            _config = config;
            
            _myCabinet = new TempCabinet();

            _filenameList = new ObservableCollection<string>();

            //Cabinet.AddBack();
            Logger.Trace("Main ViewModel");
            if (IsInDesignMode)
            {
                Logger.Trace("InDesignMode");
                NewCabinet();

                _myLight = CreateLight();
                RaisePropertyChanged(MyLightPropertyName);

                RaisePropertyChanged(CabinetViewPropertyName);

                ReadCabinetMakerDirectory();
            }
            else
            {
                Logger.Info("! InDesignMode");
                
                NewCabinet();
                
                _myLight = CreateLight();
                RaisePropertyChanged(MyLightPropertyName);

                RaisePropertyChanged(CabinetViewPropertyName);
                                                
                ReadCabinetMakerDirectory();
            }
            
        }

        private void ReadCabinetMakerDirectory()
        {
            string targetDirectory = _config.CabinetFilesDirectory();
            Logger.Info("ReadCabinetMakerDirectory(string targetDirectory)");
            Logger.Debug("targetDirectory: {0}", targetDirectory);

            if (!Directory.Exists(targetDirectory)) return;
            _filenameList.Clear();
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                _filenameList.Add(Path.GetFileNameWithoutExtension(fileName));
            }

            RaisePropertyChanged(FilenameListPropertyName);
        }
        
        private void GenerateCabinetView()
        {
            Logger.Info("Create CabinetView in MainViewModel");

            var GeneralElement = new Elements("Elementy g³ówne");
            foreach (ElementModel item in _cabinet.CabinetElements)
            {
                GeneralElement.ElementModels.Add(item);
            }
            var VerticalElement = new Elements("Elementy pionowe");
            foreach (ElementModel element in _cabinet.GetAllVerticalBarrier())
            {
                VerticalElement.ElementModels.Add(element);
            }

            var HorizontalElement = new Elements("Elementy poziome");
            foreach (ElementModel element in _cabinet.GetAllHorizontalBarrier())
            {
                HorizontalElement.ElementModels.Add(element);
            }

            var FrontElement = new Elements("Fronty");
            foreach (ElementModel element in _cabinet.GetAllFront())
            {
                FrontElement.ElementModels.Add(element);
            }

            _cabinetView = new ObservableCollection<Elements> { GeneralElement,VerticalElement,HorizontalElement,FrontElement };

            RaisePropertyChanged(CabinetViewPropertyName);


        }
          
        private void NewCabinet()
        {
            Logger.Info("New Cabinet in MainViewModel");
            _myCabinet.Name = _config.CabinetName();
            _myCabinet.Height = _config.CabinetHeight().ToString();
            _myCabinet.Width = _config.CabinetWidth().ToString();
            _myCabinet.Depth = _config.CabinetDepth().ToString();
            _myCabinet.SizeElement = _config.CabinetSizeElement().ToString();
            _myCabinet.BackSize = _config.CabinetBack().ToString();

            Logger.Info("Create Cabinet in NewCabinet/MainViewModel");
            _cabinet = new Cabinet(int.Parse(_myCabinet.Height), int.Parse(_myCabinet.Width), int.Parse(_myCabinet.Depth),int.Parse(_myCabinet.SizeElement), int.Parse(_myCabinet.BackSize),_myCabinet.Name,_config);

            Logger.Info("Create Model3D in NewCabinet/MainViewModel");
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
            RaisePropertyChanged(MyCabinetPropertyName);
            RaisePropertyChanged(MElementPropertyName);

            ReloadMyCabinet();
        }
    }
}