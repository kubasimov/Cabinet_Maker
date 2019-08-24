using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Core.Model;
using Core;
using Teigha.DatabaseServices;
using Teigha.Geometry;

namespace Cabinet_Maker_NanoCad
{
    public static class MyDimension
    {
        public static List<AlignedDimension> CabinetDimension(List<ElementModel> cabinetCabinetElements, Point3d ptStart)
        {
            var leftside = cabinetCabinetElements.FirstOrDefault(c => c.EName == EnumCabinetElement.Leftside);
            var rightside = cabinetCabinetElements.FirstOrDefault(c => c.EName == EnumCabinetElement.Rightside);
            
            return new List<AlignedDimension>
            {
                new AlignedDimension
                {
                    XLine1Point = new Point3d(ptStart.X + leftside.Ex+leftside.EWidth,ptStart.Y + leftside.Ey,0),
                    XLine2Point = new Point3d(ptStart.X + rightside.Ex,ptStart.Y + rightside.Ey,0),
                    DimLinePoint = new Point3d(ptStart.X + leftside.Ex,ptStart.Y + leftside.Ey - 80,0)
                },

                new AlignedDimension
                {
                    XLine1Point = new Point3d(ptStart.X + leftside.Ex,ptStart.Y + leftside.Ey,0),
                    XLine2Point = new Point3d(ptStart.X + rightside.Ex + rightside.EWidth,ptStart.Y + rightside.Ey,0),
                    DimLinePoint = new Point3d(ptStart.X + leftside.Ex,ptStart.Y + leftside.Ey - 160,0)
                },

                new AlignedDimension
                {
                    XLine1Point = new Point3d(ptStart.X + rightside.Ex + rightside.EWidth,ptStart.Y + rightside.Ey,0),
                    XLine2Point = new Point3d(ptStart.X + rightside.Ex + rightside.EWidth,ptStart.Y + rightside.Ey + rightside.EHeight,0),
                    DimLinePoint = new Point3d(ptStart.X + rightside.Ex + rightside.EWidth + 80,ptStart.Y + rightside.Ey,0)
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
                        XLine1Point = new Point3d(ptStart.X + item.Ex + item.EWidth, ptStart.Y + item.Ey, 0),
                        XLine2Point = new Point3d(ptStart.X + item.Ex + item.EWidth, ptStart.Y + item.Ey + item.EHeight,0),
                        DimLinePoint = new Point3d(ptStart.X + item.Ex + item.EWidth + 80,ptStart.Y + item.Ey, 0)
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
                        XLine1Point = new Point3d(ptStart.X + item.Ex, ptStart.Y + item.Ey, 0),
                        XLine2Point = new Point3d(ptStart.X + item.Ex + item.EWidth, ptStart.Y + item.Ey,0),
                        DimLinePoint = new Point3d(ptStart.X + item.Ex,ptStart.Y + item.Ey-80, 0)
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
                        XLine1Point = new Point3d(ptStart.X + item.Ex, ptStart.Y + item.Ey, 0),
                        XLine2Point = new Point3d(ptStart.X + item.Ex + item.EWidth, ptStart.Y + item.Ey, 0),
                        DimLinePoint = new Point3d(ptStart.X + item.Ex, ptStart.Y + item.Ey + 80, 0)
                    });
                _dimensionList.Add(
                    new AlignedDimension
                    {
                        XLine1Point = new Point3d(ptStart.X + item.Ex + item.EWidth, ptStart.Y + item.Ey, 0),
                        XLine2Point = new Point3d(ptStart.X + item.Ex + item.EWidth, ptStart.Y + item.Ey + item.EHeight, 0),
                        DimLinePoint = new Point3d(ptStart.X + item.Ex+item.EWidth-80, ptStart.Y + item.Ey, 0)
                    });
            };

            return _dimensionList;
        }
    }
}