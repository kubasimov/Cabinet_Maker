using GalaSoft.MvvmLight.Messaging;
using NLog;
using System.Windows;

namespace WPF3.View
{
    /// <summary>
    /// Interaction logic for VerticalBarrierWindow.xaml
    /// </summary>
    public partial class VerticalBarrierWindow : Window
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public VerticalBarrierWindow()
        {
            Logger.Info("VerticalBarrierWindow InitializeComponent");
            InitializeComponent();

            Logger.Info("VerticalBarrierWindow Initialize messenger CloseVerticalBarrier");
            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Notification == "CloseVerticalBarrier")
                {
                    Logger.Info("HorizontalVerticalBarrierWindowBarrierWindow Get messeng CloseVerticalBarrier and close window");
                    Messenger.Default.Unregister<NotificationMessage>(this, "CloseVerticalBarrier");
                    Close();
                }
            });
        }
    }
}
