using System;
using GalaSoft.MvvmLight.Command;

namespace WPF3.ViewModel
{
    public partial class MainViewModel
    {
        #region Declare Cabinet RelayCommand

        private RelayCommand _myNewCommand;

        public RelayCommand NewCommand => _myNewCommand
                                          ?? (_myNewCommand =
                                              new RelayCommand(ExecuteNewCommand));

        private RelayCommand _mySaveCommand;

        public RelayCommand SaveCommand => _mySaveCommand
                                           ?? (_mySaveCommand =
                                               new RelayCommand(ExecuteSaveCommand));

        private RelayCommand _myEndCommand;

        public RelayCommand EndCommand => _myEndCommand
                                          ?? (_myEndCommand =
                                              new RelayCommand(ExecuteEndCommand));

        private RelayCommand _myExportCommand;

        public RelayCommand ExportCommand => _myExportCommand
                                             ?? (_myExportCommand = new RelayCommand(ExecuteExportCommand));

        private RelayCommand _myReadCabinetCommand;

        public RelayCommand ReadCabinetCommand =>
            _myReadCabinetCommand ?? (_myReadCabinetCommand = new RelayCommand(ExecuteReadCabinetCommand));

        private RelayCommand _myClipboardCommand;

        public RelayCommand ClipboardCommand => _myClipboardCommand
                                            ?? (_myClipboardCommand = new RelayCommand(ExecuteClipboardCommand));

        #endregion

        #region Declare VerticalBarrier RelayCommand

        private RelayCommand _myNewVerticalBarrierCommand;

        public RelayCommand NewVerticalBarrierCommand => _myNewVerticalBarrierCommand
                                                         ?? (_myNewVerticalBarrierCommand =
                                                             new RelayCommand(ExecuteNewVerticalBarrierCommand));

        private RelayCommand _myAddVerticalBarrierCommand;

        public RelayCommand AddVerticalBarrierCommand => _myAddVerticalBarrierCommand
                                                         ?? (_myAddVerticalBarrierCommand =
                                                             new RelayCommand(ExecuteAddVerticalBarrierCommand));

        private RelayCommand _deleteVerticalBarrierCommand;

        public RelayCommand DeleteVerticalBarrierCommand => _deleteVerticalBarrierCommand
                                                            ?? (_deleteVerticalBarrierCommand =
                                                                new RelayCommand(ExecuteDeleteVerticalBarrierCommand));

        private RelayCommand _removeVerticalBarrierCommand;

        public RelayCommand RemoveVerticalBarrierCommand => _removeVerticalBarrierCommand
                                                            ?? (_removeVerticalBarrierCommand =
                                                                new RelayCommand(ExecuteRemoveVerticalBarrierCommand));

        #endregion

        #region Declare HorizontalBarrier RelayCommand

        private RelayCommand _myNewHorizontalBarrierCommand;

        public RelayCommand NewHorizontalBarrierCommand => _myNewHorizontalBarrierCommand
                                                           ?? (_myNewHorizontalBarrierCommand =
                                                               new RelayCommand(ExecuteNewHorizontalBarrierCommand));

        private RelayCommand _myAddHorizontalBarrierCommand;

        public RelayCommand AddHorizontalBarrierCommand => _myAddHorizontalBarrierCommand
                                                           ?? (_myAddHorizontalBarrierCommand =
                                                               new RelayCommand(ExecuteAddHorizontalBarrierCommand));

        private RelayCommand _deleteHorizontalBarrierCommand;

        public RelayCommand DeleteHorizontalBarrierCommand => _deleteHorizontalBarrierCommand
                                                              ?? (_deleteHorizontalBarrierCommand = 
                                                                  new RelayCommand(ExecuteDeleteHorizontalBarrierCommand));

        private RelayCommand _removeHorizontalBarrierCommand;

        public RelayCommand RemoveHorizontalBarrierCommand => _removeHorizontalBarrierCommand
                                            ?? (_removeHorizontalBarrierCommand = new RelayCommand(ExecuteRemoveHorizontalBarrierCommand));

        
        #endregion

        #region Declare Front RelayCommand

        private RelayCommand _myAddFrontCommand;

        public RelayCommand AddFrontCommand => _myAddFrontCommand
                                               ?? (_myAddFrontCommand =
                                                   new RelayCommand(ExecuteAddFrontCommand));

        #endregion

        #region Declare RelayCommand

        private RelayCommand _myReDrawCabinetCommand;

        public RelayCommand ReDrawCabinetCommand => _myReDrawCabinetCommand ??
                                                    (_myReDrawCabinetCommand =
                                                        new RelayCommand(ExecuteReDrawCabinetCommand));

        #endregion
    }
}