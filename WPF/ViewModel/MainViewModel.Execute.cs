using System;
using System.Windows;
using Core;
using Core.Model;
using GalaSoft.MvvmLight.Messaging;
using WPF.Enum;
using WPF.View;

namespace WPF.ViewModel
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
            var export = new Core.Export.ExcelExport();
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

            //var lBackup = new Dictionary<string, object>();
            //var lDataObject = Clipboard.GetDataObject();
            //var lFormats = lDataObject.GetFormats(false);
            //foreach (var lFormat in lFormats)
            //{
            //    lBackup.Add(lFormat, lDataObject.GetData(lFormat, false));
            //}

            //var obj = Clipboard.GetDataObject();

            //if (obj != null)
            //{
            //    // Find out whether the clipboard contains AutoCAD data

            //    var formats = obj.GetFormats();
            //    string formatFound = "";
            //    bool foundDwg = false;
            //    foreach (var name in formats)
            //    {
            //        if (name.Contains("AutoCAD.r"))
            //        {
            //            foundDwg = true;
            //            formatFound = name;
            //            break;
            //        }
            //    }

            //    if (foundDwg)
            //    {
            //        // If Found, discover where is the Database
            //        MemoryStream MStr = obj.GetData(formatFound) as MemoryStream;

            //        //Transform into string
            //        MStr.Position = 0;
            //        var sr = new StreamReader(MStr, Encoding.UTF8);
            //        var myStr = sr.ReadToEnd();

            //        //Remove unnecessary chars
            //        var sda = myStr.Replace("\0", "");
            //        int index = sda.IndexOf(".dwg");
            //        if (index > 0)
            //            sda = sda.Substring(0, index + 4);



            //    }
            //}
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
            _cabinet.DeleteHorizontalBarrier();
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

        private void ExecuteAddFrontCommand()
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