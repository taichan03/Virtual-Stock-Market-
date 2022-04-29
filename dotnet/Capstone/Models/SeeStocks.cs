using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class SeeStocks
    {

        public string Stock { get; set; }
        public string UserName { get; set; }
        public int Quanitity { get; set; } 
        public decimal PurchasePrice { get; set; } 
        //public decimal Sale_Price { get; set; } 
        public string Game_Name { get; set; } 
        public decimal Balance { get; set; } 


        public SeeStocks(string stock, string userName, int quanitity, decimal purchasePrice, string game_Name, decimal balance)
        {
            this.Stock = stock;
            this.UserName = userName;
            this.Quanitity = quanitity;
            this.PurchasePrice = purchasePrice;
            //this.Sale_Price = sale_Price;
            this.Game_Name = game_Name;
            this.Balance = balance;

        }

        public SeeStocks()
        {

        }


    }
}
