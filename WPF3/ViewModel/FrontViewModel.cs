using Config;
using CoreS.Enum;
using CoreS.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WPF3.Enum;
using WPF3.Interface;

namespace WPF3.ViewModel
{
    public class FrontViewModel: ViewModelBase
    {
        private readonly IDataExchangeViewModel _dataExchangeViewModel;
        private readonly IConfig _config;

        public FrontViewModel(IDataExchangeViewModel dataExchangeViewModel, IConfig config)
        {
            _dataExchangeViewModel = dataExchangeViewModel;
            _config = config;

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.Front))
            {
                var frontParameter = (FrontParameter)_dataExchangeViewModel.Item(EnumExchangeViewmodel.Front);
                _tempFrontParameter = TempFrontParameterFromFrontParametter(frontParameter);
            }
            else
            {
                _tempFrontParameter = new TempFrontParameter
                {
                    BetweenCabinet = _config.SlotsBetweenCabinet().ToString(),
                    BetweenHorizontally = _config.SlotsBetweenHorizontally().ToString(),
                    BetweenVertically = _config.SlotsBetweenVertically().ToString(),
                    Bottom = _config.SlotsBottom().ToString(),
                    Top = _config.SlotsTop().ToString(),
                    Left = _config.SlotsLeft().ToString(),
                    Right = _config.SlotsRight().ToString(),
                    Number = 2.ToString()
                };
            }

            RaisePropertyChanged(frontParameterPropertyName);
        }

        private TempFrontParameter TempFrontParameterFromFrontParametter(FrontParameter frontParameter)
        {
            return new TempFrontParameter
            {
                Number = frontParameter.Number.ToString(),
                Top = frontParameter.Slots.Top.ToString(),
                Bottom = frontParameter.Slots.Bottom.ToString(),
                Left = frontParameter.Slots.Left.ToString(),
                Right = frontParameter.Slots.Right.ToString(),
                BetweenHorizontally = frontParameter.Slots.BetweenHorizontally.ToString(),
                BetweenVertically = frontParameter.Slots.BetweenVertically.ToString(),
                BetweenCabinet = frontParameter.Slots.BetweenCabinet.ToString()
            };
        }

        private FrontParameter FrontParameterFromTempFrontParametter()
        {
            return new FrontParameter
            {
                Number = int.Parse(_tempFrontParameter.Number),
                Slots =
                {
                    Top = int.Parse(_tempFrontParameter.Top),
                    Bottom = int.Parse(_tempFrontParameter.Bottom),
                    Left = int.Parse(_tempFrontParameter.Left),
                    Right = int.Parse(_tempFrontParameter.Right),
                    BetweenHorizontally = int.Parse(_tempFrontParameter.BetweenHorizontally),
                    BetweenVertically = int.Parse(_tempFrontParameter.BetweenVertically),
                    BetweenCabinet = int.Parse(_tempFrontParameter.BetweenCabinet)
                },
                EnumFront = GetEnumFront()
            };
        }

        private EnumFront GetEnumFront()
        {
            switch (_typ.ToString())
            {
                case "Pionowo":
                    return EnumFront.Pionowo;
                    
                case "Poziomo":
                    return EnumFront.Poziomo;
                
                default:
                    return EnumFront.Pionowo;
            }
        }

        private void ExecuteOkCommand()
        {
            
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.FrontWindow, FrontParameterFromTempFrontParametter());
            Messenger.Default.Send(new NotificationMessage(this, "CloseFront"));
        }
        
        private void ExecuteAbortRelayCommand()
        {
            Messenger.Default.Send(new NotificationMessage(this, "CloseFront"));

        }

        #region BindingProperty

        public const string frontParameterPropertyName = "FrontParameter";

        private TempFrontParameter _tempFrontParameter;

        public TempFrontParameter FrontParameter
        {
            get
            {
                return _tempFrontParameter;
            }

            set
            {
                if (_tempFrontParameter == value)
                {
                    return;
                }

                _tempFrontParameter = value;
                RaisePropertyChanged(frontParameterPropertyName);
            }
        }


        public const string MyTypPropertyName = "MyTyp";

        private Typ _typ ;

        public Typ MyTyp
        {
            get
            {
                return _typ;
            }

            set
            {
                if (_typ == value)
                {
                    return;
                }

                _typ = value;
                RaisePropertyChanged(MyTypPropertyName);
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

        public class TempFrontParameter
        {
            public string Number { get; set; }              //ilosc frontow
            public string Top { get; set; }
            public string Bottom { get; set; }
            public string Left { get; set; }
            public string Right { get; set; }
            public string BetweenVertically { get; set; }
            public string BetweenHorizontally { get; set; }
            public string BetweenCabinet { get; set; }

        }

        public enum Typ
        {
            Pionowo,
            Poziomo

        }
    }
}
