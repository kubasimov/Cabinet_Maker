using System.Collections;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Core;
using NLog;

namespace WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        Cabinet Cabinet = new Cabinet();
        

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MainViewModel()
        {

            Cabinet.AddBack();

            if (IsInDesignMode)
            {

                //_model3D = CreateContent2();
                _model3D = CreateCabinet();
                RaisePropertyChanged(MyModel3DPropertyName);
                _myLight = CreateLight();
                RaisePropertyChanged(MyLightPropertyName);

            }
            else
            {
                //_model3D = CreateContent2();
                _model3D = CreateCabinet();
                RaisePropertyChanged(MyModel3DPropertyName);
                _myLight = CreateLight();
                RaisePropertyChanged(MyLightPropertyName);

            }
        }


        private Model3D CreateCabinet()
        {
            GeometryModel3D myGeometryModel = new GeometryModel3D();

            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();

            Int32Collection myTriangleIndicesCollection = new Int32Collection();

            for (int i = 0; i < Cabinet.CabinetElements.Count; i++)
            {
                var element = Cabinet.CabinetElements[i];

                Element3D element1 = new Element3D
                {
                    EWidth = (double)element.EWidth / 100,
                    EHeight = (double)element.EHeight / 100,
                    EDepth = (double)element.EDepth / 100,
                    EName = element.EName,
                    Ex = (double)element.Ex / 100,
                    Ey = (double)element.Ey / 100,
                    Ez = (double)element.Ez / 100,
                    Description = element.Description
                };

                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);

            }

            Cabinet.AddHorizontalBarrier(2);

            for (int i = 0; i < Cabinet.HorizontalBarrier.Count; i++)
            {
                var element = Cabinet.HorizontalBarrier[i];

                Element3D element1 = new Element3D
                {
                    EWidth = (double)element.EWidth / 100,
                    EHeight = (double)element.EHeight / 100,
                    EDepth = (double)element.EDepth / 100,
                    EName = element.EName,
                    Ex = (double)element.Ex / 100,
                    Ey = (double)element.Ey / 100,
                    Ez = (double)element.Ez / 100,
                    Description = element.Description
                };

                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);

            }

            Cabinet.AddVerticalBarrier(2);

            for (int i = 0; i < Cabinet.VerticalBarrier.Count; i++)
            {
                var element = Cabinet.VerticalBarrier[i];

                Element3D element1 = new Element3D
                {
                    EWidth = (double)element.EWidth / 100,
                    EHeight = (double)element.EHeight / 100,
                    EDepth = (double)element.EDepth / 100,
                    EName = element.EName,
                    Ex = (double)element.Ex / 100,
                    Ey = (double)element.Ey / 100,
                    Ez = (double)element.Ez / 100,
                    Description = element.Description
                };

                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);

            }

            Cabinet.AddFront(2);

            for (int i = 0; i < Cabinet.GetFrontList().Count; i++)
            {
                var element = Cabinet.GetFront(i);

                Element3D element1 = new Element3D
                {
                    EWidth = (double)element.EWidth / 100,
                    EHeight = (double)element.EHeight / 100,
                    EDepth = (double)element.EDepth / 100,
                    EName = element.EName,
                    Ex = (double)element.Ex / 100,
                    Ey = (double)element.Ey / 100,
                    Ez = (double)element.Ez / 100,
                    Description = element.Description
                };

                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }


            //TODO add horizontal and vertical barrier
            //TODO add front
            //TODO add dodatkowe eleemnty


            myMeshGeometry3D.TriangleIndices = myTriangleIndicesCollection;

            myGeometryModel.Geometry = myMeshGeometry3D;

            SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Blue);

            // Define material that will use the gradient.
            DiffuseMaterial myMaterial = new DiffuseMaterial(solidColorBrush);
            //myMaterial.Color = Colors.Blue;
            myGeometryModel.Material = myMaterial;

            //foreach (Point3D point3D in myMeshGeometry3D.Positions)
            //{
            //    Logger.Log(LogLevel.Trace, "Point3D x=" + point3D.X + ", y=" + point3D.Y + ", z=" + point3D.Z);
            //}

            //foreach (int index in myMeshGeometry3D.TriangleIndices)
            //{
            //    Logger.Log(LogLevel.Debug, "Triangle   " + index);
            //}



            return myGeometryModel;
        }



        private Point3DCollection cubeToPoint3DCollection(double wys, double szer, double gl, double x, double y, double z)
        {
            Point3DCollection myPositionCollection = new Point3DCollection();
            myPositionCollection.Add(new Point3D(x, y, z));

            myPositionCollection.Add(new Point3D(x + szer, y, z));

            myPositionCollection.Add(new Point3D(x, y + wys, z));

            myPositionCollection.Add(new Point3D(x + szer, y + wys, z));

            myPositionCollection.Add(new Point3D(x, y, z+gl));

            myPositionCollection.Add(new Point3D(x + szer, y, z + gl));

            myPositionCollection.Add(new Point3D(x, y + wys, z + gl));

            myPositionCollection.Add(new Point3D(x + szer, y + wys, z + gl));

            return myPositionCollection;
        }

        private void AddElementToModel3D(Element3D element, ref MeshGeometry3D myMeshGeometry3D, ref Int32Collection myTriangleIndicesCollection)
        {
            var items = cubeToPoint3DCollection(element.EHeight, element.EWidth, element.EDepth, element.Ex, element.Ey,
                element.Ez);

            foreach (Point3D point3D in items)
            {
                myMeshGeometry3D.Positions.Add(point3D);
                //Debug.WriteLine(point3D);
            }
            //Debug.WriteLine("");
            var y = myMeshGeometry3D.Positions.Count-8;

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












        private Model3D CreateContent2()

        {

            GeometryModel3D myGeometryModel = new GeometryModel3D();

            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();

           

            var modeList = new ArrayList();

            modeList.Add(new Model(7, 0.18, 5, 0, 0, 0));
            //modeList.Add(new Model(7, 0.18, 5, 5.82, 0, 0));
            //modeList.Add(new Model(0.18, 5.64, 5, 0.18, 0, 0));
            //modeList.Add(new Model(0.18, 5.64, 5, 0.18, 6.82, 0));

            Int32Collection myTriangleIndicesCollection = new Int32Collection();

            for (int i = 0,y=0; i < modeList.Count; i++,y+=8)
            {
                var ll = (Model) modeList[i];

                var z=cubeToPoint3DCollection(ll.Wys, ll.Szer, ll.Gl, ll.X, ll.Y, ll.Z);
                foreach (Point3D point3D in z)
                {
                    myMeshGeometry3D.Positions.Add(point3D);
                    Debug.WriteLine(point3D);
                }
                Debug.WriteLine("");



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


            myMeshGeometry3D.TriangleIndices = myTriangleIndicesCollection;

            myGeometryModel.Geometry = myMeshGeometry3D;
            
            SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.LightGray);

            // Define material that will use the gradient.
            DiffuseMaterial myMaterial = new DiffuseMaterial(solidColorBrush);
            //myMaterial.Color = Colors.Blue;
            myGeometryModel.Material = myMaterial;

            foreach (Point3D point3D in myMeshGeometry3D.Positions)
            {
                Logger.Log(LogLevel.Trace, "Point3D x=" + point3D.X + ", y=" + point3D.Y + ", z=" + point3D.Z);
            }

            foreach (int index in myMeshGeometry3D.TriangleIndices)
            {
                Logger.Log(LogLevel.Debug, "Triangle   " + index);
            }


            return myGeometryModel;

        }



        private Model3D CreateLight()
        {
            DirectionalLight myDirectionalLight = new DirectionalLight();

            myDirectionalLight.Color = Colors.White;

            myDirectionalLight.Direction = new Vector3D(-5, -5, -5);

            return myDirectionalLight;
        }

    #region BindingProperty


        public const string MyModel3DPropertyName = "MyModel3D";

        private Model3D _model3D;

        public Model3D MyModel3D
        {
            get
            {
                return _model3D;
            }

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
            get
            {
                return _myLight;
            }

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
    #endregion
    }
}