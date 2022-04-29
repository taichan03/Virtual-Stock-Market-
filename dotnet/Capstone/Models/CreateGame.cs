using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class CreateGame
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }



        public CreateGame(int gameId, string gameName, int userId, DateTime startDate, DateTime endDate)
        {
            this.GameId = gameId;
            this.GameName = gameName;
            this.UserId = userId;
            this.StartDate = startDate;
            this.EndDate = endDate;

        }

        public CreateGame()
        {

        }

  
    }
}
