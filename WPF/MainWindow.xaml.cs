using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Viewport3D_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Viewport3D_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void Viewport3D_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.RightButton == MouseButtonState.Pressed)
            ListBox1.Items.Add(e.GetPosition(this));
        }
    }
}
