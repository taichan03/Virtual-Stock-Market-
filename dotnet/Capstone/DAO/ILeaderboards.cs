using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
namespace Capstone.DAO
{
    public interface ILeaderboards
    {
        //userid int variable needs to change
        Dictionary<string, decimal> LeaderboardBalance(int userid);



    }
}
