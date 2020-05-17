using CoreS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WPF3.ViewModel
{
    public partial class MainViewModel
    {
        private Model3D CreateCabinet()
        {
            Logger.Info("Create Model3D Cabinet in MainViewModel");
            GeometryModel3D myGeometryModel = new GeometryModel3D();

            MeshGeometry3D myMeshGeometry3D = new MeshGeometry3D();

            Int32Collection myTriangleIndicesCollection = new Int32Collection();

            foreach (var element in _cabinet.CabinetElements)
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }

            foreach (var element in _cabinet.HorizontalBarrier)
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }

            foreach (var element in _cabinet.VerticalBarrier)
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }

            foreach (var element in _cabinet.FrontList)
            {
                Element3D element1 = GetElement3DFromElementModel(element);
                AddElementToModel3D(element1, ref myMeshGeometry3D, ref myTriangleIndicesCollection);
            }

            //TODO add dodatkowe eleemnty

            myMeshGeometry3D.TriangleIndices = myTriangleIndicesCollection;

            myGeometryModel.Geometry = myMeshGeometry3D;

            SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Blue);

            // Define material that will use the gradient.
            DiffuseMaterial myMaterial = new DiffuseMaterial(solidColorBrush);

            myGeometryModel.Material = myMaterial;

            Logger.Info("Create CabinetView in Model3DCabinet/MainViewModel");
            GenerateCabinetView();

            return myGeometryModel;
        }

        private Point3DCollection CubeToPoint3DCollection(double height, double width, double depth, double x, double y, double z, bool horizontal)
        {
            if (horizontal)
            {
                var r = height;
                height = width;
                width = r;
            }

            return new Point3DCollection
            {
                new Point3D(x, y, z),
                new Point3D(x + width, y, z),
                new Point3D(x, y + height, z),
                new Point3D(x + width, y + height, z),
                new Point3D(x, y, z + depth),
                new Point3D(x + width, y, z + depth),
                new Point3D(x, y + height, z + depth),
                new Point3D(x + width, y + height, z + depth)
            };
        }

        private void AddElementToModel3D(Element3D element, ref MeshGeometry3D myMeshGeometry3D, ref Int32Collection myTriangleIndicesCollection)
        {
            var items = CubeToPoint3DCollection(element.EHeight, element.EWidth, element.EDepth, element.Ex, element.Ey, element.Ez, element.Horizontal);

            foreach (Point3D point3D in items)
            {
                myMeshGeometry3D.Positions.Add(point3D);
            }

            var y = myMeshGeometry3D.Positions.Count - 8;

            myTriangleIndicesCollection.Add(y);
            myTriangleIndicesCollection.Add(y + 2);
            myTriangleIndicesCollection.Add(y + 1);

            myTriangleIndicesCollection.Add(y + 1);
            myTriangleIndicesCollection.Add(y + 2);
            myTriangleIndicesCollection.Add(y + 3);

            myTriangleIndicesCollection.Add(y);
            myTriangleIndicesCollection.Add(y + 4);
            myTriangleIndicesCollection.Add(y + 6);

            myTriangleIndicesCollection.Add(y);
            myTriangleIndicesCollection.Add(y + 6);
            myTriangleIndicesCollection.Add(y + 2);

            myTriangleIndicesCollection.Add(y + 1);
            myTriangleIndicesCollection.Add(y + 3);
            myTriangleIndicesCollection.Add(y + 7);

            myTriangleIndicesCollection.Add(y + 1);
            myTriangleIndicesCollection.Add(y + 7);
            myTriangleIndicesCollection.Add(y + 5);

            myTriangleIndicesCollection.Add(y + 4);
            myTriangleIndicesCollection.Add(y + 5);
            myTriangleIndicesCollection.Add(y + 6);

            myTriangleIndicesCollection.Add(y + 5);
            myTriangleIndicesCollection.Add(y + 7);
            myTriangleIndicesCollection.Add(y + 6);

            myTriangleIndicesCollection.Add(y + 7);
            myTriangleIndicesCollection.Add(y + 3);
            myTriangleIndicesCollection.Add(y + 2);

            myTriangleIndicesCollection.Add(y + 6);
            myTriangleIndicesCollection.Add(y + 7);
            myTriangleIndicesCollection.Add(y + 2);

            myTriangleIndicesCollection.Add(y);
            myTriangleIndicesCollection.Add(y + 1);
            myTriangleIndicesCollection.Add(y + 5);

            myTriangleIndicesCollection.Add(y);
            myTriangleIndicesCollection.Add(y + 5);
            myTriangleIndicesCollection.Add(y + 4);
        }

        private Model3D CreateLight()
        {
            Logger.Info("Create Light in MainViewModel");
            return new DirectionalLight
            {
                Color = Colors.White,
                Direction = new Vector3D(-5, -5, -5)
            };
        }

        private Element3D GetElement3DFromElementModel(ElementModel element)
        {
            return new Element3D
            {
                EWidth = (double)element.Width / 100,
                EHeight = (double)element.Height / 100,
                EDepth = (double)element.Depth / 100,
                EName = element.GetEnumName(),
                Ex = (double)element.X / 100,
                Ey = (double)element.Y / 100,
                Ez = (double)element.Z / 100,
                Description = element.Description,
                Horizontal = element.Horizontal
            };
        }
    }
}
