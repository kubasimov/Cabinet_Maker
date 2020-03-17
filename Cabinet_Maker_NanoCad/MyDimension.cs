using CoreS.Enum;
using CoreS.Model;
using System.Collections.Generic;
using System.Linq;
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
                    XLine1Point = new Point3d(ptStart.X + leftside.X+leftside.Width,ptStart.Y + leftside.Y,0),
                    XLine2Point = new Point3d(ptStart.X + rightside.X,ptStart.Y + rightside.Y,0),
                    DimLinePoint = new Point3d(ptStart.X + leftside.X,ptStart.Y + leftside.Y - 80,0)
                },

                new AlignedDimension
                {
                    XLine1Point = new Point3d(ptStart.X + leftside.X,ptStart.Y + leftside.Y,0),
                    XLine2Point = new Point3d(ptStart.X + rightside.X + rightside.Width,ptStart.Y + rightside.Y,0),
                    DimLinePoint = new Point3d(ptStart.X + leftside.X,ptStart.Y + leftside.Y - 160,0)
                },

                new AlignedDimension
                {
                    XLine1Point = new Point3d(ptStart.X + rightside.X + rightside.Width,ptStart.Y + rightside.Y,0),
                    XLine2Point = new Point3d(ptStart.X + rightside.X + rightside.Width,ptStart.Y + rightside.Y + rightside.Height,0),
                    DimLinePoint = new Point3d(ptStart.X + rightside.X + rightside.Width + 80,ptStart.Y + rightside.Y,0)
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
                        XLine1Point = new Point3d(ptStart.X + item.X + item.Width, ptStart.Y + item.Y, 0),
                        XLine2Point = new Point3d(ptStart.X + item.X + item.Width, ptStart.Y + item.Y + item.Height,0),
                        DimLinePoint = new Point3d(ptStart.X + item.X + item.Width + 80,ptStart.Y + item.Y, 0)
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
                        XLine1Point = new Point3d(ptStart.X + item.X, ptStart.Y + item.Y, 0),
                        XLine2Point = new Point3d(ptStart.X + item.X + item.Width, ptStart.Y + item.Y,0),
                        DimLinePoint = new Point3d(ptStart.X + item.X,ptStart.Y + item.Y-80, 0)
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
                        XLine1Point = new Point3d(ptStart.X + item.X, ptStart.Y + item.Y, 0),
                        XLine2Point = new Point3d(ptStart.X + item.X + item.Width, ptStart.Y + item.Y, 0),
                        DimLinePoint = new Point3d(ptStart.X + item.X, ptStart.Y + item.Y + 80, 0)
                    });
                _dimensionList.Add(
                    new AlignedDimension
                    {
                        XLine1Point = new Point3d(ptStart.X + item.X + item.Width, ptStart.Y + item.Y, 0),
                        XLine2Point = new Point3d(ptStart.X + item.X + item.Width, ptStart.Y + item.Y + item.Height, 0),
                        DimLinePoint = new Point3d(ptStart.X + item.X+item.Width-80, ptStart.Y + item.Y, 0)
                    });
            };

            return _dimensionList;
        }
    }
}