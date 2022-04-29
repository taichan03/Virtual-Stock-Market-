using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ViewGames
    {
        public string UserName { get; set; }
        public string GameName { get; set; }
        public decimal Balance { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndDate { get; set; }

        public int GameId { get; set; }


        public ViewGames(string userName, string gameName, decimal balance, DateTime startTime, DateTime endDate, int gameId)
        {
            this.UserName = userName;
            this.GameName = gameName;
            this.Balance = balance;
            this.StartTime = startTime;
            this.EndDate = endDate;
            this.GameId = gameId;
        }

        public ViewGames()
        {


        }

    }

}
