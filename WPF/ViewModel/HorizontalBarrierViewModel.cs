using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
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
        private readonly IDataExchangeViewModel _dataExchangeViewModel;
        private int _verticalBarrierCount;
        private List<string> _intList;

        public HorizontalBarrierViewModel(IDataExchangeViewModel dataExchangeViewModel)
        {
            _dataExchangeViewModel = dataExchangeViewModel;

            LoadDataToComboBox();
            
            if (IsInDesignMode)
            {
                _barrierNumber = 2.ToString();
                _backShelf = 0.ToString();
                _shelf = true;
                
                _verticalBarrier.Add("1");
                _verticalBarrier.Add("2");
                _verticalBarrier.Add("3");
                _verticalBarrier.Add("1 2");
                _verticalBarrier.Add("1 3");
                _verticalBarrier.Add("2 3");
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
            var barierParametter = new BarrierParameter
            {
                Number = int.Parse(_barrierNumber),
                Back = int.Parse(_backShelf)
            };

            if (Barriers < 0)
            {
                Barriers = _verticalBarrier.Count-1;
            }

            var z = _verticalBarrier[Barriers];
            var zz = z.ToCharArray();

            foreach (char c in zz)
            {
                barierParametter.AddBarrier((int)char.GetNumericValue(c));
            }
            
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.HorizontalBarrierWindow, barierParametter);
            Messenger.Default.Send(new NotificationMessage(this, "CloseHorizontalBarrier"));
        }
        
        private void ExecuteAbortRelayCommand()
        {
            Messenger.Default.Send(new NotificationMessage(this, "CloseHorizontalBarrier"));

        }

        private void ExecuteGetFocusCommand()
        {
            LoadDataToComboBox();
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
                RaisePropertyChanged(()=>BarrierNumber);
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
                RaisePropertyChanged(()=>Shelf);
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
                RaisePropertyChanged(()=>BackShelf);
            }
        }

        
        public const string VerticalBarrierPropertyName = "VerticalBarrier";

        private ObservableCollection<string> _verticalBarrier;

        public ObservableCollection<string> VerticalBarrier
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
                RaisePropertyChanged(() => VerticalBarrier);
            }
        }


        
        public const string BarriersPropertyName = "Barriers";

        private int _barriers;
        
        public int Barriers
        {
            get
            {
                return _barriers;
            }

            set
            {
                if (_barriers == value)
                {
                    return;
                }

                _barriers = value;
                RaisePropertyChanged(() => Barriers);
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

        private RelayCommand _getFocusCommand;

        
        public RelayCommand GetFocusCommand =>
            _getFocusCommand
            ?? (_getFocusCommand = new RelayCommand(ExecuteGetFocusCommand));

        
        #endregion

        private void AddPermutationToObservableCollection()
        {
            VerticalBarrier = new ObservableCollection<string>();

            foreach (var str in _intList)
            {
                VerticalBarrier.Add(str);
            }
            RaisePropertyChanged(VerticalBarrierPropertyName);
        }
        
        private static List<string> GeneratePermutationFromNumberElements(int elements)
        {
            var intElements = new int[elements+1];

            for (var i = 0; i <=elements; i++)
            {
                intElements[i] = i;
            }

            var t2 = new List<string>();

            for (var i = 0; i <=elements+1; i++)
            {
                t2 = GFG.printCombination(intElements, elements+1, i + 1);
            }


            return t2;
        }
        
        private static class GFG
        {

            // This code is contributed by m_kit 
            private static readonly List<string> lll = new List<string>();
           
            private static readonly StringBuilder stringBuilder = new StringBuilder();

            /* arr[] ---> Input Array 
            data[] ---> Temporary array to 
                        store current combination 
            start & end ---> Staring and Ending 
                            indexes in arr[] 
            index ---> Current index in data[] 
            r ---> Size of a combination 
                    to be printed */
            static void CombinationUtil(int[] arr, int[] data, int start, int end, int index, int r)
            {
                // Current combination is 
                // ready to be printed, 
                // print it 
                if (index == r)
                {
                    
                    
                    for (int j = 0; j < r; j++)
                    {

                        stringBuilder.Append(data[j]);

                       
                        Debug.Write(data[j] + " ");
                    }

                    lll.Add(stringBuilder.ToString());
                    stringBuilder.Clear();

                    Debug.WriteLine("");
                    return;
                }

                // replace index with all 
                // possible elements. The 
                // condition "end-i+1 >= 
                // r-index" makes sure that 
                // including one element 
                // at index will make a 
                // combination with remaining 
                // elements at remaining positions 
                for (int i = start; i <= end &&
                                    end - i + 1 >= r - index; i++)
                {
                    data[index] = arr[i];
                    CombinationUtil(arr, data, i + 1,
                        end, index + 1, r);
                }
            }

            // The main function that prints 
            // all combinations of size r 
            // in arr[] of size n. This 
            // function mainly uses combinationUtil() 
            public static List<string> printCombination(int[] arr, int n, int r)
            {
                // A temporary array to store 
                // all combination one by one 
                int[] data = new int[r];

                // Print all combination 
                // using temprary array 'data[]' 
                CombinationUtil(arr, data, 0,n - 1, 0, r);

                return lll;
            }

            // Driver Code 
            //static public void Main()
            //{
            //    int[] arr = { 1, 2, 3, 4, 5 };
            //    int r = 3;
            //    int n = arr.Length;
            //    printCombination(arr, n, r);
            //}
        }

        private void LoadDataToComboBox()
        {
            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.VerticalBarrier))
            {
                _verticalBarrierCount = (int)_dataExchangeViewModel.Item(EnumExchangeViewmodel.VerticalBarrier);
            }

           _intList?.Clear();


            _intList = GeneratePermutationFromNumberElements(_verticalBarrierCount);

            AddPermutationToObservableCollection();
        }
    }
}