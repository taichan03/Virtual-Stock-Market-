using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Services;


namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IClosePriceDao ClosePriceServices;
        public ServicesController(IClosePriceDao closePriceServices)
        {
            this.ClosePriceServices = closePriceServices;
        }

        [HttpGet("/getprice/{stockTicker}")]
        public ClosePrice GetPrice(string stockTicker)
        {
            ClosePrice price = ClosePriceServices.GetPrice(stockTicker);
            return price;
        }

        //[HttpGet("/getprice/{stockTicker}")]
        //public List<ClosePrice.Rootobject> GetPrice(string stockTicker)
        //{
        //    List<ClosePrice.Rootobject> price = ClosePriceServices.GetPrice(stockTicker);
        //    return price;
        //}
    }






}
