using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models; 

namespace Capstone.DAO
{
    public interface IGameDao
    {
        int CreateGameId(string gameName, int userId, DateTime startDate, DateTime endDate);

        int CreateGameById(int newGameId, int userId);

        List<ViewGames> ViewGamesByUserId(int id);

        int InvitePlayer(int userId, CreateGame gameId);

    }
}
