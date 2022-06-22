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
    public class StorageCell
    {
        public PowerEntity PowerEntity { get; set; }
        public GeometryModel3D Model3D { get; set; }

        private int _numberOfConnections;

        public int NumberOfConnections
        {
            get => _numberOfConnections;
            set
            {
                _numberOfConnections = value;

                if (Model3D == null)
                    return;

                UpdateModelColor();
            }
        }

        private bool _highlighted;

        public bool Highlighted
        {
            get => _highlighted;
            set
            {
                _highlighted = value;
                UpdateModelColor();
            }
        }

        public void UpdateModelColor()
        {
            if (_highlighted)
            {
                Model3D.Material = new DiffuseMaterial(Brushes.Green);
            }
            else if (_numberOfConnections < 3)
            {
                Model3D.Material = new DiffuseMaterial(Brushes.PaleVioletRed);
            }
            else if (_numberOfConnections <= 5)
            {
                Model3D.Material = new DiffuseMaterial(Brushes.MediumVioletRed);
            }
            else
            {
                Model3D.Material = new DiffuseMaterial(Brushes.Red);
            }
        }
    }
}
