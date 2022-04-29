using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ITradeDao
    {


        List<SeeStocks> SeeStockDao(SeeStocks seeStockDao);
        int BuyAStockDao(BuyAStock tradeInfoDao);
        decimal SellAStock(SellAStock sellAStockDao);
    }
}
