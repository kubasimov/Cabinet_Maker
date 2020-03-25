using GalaSoft.MvvmLight.Messaging;
using NLog;
using System.Windows;

namespace WPF3.View
{
    /// <summary>
    /// Interaction logic for HorizontalBarrierWindow.xaml
    /// </summary>
    public partial class HorizontalBarrierWindow : Window
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public HorizontalBarrierWindow()
        {
            Logger.Info("HorizontalBarrierWindow InitializeComponent");
            InitializeComponent();

            Logger.Info("HorizontalBarrierWindow Initialize messenger CloseHorizontalBarrier");
            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Notification == "CloseHorizontalBarrier")
                {
                    Logger.Info("HorizontalBarrierWindow Get messeng CloseHorizontalBarrier and close window");
                    Messenger.Default.Unregister<NotificationMessage>(this, "CloseHorizontalBarrier");
                    Close();
                }
            });
        }
    }
}
