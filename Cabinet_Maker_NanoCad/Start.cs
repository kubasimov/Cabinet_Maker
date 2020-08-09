using CoreS;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace Cabinet_Maker_NanoCad
{
    public partial class Start : Form
    {
        private Cabinet cabinet;

        public Start()
        {
            InitializeComponent();

            var names = Directory.GetFiles(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CabinetMaker"));

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

            var dimension = checkBox1.Checked;

            var _orientationDraw = orientationDraw();
            
            var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CabinetMaker", name.ToString()+".json");

            if(File.Exists(filename))
            {
                cabinet = JsonConvert.DeserializeObject<Cabinet>(File.ReadAllText(filename));
            }
            else
            {
                return;
            }
            
            var ptStart = GetFromNanoCad.GetCoordinates();

            var draw = new Draw();

            if (_orientationDraw == 0)
            {
                var polylinesKorpus = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.CabinetElements, ptStart);
                var polylinesVertcalBarrier = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.VerticalBarrier, ptStart);
                var polylinesHorizontalBarrier = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.HorizontalBarrier, ptStart);
                var polylinesFront = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.GetAllFront(), ptStart);

                
                draw.DrawObjectsFromPolylineList(polylinesKorpus, "Korpus");
                draw.DrawObjectsFromPolylineList(polylinesVertcalBarrier, "Korpus");
                draw.DrawObjectsFromPolylineList(polylinesHorizontalBarrier, "Korpus");
                draw.DrawObjectsFromPolylineList(polylinesFront, "Fronty");

                if (dimension)
                {
                    var alignedCabinetDimensions = MyDimension.CabinetDimension(cabinet.CabinetElements, ptStart);
                    var alignedVerticalBarrierDimension = MyDimension.VerticalBarrier(cabinet.VerticalBarrier, ptStart);
                    var alignedHorizontalBarrierDimension = MyDimension.HorizontalBarrier(cabinet.HorizontalBarrier, ptStart);
                    var alignedFrontDimension = MyDimension.Front(cabinet.GetAllFront(), ptStart);

                    draw.DrawDimensionList(alignedCabinetDimensions, "Wymiary_korpusu");
                    draw.DrawDimensionList(alignedVerticalBarrierDimension, "Wymiary_korpusu");
                    draw.DrawDimensionList(alignedHorizontalBarrierDimension, "Wymiary_korpusu");
                    draw.DrawDimensionList(alignedFrontDimension, "Wymiary frontow");
                }
            }
            else if (_orientationDraw == 1)
            {
                var polylinesKorpus = MyRectangleOnTop.GetPolylineListFromCabinetElements(cabinet.CabinetElements, ptStart);
                var polylinesFront = MyRectangleOnTop.GetPolylineListFromFrontList(cabinet.GetAllFront(), ptStart);


                draw.DrawObjectsFromPolylineList(polylinesKorpus, "Korpus");
                draw.DrawObjectsFromPolylineList(polylinesFront, "Fronty");

            }



            
        }

        private int orientationDraw()
        {
            if (radioButton1.Checked) return 0;
            if (radioButton2.Checked) return 1;
            return 0;
        }
    }
}
