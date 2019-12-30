using System.Collections.Generic;
using System.Linq;
using CoreS.Enum;
using CoreS.Model;
using Teigha.DatabaseServices;
using Teigha.Geometry;

namespace Cabinet_Maker_NanoCad
{
    public static class MyDimension
    {
        public static List<AlignedDimension> CabinetDimension(List<ElementModel> cabinetCabinetElements, Point3d ptStart)
        {
            var leftside = cabinetCabinetElements.FirstOrDefault(c => c.GetEnumName() == EnumCabinetElement.Leftside);
            var rightside = cabinetCabinetElements.FirstOrDefault(c => c.GetEnumName() == EnumCabinetElement.Rightside);
            
            return new List<AlignedDimension>
            {
                new AlignedDimension
                {
                    XLine1Point = new Point3d(ptStart.X + leftside.GetX()+leftside.GetWidth(),ptStart.Y + leftside.GetY(),0),
                    XLine2Point = new Point3d(ptStart.X + rightside.GetX(),ptStart.Y + rightside.GetY(),0),
                    DimLinePoint = new Point3d(ptStart.X + leftside.GetX(),ptStart.Y + leftside.GetY() - 80,0)
                },

                new AlignedDimension
                {
                    XLine1Point = new Point3d(ptStart.X + leftside.GetX(),ptStart.Y + leftside.GetY(),0),
                    XLine2Point = new Point3d(ptStart.X + rightside.GetX() + rightside.GetWidth(),ptStart.Y + rightside.GetY(),0),
                    DimLinePoint = new Point3d(ptStart.X + leftside.GetX(),ptStart.Y + leftside.GetY() - 160,0)
                },

                new AlignedDimension
                {
                    XLine1Point = new Point3d(ptStart.X + rightside.GetX() + rightside.GetWidth(),ptStart.Y + rightside.GetY(),0),
                    XLine2Point = new Point3d(ptStart.X + rightside.GetX() + rightside.GetWidth(),ptStart.Y + rightside.GetY() + rightside.GetHeight(),0),
                    DimLinePoint = new Point3d(ptStart.X + rightside.GetX() + rightside.GetWidth() + 80,ptStart.Y + rightside.GetY(),0)
                }
            };
        }

        public static List<AlignedDimension> VerticalBarrier(List<ElementModel> elements, Point3d ptStart)
        {
            var _dimensionList = new List<AlignedDimension>();

            foreach (var item in elements)
            {
                _dimensionList.Add(
                    new AlignedDimension
                    {
                        XLine1Point = new Point3d(ptStart.X + item.GetX() + item.GetWidth(), ptStart.Y + item.GetY(), 0),
                        XLine2Point = new Point3d(ptStart.X + item.GetX() + item.GetWidth(), ptStart.Y + item.GetY() + item.GetHeight(),0),
                        DimLinePoint = new Point3d(ptStart.X + item.GetX() + item.GetWidth() + 80,ptStart.Y + item.GetY(), 0)
                    });
            };

            return _dimensionList;
        }

        public static List<AlignedDimension> HorizontalBarrier(List<ElementModel> elements, Point3d ptStart)
        {
            var _dimensionList = new List<AlignedDimension>();

            foreach (var item in elements)
            {
                _dimensionList.Add(
                    new AlignedDimension
                    {
                        XLine1Point = new Point3d(ptStart.X + item.GetX(), ptStart.Y + item.GetY(), 0),
                        XLine2Point = new Point3d(ptStart.X + item.GetX() + item.GetWidth(), ptStart.Y + item.GetY(),0),
                        DimLinePoint = new Point3d(ptStart.X + item.GetX(),ptStart.Y + item.GetY()-80, 0)
                    });
            };

            return _dimensionList;
        }

        public static List<AlignedDimension> Front(List<ElementModel> elements, Point3d ptStart)
        {
            var _dimensionList = new List<AlignedDimension>();

            foreach (var item in elements)
            {
                _dimensionList.Add(
                    new AlignedDimension
                    {
                        XLine1Point = new Point3d(ptStart.X + item.GetX(), ptStart.Y + item.GetY(), 0),
                        XLine2Point = new Point3d(ptStart.X + item.GetX() + item.GetWidth(), ptStart.Y + item.GetY(), 0),
                        DimLinePoint = new Point3d(ptStart.X + item.GetX(), ptStart.Y + item.GetY() + 80, 0)
                    });
                _dimensionList.Add(
                    new AlignedDimension
                    {
                        XLine1Point = new Point3d(ptStart.X + item.GetX() + item.GetWidth(), ptStart.Y + item.GetY(), 0),
                        XLine2Point = new Point3d(ptStart.X + item.GetX() + item.GetWidth(), ptStart.Y + item.GetY() + item.GetHeight(), 0),
                        DimLinePoint = new Point3d(ptStart.X + item.GetX()+item.GetWidth()-80, ptStart.Y + item.GetY(), 0)
                    });
            };

            return _dimensionList;
        }
    }
}