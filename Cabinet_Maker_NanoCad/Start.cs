using System;
using System.IO;
using System.Windows.Forms;
using Core;
using Newtonsoft.Json;

namespace Cabinet_Maker_NanoCad
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();

            var names = Directory.GetFiles(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cabinet_Maker"));

            foreach (var name in names)
            {
                listBox1.Items.Add(Path.GetFileNameWithoutExtension(name));
            }

            listBox1.SelectedIndex = 0;
        }

        private void BtnEnd_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var name = listBox1.Items[listBox1.SelectedIndex];
            

            var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cabinet_Maker", name.ToString()+".json");

            var cabinet = File.Exists(filename) ? JsonConvert.DeserializeObject<Cabinet>(File.ReadAllText(filename)) : new Cabinet();

            var ptStart = GetFromNanoCad.GetCoordinates();

            var polylines = MyRectangle.Cabinet(cabinet, ptStart);
            var draw = new Draw();
            draw.DrawObject(polylines);
        }
    }
}
