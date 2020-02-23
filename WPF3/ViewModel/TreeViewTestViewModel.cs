using CoreS;
using CoreS.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;

namespace WPF3.ViewModel
{
    public class TreeViewTestViewModel : ViewModelBase
    {
        private Cabinet _cabinet;

        public TreeViewTestViewModel()
        {
           
            if (IsInDesignMode)
            {
               
                NewMethod2();
            }
            else
            {
                
                NewMethod2();
            }
        }

        private void NewMethod2()
        {
            _cabinet = new Cabinet();
            
            _cabinet.AddVerticalBarrier(1);
            _cabinet.AddHorizontalBarrier(1);

            var z = new Elements("Podstawa");
            foreach (ElementModelDTO element in _cabinet.CabinetElements)
            {
                z.elementModels.Add(element);
            }

            var z1 = new Elements("Vertical Barrier");
            foreach (ElementModelDTO element in _cabinet.GetAllVerticalBarrier())
            {
                z1.elementModels.Add(element);
            }

            var z2 = new Elements("Horizontal Barrier");
            foreach (ElementModelDTO element in _cabinet.GetAllHorizontalBarrier())
            {
                z2.elementModels.Add(element);
            }
            _cabinetView = new ObservableCollection<Elements> {z, z1,z2};
            RaisePropertyChanged(CabinetViewPropertyName);

        }

        

        private RelayCommand<object> _myCommand;

        public RelayCommand<object> MyCommand
        {
            get
            {
                return _myCommand
                    ?? (_myCommand = new RelayCommand<object>(ExecuteMyCommand));
            }
        }

        private void ExecuteMyCommand(object parameter)
        {
            
            if (parameter.GetType()==typeof(ElementModelDTO))
            {
                var tmp = (ElementModelDTO)parameter;

                foreach (var elements in CabinetView)
                {
                    if (elements.elementModels.Any(p => p.GetGuid() == tmp.GetGuid()))
                    {
                        MElement = elements.elementModels.First(p => p.GetGuid() == tmp.GetGuid());
                        RaisePropertyChanged(MElementPropertyName);
                        break;
                    }
                }
            }
        }


        public class Elements
        {
            public string Name { get;}
            public ObservableCollection<ElementModelDTO> elementModels { get; }

            public Elements(string name)
            {
                Name = name;
                elementModels = new ObservableCollection<ElementModelDTO>();
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

        private ElementModelDTO _element  ;

        public ElementModelDTO MElement
        {
            get
            {
                return _element;
            }

            set
            {
                if (_element == value)
                {
                    return;
                }

                _element = value;
                RaisePropertyChanged(MElementPropertyName);
            }
        }

        
        
    }
}
