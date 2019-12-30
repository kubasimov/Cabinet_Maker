﻿using System.Collections.Generic;
using CoreS.Model;
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

        public static List<Polyline> GetPolylineListFromElementModelList(List<ElementModel> cabinet, Point3d start)
        {
            List<Polyline> poly = new List<Polyline>();

            foreach (var t in cabinet)
            {
                poly.Add(Get(t.GetX() + start.X, t.GetY() + start.Y, t.GetWidth(), t.GetHeight(),t.GetHorizontal()));
            }

            return poly;
        }
    }
}