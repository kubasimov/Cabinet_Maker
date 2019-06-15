using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WPF.Enum;
using WPF.Interface;

namespace WPF.ViewModel
{
    public class HorizontalBarrierViewModel:ViewModelBase
    {
        private IDataExchangeViewModel _dataExchangeViewModel;
        private int _verticalBarrierCount;

        public HorizontalBarrierViewModel(IDataExchangeViewModel dataExchangeViewModel)
        {
            _dataExchangeViewModel = dataExchangeViewModel;

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.VerticalBarrier))
            {
                _verticalBarrierCount = (int)_dataExchangeViewModel.Item(EnumExchangeViewmodel.VerticalBarrier);
            }

            VerticalBarrier=new ObservableCollection<MyModel>();

            if (_verticalBarrierCount > 0)
            {
                for (var i = 0; i <= _verticalBarrierCount; i++)
                {
                    var myModel = new MyModel{txt=i.ToString(),list =  new List<int>{i}};
                    VerticalBarrier.Add(myModel);
                }
            }

            if (IsInDesignMode)
            {
                _barrierNumber = 2.ToString();
                _backShelf = 0.ToString();
                _shelf = true;
                var myModel = new MyModel {txt = "1", list = new List<int> {1}};
                var myModel1 = new MyModel {txt = "1,2", list = new List<int> {1, 2}};
                _verticalBarrier.Add(myModel);
                _verticalBarrier.Add(myModel1);
            }
            else
            {
                _barrierNumber = 0.ToString();
                _backShelf = 0.ToString();
                _shelf = false;
                }

        }


        private void ExecuteOkCommand()
        {
            var barierParametter = new BarrierParameter();

            barierParametter.Number = int.Parse(_barrierNumber);
            barierParametter.Back = int.Parse(_backShelf);
            
            //barierParametter.Barrier=

            _dataExchangeViewModel.Add(EnumExchangeViewmodel.HorizontalBarrierWindow, barierParametter);
            Messenger.Default.Send(new NotificationMessage(this, "CloseHorizontalBarrier"));
        }


        private void ExecuteAbortRelayCommand()
        {
            Messenger.Default.Send(new NotificationMessage(this, "CloseHorizontalBarrier"));

        }

        #region BindingProperty

        public const string BarrierNumberPropertyName = "BarrierNumber";

        private string _barrierNumber;

        public string BarrierNumber
        {
            get
            {
                return _barrierNumber;
            }

            set
            {
                if (_barrierNumber == value)
                {
                    return;
                }

                _barrierNumber = value;
                RaisePropertyChanged(BarrierNumberPropertyName);
            }
        }


        public const string ShelfPropertyName = "Shelf";

        private bool _shelf = false;

        public bool Shelf
        {
            get
            {
                return _shelf;
            }

            set
            {
                if (_shelf == value)
                {
                    return;
                }

                _shelf = value;
                RaisePropertyChanged(ShelfPropertyName);
            }
        }


        public const string BackShelfPropertyName = "BackShelf";

        private string _backShelf;

        public string BackShelf
        {
            get
            {
                return _backShelf;
            }

            set
            {
                if (_backShelf == value)
                {
                    return;
                }

                _backShelf = value;
                RaisePropertyChanged(BackShelfPropertyName);
            }
        }

        
        public const string VerticalBarrierPropertyName = "VerticalBarrier";

        private ObservableCollection<MyModel> _verticalBarrier;

        public ObservableCollection<MyModel> VerticalBarrier
        {
            get
            {
                return _verticalBarrier;
            }

            set
            {
                if (_verticalBarrier == value)
                {
                    return;
                }

                _verticalBarrier = value;
                RaisePropertyChanged(VerticalBarrierPropertyName);
            }
        }

        #endregion


        #region RelayMethod
        private RelayCommand _okRelayCommand;

        public RelayCommand OkRelayCommand =>_okRelayCommand
            ?? (_okRelayCommand = new RelayCommand(ExecuteOkCommand));

        

        private RelayCommand _abortCommand;

        public RelayCommand AbortRelayCommand =>_abortCommand
            ?? (_abortCommand = new RelayCommand(ExecuteAbortRelayCommand));
        
        #endregion


        public class MyModel
        {
            public string txt { get; set; }
            public List<int> list;
        }
    }
}