using CoreS;
using CoreS.Model;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using WPF3.Enum;
using WPF3.View;
using System.Linq;
using CoreS.Enum;
using System.Reflection;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;

namespace WPF3.ViewModel
{
    public partial class MainViewModel
    {
        private Cabinet _cabinet;

        #region Cabinet ExecuteCommand

        private void ExecuteNewCommand()
        {
            Logger.Info("ExecuteNewCommand in MainViewModel"); 
            NewCabinet();
            RaisePropertyChanged(MyCabinetPropertyName);
        }

        private void ExecuteSaveCommand(object obj)
        {
            //var t = (System.Windows.Controls.Viewport3D)obj;

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


            Logger.Info("ExecuteSaveCommand in MainViewModel");
            if (string.IsNullOrEmpty(_myCabinet.Name))
            {
                MessageBox.Show("Nazwa jest wymagana", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                _cabinet.Serialize();
                MessageBox.Show("Zapisano", "Zapis", MessageBoxButton.OK);
                ReadCabinetMakerDirectory();
            }
        }

        private void ExecuteLoadFileFromListViewCommand(string str)
        {
            Logger.Info("ExecuteLoadFileFromListViewCommand(string str) in MainViewModel");
            Logger.Debug("str: {0}", str);
            var path = Path.Combine(_config.CabinetFilesDirectory(), str + ".json");
            _cabinet.Deserialize(path);
            Create3DCabinet();
            ReloadMyCabinet();
        }

        private void ExecuteEndCommand()
        {
            Logger.Info("ExecuteEndCommand in MainViewModel");
            Messenger.Default.Send(new NotificationMessage(this, "CloseMain"));
        }

        private void ExecuteExportCommand()
        {
            Logger.Info("ExecuteExportCommand in MainViewModel");
            _cabinet.ExcelExport();
            
        }

        private void ExecuteReadCabinetCommand()
        {
            Logger.Info("ExecuteReadCabinetCommand in MainViewModel");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = _config.CabinetFilesDirectory();
            openFileDialog.Title = "Otworz plik z szafk¹";
            
            if (openFileDialog.ShowDialog() == true)
            {
                _cabinet.Deserialize(openFileDialog.FileName);
                Create3DCabinet();
            }
            
            ReloadMyCabinet();   
        }

        private void ReloadMyCabinet()
        {
            _myCabinet.Height = _cabinet.Height().ToString();
            _myCabinet.Width = _cabinet.Width().ToString();
            _myCabinet.Depth = _cabinet.Depth().ToString();
            _myCabinet.Name = _cabinet.Name().ToString();
            _myCabinet.SizeElement = _cabinet.SizeElement().ToString();
            RaisePropertyChanged(MyCabinetPropertyName);
        }

        private void ExecuteClipboardCommand()
        {
            Logger.Info("ExecuteClipboardCommand in MainViewModel");
            if (string.IsNullOrEmpty(_myCabinet.Name))
            {
                MessageBox.Show("Nazwa jest wymagana", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                _cabinet.ClipboardExport();
            }
                        
        }

        #endregion

        #region Declare VerticalBarrier ExecuteCommand

        private void ExecuteNewVerticalBarrierCommand()
        {
            Logger.Info("ExecuteNewVerticalBarrierCommand in MainViewModel");
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.HorizontalBarrier, _cabinet.HorizontalBarrier.Count);
            new VerticalBarrierWindow().ShowDialog();

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.verticalBarrierWindow))
            {
                var t = (BarrierParameter)_dataExchangeViewModel.Item(EnumExchangeViewmodel.verticalBarrierWindow);

                _cabinet.NewVerticalBarrier(t);
                Create3DCabinet();
            }
        }

        private void ExecuteAddVerticalBarrierCommand()
        {
            Logger.Info("ExecuteAddVerticalBarrierCommand in MainViewModel");
            _cabinet.AddVerticalBarrier(1);
            Create3DCabinet();
        }

        private void ExecuteDeleteVerticalBarrierCommand()
        {
            Logger.Info("ExecuteDeleteVerticalBarrierCommand in MainViewModel");
            _cabinet.DeleteVerticalBarrier(1);
            Create3DCabinet();
        }

