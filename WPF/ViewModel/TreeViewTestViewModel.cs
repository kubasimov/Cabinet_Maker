using System;
using System.Collections.ObjectModel;
using System.Linq;
using Core;
using Core.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WPF.ViewModel
{
    public class TreeViewTestViewModel : ViewModelBase
    {
        private Cabinet cabinet;

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
            cabinet = new Cabinet();
            
            cabinet.AddVerticalBarrier(1);
            cabinet.AddHorizontalBarrier(1);

            var z = new Elements("Podstawa");
            foreach (ElementModel element in cabinet.CabinetElements)
            {
                z.elementModels.Add(element);
            }

            var z1 = new Elements("Vertical Barrier");
            foreach (ElementModel element in cabinet.GetAllVerticalBarrier())
            {
                z1.elementModels.Add(element);
            }

            var z2 = new Elements("Horizontal Barrier");
            foreach (ElementModel element in cabinet.GetAllHorizontalBarrier())
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
            
            if (parameter.GetType()==typeof(ElementModel))
            {
                var tmp = (ElementModel)parameter;

                foreach (var elements in CabinetView)
                {
                    if (elements.elementModels.Any(p => p.Guid == tmp.Guid))
                    {
                        MElement = elements.elementModels.First(p => p.Guid == tmp.Guid);
                        RaisePropertyChanged(MElementPropertyName);
                        break;
                    }
                }
            }
        }


        public class Elements
        {
            public string Name { get;}
            public ObservableCollection<ElementModel> elementModels { get; }

            public Elements(string name)
            {
                Name = name;
                elementModels = new ObservableCollection<ElementModel>();
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

        private ElementModel _element  ;

        public ElementModel MElement
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
