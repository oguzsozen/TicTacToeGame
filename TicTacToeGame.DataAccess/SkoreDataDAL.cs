using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeGame.Entities;

namespace TicTacToeGame.DataAccess
{
    class SkoreDataDAL
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb; initial catalog=TicTacToeGame; integrated security=true");

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public List<SkoreData> GetAll()
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand("Select * from SkoreDatas", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<SkoreData> skores = new List<SkoreData>();

            while(reader.Read())
            {
                SkoreData skore = new SkoreData
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
        public void Add(SkoreData skore)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand(
                "Insert into SkoreDatas values(@PlayerOneName, @PlayerTwoName, @PlayerOneWins, @PlayerTwoWins, @RoundCount, @Date)",
                _connection);

            command.Parameters.AddWithValue("@PlayerOneName", skore.PlayerOneName);
            command.Parameters.AddWithValue("@PlayerTwoName", skore.PlayerTwoName);
            command.Parameters.AddWithValue("@PlayerOneWins", skore.PlayerOneWins);
            command.Parameters.AddWithValue("@PlayerTwoWins", skore.PlayerTwoWins);
            command.Parameters.AddWithValue("@RoundCount", skore.RoundCount);
            command.Parameters.AddWithValue("@Date", skore.Date);

            command.ExecuteNonQuery();

            _connection.Close();
        }
        public void Update(SkoreData skore)
        {
            ConnectionControl();

            SqlCommand command = new SqlCommand(
                "Insert into SkoreDatas values(@PlayerOneName, @PlayerTwoName, @PlayerOneWins, @PlayerTwoWins, @RoundCount, @Date)",
                _connection);

            command.Parameters.AddWithValue("@PlayerOneName", skore.PlayerOneName);
            command.Parameters.AddWithValue("@PlayerTwoName", skore.PlayerTwoName);
            command.Parameters.AddWithValue("@PlayerOneWins", skore.PlayerOneWins);
            command.Parameters.AddWithValue("@PlayerTwoWins", skore.PlayerTwoWins);
            command.Parameters.AddWithValue("@RoundCount", skore.RoundCount);
            command.Parameters.AddWithValue("@Date", skore.Date);

            command.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
