using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;

namespace WPF3.View
{
    /// <summary>
    /// Interaction logic for FrontWindow.xaml
    /// </summary>
    public partial class FrontWindow : Window
    {
        public FrontWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Notification == "CloseFront")
                {
                    Messenger.Default.Unregister<NotificationMessage>(this, "CloseFront");
                    Close();
                }
            });
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
