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

        private RelayCommand _myReDrawCabinetCommand;

        public RelayCommand ReDrawCabinetCommand => _myReDrawCabinetCommand ??
                                                    (_myReDrawCabinetCommand =
                                                        new RelayCommand(ExecuteReDrawCabinetCommand));

        private RelayCommand<object> _myElementTreeView;

        public RelayCommand<object> MyElementTreeView
        {
            get
            {
                return _myElementTreeView
                    ?? (_myElementTreeView = new RelayCommand<object>(ExecuteMyElementTreeView));
            }
        }

        #endregion

        #region Declare ChangeElement RelayCommand

        private RelayCommand<string> _changeWidthMElementCommand;

        public RelayCommand<string> ChangeWidthMElementCommand => _changeWidthMElementCommand
                                            ?? (_changeWidthMElementCommand = new RelayCommand<string>(ExecuteChangeWidthMElementCommand));

        
        private RelayCommand<string> _changeHeihgtMElementCommand;

        public RelayCommand<string> ChangeHeightMElementCommand => _changeHeihgtMElementCommand
                    ?? (_changeHeihgtMElementCommand = new RelayCommand<string>(ExecuteChangeHeightMElementCommand));

        
        private RelayCommand<string> _changeDepthMElementCommand;

        public RelayCommand<string> ChangeDepthMElementCommand => _changeDepthMElementCommand
                                            ?? (_changeDepthMElementCommand = new RelayCommand<string>(ExecuteChangeDepthMElementCommand));

   
        private RelayCommand<string> _changeDescriptionMElementCommand; 

        public RelayCommand<string> ChangeDescriptionMElementCommand => _changeDescriptionMElementCommand
            ?? (_changeDescriptionMElementCommand = new RelayCommand<string>(ExecuteChangeDescriptionMElementCommand));
        

        private RelayCommand<string> _changeXMelementCommand;

        public RelayCommand<string> ChangeXMelementCommand
        {
            get
            {
                return _changeXMelementCommand
                    ?? (_changeXMelementCommand = new RelayCommand<string>(ExecuteChangeXMelementCommand));
            }
        }


        private RelayCommand<string> _changeYMelementCommand;

        public RelayCommand<string> ChangeYMElementCommand => _changeYMelementCommand
                    ?? (_changeYMelementCommand = new RelayCommand<string>(ExecuteChangeYMElementCommand));


        private RelayCommand<string> _changeZMElementCommand;

        public RelayCommand<string> ChangeZElementCommand => _changeZMElementCommand
                    ?? (_changeZMElementCommand = new RelayCommand<string>(ExecuteChangeZElementCommand));




        private RelayCommand<KeyEventArgs> _ChangeWidthTextCommand;

        public RelayCommand<KeyEventArgs> ChangeWidthTextCommand => _ChangeWidthTextCommand
                                            ?? (_ChangeWidthTextCommand = new RelayCommand<KeyEventArgs>(ExecuteChangeWidthTextCommand));

        private void ExecuteChangeWidthTextCommand(KeyEventArgs obj)
        {

            if (obj == null || _mElement == null || obj.Source==null) return;
                        
            var TextBoxObject = (System.Windows.Controls.TextBox)obj.Source;
            var ParameterName = TextBoxObject.Name.Substring(TextBoxObject.Name.IndexOf('_') + 1);
            EnumElementParameter enumElementParameter = (EnumElementParameter) System.Enum.Parse(typeof(EnumElementParameter), ParameterName);

            
            Type type = typeof(ElementModel);
            MethodInfo method = type.GetMethod("Set"+ParameterName);
            
            PropertyInfo[] propertyInfo = type.GetProperties();
            string PropertyResult="";

            foreach (var item in propertyInfo)
            {
                if (item.Name==ParameterName)
                {
                     PropertyResult = item.GetValue(_mElement).ToString();
                }
            }

            if (PropertyResult == "") return;
                                          
            switch (obj.Key)
            {
                case Key.Enter:
                
                    if (int.TryParse(TextBoxObject.Text,out int result))
                    {
                        //method.Invoke(_mElement, new object[] { result });
                        _cabinet.ChangeElemenet(_mElement, enumElementParameter, TextBoxObject.Text);
                        _model3D = CreateCabinet();

                        RaisePropertyChanged(MyModel3DPropertyName);
                        RaisePropertyChanged(MElementPropertyName);
                    }
                    
                    break;

                case Key.Up:
                    //method.Invoke(_mElement, new object[] { int.Parse(PropertyResult)+1});
                    _cabinet.ChangeElemenet(_mElement, enumElementParameter, (int.Parse(PropertyResult) + 1).ToString());
                    _model3D = CreateCabinet();

                    RaisePropertyChanged(MyModel3DPropertyName);
                    RaisePropertyChanged(MElementPropertyName);
                    break;

                case Key.Down:
                    //method.Invoke(_mElement, new object[] { int.Parse(PropertyResult) - 1 });
                    _cabinet.ChangeElemenet(_mElement, enumElementParameter, (int.Parse(PropertyResult) - 1).ToString());
                    _model3D = CreateCabinet();

                    RaisePropertyChanged(MyModel3DPropertyName);
                    RaisePropertyChanged(MElementPropertyName);
                    break;

                default:
                    break;

            }

        }

        #endregion
    }
}




 