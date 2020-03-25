using GalaSoft.MvvmLight.Messaging;
using NLog;
using System.Windows;

namespace WPF3.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            Logger.Info("MainWindow InitializeComponent");
            InitializeComponent();

            Logger.Info("MainWindow Initialize messenger CloseMain");
            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Notification == "CloseMain")
                {
                    Logger.Info("MainWindow Get messeng CloseMain and close window");
                    Messenger.Default.Unregister<NotificationMessage>(this, "CloseMain");
                    Close();
                }
            });
        }
                
    }
}
