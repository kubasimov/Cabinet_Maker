using CoreS;
using CoreS.Model;
using GalaSoft.MvvmLight;
using NLog;
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

            //Cabinet.AddBack();
            Logger.Info("Start");
            if (IsInDesignMode)
            {
                NewCabinet();

                _myLight = CreateLight();
                RaisePropertyChanged(MyLightPropertyName);

                _cabinetView = _cabinet;
                RaisePropertyChanged(CabinetViewPropertyName);
            }
            else
            {
                NewCabinet();
                
                _myLight = CreateLight();
                RaisePropertyChanged(MyLightPropertyName);

                _cabinetView = _cabinet;
                RaisePropertyChanged(CabinetViewPropertyName);
            }
            
        }
        
        private Model3D CreateCabinet()
        {
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
            
            foreach (var element in _cabinet.GetFrontList())
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }

            //TODO add dodatkowe eleemnty
            
            myMeshGeometry3D.TriangleIndices = myTriangleIndicesCollection;

            myGeometryModel.Geometry = myMeshGeometry3D;

            SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Gray);

            // Define material that will use the gradient.
            DiffuseMaterial myMaterial = new DiffuseMaterial(solidColorBrush);
            //myMaterial.Color = Colors.Blue;
            myGeometryModel.Material = myMaterial;
            
            return myGeometryModel;
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
            return new DirectionalLight
            {
                Color = Colors.White, Direction = new Vector3D(-5, -5, -5)
            };
        }

        private Element3D GetElement3DFromElementModel(ElementModelDTO element)
        {
            return new Element3D
            {
                EWidth = (double)element.GetWidth() / 100,
                EHeight = (double)element.GetHeight() / 100,
                EDepth = (double)element.GetDepth() / 100,
                EName = element.GetEnumName(),
                Ex = (double)element.GetX() / 100,
                Ey = (double)element.GetY() / 100,
                Ez = (double)element.GetZ() / 100,
                Description = element.GetDescription(),
                Horizontal=element.GetHorizontal()
            };
        }
        
        #region BindingProperty


        public const string MyModel3DPropertyName = "MyModel3D";

        private Model3D _model3D;

        public Model3D MyModel3D
        {
            get { return _model3D; }

            set
            {
                if (_model3D == value)
                {
                    return;
                }

                _model3D = value;
                RaisePropertyChanged(MyModel3DPropertyName);
            }
        }



        public const string MyLightPropertyName = "MyLight";

        private Model3D _myLight;

        public Model3D MyLight
        {
            get { return _myLight; }

            set
            {
                if (_myLight == value)
                {
                    return;
                }

                _myLight = value;
                RaisePropertyChanged(MyLightPropertyName);
            }
        }


        public const string MyCabinetPropertyName = "MyCabinet";

        private TempCabinet _myCabinet;

        public TempCabinet MyCabinet
        {
            get { return _myCabinet; }

            set
            {
                if (_myCabinet == value)
                {
                    return;
                }

                _myCabinet = value;
                RaisePropertyChanged(MyCabinetPropertyName);
            }
        }

        public const string CabinetViewPropertyName = "CabinetView";

        private Cabinet _cabinetView;

        public Cabinet CabinetView
        {
            get
            {
                return _cabinetView;
            }

            set
            {
                if (_cabinetView == value)
                {
                    return;
                }

                _cabinetView = value;
                RaisePropertyChanged(CabinetViewPropertyName);
            }
        }
        #endregion

        public class TempCabinet
        {
            public string Name { get; set; }
            public string height { get; set; }
            public string width { get; set; }
            public string depth { get; set; }
            public string sizeElement { get; set; }
            public string BackSize { get; set; }

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
            _myCabinet.Name = "Default";
            _myCabinet.height = 720.ToString();
            _myCabinet.width = 600.ToString();
            _myCabinet.depth = 500.ToString();
            _myCabinet.sizeElement = 18.ToString();
            _myCabinet.BackSize = 3.ToString();


            _cabinet = new Cabinet().Height(int.Parse(_myCabinet.height)).Width(int.Parse(_myCabinet.width)).Depth(int.Parse(_myCabinet.depth)).Name(_myCabinet.Name);

            
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }
    }
}