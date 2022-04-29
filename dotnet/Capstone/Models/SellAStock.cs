using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class SellAStock
    {

        public string Stock { get; set; }
        public int User_Id { get; set; }
        public int Game_Id { get; set; }
        public decimal Purchase_Price { get; set; }
        public decimal Sale_Price { get; set; }

        public int Quantity { get; set; }

        public decimal Balance { get; set; }

        public SellAStock()
        {

        }

        public SellAStock(string stock, int user_id, int game_id, decimal purchase_price, decimal sale_price, int quantity, decimal balance)
        {
            this.Stock = stock;
            this.User_Id = user_id;
            this.Game_Id = game_id;
            this.Purchase_Price = purchase_price;
            this.Sale_Price = sale_price;
            this.Quantity = quantity;
            this.Balance = balance; 


        }


    }
}
