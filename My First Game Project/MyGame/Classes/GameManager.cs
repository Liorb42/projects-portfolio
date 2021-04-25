using MyGame.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyGame
{
        
    public class GameManager
    {
        #region Properties
        Ball _ball;
        Image _line;
        List<Brick> _bricks;
        Canvas _canvas;
        double _movmentSpeed;
        double _lineMovementFactor;
        bool _isGameLost;
        bool _isGameWon;
        string _key;
        int _numberOfBricks;
        int _score;

        public string Key { get { return _key; } set { _key = value; } }
        public bool IsGameLost { get { return _isGameLost; } }
        public bool IsGameWon { get { return _isGameWon; } }
        public int NumberOfBricks { get { return _numberOfBricks; } }
        public int Score { get { return _score; } set { _score = value; } }

        #endregion
        #region ctor
        public GameManager(Image line, Canvas canvas, double movmentSpeed, double lineMovementFactor )
        {
            _canvas = canvas; 
            _bricks = new List<Brick>();
            _line = line;            
            _movmentSpeed = movmentSpeed;
            _lineMovementFactor = lineMovementFactor;
            _isGameLost = false;
            _isGameWon = false;
            _score = 0;
        }     
        #endregion
        #region Init Bricks
        public void InitializeBricks(double brickHeight, double brickWidth, string imagePath, int numberOfBricks)
        {
            _numberOfBricks = numberOfBricks;

            for (int i = 0; i < numberOfBricks; i++)
            {
                Brick brick = new Brick(brickHeight, brickWidth, imagePath, _canvas);
                _bricks.Add(brick);                
            }
        }
        public void PlaceBricksOnCanvas(double blockLeft, double blockTop, int bricksInRow)
        {
            double firstBlockLeft = blockLeft;
            int counterBlock = 1;
            int counterRow = 1;

            while (counterRow <= _bricks.Count / bricksInRow)
            {
                foreach (Brick item in _bricks)
                {
                    item.SetTop(blockTop);
                    item.SetLeft(blockLeft);
                    counterBlock++;
                    if (counterBlock > bricksInRow)
                    {
                        blockLeft = firstBlockLeft;  //set left to begining of row
                        blockTop += item.Height; // set top to the next row
                        counterRow++;
                        counterBlock = 1;
                    }
                    else blockLeft += item.Width;
                }
            }
        }
        #endregion
        #region Init Ball
        public void InitBall(double ballLeft, double ballTop)
        {
            _ball = new Ball(50, 50, "/Assets/monkey.png", _canvas, "up", "right");
            _ball.SetLeft(ballLeft);
            _ball.SetTop(ballTop);
        }
        #endregion
        #region Ball Movement
        public void HanddleBallMovment()
        {
            double top = _ball.GetTop();
            double left = _ball.GetLeft();

            //check if ball is colliding with something
            bool isCollisionWithBricks = IsBallCollidingWithBricks();
            bool isCollisionWithLine = IsBallCollidingWithLine();
            bool isCollisionWithSides = IsBallCollidingWithSides();
            bool isBallFreeToMove;
            if (!isCollisionWithBricks && !isCollisionWithLine && !isCollisionWithSides) isBallFreeToMove = true;
            else isBallFreeToMove = false;

            //move the ball up + right
            if (isBallFreeToMove && _ball.BallDirectionUpdown == "up" && _ball.BallDirectionRightLet == "right")
            {
                _ball.SetLeft(left + _movmentSpeed);
                _ball.SetTop(top - _movmentSpeed);
            }
            //move the ball up + left
            else if (isBallFreeToMove && _ball.BallDirectionUpdown == "up" && _ball.BallDirectionRightLet == "left")
            {
                _ball.SetLeft(left - _movmentSpeed);
                _ball.SetTop(top - _movmentSpeed);
            }
            //move the ball down + left
            else if (isBallFreeToMove && _ball.BallDirectionUpdown == "down" && _ball.BallDirectionRightLet == "left")
            {
                _ball.SetLeft(left - _movmentSpeed);
                _ball.SetTop(top + _movmentSpeed);
            }
            //move the ball down + right
            else if (isBallFreeToMove && _ball.BallDirectionUpdown == "down" && _ball.BallDirectionRightLet == "right")
            {
                _ball.SetLeft(left + _movmentSpeed);
                _ball.SetTop(top + _movmentSpeed);
            }

            //change directions if ball is colliding
            else if (isCollisionWithSides) SwitchBallDirections();
            else if (isCollisionWithLine) SwitchBallDirections();
            else if (isCollisionWithBricks)
            {
                SwitchBallDirections();
                RemoveBricks();
            }
        }        
        private bool IsBallCollidingWithSides()
        {
            double top = _ball.GetTop();
            double left = _ball.GetLeft();

            //colliding with the top of the screen
            if (top < 0)
            {
                BallCollidedFromThisSide("up");
                return true;
            }
            //colliding with the buttom of the screen. GAME LOST!
            else if (top + _ball.Height > _canvas.ActualHeight)
            {
                _isGameLost = true;
                return true;
            }
            //colliding with the right of the screen
            else if (left + _ball.Width > _canvas.ActualWidth)
            {
                BallCollidedFromThisSide("right");
                return true;
            }
            //colliding with the left of the screen
            else if (left < 0)
            {
                BallCollidedFromThisSide("left");
                return true;
            }
            else return false;
        }
        private bool IsBallCollidingWithLine()
        {
            double topBall = _ball.GetTop();
            double leftBall = _ball.GetLeft();
            double topLine = Canvas.GetTop(_line);
            double leftLine = Canvas.GetLeft(_line);
            bool isBallCollidingWithLine = false;


            //The top-left corner of the ball is inside line
            if (topBall < (topLine + _line.Height) && topBall > topLine
            && leftBall > leftLine && leftBall < (leftLine + _line.Width))
            {
                _ball.IsTopLeftCornerIsColliding = true;
                isBallCollidingWithLine = true;
            }
            //The buttom-left corner of the ball is inside line                        
            if ((topBall + _ball.Height) > (topLine + _line.Height * 0.5) && (topBall + _ball.Height) < (topLine + _line.Height)
            && leftBall > (leftLine - _line.Width * 0.1) && leftBall < (leftLine + _line.Width))
            {
                _ball.IsButtomLeftCornerIsColliding = true;
                isBallCollidingWithLine = true;
            }
            //The buttom-right corner of the ball is inside line
            if ((topBall + _ball.Height) > (topLine + _line.Height * 0.5) && (topBall + _ball.Height) < (topLine + _line.Height)
            && (leftBall + _ball.Width) > (leftLine + _line.Width * 0.1) && (leftBall + _ball.Width) < (leftLine + _line.Width))
            {
                _ball.IsButtomRightCornerIsColliding = true;
                isBallCollidingWithLine = true;
            }
            //The top-right corner of the ball is inside line
            if (topBall < (topLine + _line.Height) && topBall > topLine
            && (leftBall + _ball.Width) > leftLine && (leftBall + _ball.Width) < (leftLine + _line.Width))
            {
                _ball.IsTopRightCornerIsColliding = true;
                isBallCollidingWithLine = true;
            }
            return isBallCollidingWithLine;

        }
        private bool IsBallCollidingWithBricks()
        {
            double topBall = _ball.GetTop();
            double leftBall = _ball.GetLeft();
            bool isBallCollidingWithBricks = false;
            BallCollidedFromThisSide("non");

            for (int i = 0; i < _bricks.Count; i++)
            {
                double topBrick = _bricks[i].GetTop();
                double leftBrick = _bricks[i].GetLeft();

                //The top-left corner of the ball is inside brick
                if (topBall < (topBrick + _bricks[i].Height) && topBall > topBrick
                && leftBall > leftBrick && leftBall < (leftBrick + _bricks[i].Width))
                {
                    _ball.IsTopLeftCornerIsColliding = true;
                    isBallCollidingWithBricks = true;
                    _bricks[i].BallCollidedWithBrick = true;
                }
                //The buttom-left corner of the ball is inside brick
                if ((topBall + _ball.Height) > (topBrick + _bricks[i].Height * 0.4) && (topBall + _ball.Height) < (topBrick + _bricks[i].Height)
                && leftBall > leftBrick && leftBall < (leftBrick + _bricks[i].Width))
                {

                    _ball.IsButtomLeftCornerIsColliding = true;
                    isBallCollidingWithBricks = true;
                    _bricks[i].BallCollidedWithBrick = true;

                }
                //The buttom-right corner of the ball is inside brick
                if ((topBall + _ball.Height) > (topBrick + _bricks[i].Height * 0.4) && (topBall + _ball.Height) < (topBrick + _bricks[i].Height)
                && (leftBall + _ball.Width) > leftBrick && (leftBall + _ball.Width) < (leftBrick + _bricks[i].Width))
                {

                    _ball.IsButtomRightCornerIsColliding = true;
                    isBallCollidingWithBricks = true;
                    _bricks[i].BallCollidedWithBrick = true;

                }
                //The top-right corner of the ball is inside brick
                if (topBall < (topBrick + _bricks[i].Height) && topBall > topBrick
                && (leftBall + _ball.Width) > leftBrick && (leftBall + _ball.Width) < (leftBrick + _bricks[i].Width))
                {

                    _ball.IsTopRightCornerIsColliding = true;
                    isBallCollidingWithBricks = true;
                    _bricks[i].BallCollidedWithBrick = true;

                }
            }
            return isBallCollidingWithBricks;
        }
        private void BallCollidedFromThisSide(string side)
        {
            if (side == "up")
            {
                _ball.IsTopLeftCornerIsColliding = true;
                _ball.IsTopRightCornerIsColliding = true;
                _ball.IsButtomRightCornerIsColliding = false;
                _ball.IsButtomLeftCornerIsColliding = false;

            }
            if (side == "down")
            {
                _ball.IsTopLeftCornerIsColliding = false;
                _ball.IsTopRightCornerIsColliding = false;
                _ball.IsButtomRightCornerIsColliding = true;
                _ball.IsButtomLeftCornerIsColliding = true;
            }
            if (side == "right")
            {
                _ball.IsTopLeftCornerIsColliding = false;
                _ball.IsTopRightCornerIsColliding = true;
                _ball.IsButtomRightCornerIsColliding = true;
                _ball.IsButtomLeftCornerIsColliding = false;
            }
            if (side == "left")
            {
                _ball.IsTopLeftCornerIsColliding = true;
                _ball.IsTopRightCornerIsColliding = false;
                _ball.IsButtomRightCornerIsColliding = false;
                _ball.IsButtomLeftCornerIsColliding = true;
            }
            if (side == "non")
            {
                _ball.IsTopLeftCornerIsColliding = false;
                _ball.IsTopRightCornerIsColliding = false;
                _ball.IsButtomRightCornerIsColliding = false;
                _ball.IsButtomLeftCornerIsColliding = false;
            }
        }
        private void SwitchBallDirections()
        {
            double topBall = _ball.GetTop();
            double leftBall = _ball.GetLeft();

            //The ball was hitting 2 bricks from it's top
                if (_ball.IsTopLeftCornerIsColliding && _ball.IsTopRightCornerIsColliding)
            {
                _ball.BallDirectionUpdown = "down";
                _ball.SetTop(topBall + _movmentSpeed);
            }
            //The ball was hitting 1 brick from top-left corner
                else if (_ball.IsTopLeftCornerIsColliding && !_ball.IsTopRightCornerIsColliding && !_ball.IsButtomLeftCornerIsColliding)
            {
                _ball.BallDirectionUpdown = "down";
                _ball.SetTop(topBall + _movmentSpeed);
            }
            //The ball was hitting 1 brick from top-right corner
                else if (_ball.IsTopRightCornerIsColliding && !_ball.IsTopLeftCornerIsColliding && !_ball.IsButtomRightCornerIsColliding)
            {
                _ball.BallDirectionUpdown = "down";
                _ball.SetTop(topBall + _movmentSpeed);
            }
            //The ball was hitting 2 bricks from it's buttom
                else if (_ball.IsButtomLeftCornerIsColliding && _ball.IsButtomRightCornerIsColliding) // ADD GAME LOST
            {
                _ball.BallDirectionUpdown = "up";
                _ball.SetTop(topBall - _movmentSpeed);
            }
            //The ball was hitting 1 brick from buttom-right corner
                else if (_ball.IsButtomRightCornerIsColliding && !_ball.IsButtomLeftCornerIsColliding && !_ball.IsTopRightCornerIsColliding)
            {
                _ball.BallDirectionUpdown = "up";
                _ball.SetTop(topBall - _movmentSpeed);
            }

            //The ball was hitting 1 brick from buttom-left corner
                else if (_ball.IsButtomLeftCornerIsColliding && !_ball.IsButtomRightCornerIsColliding && !_ball.IsTopLeftCornerIsColliding)
            {
                _ball.BallDirectionUpdown = "up";
                _ball.SetTop(topBall - _movmentSpeed);
            }

            //The ball was hitting 2 bricks from it's right
                else if (_ball.IsTopRightCornerIsColliding && _ball.IsButtomRightCornerIsColliding)
            {
                _ball.BallDirectionRightLet = "left";
                _ball.SetLeft(leftBall - _movmentSpeed);
            }

            //The ball was hitting 2 bricks from it's left
                else if (_ball.IsTopLeftCornerIsColliding && _ball.IsButtomLeftCornerIsColliding)
            {
                _ball.BallDirectionRightLet = "right";
                _ball.SetLeft(leftBall + _movmentSpeed);
            }
        }
        #endregion
        #region Brick Removal & Score    
        private void RemoveBricks()
        {            
            for (int i = _bricks.Count -1; i >= 0; i--)
            {
                if (_bricks[i].BallCollidedWithBrick)
                {
                    _bricks[i].RemoveFromCanvas(_canvas);
                    _bricks.RemoveAt(i);
                    CalcScore();                   
                }
            }
        }
        private void CalcScore()
        {
            _score++;
            if (_score == _numberOfBricks) _isGameWon = true;      
        }

        #endregion
        #region Line Movement
        public void HanddleLineMovment()
        {
            if (_key == "Right")
            {
                double left = Canvas.GetLeft(_line);
                //check if line is colliding with the right side of the gameboard
                    if ((left + _line.ActualWidth) < _canvas.ActualWidth)
                    Canvas.SetLeft(_line, left + _movmentSpeed * _lineMovementFactor);
            }
            else if (_key == "Left")
            {
                double left = Canvas.GetLeft(_line);
                //check if line is colliding with the left side of the gameboard
                    if (left  > 0)
                    Canvas.SetLeft(_line, left - _movmentSpeed * _lineMovementFactor);
            }
        }


        #endregion    
    }
}
