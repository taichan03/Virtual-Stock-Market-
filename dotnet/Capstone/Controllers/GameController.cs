using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization; 

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class GameController : ControllerBase
    {
        private IGameDao gameDAO;
        private ILeaderboards leaderboardsDAO;

        public GameController(IGameDao gameDAO, ILeaderboards leaderboardsDAO)
        {
            this.gameDAO = gameDAO;
            this.leaderboardsDAO = leaderboardsDAO;
        }

        [HttpPost("create")]
        public int CreateGame(CreateGame gameInfo)
        {
            int userId = gameInfo.UserId;
            string gameName = gameInfo.GameName;
            DateTime startDate = gameInfo.StartDate;
            DateTime endDate = gameInfo.EndDate;

            int createdGameId = gameDAO.CreateGameId(gameName, userId, startDate, endDate);
            return createdGameId;
        }

        [HttpGet("viewgame/{userId}")]
        public List<ViewGames> ViewGameByUserId(int userId)
        {
            List<ViewGames> listGames = gameDAO.ViewGamesByUserId(userId);
            return listGames;
        }

        [HttpPost("invite/{userId}")]
        public ActionResult<int> InvitePlayerGame(int userId, CreateGame gameId)
        {
            int added = gameDAO.InvitePlayer(userId, gameId);
            return Created($"/game/{added}", added);
        }

        [HttpGet("leaderboards/{leaderboards}")]
        public Dictionary<string, decimal> Leaderboard(int leaderboards)
        {
            Dictionary<string, decimal> leaderboards1 = leaderboardsDAO.LeaderboardBalance(leaderboards);
            return leaderboards1;
        }
    }
}
