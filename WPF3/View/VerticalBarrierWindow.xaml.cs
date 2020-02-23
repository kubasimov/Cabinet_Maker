using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace WPF3.View
{
    /// <summary>
    /// Interaction logic for VerticalBarrierWindow.xaml
    /// </summary>
    public partial class VerticalBarrierWindow : Window
    {
        public VerticalBarrierWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Notification == "CloseVerticalBarrier")
                {
                    Messenger.Default.Unregister<NotificationMessage>(this, "CloseVerticalBarrier");
                    Close();
                }
            });
        }
    }
}
