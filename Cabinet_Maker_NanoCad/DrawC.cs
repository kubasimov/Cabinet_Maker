using CoreS;
using CoreS.Model;

namespace Cabinet_Maker_NanoCad
{
    static class DrawC
    {
        public static void DrawFront(CabinetModelDTO cabinet)
        {
            var ptStart = GetFromNanoCad.GetCoordinates();
            var draw = new Draw();
                        
            var polylinesKorpus = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.CabinetElements, ptStart);
            var polylinesVertcalBarrier = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.VerticalBarrier, ptStart);
            var polylinesHorizontalBarrier = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.HorizontalBarrier, ptStart);
            var polylinesFront = MyRectangleOnFront.GetPolylineListFromElementModelList(cabinet.FrontList, ptStart);
                       
            draw.DrawObjectsFromPolylineList(polylinesKorpus, "Korpus");
            draw.DrawObjectsFromPolylineList(polylinesVertcalBarrier, "Korpus");
            draw.DrawObjectsFromPolylineList(polylinesHorizontalBarrier, "Korpus");
            draw.DrawObjectsFromPolylineList(polylinesFront, "Fronty");
        }

        public static void DrawTop(CabinetModelDTO cabinet)
        {
            var ptStart = GetFromNanoCad.GetCoordinates();
            var draw = new Draw();

            var polylinesKorpus = MyRectangleOnTop.GetPolylineListFromCabinetElements(cabinet.CabinetElements, ptStart);
            var polylinesFront = MyRectangleOnTop.GetPolylineListFromFrontList(cabinet.FrontList, ptStart);
            
            draw.DrawObjectsFromPolylineList(polylinesKorpus, "Korpus");
            draw.DrawObjectsFromPolylineList(polylinesFront, "Fronty");
        }
    }
}
