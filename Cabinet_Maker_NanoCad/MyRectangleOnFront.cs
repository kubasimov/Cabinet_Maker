using CoreS.Model;
using System.Collections.Generic;
using Teigha.DatabaseServices;
using Teigha.Geometry;

namespace Cabinet_Maker_NanoCad
{
    public static class MyRectangleOnFront
    {
        private static Polyline Get(double startx, double starty, int szerokosc, int wysokosc,bool poziom)
        {
            if (poziom)
            {
                var tmp = szerokosc;
                szerokosc = wysokosc;
                wysokosc = tmp;
            }

            var acPoly = new Polyline();
            acPoly.SetDatabaseDefaults();
            acPoly.AddVertexAt(0, new Point2d(startx, starty), 0, 0, 0);
            acPoly.AddVertexAt(1, new Point2d(startx + szerokosc, starty), 0, 0, 0);
            acPoly.AddVertexAt(2, new Point2d(startx + szerokosc, starty + wysokosc), 0, 0, 0);
            acPoly.AddVertexAt(3, new Point2d(startx, starty + wysokosc), 0, 0, 0);
            acPoly.Closed = true;

            return acPoly;
        }

        public static List<Polyline> GetPolylineListFromElementModelList(List<ElementModel> elements, Point3d start)
        {
            List<Polyline> poly = new List<Polyline>();

            foreach (var t in elements)
            {
                poly.Add(Get(t.X + start.X, t.Y + start.Y, t.Width, t.Height,t.Horizontal));
            }

            return poly;
        }
    }
}