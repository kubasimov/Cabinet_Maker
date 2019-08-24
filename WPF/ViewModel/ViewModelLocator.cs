using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using WPF.Implement;
using WPF.Interface;

namespace WPF.ViewModel
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
        }

        public MainViewModel MainView
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public HorizontalBarrierViewModel HorizontalBarrierView
        {
            get { return ServiceLocator.Current.GetInstance<HorizontalBarrierViewModel>(); }
        }

        public VerticalBarrierViewModel VerticalBarrierView
        {
            get { return ServiceLocator.Current.GetInstance<VerticalBarrierViewModel>(); }
        }

        public FrontViewModel FrontView
        {
            get { return ServiceLocator.Current.GetInstance<FrontViewModel>(); }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}