using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Leaderboards
    {

        public int GameId { get; set; }
        public int UserId { get; set; }
        public string Stock { get; set; }
        public int Quanitity { get; set; }

        public string Username { get; set; }

        public decimal Balance { get; set; }

        public Leaderboards(int game_id, int user_id, string stock, int quantity, string username, decimal balance)
        {
            this.GameId = game_id;
            this.UserId = user_id;
            this.Stock = stock;
            this.Quanitity = quantity;
            this.Username = username;
            this.Balance = balance;

            //
        }

        public Leaderboards()
        {
        }
    }


}
