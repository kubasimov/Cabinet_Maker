using CoreS;
using CoreS.Model;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using WPF3.Enum;
using WPF3.View;

namespace WPF3.ViewModel
{
    public partial class MainViewModel
    {
        private Cabinet _cabinet;

        #region Cabinet ExecuteCommand

        private void ExecuteNewCommand()
        {
            NewCabinet();
            RaisePropertyChanged(MyCabinetPropertyName);
        }

        private void ExecuteSaveCommand()
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

            if (string.IsNullOrEmpty(_myCabinet.Name))
            {
                MessageBox.Show("Nazwa jest wymagana", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                _cabinet.Serialize();
                MessageBox.Show("Zapisano", "Zapis", MessageBoxButton.OK);
            }
        }

        private void ExecuteEndCommand()
        {
            Messenger.Default.Send(new NotificationMessage(this, "CloseMain"));
        }

        private void ExecuteExportCommand()
        {
            var export = new CoreS.Export.ExcelExport();
            export.Export(_cabinet);
        }

        private void ExecuteReadCabinetCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteClipboardCommand()
        {
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
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.HorizontalBarrier, _cabinet.HorizontalBarrier.Count);
            new VerticalBarrierWindow().ShowDialog();

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.verticalBarrierWindow))
            {
                var t = (BarrierParameter)_dataExchangeViewModel.Item(EnumExchangeViewmodel.verticalBarrierWindow);

                _cabinet.NewVerticalBarrier(t);
                _model3D = CreateCabinet();
                RaisePropertyChanged(MyModel3DPropertyName);
            }
        }

        private void ExecuteAddVerticalBarrierCommand()
        {
            _cabinet.AddVerticalBarrier(1);
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        private void ExecuteDeleteVerticalBarrierCommand()
        {
            _cabinet.DeleteVerticalBarrier();
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        private void ExecuteRemoveVerticalBarrierCommand()
        {
            _cabinet.RemoveVerticalBarrier();
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        #endregion

        #region Declare HorizontalBarrier RelayCommand

        private void ExecuteNewHorizontalBarrierCommand()
        {
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.VerticalBarrier, _cabinet.VerticalBarrier.Count);
            new HorizontalBarrierWindow().ShowDialog();

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.HorizontalBarrierWindow))
            {
                var t = (BarrierParameter)_dataExchangeViewModel.Item(EnumExchangeViewmodel.HorizontalBarrierWindow);

                _cabinet.NewHorizontalBarrier(t);
                _model3D = CreateCabinet();
                RaisePropertyChanged(MyModel3DPropertyName);
            }
        }

        private void ExecuteAddHorizontalBarrierCommand()
        {
            _cabinet.AddHorizontalBarrier(1);
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        private void ExecuteDeleteHorizontalBarrierCommand()
        {
            _cabinet.DeleteHorizontalBarrier(1);
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        private void ExecuteRemoveHorizontalBarrierCommand()
        {
            _cabinet.DeleteAllHorizontalBarrier();
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        #endregion

        #region Front ExecuteCommand

        private void ExecuteNewFrontCommand()
        {
            _dataExchangeViewModel.Add(EnumExchangeViewmodel.Front, new FrontParameter());
            new FrontWindow().ShowDialog();

            if (_dataExchangeViewModel.ContainsKey(EnumExchangeViewmodel.FrontWindow))
            {
                var frontParameter = (FrontParameter) _dataExchangeViewModel.Item(EnumExchangeViewmodel.FrontWindow);
                

                _cabinet.AddFront(frontParameter);
                _model3D = CreateCabinet();
                RaisePropertyChanged(MyModel3DPropertyName);
            }
        }

        private void ExecuteAddFrontCommand()
        {
            _cabinet.AddFront(1);
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        private void ExecuteDeleteFrontCommand()
        {
            _cabinet.DeleteFront(1);
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        private void ExecuteRemoveFrontCommand()
        {

            _cabinet.DeleteAllFront();
            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        #endregion

        #region Cabinet ExecuteCommand

        private void ExecuteReDrawCabinetCommand()
        {
            _cabinet.Height(int.Parse(_myCabinet.height)).Width(int.Parse(_myCabinet.width)).Depth(int.Parse(_myCabinet.depth)).SizeElement(int.Parse(_myCabinet.sizeElement)).Name(_myCabinet.Name);

            _cabinet.BackSize = int.Parse(_myCabinet.BackSize);
            

            _cabinet.Redraw();

            _model3D = CreateCabinet();
            RaisePropertyChanged(MyModel3DPropertyName);
        }

        #endregion
    }
}