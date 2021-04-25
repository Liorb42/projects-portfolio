using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyGame
{
    public class Brick : ObjectsOnCanvas
    {
        #region Properties
        Canvas _canvas;
        public bool BallCollidedWithBrick { get; set; }
        
        #endregion

        #region ctor
        public Brick(double height, double width, string uri, Canvas canvas)
            : base(height, width, 0, 0, uri)
        {
            _canvas = canvas;
            this.AddToCanvas(_canvas);
            BallCollidedWithBrick = false;
        } 
        #endregion
    }
}
