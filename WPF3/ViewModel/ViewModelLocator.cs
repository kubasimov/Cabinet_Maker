using CommonServiceLocator;
using Config;
using GalaSoft.MvvmLight.Ioc;
using NLog;
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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            Logger.Info("ViewModelLocator initialize constructor");
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            
            SimpleIoc.Default.Register<IDataExchangeViewModel, DataExchangeViewModel>(true);
            SimpleIoc.Default.Register<IConfig, Config.Config>(true);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HorizontalBarrierViewModel>();
            SimpleIoc.Default.Register<VerticalBarrierViewModel>();
            SimpleIoc.Default.Register<FrontViewModel>();
                        
        }
        public MainViewModel MainView
        {
            get
            {
                Logger.Info("ViewModelLocator ServiceLocator.Current.GetInstance<MainViewModel>");
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public HorizontalBarrierViewModel HorizontalBarrierView
        {
            get
            {
                Logger.Info("ViewModelLocator ServiceLocator.Current.GetInstance<HorizontalBarrierViewModel>");
                return ServiceLocator.Current.GetInstance<HorizontalBarrierViewModel>();
            }
        }

        public VerticalBarrierViewModel VerticalBarrierView
        {
            get
            {
                Logger.Info("ViewModelLocator ServiceLocator.Current.GetInstance<VerticalBarrierViewModel>");
                return ServiceLocator.Current.GetInstance<VerticalBarrierViewModel>();
            }
        }

        public FrontViewModel FrontView
        {
            get
            {
                Logger.Info("ViewModelLocator ServiceLocator.Current.GetInstance<FrontViewModel>");
                return ServiceLocator.Current.GetInstance<FrontViewModel>();
            }
        }
                
        public static void Cleanup()
        {
            Logger.Info("ViewModelLocator Cleanup()");

        }
    }
}