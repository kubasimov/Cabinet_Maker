using System.Collections;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Core;
using NLog;
using GalaSoft.MvvmLight.Command;
using WPF.Enum;
using WPF.Interface;
using Core.Model;
using WPF.View;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        Cabinet Cabinet;
        
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private IDataExchangeViewModel _dataExchangeViewModel;

        public MainViewModel(IDataExchangeViewModel dataExchangeViewModel)
        {
            _dataExchangeViewModel = dataExchangeViewModel;

            _myCabinet = new TempCabinet();

            //Cabinet.AddBack();

            if (IsInDesignMode)
            {
                NewCabinet();

                _myLight = CreateLight();
                RaisePropertyChanged(MyLightPropertyName);

            }
            else
            {
                NewCabinet();
                
                _myLight = CreateLight();
                RaisePropertyChanged(MyLightPropertyName);

            }
            
        }
        
        private Model3D CreateCabinet()
        {
            GeometryModel3D myGeometryModel = new GeometryModel3D();

            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();

            Int32Collection myTriangleIndicesCollection = new Int32Collection();

            foreach (var element in Cabinet.CabinetElements)
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }
            
            //Cabinet.AddHorizontalBarrier(2);
            foreach (var element in Cabinet.HorizontalBarrier)
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }
            
            //Cabinet.AddVerticalBarrier(2);
            foreach (var element in Cabinet.VerticalBarrier)
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }
            
            //Cabinet.AddFront(2);
            foreach (var element in Cabinet.GetFrontList())
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

        private Point3DCollection cubeToPoint3DCollection(double wys, double szer, double gl, double x, double y,double z)
        {
            Point3DCollection myPositionCollection = new Point3DCollection();
            myPositionCollection.Add(new Point3D(x, y, z));

            myPositionCollection.Add(new Point3D(x + szer, y, z));

            myPositionCollection.Add(new Point3D(x, y + wys, z));

            myPositionCollection.Add(new Point3D(x + szer, y + wys, z));

            myPositionCollection.Add(new Point3D(x, y, z + gl));

            myPositionCollection.Add(new Point3D(x + szer, y, z + gl));

            myPositionCollection.Add(new Point3D(x, y + wys, z + gl));

            myPositionCollection.Add(new Point3D(x + szer, y + wys, z + gl));

            return myPositionCollection;
        }

        private void AddElementToModel3D(Element3D element, ref MeshGeometry3D myMeshGeometry3D,ref Int32Collection myTriangleIndicesCollection)
        {
            var items = cubeToPoint3DCollection(element.EHeight, element.EWidth, element.EDepth, element.Ex, element.Ey,element.Ez);

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
            DirectionalLight myDirectionalLight = new DirectionalLight();

            myDirectionalLight.Color = Colors.White;

            myDirectionalLight.Direction = new Vector3D(-5, -5, -5);

            return myDirectionalLight;
        }

        private Element3D GetElement3DFromElementModel(ElementModel element)
        {
            return new Element3D
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

        #endregion

        #region DeclareRelayCommand

        private RelayCommand _myDrawCabinetCommand;

        public RelayCommand DrawCabinetCommand => _myDrawCabinetCommand
                                                  ?? (_myDrawCabinetCommand =
                                                      new RelayCommand(DrawCabinetRelayCommand));

        private RelayCommand _myNewVerticalBarrierCommand;

        public RelayCommand NewVerticalBarrierCommand => _myNewVerticalBarrierCommand
                                                         ?? (_myNewVerticalBarrierCommand =
                                                             new RelayCommand(NewVerticalBarrierRelayCommand));

        private RelayCommand _myAddVerticalBarrierCommand;

        public RelayCommand AddVerticalBarrierCommand => _myAddVerticalBarrierCommand
                                                           ?? (_myAddVerticalBarrierCommand =
                                                               new RelayCommand(AddVerticalBarrierRelayCommand));

        private RelayCommand _myNewHorizontalBarrierCommand;

        public RelayCommand NewHorizontalBarrierCommand => _myNewHorizontalBarrierCommand
                                                           ?? (_myNewHorizontalBarrierCommand =
                                                               new RelayCommand(NewHorizontalBarrierRelayCommand));

        private RelayCommand _myAddHorizontalBarrierCommand;

        public RelayCommand AddHorizontalBarrierCommand => _myAddHorizontalBarrierCommand
                                                           ?? (_myAddHorizontalBarrierCommand =
                                                               new RelayCommand(AddHorizontalBarrierRelayCommand));

        private RelayCommand _myAddFrontCommand;

        public RelayCommand AddFrontCommand => _myAddFrontCommand
                                                           ?? (_myAddFrontCommand =
                                                               new RelayCommand(AddFrontRelayCommand));

        private RelayCommand _myNewCommand;

        public RelayCommand NewCommand => _myNewCommand
                                           ?? (_myNewCommand =
                                               new RelayCommand(NewRelayCommand));

        private RelayCommand _mySaveCommand;

        public RelayCommand SaveCommand => _mySaveCommand
                                                           ?? (_mySaveCommand =
                                                               new RelayCommand(SaveRelayCommand));

        private RelayCommand _myEndCommand;

        public RelayCommand EndCommand => _myEndCommand
                                            ?? (_myEndCommand =
                                                new RelayCommand(EndRelayCommand));

        private RelayCommand _myExportCommand;

        public RelayCommand ExportCommand => _myExportCommand
                    ?? (_myExportCommand =
                        new RelayCommand(ExportRelayCommand));
        #endregion

        #region RelayCommand

        private void DrawCabinetRelayCommand()
        {
            Cabinet.Height = int.Parse(_myCabinet.height);
            Cabinet.Width = int.Parse(_myCabinet.width);
            Cabinet.Depth = int.Parse(_myCabinet.depth);
            Cabinet.SizeElement = int.Parse(_myCabinet.sizeElement);
            Cabinet.BackSize = int.Parse(_myCabinet.BackSize);
            Cabinet.Name = _myCabinet.Name;

            Cabinet.Redraw();

            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        private void NewVerticalBarrierRelayCommand()
        {
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.HorizontalBarrier, Cabinet.HorizontalBarrier.Count);
            new VerticalBarrierWindow().ShowDialog();

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.verticalBarrierWindow))
            {
                var t = (BarrierParameter) _dataExchangeViewModel.Item(EnumExchangeViewmodel.verticalBarrierWindow);

                Cabinet.NewVerticalBarrier(t);
                _model3D = CreateCabinet();
                RaisePropertyChanged(MyModel3DPropertyName);
            }
        }

        private void AddVerticalBarrierRelayCommand()
        {
            Cabinet.AddVerticalBarrier(1);
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        private void NewHorizontalBarrierRelayCommand()
        {
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.VerticalBarrier, Cabinet.VerticalBarrier.Count);
            new HorizontalBarrierWindow().ShowDialog();

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.HorizontalBarrierWindow))
            {
                var t = (BarrierParameter)_dataExchangeViewModel.Item(EnumExchangeViewmodel.HorizontalBarrierWindow);

                Cabinet.NewHorizontalBarrier(t);
                _model3D = CreateCabinet();
                RaisePropertyChanged(MyModel3DPropertyName);
            }
        }

        private void AddHorizontalBarrierRelayCommand()
        {

        }

        private void AddFrontRelayCommand()
        {
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.Front, new FrontParameter());
            new FrontWindow().ShowDialog();

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.FrontWindow))
            {
                var frontParameter = (FrontParameter) _dataExchangeViewModel.Item(EnumExchangeViewmodel.FrontWindow);
                

                Cabinet.AddFront(frontParameter);
                _model3D = CreateCabinet();
                RaisePropertyChanged(MyModel3DPropertyName);
            }
        }

        private void NewRelayCommand()
        {
            NewCabinet();
            RaisePropertyChanged(MyCabinetPropertyName);
        }

        private void SaveRelayCommand()
        {
            //var t = (System.Windows.Controls.Viewport3D)ob;

            //RenderTargetBitmap bitmap = new RenderTargetBitmap((int)t.ActualWidth, (int)t.ActualHeight, 100, 100, PixelFormats.Pbgra32);
            //bitmap.Render(t);

            //// create a png bitmap encoder to save the png file
            //BitmapEncoder encoder = new PngBitmapEncoder();
            //encoder.Frames.Clear();

            //// create frame from the writable bitmap and add to encoder
            //encoder.Frames.Add(BitmapFrame.Create(bitmap));

            //// optionally write .png to the disk
            //using (FileStream fs = new FileStream(@"d:\aaa.png", FileMode.Create))
            //{
            //    encoder.Save(fs);
            //}

            if (_myCabinet.Name == ""||_myCabinet.Name==null)
            {
                MessageBox.Show("Nazwa jest wymagana", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                
            }
            else
            {
                Cabinet.Serialize();
                MessageBox.Show("Zapisano", "Zapis", MessageBoxButton.OK);
            }

            
        }

        private void EndRelayCommand()
        {
            Messenger.Default.Send(new NotificationMessage(this, "CloseMain"));
        }

        private void ExportRelayCommand()
        {
            var export = new Core.Export.ExcelExport();
            export.Export(Cabinet);
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

            for (int i = 0, y = 0; i < modeList.Count; i++, y += 8)
            {
                var ll = (Model)modeList[i];

                var z = cubeToPoint3DCollection(ll.Wys, ll.Szer, ll.Gl, ll.X, ll.Y, ll.Z);
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

        private void NewCabinet()
        {
            _myCabinet.Name = "";
            _myCabinet.height = 720.ToString();
            _myCabinet.width = 600.ToString();
            _myCabinet.depth = 500.ToString();
            _myCabinet.sizeElement = 18.ToString();
            _myCabinet.BackSize = 3.ToString();


            Cabinet = new Cabinet(
                int.Parse(_myCabinet.height),
                int.Parse(_myCabinet.width),
                int.Parse(_myCabinet.depth),
                name: _myCabinet.Name);

            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }
    }
}