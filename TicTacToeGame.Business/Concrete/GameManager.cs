using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.DataAccess;
using TicTacToeGame.Entities;

namespace TicTacToeGame.Business.Concrete
{
    public class GameManager
    {
        ScoreData _score = new ScoreData();
        Marker[] _marker;
        int _roundCount;
        bool _roundEnded;
        bool _playerTurn;

        public string PlayerOneName
        {
            get
            {
                return _score.PlayerOneName;
            }
        }
        public string PlayerTwoName
        {
            get
            {
                return _score.PlayerTwoName;
            }
        }
        public int PlayerOneWins
        {
            get
            {
                return _score.PlayerOneWins;
            }
        }
        public int PlayerTwoWins
        {
            get
            {
                return _score.PlayerTwoWins;
            }
        }
        public int RoundCount
        {
            get
            {
                return _roundCount;
            }
        }
        public bool RoundEnded
        {
            get
            {
                return _roundEnded;
            }
        }
        public bool GameEnded
        {
            get
            {
                if (_roundCount == _score.RoundCount) return true;
                else return false;
            }
        }
        public bool PlayerTurn
        {
            get
            {
                return _playerTurn;
            }
        }

        public GameManager(ScoreData score)
        {
            _score = score;

            _roundCount = 0;

            NewRound();
        }

        public void NewRound()
        {
            _marker = new Marker[9];

            for (int i = 0; i < _marker.Length; i++)
            {
                _marker[i] = Marker.Empty;
            }

            if (_roundCount % 2 == 0) _playerTurn = true;
            else _playerTurn = false;

            _roundEnded = false;
        }
        public void RoundEnd(int x)
        {
            if (x == 1) _score.PlayerOneWins++;
            if (x == 2) _score.PlayerTwoWins++;

            _roundEnded = true;
            _roundCount++;
        }
        public void PlayerChoice(int index)
        {
            _marker[index] = _playerTurn ? Marker.Cross : Marker.Circle;

            _playerTurn ^= true;
        }
        public bool PlayerChoiceCheck(int index)
        {
            if (_marker[index] != Marker.Empty) return false;

            return true;
        }
        public int[] CheckWinner()
        {
            for(int i=0; i<7; i+=3)
            {
                if (_marker[i] != Marker.Empty && (_marker[i] & _marker[i + 1] & _marker[i + 2]) == _marker[i])
                {
                    RoundEnd(!_playerTurn ? 1 : 2);
                    return new int[] { i, i + 1, i + 2 };
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (_marker[i] != Marker.Empty && (_marker[i] & _marker[i + 3] & _marker[i + 6]) == _marker[i])
                {
                    RoundEnd(!_playerTurn ? 1 : 2);
                    return new int[] { i, i + 3, i + 6 };
                }
            }

            if (_marker[0] != Marker.Empty && (_marker[0] & _marker[4] & _marker[8]) == _marker[0])
            {
                RoundEnd(!_playerTurn ? 1 : 2);
                return new int[] { 0, 4, 8 };
            }

            if (_marker[2] != Marker.Empty && (_marker[2] & _marker[4] & _marker[6]) == _marker[2])
            {
                RoundEnd(!_playerTurn ? 1 : 2);
                return new int[] { 2, 4, 6 };
            }

            if (!_marker.Any(f => f == Marker.Empty) && !_roundEnded)
            {
                RoundEnd(0);
                return new int[] { 0 };
            }
                

            return new int[] { 1 };
        }
        public void SaveScore()
        {
            ScoreManager.AddScoreData(_score);
        }
    }
}
