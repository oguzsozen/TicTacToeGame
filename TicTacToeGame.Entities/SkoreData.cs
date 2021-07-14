using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame.Entities
{
    public class SkoreData
    {
        public int Id { get; set; }
        public string PlayerOneName { get; set; }
        public string PlayerTwoName { get; set; }
        public int PlayerOneWins { get; set; }
        public int PlayerTwoWins { get; set; }
        public int RoundCount { get; set; }
        public DateTime Date { get; set; }
    }
}
