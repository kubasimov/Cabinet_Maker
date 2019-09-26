using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using WPF3.Implement;
using WPF3.Interface;

namespace WPF3.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            
            SimpleIoc.Default.Register<IDataExchangeViewModel, DataExchangeViewModel>(true);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HorizontalBarrierViewModel>();
            SimpleIoc.Default.Register<VerticalBarrierViewModel>();
            SimpleIoc.Default.Register<FrontViewModel>();
            SimpleIoc.Default.Register<TreeViewTestViewModel>();
        }

        public MainViewModel MainView => ServiceLocator.Current.GetInstance<MainViewModel>();

        public HorizontalBarrierViewModel HorizontalBarrierView => ServiceLocator.Current.GetInstance<HorizontalBarrierViewModel>();

        public VerticalBarrierViewModel VerticalBarrierView => ServiceLocator.Current.GetInstance<VerticalBarrierViewModel>();

        public FrontViewModel FrontView => ServiceLocator.Current.GetInstance<FrontViewModel>();

        public TreeViewTestViewModel TreeViewTestModel => ServiceLocator.Current.GetInstance<TreeViewTestViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}