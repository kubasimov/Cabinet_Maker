using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using System.Reflection;
using CoreS.Enum;
using CoreS.Model;

namespace WPF3.ViewModel
{
    public partial class MainViewModel
    {
        #region Declare Cabinet RelayCommand

        private RelayCommand _myNewCommand;

        public RelayCommand NewCommand => _myNewCommand
                                            ?? (_myNewCommand =
                                                new RelayCommand(ExecuteNewCommand));

        private RelayCommand<object> _mySaveCommand;

        public RelayCommand<object> SaveCommand => _mySaveCommand
                                           ?? (_mySaveCommand =
                                               new RelayCommand<object>(ExecuteSaveCommand));

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

        private RelayCommand _myAddHorizontalBarrierByEveryCommand;

        public RelayCommand AddHorizontalBarrierByEveryCommand => _myAddHorizontalBarrierByEveryCommand
                                                           ?? (_myAddHorizontalBarrierByEveryCommand =
                                                               new RelayCommand(ExecuteAddHorizontalBarrierByEveryCommand));

        private RelayCommand _deleteHorizontalBarrierCommand;

        public RelayCommand DeleteHorizontalBarrierCommand => _deleteHorizontalBarrierCommand
                                                              ?? (_deleteHorizontalBarrierCommand =
                                                                  new RelayCommand(ExecuteDeleteHorizontalBarrierCommand));

        private RelayCommand _removeHorizontalBarrierCommand;

        public RelayCommand RemoveHorizontalBarrierCommand => _removeHorizontalBarrierCommand
                                            ?? (_removeHorizontalBarrierCommand = new RelayCommand(ExecuteRemoveHorizontalBarrierCommand));

        #endregion

        #region Declare Front RelayCommand

        private RelayCommand _myNewFrontCommand;

        public RelayCommand NewFrontCommand => _myNewFrontCommand
                                               ?? (_myNewFrontCommand =
                                                   new RelayCommand(ExecuteNewFrontCommand));

        private RelayCommand _myAddFrontCommand;

        public RelayCommand AddFrontCommand => _myAddFrontCommand
            ?? (_myAddFrontCommand = new RelayCommand(ExecuteAddFrontCommand));
        
        private RelayCommand _mydeleteFrontCommand;

        public RelayCommand DeleteFrontCommand => _mydeleteFrontCommand
            ?? (_mydeleteFrontCommand = new RelayCommand(ExecuteDeleteFrontCommand));
        
        private RelayCommand _myremoveFrontCommand;

        public RelayCommand RemoveFrontCommand => _myremoveFrontCommand
            ?? (_myremoveFrontCommand = new RelayCommand(ExecuteRemoveFrontCommand));

        #endregion

        #region Declare RelayCommand

        private RelayCommand _changeCabinetWhenLostFocusCommand;

        public RelayCommand ChangeCabinetWhenLostFocusCommand => _changeCabinetWhenLostFocusCommand 
                    ?? (_changeCabinetWhenLostFocusCommand = new RelayCommand(ExecutechangeCabinetWhenLostFocusCommand));
                
        private RelayCommand<KeyEventArgs> _changeCabinetWhenKeyDownCommand;

        public RelayCommand<KeyEventArgs> ChangeCabinetWhenKeyDownCommand => _changeCabinetWhenKeyDownCommand
                    ?? (_changeCabinetWhenKeyDownCommand = new RelayCommand<KeyEventArgs>(ExecuteChangeCabinetWhenKeyDownCommand));
                
        private RelayCommand<object> _myElementTreeView;

        public RelayCommand<object> MyElementTreeView => _myElementTreeView
                    ?? (_myElementTreeView = new RelayCommand<object>(ExecuteMyElementTreeView));
            

        #endregion

        #region Declare ChangeElement RelayCommand

        private RelayCommand<KeyEventArgs> _ChangeTextWhenKeyDownCommand;

        public RelayCommand<KeyEventArgs> ChangeTextWhenKeyDownCommand => _ChangeTextWhenKeyDownCommand
                                            ?? (_ChangeTextWhenKeyDownCommand = new RelayCommand<KeyEventArgs>(ExecuteChangeTextWhenKeyDownCommand));

        private RelayCommand<object> _ChangeTextWhenLostFocusCommand;

        public RelayCommand<object> ChangeTextWhenLostFocusCommand => _ChangeTextWhenLostFocusCommand
                                            ?? (_ChangeTextWhenLostFocusCommand = new RelayCommand<object>(ExecuteChangeTextWhenLostFocusCommand));

        #endregion



        private RelayCommand<object> _deleteElementHorizontalBarrierCommand;

        public RelayCommand<object> DeleteElementHorizontalBarrierCommand => _deleteElementHorizontalBarrierCommand
                    ?? (_deleteElementHorizontalBarrierCommand = new RelayCommand<object>(ExecuteDeleteElementHorizontalBarrierCommand));


        private RelayCommand _RecalFrontCommand;

        public RelayCommand RecalFrontCommand => _RecalFrontCommand
                                            ?? (_RecalFrontCommand = new RelayCommand(ExecuteRecalFrontCommand));

        private void ExecuteRecalFrontCommand()
        {
            _cabinet.FrontRecall();
        }


        private RelayCommand<string> _loadFileFromListViewCommand;

        public RelayCommand<string> LoadFileFromListViewCommand => _loadFileFromListViewCommand
                                            ?? (_loadFileFromListViewCommand = new RelayCommand<string>(ExecuteLoadFileFromListViewCommand));

        


















        private RelayCommand _TreeViewContextMenuCommand;

        public RelayCommand TreeViewContextMenuCommand => _TreeViewContextMenuCommand
                                            ?? (_TreeViewContextMenuCommand = new RelayCommand(ExecuteTreeViewContextMenuCommand));

        private void ExecuteTreeViewContextMenuCommand()
        {
           
        }


    }
}




 