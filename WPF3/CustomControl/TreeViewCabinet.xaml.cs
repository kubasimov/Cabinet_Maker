using CoreS;
using CoreS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WPF3.CustomControl
{
    /// <summary>
    /// Interaction logic for TreeViewCabinet.xaml
    /// </summary>
    public partial class TreeViewCabinet : UserControl
    {
        public TreeViewCabinet()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public Cabinet Cabinet
        {
            get => (Cabinet)GetValue(CabinetProperty);
            set => SetValue(CabinetProperty, value);
        }
        public static readonly DependencyProperty CabinetProperty =
                    DependencyProperty.Register("Cabinet", typeof(Cabinet),
                        typeof(TreeViewCabinet), new PropertyMetadata(null));



        public ObservableCollection<Elements> CabinetView
        {
            get; set;
        }
        public string boopol { get; set; }


        public class Elements
        {
            public string Name { get; }
            public ObservableCollection<ElementModel> elementModels { get; }

            public Elements(string name)
            {
                Name = name;
                elementModels = new ObservableCollection<ElementModel>();
            }

            
        }
        
        public ElementModel MElement { get; set; }
    }
}
