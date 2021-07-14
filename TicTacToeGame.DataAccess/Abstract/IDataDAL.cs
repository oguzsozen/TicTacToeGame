using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame.DataAccess.Abstract
{
    interface IDataDAL
    {
        void DeleteAll();
        void Delete(int id);
    }
}
