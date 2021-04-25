using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyGame.Classes
{
    public class Ball : ObjectsOnCanvas
    {
        #region Properties
        Canvas _canvas;
        public string BallDirectionUpdown { get; set; }
        public string BallDirectionRightLet { get; set; }
        public bool IsTopLeftCornerIsColliding { get; set; }
        public bool IsTopRightCornerIsColliding { get; set; }
        public bool IsButtomRightCornerIsColliding { get; set; }
        public bool IsButtomLeftCornerIsColliding { get; set; }
        #endregion
        #region ctor
        public Ball(double height, double width, string uri, Canvas canvas, string ballDirectionUpdown, string ballDirectionRightLet)
            : base(height, width, 0, 0, uri)
        {
            _canvas = canvas;
            this.AddToCanvas(_canvas);
            BallDirectionUpdown = ballDirectionUpdown;
            BallDirectionRightLet = ballDirectionRightLet;
            IsTopLeftCornerIsColliding = false;
            IsTopRightCornerIsColliding = false;
            IsButtomRightCornerIsColliding = false;
            IsButtomLeftCornerIsColliding = false;           
        } 
        #endregion
        
    }
}
