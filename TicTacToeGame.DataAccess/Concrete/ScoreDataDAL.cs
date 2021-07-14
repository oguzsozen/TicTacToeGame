using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.Entities;
using TicTacToeGame.DataAccess.Abstract;

namespace TicTacToeGame.DataAccess
{
    public class ScoreDataDAL : IDataDAL
    {
        /// <summary>
        /// Veritabanı ile gerekli bağlantıyı kurar.
        /// </summary>
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb; initial catalog=TicTacToeGame; integrated security=true");
        
        /// <summary>
        /// Veritabanı bağlantısının açık olup olmadığını kontrol ederek bağlantıyı açar.
        /// </summary>
        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        
        /// <summary>
        /// Veritabanından tüm skorları çeker.
        /// </summary>
        /// <returns></returns>
        public List<ScoreData> GetAll()
        {
            ConnectionControl();

            //Sql sorgusu çalıştırılır ve gelen veri önce reader'a, sonra da skores

            SqlCommand command = new SqlCommand("Select * from ScoreDatas", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<ScoreData> skores = new List<ScoreData>();

            while(reader.Read())
            {
                ScoreData skore = new ScoreData
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    PlayerOneName = reader["PlayerOneName"].ToString(),
                    PlayerTwoName = reader["PlayerTwoName"].ToString(),
                    PlayerOneWins = Convert.ToInt32(reader["PlayerOneWins"]),
                    PlayerTwoWins = Convert.ToInt32(reader["PlayerTwoWins"]),
                    RoundCount = Convert.ToInt32(reader["RoundCount"]),
                    Date = Convert.ToDateTime(reader["Date"])
                };
                skores.Add(skore);
            }

            reader.Close();
            _connection.Close();
            return skores;
        }
        
        /// <summary>
        /// Veritabanına yeni skor ekler.
        /// </summary>
        /// <param name="skore"></param>
        public void Add(ScoreData skore)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand(
                "Insert into ScoreDatas values(@playerOneName, @playerTwoName, @playerOneWins, @playerTwoWins, @roundCount, @date)",
                _connection);

            command.Parameters.AddWithValue("@playerOneName", skore.PlayerOneName);
            command.Parameters.AddWithValue("@playerTwoName", skore.PlayerTwoName);
            command.Parameters.AddWithValue("@playerOneWins", skore.PlayerOneWins);
            command.Parameters.AddWithValue("@playerTwoWins", skore.PlayerTwoWins);
            command.Parameters.AddWithValue("@roundCount", skore.RoundCount);
            command.Parameters.AddWithValue("@date", skore.Date);

            command.ExecuteNonQuery();

            _connection.Close();
        }
        
        /// <summary>
        /// Veritabanında kayıtlı tüm skorları kaldırır.
        /// </summary>
        public void DeleteAll()
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("Delete from ScoreDatas", _connection);

            command.ExecuteNonQuery();

            _connection.Close();
        }
        
        /// <summary>
        /// Seçilen oyunun skorunu veritabanından kaldırır.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("Delete from ScoreDatas where Id=@id", _connection);

            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
