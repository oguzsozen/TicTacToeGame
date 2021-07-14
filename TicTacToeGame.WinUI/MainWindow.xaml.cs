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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicTacToeGame.Business;
using TicTacToeGame.Business.Concrete;
using TicTacToeGame.Entities;

namespace TicTacToeGame.WinUI
{
    public partial class MainWindow : Window
    {
        GameManager _game;

        Button[] buttons;

        public MainWindow()
        {
            InitializeComponent();

            buttons = new Button[9];

            int i = 0;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                buttons[i] = button;
                i++;
            });

            StartMenu.Visibility = Visibility.Visible;
        }

        private void NewRound()
        {
            _game.NewRound();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Content = string.Empty;
                buttons[i].Background = Brushes.White;
                buttons[i].Foreground = Brushes.Blue;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(_game.RoundEnded)
            {
                NewRound();
                return;
            }

            var button = (Button)sender;

            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);

            int index = column + (row * 3);

            if (!_game.PlayerChoiceCheck(index)) return;

            button.Content = _game.PlayerTurn ? "X" : "O";

            if (!_game.PlayerTurn)
                button.Foreground = Brushes.Red;

            _game.PlayerChoice(index);

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            int[] result = _game.CheckWinner();

            if(result.Length == 1 && result[0] == 0)
            {
                for(int i=0; i < buttons.Length; i++)
                {
                    buttons[i].Background = Brushes.Orange;
                }
            }

            if(result.Length == 3)
            {
                buttons[result[0]].Background = buttons[result[1]].Background = buttons[result[2]].Background = Brushes.Green;
                BorderUpdate();
            }

            if(_game.GameEnded)
            {
                Container.IsEnabled = false;
                GameEndMenu.Visibility = Visibility.Visible;
            }
        }

        private void BorderCreate()
        {
            LblPlayerOneName.Content = "X - " + _game.PlayerOneName;
            LblPlayerTwoName.Content = "O - " + _game.PlayerTwoName;

            BorderUpdate();
        }
        private void BorderUpdate()
        {
            LblRoundCount.Content = _game.RoundCount;
            LblPlayerOneWins.Content = _game.PlayerOneWins;
            LblPlayerTwoWins.Content = _game.PlayerTwoWins;

            if (_game.PlayerOneWins > _game.PlayerTwoWins) BorderCanvas.Background = Brushes.MediumBlue;
            else if (_game.PlayerOneWins < _game.PlayerTwoWins) BorderCanvas.Background = Brushes.DarkRed;
            else BorderCanvas.Background = Brushes.LightGray;
        }

        private void InputMenu_Click(object sender, RoutedEventArgs e)
        {
            StartMenu.Visibility = Visibility.Hidden;
            GameEndMenu.Visibility = Visibility.Hidden;
            InputMenu.Visibility = Visibility.Visible;
        }
        private void ScoreTableMenu_Click(object sender, RoutedEventArgs e)
        {
            StartMenu.Visibility = Visibility.Hidden;
            GameEndMenu.Visibility = Visibility.Hidden;
            ScoreTableMenu.Visibility = Visibility.Visible;
        }
        private void SaveMenu_Click(object sender, RoutedEventArgs e)
        {
            _game.SaveScore();

            GameEndMenu.Visibility = Visibility.Hidden;
            StartMenu.Visibility = Visibility.Visible;
        }
        private void StartMenu_Click(object sender, RoutedEventArgs e)
        {
            GameEndMenu.Visibility = Visibility.Hidden;
            StartMenu.Visibility = Visibility.Visible;
        }
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            ScoreData score = new ScoreData();

            score.PlayerOneName = tbxPlayerOneName.Text;
            score.PlayerTwoName = tbxPlayerTwoName.Text;
            score.PlayerOneWins = 0;
            score.PlayerTwoWins = 0;
            score.RoundCount = Convert.ToInt32(tbxRoundCount.Text);
            score.Date = DateTime.Now;

            _game = new GameManager(score);

            BorderCreate();

            NewRound();

            InputMenu.Visibility = Visibility.Hidden;
            Container.IsEnabled = true;
        }
    }
}
