using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace WPF3.View
{
    /// <summary>
    /// Interaction logic for HorizontalBarrierWindow.xaml
    /// </summary>
    public partial class HorizontalBarrierWindow : Window
    {
        public HorizontalBarrierWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Notification == "CloseHorizontalBarrier")
                {
                    Messenger.Default.Unregister<NotificationMessage>(this, "CloseHorizontalBarrier");
                    Close();
                }
            });
        }
    }
}
