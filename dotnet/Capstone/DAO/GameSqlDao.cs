using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class GameSqlDao : IGameDao
    {

        private readonly string connectionSting;

        public GameSqlDao(string dbConnectionString)
        {
            connectionSting = dbConnectionString;
        }

        public int CreateGameId(string gameName, int userId, DateTime startDate, DateTime endDate)
        {
            int newGameId = 0;
            using (SqlConnection conn = new SqlConnection(connectionSting))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO GAME (game_name, startdate, enddate) " +
                                                "OUTPUT INSERTED.game_id " +
                                                "VALUES (@game_Name, @start_Date, @end_Date);", conn);
                cmd.Parameters.AddWithValue("@game_Name", gameName);
                cmd.Parameters.AddWithValue("@start_Date", startDate);
                cmd.Parameters.AddWithValue("@end_Date", endDate);

                newGameId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return CreateGameById(newGameId, userId);
        }


        public int CreateGameById(int newGameId, int userId)
        {
           
            using (SqlConnection conn = new SqlConnection(connectionSting))
            {
                int createdGameId = 0;
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO balance (balance, game_id, user_id) " +
                                                "OUTPUT INSERTED.game_id " +
                                                "values (100000, @game_id, @user_id); ", conn);

                cmd.Parameters.AddWithValue("@game_id", newGameId);
                cmd.Parameters.AddWithValue("@user_id", userId);

                createdGameId = Convert.ToInt32(cmd.ExecuteScalar());
                return createdGameId;
            }
           
        }

        public List<ViewGames> ViewGamesByUserId(int userId)
        {
            List<ViewGames> listGames = new List<ViewGames>();

            using (SqlConnection conn = new SqlConnection(connectionSting))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT U.username, G.game_name, G.startdate, G.enddate, B.balance, G.game_id " +
                                                "FROM GAME G JOIN balance B ON G.game_id = B.game_id " +
                                                "JOIN users U ON B.user_id = U.user_id " +
                                                "WHERE B.user_id = @user_id;", conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ViewGames games = ViewGamesByUserIdReader(reader);
                    listGames.Add(games);
                }
            }
            return listGames;
        }

        private ViewGames ViewGamesByUserIdReader(SqlDataReader reader)
        {
            ViewGames g = new ViewGames();

            g.UserName = Convert.ToString(reader["username"]);
            g.GameName = Convert.ToString(reader["game_Name"]);
            g.Balance = Convert.ToDecimal(reader["balance"]);
            g.StartTime = Convert.ToDateTime(reader["startdate"]);
            g.EndDate = Convert.ToDateTime(reader["enddate"]);
            g.GameId = Convert.ToInt16(reader["game_id"]);
            return g;
        }

        public int InvitePlayer(int userId, CreateGame gameId)
        {

            using (SqlConnection conn = new SqlConnection(connectionSting))
            {
                int transferGame = 0;
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Balance (balance, game_id, user_id) " +
                                                "OUTPUT INSERTED.game_id " +
                                                "VALUES (100000, (SELECT game_id FROM game WHERE game_name=@game_name), @user_id); ", conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@game_name", gameId.GameName);

                transferGame = (int)cmd.ExecuteScalar();
   
                return transferGame;
            }

        }



    }
}
