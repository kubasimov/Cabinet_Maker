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

        public MainViewModel(IDataExchangeViewModel dataExchangeViewModel)
        {
            _dataExchangeViewModel = dataExchangeViewModel;

            _myCabinet = new TempCabinet();

            _filenameList = new List<string>();

            //Cabinet.AddBack();
            Logger.Trace("Main ViewModel");
            if (IsInDesignMode)
            {
                //Logger.Trace("InDesignMode");
                //NewCabinet();

                //_myLight = CreateLight();
                //RaisePropertyChanged(MyLightPropertyName);

                //RaisePropertyChanged(CabinetViewPropertyName);

                //ReadCabinetMakerDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cabinet_Maker"));
            }
            else
            {
                Logger.Info("! InDesignMode");
                NewCabinet();
                
                _myLight = CreateLight();
                RaisePropertyChanged(MyLightPropertyName);

                RaisePropertyChanged(CabinetViewPropertyName);

                ReadCabinetMakerDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cabinet_Maker"));
            }
            
        }

        private void ReadCabinetMakerDirectory(string targetDirectory)
        {
            Logger.Info("ReadCabinetMakerDirectory(string targetDirectory)");
            Logger.Debug("targetDirectory: {0}", targetDirectory);
            //TODO: dodanie sprawdzenia istnienia katalogu
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                _filenameList.Add(Path.GetFileNameWithoutExtension(fileName));
            }
            RaisePropertyChanged(FilenameListPropertyName);
        }

        private Model3D CreateCabinet()
        {
            Logger.Info("Create Model3D Cabinet in MainViewModel");
            GeometryModel3D myGeometryModel = new GeometryModel3D();

            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();

            Int32Collection myTriangleIndicesCollection = new Int32Collection();

            foreach (var element in _cabinet.CabinetElements)
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }
            
            foreach (var element in _cabinet.HorizontalBarrier)
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }
            
            foreach (var element in _cabinet.VerticalBarrier)
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }
            
            foreach (var element in _cabinet.FrontList)
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }

            //TODO add dodatkowe eleemnty
            
            myMeshGeometry3D.TriangleIndices = myTriangleIndicesCollection;

            myGeometryModel.Geometry = myMeshGeometry3D;

            SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Blue);

            // Define material that will use the gradient.
            DiffuseMaterial myMaterial = new DiffuseMaterial(solidColorBrush);
             
            myGeometryModel.Material = myMaterial;

            Logger.Info("Create CabinetView in Model3DCabinet/MainViewModel");
            GenerateCabinetView();
            
            return myGeometryModel;
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
                     
        private Point3DCollection CubeToPoint3DCollection(double wys, double szer, double gl, double x, double y,double z,bool hor)
        {
            if (hor)
            {
                var r = wys;
                wys = szer;
                szer = r;
            }

            return  new Point3DCollection
            {
                new Point3D(x, y, z),
                new Point3D(x + szer, y, z),
                new Point3D(x, y + wys, z),
                new Point3D(x + szer, y + wys, z),
                new Point3D(x, y, z + gl),
                new Point3D(x + szer, y, z + gl),
                new Point3D(x, y + wys, z + gl),
                new Point3D(x + szer, y + wys, z + gl)
            };
        }

        private void AddElementToModel3D(Element3D element, ref MeshGeometry3D myMeshGeometry3D,ref Int32Collection myTriangleIndicesCollection)
        {
            var items = CubeToPoint3DCollection(element.EHeight, element.EWidth, element.EDepth, element.Ex, element.Ey,element.Ez, element.Horizontal);

            foreach (Point3D point3D in items)
            {
                myMeshGeometry3D.Positions.Add(point3D);
            }
            
            var y = myMeshGeometry3D.Positions.Count - 8;

            myTriangleIndicesCollection.Add(y);
            myTriangleIndicesCollection.Add(y + 2);
            myTriangleIndicesCollection.Add(y + 1);

            myTriangleIndicesCollection.Add(y + 1);
            myTriangleIndicesCollection.Add(y + 2);
            myTriangleIndicesCollection.Add(y + 3);

            myTriangleIndicesCollection.Add(y);
            myTriangleIndicesCollection.Add(y + 4);
            myTriangleIndicesCollection.Add(y + 6);

            myTriangleIndicesCollection.Add(y);
            myTriangleIndicesCollection.Add(y + 6);
            myTriangleIndicesCollection.Add(y + 2);

            myTriangleIndicesCollection.Add(y + 1);
            myTriangleIndicesCollection.Add(y + 3);
            myTriangleIndicesCollection.Add(y + 7);

            myTriangleIndicesCollection.Add(y + 1);
            myTriangleIndicesCollection.Add(y + 7);
            myTriangleIndicesCollection.Add(y + 5);

            myTriangleIndicesCollection.Add(y + 4);
            myTriangleIndicesCollection.Add(y + 5);
            myTriangleIndicesCollection.Add(y + 6);

            myTriangleIndicesCollection.Add(y + 5);
            myTriangleIndicesCollection.Add(y + 7);
            myTriangleIndicesCollection.Add(y + 6);

            myTriangleIndicesCollection.Add(y + 7);
            myTriangleIndicesCollection.Add(y + 3);
            myTriangleIndicesCollection.Add(y + 2);

            myTriangleIndicesCollection.Add(y + 6);
            myTriangleIndicesCollection.Add(y + 7);
            myTriangleIndicesCollection.Add(y + 2);

            myTriangleIndicesCollection.Add(y);
            myTriangleIndicesCollection.Add(y + 1);
            myTriangleIndicesCollection.Add(y + 5);

            myTriangleIndicesCollection.Add(y);
            myTriangleIndicesCollection.Add(y + 5);
            myTriangleIndicesCollection.Add(y + 4);
        }
        
        private Model3D CreateLight()
        {
            Logger.Info("Create Light in MainViewModel");
            return new DirectionalLight
            {
                Color = Colors.White, Direction = new Vector3D(-5, -5, -5)
            };
        }

        private Element3D GetElement3DFromElementModel(ElementModel element)
        {
            return new Element3D
            {
                EWidth = (double)element.Width / 100,
                EHeight = (double)element.Height / 100,
                EDepth = (double)element.Depth / 100,
                EName = element.GetEnumName(),
                Ex = (double)element.X / 100,
                Ey = (double)element.Y / 100,
                Ez = (double)element.Z / 100,
                Description = element.Description,
                Horizontal=element.Horizontal
            };
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

        private void NewCabinet()
        {
            Logger.Info("New Cabinet in MainViewModel");
            _myCabinet.Name = "Default";
            _myCabinet.Height = 720.ToString();
            _myCabinet.Width = 600.ToString();
            _myCabinet.Depth = 510.ToString();
            _myCabinet.SizeElement = 18.ToString();
            _myCabinet.BackSize = 3.ToString();

            Logger.Info("Create Cabinet in NewCabinet/MainViewModel");
            _cabinet = new Cabinet().Height(int.Parse(_myCabinet.Height)).Width(int.Parse(_myCabinet.Width)).Depth(int.Parse(_myCabinet.Depth)).Name(_myCabinet.Name);

            Logger.Info("Create Model3D in NewCabinet/MainViewModel");
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }
    }
}