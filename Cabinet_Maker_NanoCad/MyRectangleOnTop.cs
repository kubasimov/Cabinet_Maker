using System.Collections.Generic;
using System.Linq;
using CoreS.Enum;
using CoreS.Model;
using Teigha.DatabaseServices;
using Teigha.Geometry;

namespace Cabinet_Maker_NanoCad
{
    internal static class MyRectangleOnTop
    {
        private static Polyline Get(double startx, double starty, int szerokosc, int wysokosc)
        {
            var acPoly = new Polyline();
            acPoly.SetDatabaseDefaults();
            acPoly.AddVertexAt(0, new Point2d(startx, starty), 0, 0, 0);
            acPoly.AddVertexAt(1, new Point2d(startx + szerokosc, starty), 0, 0, 0);
            acPoly.AddVertexAt(2, new Point2d(startx + szerokosc, starty + wysokosc), 0, 0, 0);
            acPoly.AddVertexAt(3, new Point2d(startx, starty + wysokosc), 0, 0, 0);
            acPoly.Closed = true;

            return acPoly;
        }


        public static List<Polyline> GetPolylineListFromCabinetElements(List<ElementModel> cabinetElements, Point3d ptStart)
        {
            var leftSide = cabinetElements.FirstOrDefault(c => c.EName == EnumCabinetElement.Leftside);
            var rightSide = cabinetElements.FirstOrDefault(c => c.EName == EnumCabinetElement.Rightside);
            var top = cabinetElements.FirstOrDefault(c => c.EName == EnumCabinetElement.Top);

            var poly = new List<Polyline>
            {
                Get(ptStart.X, ptStart.Y, leftSide.EWidth, leftSide.EDepth),
                Get(ptStart.X + rightSide.Ex,ptStart.Y,rightSide.EWidth,rightSide.EDepth),
                Get(ptStart.X + top.Ex,ptStart.Y,top.EWidth,top.EDepth)
            };
            
            return poly;
        }



        public static List<Polyline> GetPolylineListFromFrontList(List<ElementModel> frontList, Point3d ptStart)
        {
            var poly = new List<Polyline>();

            foreach (var elementModel in frontList)
            {
                poly.Add(Get(ptStart.X + elementModel.Ex, ptStart.Y - elementModel.Ey-elementModel.EDepth, elementModel.EWidth,
                    elementModel.EDepth));
            }
            
            return poly;
        }
    }
}