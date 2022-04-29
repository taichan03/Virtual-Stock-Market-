using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class BuyAStock
    {
        public string Stock { get; set; }
        public int User_Id { get; set; }
        public int Game_Id { get; set; }
        public int Quantity { get; set; }
        public decimal Purchase_Price { get; set; }
        //public decimal Sale_Price { get; set; }


        public BuyAStock(string stock, int user_Id, int game_Id, int quantity, decimal purchase_Price)
        {
            this.Stock = stock;
            this.User_Id = user_Id;
            this.Game_Id = game_Id;
            this.Quantity = quantity;
            this.Purchase_Price = purchase_Price;

        }

        public BuyAStock()
        {

        }




    }
}
