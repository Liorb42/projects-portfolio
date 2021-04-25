using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace MyGame
{
    public class ObjectsOnCanvas
    {
        #region Properties
        Image _image;
        double _height;
        double _width;
        double _top;
        double _left;
        string _uri;

        public double Height
        {
            get { return _image.ActualHeight; }
            set { _image.Height = value; }
        }
        public double Width
        {
            get { return _image.ActualWidth; }
            set { _image.Width = value; }
        }
        public double Top
        {
            get { return Canvas.GetTop(_image); }
            set { Canvas.SetTop(_image, value); }
        }
        public double Left
        {
            get { return Canvas.GetLeft(_image); }
            set { Canvas.SetLeft(_image, value); }
        }
        #endregion
        #region ctor
        public ObjectsOnCanvas(double height, double width, double top, double left, string uri)
        {
            _height = height;
            _width = width;
            _top = top;
            _left = left;
            _uri = uri;
            _image = new Image();
            _image.Height = _height;
            _image.Width = _width;
            Canvas.SetTop(_image, _top);
            Canvas.SetLeft(_image, _left);
            _image.Source = new BitmapImage(new Uri($"ms-appx://{_uri}"));
        } 
        #endregion
        public void AddToCanvas (Canvas canvas)
        {
            canvas.Children.Add(_image);
        }
        public void RemoveFromCanvas(Canvas canvas)
        {
            canvas.Children.Remove(_image);
        }
        public double GetLeft()
        {
            return Canvas.GetLeft(_image);
        }
        public double GetTop()
        {
            return Canvas.GetTop(_image);
        }
        public void SetLeft(double newLeft)
        {
            Canvas.SetLeft(_image, newLeft);           
        }
        public void SetTop(double newTop)
        {
            Canvas.SetTop(_image, newTop);
        }
    }
}
