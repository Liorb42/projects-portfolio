using MyGame.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyGame
{
    
    public sealed partial class Game : Page
    {
        #region Properties
        const double MOVEMENT_SPEED = 6;
        const double LINE_MOVMENT_FACTOR = 1.5;
        Canvas _gameBoard;
        Button _toggleTimerBtn;
        DispatcherTimer _timer;
        bool _isTimerRunning;
        Image _line;
        
        GameManager _gameManager;

        #endregion        
        public Game()
        {
            this.InitializeComponent();
            _gameBoard = GameBoard;
            _toggleTimerBtn = ToggleTimerBtn;
            _timer = new DispatcherTimer();
            _timer.Tick += GameLoop;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            _timer.Start();
            _isTimerRunning = true;
            _line = Line;
            
            _gameManager = new GameManager(_line, _gameBoard, MOVEMENT_SPEED, LINE_MOVMENT_FACTOR);

            InitializeGame();
            Window.Current.CoreWindow.KeyDown += UserInputKeyDown;
            Window.Current.CoreWindow.KeyUp += UserInputKeyUp;     
        }
        private void GameLoop(object sender, object e)
        {
            _gameManager.HanddleLineMovment();
            _gameManager.HanddleBallMovment();
            ShowScore();
            if (_gameManager.IsGameLost)
            {
                _timer.Stop();
                Frame.Navigate(typeof(LosePage));
            }
            
            if (_gameManager.IsGameWon)
            {
                _timer.Stop();
                Frame.Navigate(typeof(WinPage));
            }
        }
        private void InitializeGame()
        {
            _gameManager.InitializeBricks(50, 100, "/Assets/bannana.png", 36);
            _gameManager.PlaceBricksOnCanvas(90, 100, 9);
            _gameManager.InitBall(535, 590); //center on line is 535,590
        }

        private void ShowScore()
        {           
            ScoreTextBox.Text = $"Score: {_gameManager.Score}";           
        }

        #region Keys Input
        private void UserInputKeyUp(CoreWindow sender, KeyEventArgs args)
        {
            _gameManager.Key = "";
        }
        private void UserInputKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            _gameManager.Key = args.VirtualKey.ToString();
        }
        #endregion
        #region Buttons
        private void BackToMenu_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));

        }
        private void ToggleTimerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_isTimerRunning)
            {
                _timer.Stop();
                _toggleTimerBtn.Content = "Resume Game";
            }
            else
            {
                _timer.Start();
                _toggleTimerBtn.Content = "Pause Game";
            }
            _isTimerRunning = !_isTimerRunning;



        }
    } 
    #endregion
}
