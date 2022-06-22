using pz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace pz2.Helpers
{
    public class LineEntityTo3DMapper
    {
        private readonly IPlaneMapper _mapper;

        public LineEntityTo3DMapper(IPlaneMapper mapper)
        {
            _mapper = mapper;
        }

        public Brush Brush { get; set; } //= Brushes.DarkRed;
        public double LineWidth { get; set; } = 0.0005;

        public List<GeometryModel3D> MapTo3D(LineEntity entity)
        {
            var verts = entity.Vertices;
            var geometryLines = new List<GeometryModel3D>();

            for (int i = 0; i < verts.Count - 1; ++i)
            {
                var first = MapToPlanePoint3D(verts[i]);
                var second = MapToPlanePoint3D(verts[i + 1]);
                var line3d = Make3DLine(first, second, tooltip: entity, CreateBrush(entity.ConductorMaterial));
                geometryLines.Add(line3d);

             
            }

            return geometryLines;
        }

        private Point3D MapToPlanePoint3D(Point vertice)
        {
            double planeX = _mapper.MapLongitudeToPlaneX(vertice.Y);
            double planeY = _mapper.MapLatitudeToPlaneY(vertice.X);
            return new Point3D(planeX, planeY, z: LineWidth);
        }

        private GeometryModel3D Make3DLine(Point3D start, Point3D end, LineEntity tooltip, Brush brush)
        {
            var vecDiff = end - start;

            var nVector = Vector3D.CrossProduct(vecDiff, new Vector3D(0, 0, 1));
            nVector = Vector3D.Divide(nVector, nVector.Length);
            nVector = Vector3D.Multiply(nVector, LineWidth);

            var points = new Point3DCollection()
            {
                start - nVector,
                start + nVector,
                end + nVector,
                end - nVector
            };

            var meshGeometry = new MeshGeometry3D
            {
                Positions = points,
                TriangleIndices = Indices.Square
            };

            var model = new GeometryModel3D
            {
                Material = new DiffuseMaterial(brush),
                Geometry = meshGeometry
            };

            model.SetValue(System.Windows.FrameworkElement.TagProperty, tooltip);
            //var material = new DiffuseMaterial(brush);
            return model;
        }
        private static Brush CreateBrush(string material)
        {
            switch (material)
            {
                case "Steel":
                    return Brushes.DarkBlue;
                case "Copper":
                    return Brushes.Red;
                case "Acsr":
                    return Brushes.Gold;
                default:
                    return Brushes.Black;

            }
        }
      
    }
}
