using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Capstone.Models;
using Capstone.Services;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class LeaderboardDao : ILeaderboards
    {
        private readonly string connectionSting;
        private readonly IClosePriceDao Close;

        public LeaderboardDao(string dbConnectionString)
        {
            connectionSting = dbConnectionString;
            Close = new ClosePriceServices();
        }
        private readonly IClosePriceDao ClosePriceServices;
        public LeaderboardDao(IClosePriceDao closePriceServices)
        {
            this.ClosePriceServices = closePriceServices;
        }
        //get value of quantity of stocks * daily value
        public Leaderboards TradesReader(SqlDataReader reader)
        {
            Leaderboards list = new Leaderboards();
            list.Stock = Convert.ToString(reader["stock"]);
            list.UserId = Convert.ToInt32(reader["user_id"]);
            list.Quanitity = Convert.ToInt32(reader["quantity"]);
            list.GameId = Convert.ToInt32(reader["game_id"]);
            list.Username = Convert.ToString(reader["username"]);
            return list;
        }

        //leaderboards int variable needs to change since we changed to to pass a static game id and not use a variable where the game id would be passed in
        public Dictionary<string, decimal> LeaderboardBalance(int leaderboard)
        {

            List<Leaderboards> results = new List<Leaderboards>();

            Dictionary<string, decimal> balances = new Dictionary<string, decimal>();

            using (SqlConnection conn = new SqlConnection(connectionSting))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT H.Stock, H.user_id, H.quantity, H.game_id, U.username " +
                                                "FROM holdings H join users U on H.user_id = U.user_id " +
                                                "WHERE game_id = @game_id ", conn);
                cmd.Parameters.AddWithValue("@game_id", leaderboard);
                SqlDataReader test = cmd.ExecuteReader();
                while (test.Read())
                {
                    Leaderboards newLeaderboard = TradesReader(test);
                    results.Add(newLeaderboard);
                }

          
                for (int i = 1; i <= results.Count; i++)
                {
                    decimal playerBalance = 0;

                    string ticker = results[i - 1].Stock;
                    ClosePrice price = Close.GetPrice(ticker);
                    decimal finalPrice = price.close;
                    playerBalance = finalPrice * results[i - 1].Quanitity;

                    string key = results[i - 1].Username.ToString();


                    balances[key] = playerBalance;


                }
            }
                Dictionary<string, decimal> balanceResult = new Dictionary<string, decimal>();

                using (SqlConnection cashBalance = new SqlConnection(connectionSting))
                {
                    cashBalance.Open();
                    SqlCommand cb = new SqlCommand("SELECT B.balance, U.username " +
                                                   "FROM balance B " +
                                                   "JOIN users U ON B.user_id = U.user_id " +
                                                   "WHERE game_id = @game_id ", cashBalance);
                    cb.Parameters.AddWithValue("@game_id", leaderboard);
                    SqlDataReader cashBalanceTotal = cb.ExecuteReader();
                    while (cashBalanceTotal.Read())
                    {
                        Leaderboards newLeaderboard = BalanceTradesReader(cashBalanceTotal);
                        balanceResult[newLeaderboard.Username.ToString()] = newLeaderboard.Balance;
                    }


                Dictionary<string, decimal> finalFinalBalance = new Dictionary<string, decimal>();


                ////for (int i = 1; i <= results.Count; i++)


                //foreach (KeyValuePair<string, decimal> bvp in balances)
                //{

                //    foreach (KeyValuePair<string, decimal> kvp in balanceResult)
                //    {

                //        if (balances.ContainsKey(kvp.Key))
                //        {

                //            finalFinalBalance[kvp.Key] = kvp.Value + bvp.Value;

                //        }

                //    }
                //}

                //}


                Dictionary<string, decimal> resDict = new Dictionary<string, decimal>();
                var dict1 = finalFinalBalance;
                var dict2 = balanceResult;
                var dict3 = balances;

                resDict = dict1.Concat(dict2)
                                 .Concat(dict3)
                                 .GroupBy(x => x.Key)
                                 .ToDictionary(x => x.Key, x => x.Sum(y => y.Value));

                return resDict;
                }
            
        }

        public Leaderboards BalanceTradesReader(SqlDataReader reader)
        {
            Leaderboards list = new Leaderboards();
            list.Balance = Convert.ToDecimal(reader["balance"]);
            list.Username = Convert.ToString(reader["username"]);
            return list;
        }
    }
}


