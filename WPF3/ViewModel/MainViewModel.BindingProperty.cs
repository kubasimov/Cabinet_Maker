using CoreS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media.Media3D;

namespace WPF3.ViewModel
{
    public partial class MainViewModel
    {
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

        private ObservableCollection<Elements> _cabinetView;

        public ObservableCollection<Elements> CabinetView
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


        public const string MElementPropertyName = "MElement";

        private ElementModel _mElement;

        public ElementModel MElement
        {
            get
            {
                return _mElement;
            }

            set
            {
                if (_mElement == value)
                {
                    return;
                }

                _mElement = value;

                RaisePropertyChanged(MElementPropertyName);
            }
        }


        public const string HorizontalSizePropertyName = "HorizontalSize";

        private string _horizontaSize;

        public string HorizontalSize
        {
            get
            {
                return _horizontaSize;
            }

            set
            {
                if (_horizontaSize == value)
                {
                    return;
                }

                _horizontaSize = value;
                RaisePropertyChanged(HorizontalSizePropertyName);
            }
        }

        
        
        public const string FilenameListPropertyName = "FilenameList";

        private ObservableCollection<string> _filenameList;

        public ObservableCollection<string> FilenameList
        {
            get
            {
                return _filenameList;
            }

            set
            {
                if (_filenameList == value)
                {
                    return;
                }

                _filenameList = value;
                RaisePropertyChanged(FilenameListPropertyName);
            }
        }


        public class TempCabinet
        {
            public string Name { get; set; }
            public string Height { get; set; }
            public string Width { get; set; }
            public string Depth { get; set; }
            public string SizeElement { get; set; }
            public string BackSize { get; set; }

        }

        public class Elements
        {
            public string Name { get; }
            public ObservableCollection<ElementModel> ElementModels { get; }

            public Elements(string name)
            {
                Name = name;
                ElementModels = new ObservableCollection<ElementModel>();
            }
        }
    }
}