        private void ExecuteRemoveVerticalBarrierCommand()
        {
            Logger.Info("ExecuteRemoveVerticalBarrierCommand in MainViewModel");
            _cabinet.RemoveVerticalBarrier();
            Create3DCabinet();
        }

        #endregion

        #region Declare HorizontalBarrier RelayCommand

        private void ExecuteNewHorizontalBarrierCommand()
        {
            Logger.Info("ExecuteNewHorizontalBarrierCommand in MainViewModel");
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.VerticalBarrier, _cabinet.VerticalBarrier.Count);
            new HorizontalBarrierWindow().ShowDialog();

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.HorizontalBarrierWindow))
            {
                var t = (BarrierParameter)_dataExchangeViewModel.Item(EnumExchangeViewmodel.HorizontalBarrierWindow);

                _cabinet.NewHorizontalBarrier(t);
                Create3DCabinet();
            }
        }

        private void ExecuteAddHorizontalBarrierCommand()
        {
            Logger.Info("ExecuteAddHorizontalBarrierCommand in MainViewModel");
            _cabinet.AddHorizontalBarrier(1);
            Create3DCabinet();
        }

        private void ExecuteAddHorizontalBarrierByEveryCommand()
        {
            Logger.Info("ExecuteAddHorizontalBarrierByEveryCommand in MainViewModel");
            _cabinet.AddHorizontalBarrierByEvery(int.Parse(_horizontaSize));
            Create3DCabinet();
        }

        private void ExecuteDeleteHorizontalBarrierCommand()
        {
            Logger.Info("ExecuteDeleteHorizontalBarrierCommand in MainViewModel");
            _cabinet.DeleteHorizontalBarrier(1);
            Create3DCabinet();
        }

        private void ExecuteRemoveHorizontalBarrierCommand()
        {
            Logger.Info("ExecuteRemoveHorizontalBarrierCommand in MainViewModel");
            _cabinet.RemoveHorizontalBarrier();
            Create3DCabinet();
        }

        #endregion

        #region Front ExecuteCommand

        private void ExecuteNewFrontCommand()
        {
            Logger.Info("ExecuteNewFrontCommand in MainViewModel");
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.Front, new FrontParameter());
            new FrontWindow().ShowDialog();

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.FrontWindow))
            {
                var frontParameter = (FrontParameter) _dataExchangeViewModel.Item(EnumExchangeViewmodel.FrontWindow);
                
                _cabinet.AddFront(frontParameter);
                Create3DCabinet();
            }
        }

        private void ExecuteAddFrontCommand()
        {
            Logger.Info("ExecuteAddFrontCommand in MainViewModel");
            _cabinet.AddFront(1);
            Create3DCabinet();
        }

        private void ExecuteDeleteFrontCommand()
        {
            Logger.Info("ExecuteDeleteFrontCommand in MainViewModel");
            _cabinet.DeleteLast_x_Front(1);
            Create3DCabinet();
        }

        private void ExecuteRemoveFrontCommand()
        {

            Logger.Info("ExecuteRemoveFrontCommand in MainViewModel");
            _cabinet.RemoveFront();
            Create3DCabinet();
        }

        #endregion

        #region Cabinet ExecuteCommand

        private void ExecutechangeCabinetWhenLostFocusCommand()
        {
            Logger.Info("ExecutechangeCabinetWhenLostFocusCommand in MainViewModel");
            _cabinet.Height(int.Parse(_myCabinet.Height)).Width(int.Parse(_myCabinet.Width)).Depth(int.Parse(_myCabinet.Depth)).SizeElement(int.Parse(_myCabinet.SizeElement)).Name(_myCabinet.Name);

            _cabinet.BackSize = int.Parse(_myCabinet.BackSize);
            
            _cabinet.Redraw();

            Create3DCabinet();
        }

        private void ExecuteChangeCabinetWhenKeyDownCommand(KeyEventArgs parameter)
        {
            Logger.Info("ExecuteChangeCabinetWhenKeyDownCommand(KeyEventArgs parameter) in MainViewModel");
            if (parameter.Key==Key.Enter)
            {
                ExecutechangeCabinetWhenLostFocusCommand();
            }
        }

        private void ExecuteMyElementTreeView(object parameter)
        {
            Logger.Info("ExecuteMyElementTreeView(object parameter) in MainViewModel");
            if (parameter.GetType() == typeof(ElementModel))
            {
                var tmp = (ElementModel)parameter;

                foreach (var elements in CabinetView)
                {
                    if (elements.ElementModels.Any(p => p.GetGuid() == tmp.GetGuid()))
                    {
                        _mElement = elements.ElementModels.First(p => p.GetGuid() == tmp.GetGuid());
                        RaisePropertyChanged(MElementPropertyName);
                        break;
                    }
                }
            }
        }

        private void ExecuteChangeTextWhenLostFocusCommand(object obj)
        {
            //var TextBoxObject = (System.Windows.Controls.TextBox)obj;
            //var ParameterName = TextBoxObject.Name.Substring(TextBoxObject.Name.IndexOf('_') + 1);
            //EnumElementParameter enumElementParameter = (EnumElementParameter)System.Enum.Parse(typeof(EnumElementParameter), ParameterName);

            //_cabinet.ChangeElemenet(_mElement, enumElementParameter, TextBoxObject.Text);
            //Create3DCabinet();
        }

        private void ExecuteChangeTextWhenKeyDownCommand(KeyEventArgs obj)
        {
            Logger.Info("ExecuteChangeTextWhenKeyDownCommand(KeyEventArgs obj) in MainViewModel");
            if (obj == null || _mElement == null || obj.Source == null) return;

            var TextBoxObject = (System.Windows.Controls.TextBox)obj.Source;
            var ParameterName = TextBoxObject.Name.Substring(TextBoxObject.Name.IndexOf('_') + 1);
            EnumElementParameter enumElementParameter = (EnumElementParameter)System.Enum.Parse(typeof(EnumElementParameter), ParameterName);

            string PropertyResult = GetPropertyFromClass(ParameterName);

            switch (obj.Key)
            {
                case Key.Enter:

                    if (int.TryParse(TextBoxObject.Text, out int result))
                    {
                        _mElement= _cabinet.ChangeElemenet(_mElement, enumElementParameter, TextBoxObject.Text);
                        Create3DCabinet();
                    }
                    break;

                case Key.Up:
                    _mElement = _cabinet.ChangeElemenet(_mElement, enumElementParameter, (int.Parse(PropertyResult) + 1).ToString());
                    Create3DCabinet();
                    break;

                case Key.Down:
                    _mElement = _cabinet.ChangeElemenet(_mElement, enumElementParameter, (int.Parse(PropertyResult) - 1).ToString());
                    Create3DCabinet();
                    break;

                default:
                    break;

            }

        }

        private void Create3DCabinet()
        {
            Logger.Info("Create3DCabinet in MainViewModel");
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
            RaisePropertyChanged(MElementPropertyName);
        }

        private string GetPropertyFromClass(string ParameterName)
        {
            Logger.Info("GetPropertyFromClass(string ParameterName) in MainViewModel");
            Type type = typeof(ElementModel);

            PropertyInfo[] propertyInfo = type.GetProperties();
            string PropertyResult = "";

            foreach (var item in propertyInfo)
            {
                if (item.Name == ParameterName)
                {
                    PropertyResult = item.GetValue(_mElement).ToString();
                }
            }

            return PropertyResult;
        }

        #endregion

        private void ExecuteDeleteElementHorizontalBarrierCommand(object parameter)
        {
            Logger.Info("ExecuteDeleteElementHorizontalBarrierCommand(object parameter) in MainViewModel");

            if (parameter == null || !(parameter is ElementModel)) return;


            var element = (ElementModel)parameter;
            
            foreach (var item in _cabinet.HorizontalBarrier)
            {
                if(item.GetGuid()==element.GetGuid()) _cabinet.DeleteElementHorizontalBarrier(element);
            }

            foreach (var item in _cabinet.VerticalBarrier)
            {
                if (item.GetGuid() == element.GetGuid()) _cabinet.DeleteElementVerticalBarrier(element);
            }

            foreach (var item in _cabinet.FrontList)
            {
                if (item.GetGuid() == element.GetGuid())
                {
                    _cabinet.DeleteFront(element);
                    //break;
                }
            }

            Create3DCabinet();
        }

    }
}