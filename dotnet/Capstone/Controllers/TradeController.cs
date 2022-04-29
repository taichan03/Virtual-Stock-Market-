using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private ITradeDao TradeDao;

        public TradeController(ITradeDao tradeDao)
        {
            this.TradeDao = tradeDao;

        }

        [HttpGet("seestocks")]
        public List<SeeStocks> SeeStocks(SeeStocks gameStatus)
        {
            List<SeeStocks> seeStocksList = TradeDao.SeeStockDao(gameStatus);

            return seeStocksList;
        }

        [HttpPost("buyastock")]
        public int BuyAStock(BuyAStock buyAStockInfo)
        {
            int game_Id = TradeDao.BuyAStockDao(buyAStockInfo);

            return game_Id;
        }

        [HttpPut("sellastock")]
        public decimal SellAStock(SellAStock sellAStockInfo)
        {
            //SeeStocks seeStocks = new SeeStocks(sellAStockInfo);

           

            decimal game_Id = TradeDao.SellAStock(sellAStockInfo);

            return game_Id;
        }



    }
}
