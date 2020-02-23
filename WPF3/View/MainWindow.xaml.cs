using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace WPF3.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Notification == "CloseMain")
                {
                    Messenger.Default.Unregister<NotificationMessage>(this, "CloseMain");
                    Close();
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
