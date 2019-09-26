using System.Collections.Generic;
using Core.Model;
using WPF3.Enum;
using WPF3.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace WPF3.ViewModel
{
    public class VerticalBarrierViewModel:ViewModelBase
    {
        private IDataExchangeViewModel _dataExchangeViewModel;
        private int _horizontalBarrierCount;

        public VerticalBarrierViewModel(IDataExchangeViewModel dataExchangeViewModel)
        {
            _dataExchangeViewModel = dataExchangeViewModel;

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.HorizontalBarrier))
            {
                _horizontalBarrierCount = (int)_dataExchangeViewModel.Item(EnumExchangeViewmodel.HorizontalBarrier);
            }
            _barrierNumber = 0.ToString();
            _backShelf = 0.ToString();
            _shelf = false;

        }

        private void ExecuteOkCommand()
        {
            var barierParametter = new BarrierParameter
            {
                Number = int.Parse(_barrierNumber),
                Back = int.Parse(_backShelf)
            };
            
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.verticalBarrierWindow, barierParametter);
            Messenger.Default.Send(new NotificationMessage(this, "CloseVerticalBarrier"));
        }


        private void ExecuteAbortRelayCommand()
        {
            Messenger.Default.Send(new NotificationMessage(this, "CloseVerticalBarrier"));

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

        #endregion

        #region RelayMethod
        private RelayCommand _okRelayCommand;

        public RelayCommand OkRelayCommand => _okRelayCommand
                                              ?? (_okRelayCommand = new RelayCommand(ExecuteOkCommand));



        private RelayCommand _abortCommand;

        public RelayCommand AbortRelayCommand => _abortCommand
                                                 ?? (_abortCommand = new RelayCommand(ExecuteAbortRelayCommand));

        #endregion
    }
}
