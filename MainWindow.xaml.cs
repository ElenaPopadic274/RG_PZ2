using pz2.Helpers;
using pz2.Helpers.Behaviors;
using pz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Point = System.Windows.Point;

namespace pz2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int _zoomMax = 30;
        private Point _diffOffset = new Point();
        private Point _start = new Point();
        private int _zoomCurent = 1;
        private double _prevOffset = 0;
        private readonly Configuration _config;
        private readonly Storage _storage;
        private readonly LineClickBehavior _lineClickBehavior;
        private readonly PowerEntityClickBehavior _powerEntityClickBehavior;

        List<GeometryModel3D> lines1 = new List<GeometryModel3D>();
        List<GeometryModel3D> lines12 = new List<GeometryModel3D>();
        List<GeometryModel3D> lines2 = new List<GeometryModel3D>();


        public MainWindow()
        {
            InitializeComponent();
            _config = new Configuration();
            _storage = new Storage();

            _lineClickBehavior = new LineClickBehavior(_storage);
            _powerEntityClickBehavior = new PowerEntityClickBehavior(this, _storage);

        }
        private void Viewport_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _viewport.CaptureMouse();
            _start = e.GetPosition(this);
            _diffOffset.X = _translateTransform.OffsetX;
            _diffOffset.Y = _translateTransform.OffsetY;

            var mouseposition = e.GetPosition(_viewport);
            var testpoint3D = new Point3D(mouseposition.X, mouseposition.Y, 0);
            var testdirection = new Vector3D(mouseposition.X, mouseposition.Y, 10);

            var pointparams = new PointHitTestParameters(mouseposition);
            var rayparams = new RayHitTestParameters(testpoint3D, testdirection);

            VisualTreeHelper.HitTest(_viewport, null, HTResult, pointparams);
        }

        private void Viewport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _viewport.ReleaseMouseCapture();
        }

        private void Viewport_MouseMove(object sender, MouseEventArgs e)
        {
            if (_viewport.IsMouseCaptured)
            {
                var end = e.GetPosition(this);
                double offsetX = end.X - _start.X;
                double offsetY = end.Y - _start.Y;
                double w = Width;
                double h = Height;
                double translateX = (offsetX * 100) / w;
                double translateY = -(offsetY * 100) / h;

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    _translateTransform.OffsetX = _diffOffset.X + (translateX / (100 * _scaleTransform.ScaleX));
                    _translateTransform.OffsetY = _diffOffset.Y + (translateY / (100 * _scaleTransform.ScaleX));
                }

                if (e.RightButton == MouseButtonState.Pressed)
                {
                    double rotOffset = offsetY > _prevOffset ? translateY : -translateY;
                    _rotateAxisZ.Angle = (_rotateAxisZ.Angle + rotOffset / 10) % 360;
                }
                _prevOffset = offsetY;
            }
        }

        private void Viewport_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var p = e.MouseDevice.GetPosition(this);
            double scaleX = 1;
            double scaleY = 1;
            if (e.Delta > 0 && _zoomCurent < _zoomMax)
            {
                scaleX = _scaleTransform.ScaleX + 0.1;
                scaleY = _scaleTransform.ScaleY + 0.1;
                _zoomCurent++;
                _scaleTransform.ScaleX = scaleX;
                _scaleTransform.ScaleY = scaleY;
            }
            else if (e.Delta <= 0 && _zoomCurent > -_zoomMax)
            {
                scaleX = _scaleTransform.ScaleX - 0.1;
                scaleY = _scaleTransform.ScaleY - 0.1;
                _zoomCurent--;
                _scaleTransform.ScaleX = scaleX;
                _scaleTransform.ScaleY = scaleY;
            }
        }

        private HitTestResultBehavior HTResult(HitTestResult rawresult)
        {
            if (rawresult is RayHitTestResult rayResult)
            {
                var tagProp = rayResult.ModelHit.GetValue(TagProperty);

                if (tagProp is PowerEntity powerEntity)
                {
                    _lineClickBehavior.UndoPrevClick();
                    _powerEntityClickBehavior.OnClick(powerEntity);

                    Console.WriteLine($"Clicked on {powerEntity}");
                }
                else if (tagProp is LineEntity lineEntity)
                {
                    _lineClickBehavior.OnClick(lineEntity);

                    Console.WriteLine($"Clicked on {lineEntity}");
                }
            }

            return HitTestResultBehavior.Stop;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var loader = new GeographicXmlLoader()
            {
                LatitudeRange = _config.LatitudeRange,
                LongitudeRange = _config.LongitudeRange
            };

            StorageFactory.LoadXMLToStorage(loader, _storage);

            var latlonToPlaneMapper = new LatLonToPlaneMapper(_config);
            var powerEntityMapper = new PowerEntityTo3DMapper(latlonToPlaneMapper);
            var lineMapper = new LineEntityTo3DMapper(latlonToPlaneMapper);
            var painter = new Painter3D(_modelGroup, powerEntityMapper, lineMapper);

            painter.DrawEntities(_storage);
        }

        private void Viewport_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _viewport.CaptureMouse();
        }

        private void Viewport_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _viewport.ReleaseMouseCapture();
        }

      

        #region Pokusaj dodatnog 2

        private Dictionary<long, Tuple<string, PowerEntity>> powerEntities = new Dictionary<long, Tuple<string, PowerEntity>>();
        private Dictionary<long, GeometryModel3D> nodes3D = new Dictionary<long, GeometryModel3D>();
        private void HideAndSeek(object sender, SelectionChangedEventArgs e)
        {
            
            string text = Options.SelectedItem.ToString();
            if (text.Contains("1"))
            {
               
                foreach (Tuple<string, PowerEntity> powerEntity in powerEntities.Values)
                {
                    if (powerEntity.Item2.Connections >= 0 && powerEntity.Item2.Connections < 3)
                    {
                        foreach (long Id in nodes3D.Keys)
                        {
                            if (Id == powerEntity.Item2.Id)
                            {
                                if (!_modelGroup.Children.Contains(nodes3D[Id]))
                                {
                                    _modelGroup.Children.Add(nodes3D[Id]);
                                }
                                // nodes3DTemp.Add(Id, nodes3D[Id]);
                            }
                            else
                            {
                                if (_modelGroup.Children.Contains(nodes3D[Id]))
                                {
                                    _modelGroup.Children.Remove(nodes3D[Id]);
                                }
                            }
                        }
                        // AddPowerEntity(powerEntity);
                    }
                    else
                    {
                        foreach (long Id in nodes3D.Keys)
                        {
                            if (Id == powerEntity.Item2.Id)
                            {
                                if (_modelGroup.Children.Contains(nodes3D[Id]))
                                {
                                    _modelGroup.Children.Remove(nodes3D[Id]);
                                }

                            }
                        }
                    }
                }

            }
            else if (text.Contains("2"))
            {
                // Map.Children.Clear();
                foreach (Tuple<string, PowerEntity> powerEntity in powerEntities.Values)
                {
                    if (powerEntity.Item2.Connections >= 3 && powerEntity.Item2.Connections < 5)
                    {
                        foreach (long Id in nodes3D.Keys)
                        {
                            if (Id == powerEntity.Item2.Id)
                            {
                                if (!_modelGroup.Children.Contains(nodes3D[Id]))
                                {
                                    _modelGroup.Children.Add(nodes3D[Id]);
                                }
                                // nodes3DTemp.Add(Id, nodes3D[Id]);
                            }
                            else
                            {
                                if (_modelGroup.Children.Contains(nodes3D[Id]))
                                {
                                    _modelGroup.Children.Remove(nodes3D[Id]);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (long Id in nodes3D.Keys)
                        {
                            if (Id == powerEntity.Item2.Id)
                            {
                                if (_modelGroup.Children.Contains(nodes3D[Id]))
                                {
                                    _modelGroup.Children.Remove(nodes3D[Id]);
                                }

                            }
                        }
                    }
                }
            }
            else if (text.Contains("3"))
            {
                // Map.Children.Clear();
                foreach (Tuple<string, PowerEntity> powerEntity in powerEntities.Values)
                {
                    if (powerEntity.Item2.Connections >= 5)
                    {
                        foreach (long Id in nodes3D.Keys)
                        {
                            if (Id == powerEntity.Item2.Id)
                            {
                                if (!_modelGroup.Children.Contains(nodes3D[Id]))
                                {
                                    _modelGroup.Children.Add(nodes3D[Id]);
                                }
                                // nodes3DTemp.Add(Id, nodes3D[Id]);
                            }
                            else
                            {
                                if (_modelGroup.Children.Contains(nodes3D[Id]))
                                {
                                    _modelGroup.Children.Remove(nodes3D[Id]);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (long Id in nodes3D.Keys)
                        {
                            if (Id == powerEntity.Item2.Id)
                            {
                                if (_modelGroup.Children.Contains(nodes3D[Id]))
                                {
                                    _modelGroup.Children.Remove(nodes3D[Id]);
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                foreach (GeometryModel3D model in nodes3D.Values)
                {
                    if (_modelGroup.Children.Contains(model))
                    {
                        _modelGroup.Children.Remove(model);
                    }
                }
                nodes3D.Clear();
                // Map.Children.Clear();
                /*
                foreach (var entity in powerEntities.Values)
                {
                    Storage.AddRange(entity);
                }
              */
            }
            
    
        }
        #endregion

        #region Boje Vodova
        private void LinesColor_Checked(object sender, RoutedEventArgs e)
        {
            
            foreach (GeometryModel3D model in lines1)
            {
                model.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Red));
                _modelGroup.Children.Add(model);
            }
            foreach (GeometryModel3D model in lines12)
            {
                model.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Orange));
                _modelGroup.Children.Add(model);
            }
            foreach (GeometryModel3D model in lines2)
            {
                model.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Yellow));
                _modelGroup.Children.Add(model);
            }
        }

        private void LinesColor_Unchecked(object sender, RoutedEventArgs e)
        {

            foreach (GeometryModel3D model in lines1)
            {
                model.Material = new DiffuseMaterial(new SolidColorBrush(Colors.DarkBlue));
                _modelGroup.Children.Add(model);
            }
            foreach (GeometryModel3D model in lines12)
            {
                model.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Red));
                _modelGroup.Children.Add(model);
            }
            foreach (GeometryModel3D model in lines2)
            {
                model.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Gold));
                _modelGroup.Children.Add(model);
            }
        }
        #endregion
    }
}
