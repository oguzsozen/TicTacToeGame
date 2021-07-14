using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.DataAccess;
using TicTacToeGame.Entities;

namespace TicTacToeGame.Business.Concrete
{
    public static class ScoreManager
    {
        public static List<ScoreData> GetAllScoreData()
        {
            return new ScoreDataDAL().GetAll();
        }
        public static void AddScoreData(ScoreData score)
        {
            new ScoreDataDAL().Add(score);
        }
        public static void DeleteAllScoreData()
        {
            new ScoreDataDAL().DeleteAll();
        }
        public static void DeleteScoreData(int id)
        {
            new ScoreDataDAL().Delete(id);
        }
    }
}
