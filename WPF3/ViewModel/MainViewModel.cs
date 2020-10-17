using Config;
using CoreS;
using CoreS.Model;
using GalaSoft.MvvmLight;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Media3D;
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


//private Model3D CreateContent2()
//{

//    GeometryModel3D myGeometryModel = new GeometryModel3D();

//    MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();



//    var modeList = new ArrayList();

//    modeList.Add(new Model(7, 0.18, 5, 0, 0, 0));
//    //modeList.Add(new Model(7, 0.18, 5, 5.82, 0, 0));
//    //modeList.Add(new Model(0.18, 5.64, 5, 0.18, 0, 0));
//    //modeList.Add(new Model(0.18, 5.64, 5, 0.18, 6.82, 0));

//    Int32Collection myTriangleIndicesCollection = new Int32Collection();

//    for (int i = 0, y = 0; i < modeList.Count; i++, y += 8)
//    {
//        var ll = (Model)modeList[i];

//        var z = CubeToPoint3DCollection(ll.Wys, ll.Szer, ll.Gl, ll.X, ll.Y, ll.Z,false);
//        foreach (Point3D point3D in z)
//        {
//            myMeshGeometry3D.Positions.Add(point3D);
//            Debug.WriteLine(point3D);
//        }

//        Debug.WriteLine("");



//        myTriangleIndicesCollection.Add(y);
//        myTriangleIndicesCollection.Add(y + 2);
//        myTriangleIndicesCollection.Add(y + 1);

//        myTriangleIndicesCollection.Add(y + 1);
//        myTriangleIndicesCollection.Add(y + 2);
//        myTriangleIndicesCollection.Add(y + 3);

//        myTriangleIndicesCollection.Add(y);
//        myTriangleIndicesCollection.Add(y + 4);
//        myTriangleIndicesCollection.Add(y + 6);

//        myTriangleIndicesCollection.Add(y);
//        myTriangleIndicesCollection.Add(y + 6);
//        myTriangleIndicesCollection.Add(y + 2);

//        myTriangleIndicesCollection.Add(y + 1);
//        myTriangleIndicesCollection.Add(y + 3);
//        myTriangleIndicesCollection.Add(y + 7);

//        myTriangleIndicesCollection.Add(y + 1);
//        myTriangleIndicesCollection.Add(y + 7);
//        myTriangleIndicesCollection.Add(y + 5);

//        myTriangleIndicesCollection.Add(y + 4);
//        myTriangleIndicesCollection.Add(y + 5);
//        myTriangleIndicesCollection.Add(y + 6);

//        myTriangleIndicesCollection.Add(y + 5);
//        myTriangleIndicesCollection.Add(y + 7);
//        myTriangleIndicesCollection.Add(y + 6);

//        myTriangleIndicesCollection.Add(y + 7);
//        myTriangleIndicesCollection.Add(y + 3);
//        myTriangleIndicesCollection.Add(y + 2);

//        myTriangleIndicesCollection.Add(y + 6);
//        myTriangleIndicesCollection.Add(y + 7);
//        myTriangleIndicesCollection.Add(y + 2);

//        myTriangleIndicesCollection.Add(y);
//        myTriangleIndicesCollection.Add(y + 1);
//        myTriangleIndicesCollection.Add(y + 5);

//        myTriangleIndicesCollection.Add(y);
//        myTriangleIndicesCollection.Add(y + 5);
//        myTriangleIndicesCollection.Add(y + 4);




//    }


//    myMeshGeometry3D.TriangleIndices = myTriangleIndicesCollection;

//    myGeometryModel.Geometry = myMeshGeometry3D;

//    SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.LightGray);

//    // Define material that will use the gradient.
//    DiffuseMaterial myMaterial = new DiffuseMaterial(solidColorBrush);
//    //myMaterial.Color = Colors.Blue;
//    myGeometryModel.Material = myMaterial;

//    foreach (Point3D point3D in myMeshGeometry3D.Positions)
//    {
//        Logger.Log(LogLevel.Trace, "Point3D x=" + point3D.X + ", y=" + point3D.Y + ", z=" + point3D.Z);
//    }

//    foreach (int index in myMeshGeometry3D.TriangleIndices)
//    {
//        Logger.Log(LogLevel.Debug, "Triangle   " + index);
//    }


//    return myGeometryModel;

//}