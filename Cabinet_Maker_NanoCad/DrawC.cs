using CoreS;

namespace Cabinet_Maker_NanoCad
{
    class DrawC
    {
        public void drawC(Cabinet cabinet)
        {
            var ptStart = GetFromNanoCad.GetCoordinates();

            var draw = new Draw();

            
                var polylinesKorpus = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.CabinetElements, ptStart);
                var polylinesVertcalBarrier = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.VerticalBarrier, ptStart);
                var polylinesHorizontalBarrier = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.HorizontalBarrier, ptStart);
                var polylinesFront = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.GetFrontList(), ptStart);


                draw.DrawObjectsFromPolylineList(polylinesKorpus, "Korpus");
                draw.DrawObjectsFromPolylineList(polylinesVertcalBarrier, "Korpus");
                draw.DrawObjectsFromPolylineList(polylinesHorizontalBarrier, "Korpus");
                draw.DrawObjectsFromPolylineList(polylinesFront, "Fronty");

               
            
        }
    }
}
