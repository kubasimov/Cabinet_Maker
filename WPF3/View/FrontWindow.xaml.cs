using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;
using NLog;

namespace WPF3.View
{
    /// <summary>
    /// Interaction logic for FrontWindow.xaml
    /// </summary>
    public partial class FrontWindow : Window
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public FrontWindow()
        {
            Logger.Info("FrontWindow InitializeComponent");
            InitializeComponent();

            Logger.Info("FrontWindow Initialize messenger CloseFront");
            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Notification == "CloseFront")
                {
                    Logger.Info("FrontWindow Get messeng CloseFront and close window");
                    Messenger.Default.Unregister<NotificationMessage>(this, "CloseFront");
                    Close();
                }
            });
        }
    }
}
