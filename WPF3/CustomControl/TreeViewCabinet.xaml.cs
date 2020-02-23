using CoreS;
using CoreS.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
            public ObservableCollection<ElementModelDTO> elementModels { get; }

            public Elements(string name)
            {
                Name = name;
                elementModels = new ObservableCollection<ElementModelDTO>();
            }

            
        }
        
        public ElementModelDTO MElement { get; set; }
    }
}
