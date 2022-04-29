using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class TradeDao : ITradeDao
    {
        private readonly string tradeDao;

        public TradeDao(string dbConnectionString)
        {
            this.tradeDao = dbConnectionString;
        }

        public List<SeeStocks> SeeStockDao(SeeStocks seeStockDao)
        {
            List<SeeStocks> seeStockList = new List<SeeStocks>();

            using (SqlConnection conn = new SqlConnection(tradeDao))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT U.username, B.balance, G.game_name, H.stock,H.quantity, H.purchase_price, H.sale_price FROM balance B " +
                                                "JOIN Game G ON B.game_id = G.game_id " +
                                                "JOIN holdings H ON G.game_id = H.game_id " +
                                                "JOIN users U ON h.user_id = U.user_id " +
                                                "WHERE H.user_id = (SELECT user_id FROM users where username = @username); ", conn);
                cmd.Parameters.AddWithValue("@username", seeStockDao.UserName);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    SeeStocks See = TradesReader(read);
                    seeStockList.Add(See);
                }
                return seeStockList;
            }
        }
        public SeeStocks TradesReader(SqlDataReader reader)
        {
            SeeStocks list = new SeeStocks();

            list.Stock = Convert.ToString(reader["stock"]);
            list.UserName = Convert.ToString(reader["username"]);
            list.Quanitity = Convert.ToInt16(reader["quantity"]);
            list.PurchasePrice = Convert.ToDecimal(reader["purchase_price"]);
            //list.Sale_Price = Convert.ToDecimal(reader["sale_price"]);
            list.Game_Name = Convert.ToString(reader["game_name"]);
            list.Balance = Convert.ToDecimal(reader["balance"]);

            return list;
        }
        public int BuyAStockDao(BuyAStock tradeInfoDao)
        {
            int result = 0;
            SeeStocks readBalance = new SeeStocks();

            using (SqlConnection conn = new SqlConnection(tradeDao))
            {
                conn.Open();

                SqlCommand balanceCmd = new SqlCommand("SELECT balance " +
                                                       "FROM balance " +
                                                       "WHERE game_id = @game_id AND user_id = @user_id ", conn);

                balanceCmd.Parameters.AddWithValue("@game_id", tradeInfoDao.Game_Id);
                balanceCmd.Parameters.AddWithValue("@user_id", tradeInfoDao.User_Id);

                SqlDataReader sqlBalance = balanceCmd.ExecuteReader();

                while (sqlBalance.Read())
                {
                    readBalance = BuyAStockReader(sqlBalance);
                }
            }

            using (SqlConnection secondConn = new SqlConnection(tradeDao))
            {
                secondConn.Open();
                if (readBalance.Balance > (tradeInfoDao.Quantity * tradeInfoDao.Purchase_Price))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO holdings (stock,  user_id, game_id, quantity, purchase_price, sale_price) " +
                                                    "OUTPUT INSERTED.game_id " +
                                                    "VALUES (@stock, @user_id, @game_id, @quantity, @purchase_price, 0 ) ", secondConn);
                    cmd.Parameters.AddWithValue("@stock", tradeInfoDao.Stock);
                    cmd.Parameters.AddWithValue("@user_id", tradeInfoDao.User_Id);
                    cmd.Parameters.AddWithValue("@game_id", tradeInfoDao.Game_Id);
                    cmd.Parameters.AddWithValue("@quantity", tradeInfoDao.Quantity);
                    cmd.Parameters.AddWithValue("@purchase_price", tradeInfoDao.Purchase_Price);

                    result = Convert.ToInt16(cmd.ExecuteScalar());
                    decimal cost = Convert.ToDecimal(tradeInfoDao.Quantity * tradeInfoDao.Purchase_Price);
                    decimal finalBalance = readBalance.Balance - cost;

                    using (SqlConnection threeConn = new SqlConnection(tradeDao))
                    {
                        threeConn.Open();
                        {
                            SqlCommand threeCmd = new SqlCommand("UPDATE balance SET balance = @finalBalance " +
                                                            "WHERE user_id = @user_id AND game_id = @game_id ", threeConn);
                            threeCmd.Parameters.AddWithValue("@finalBalance", finalBalance);
                            threeCmd.Parameters.AddWithValue("@user_id", tradeInfoDao.User_Id);
                            threeCmd.Parameters.AddWithValue("@game_id", tradeInfoDao.Game_Id);
                          
                            threeCmd.ExecuteNonQuery();
                        }
                    }
                }
                else result = 0;
            }
            return result;
        }
        public SeeStocks BuyAStockReader(SqlDataReader reader)
        {
            SeeStocks list = new SeeStocks();

            list.Balance = Convert.ToDecimal(reader["balance"]);

            return list;
        }
        public int GetQuantity(SellAStock sellAStock)
        {
            int quantity = 0; 

            using (SqlConnection conn = new SqlConnection(tradeDao))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT quantity FROM holdings " +
                                                "WHERE stock = @stock AND user_id = @user_id AND game_id = @game_id AND purchase_price = @purchase_price;", conn);
                cmd.Parameters.AddWithValue("@stock", sellAStock.Stock);
                cmd.Parameters.AddWithValue("@user_id", sellAStock.User_Id);
                cmd.Parameters.AddWithValue("@game_id", sellAStock.Game_Id);
                cmd.Parameters.AddWithValue("@purchase_price", sellAStock.Purchase_Price);

                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    SellAStock see = new SellAStock();
                    see = CreateQuantityFromReader(read);
                    quantity = see.Quantity;
                }

                return quantity; 
            }
        }


        public decimal SellAStock(SellAStock sellAStockDao)
        {
            decimal result = 0.00M;

            int sqlQuantity = GetQuantity(sellAStockDao);

            if(sqlQuantity < sellAStockDao.Quantity)
            {
                result = 0;
            }
            else if (sqlQuantity > sellAStockDao.Quantity)
            {
                using (SqlConnection conn = new SqlConnection(tradeDao))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Update holdings set quantity = @sqlQuantity - @quantity " +
                                                    "WHERE stock = @stock AND user_id = @user_id AND game_id = @game_id AND purchase_price = @purchase_price", conn);
                    cmd.Parameters.AddWithValue("@stock", sellAStockDao.Stock);
                    cmd.Parameters.AddWithValue("@user_id", sellAStockDao.User_Id);
                    cmd.Parameters.AddWithValue("@game_id", sellAStockDao.Game_Id);
                    cmd.Parameters.AddWithValue("@purchase_price", sellAStockDao.Purchase_Price);
                    cmd.Parameters.AddWithValue("@quantity", sellAStockDao.Quantity);
                    cmd.Parameters.AddWithValue("@sqlQuantity", sqlQuantity);

                    cmd.ExecuteNonQuery();

                    using (SqlConnection balanceConn = new SqlConnection(tradeDao))
                    {
                        decimal balance = 0.00M;

                        balanceConn.Open();
                        SqlCommand reader = new SqlCommand("SELECT balance FROM Balance " +
                                                "Where user_id = @user_id AND game_id = @game_id;", balanceConn);
                        reader.Parameters.AddWithValue("@user_id", sellAStockDao.User_Id);
                        reader.Parameters.AddWithValue("@game_id", sellAStockDao.Game_Id);

                        SqlDataReader sqlBalance = reader.ExecuteReader();

                        while (sqlBalance.Read())
                        {
                            SellAStock see = new SellAStock();
                            see = CreateSellAStockFromReader(sqlBalance);
                            balance = see.Balance;
                        }

                        balance += sellAStockDao.Sale_Price * sellAStockDao.Quantity;

                        using (SqlConnection newBalance = new SqlConnection(tradeDao))
                        {
                            newBalance.Open();
                            SqlCommand update = new SqlCommand("Update balance Set balance = @balance " +
                                                               "Where user_id = @user_id AND game_id = @game_id", newBalance);
                            update.Parameters.AddWithValue("@balance", balance);
                            update.Parameters.AddWithValue("@user_id", sellAStockDao.User_Id);
                            update.Parameters.AddWithValue("@game_id", sellAStockDao.Game_Id);

                            update.ExecuteNonQuery();
                        }
                        result = balance;
                    }
                }
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(tradeDao))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE holdings SET sale_price = @sale_price " +
                                                    "WHERE stock = @stock AND user_id = @user_id AND game_id = @game_id AND purchase_price = @purchase_price;", conn);
                    cmd.Parameters.AddWithValue("@sale_price", sellAStockDao.Sale_Price);
                    cmd.Parameters.AddWithValue("@stock", sellAStockDao.Stock);
                    cmd.Parameters.AddWithValue("@user_id", sellAStockDao.User_Id);
                    cmd.Parameters.AddWithValue("@game_id", sellAStockDao.Game_Id);
                    cmd.Parameters.AddWithValue("@purchase_price", sellAStockDao.Purchase_Price);

                    cmd.ExecuteNonQuery();

                    using (SqlConnection balanceConn = new SqlConnection(tradeDao))
                    {
                        decimal balance = 0.00M;

                        balanceConn.Open();
                        SqlCommand reader = new SqlCommand("SELECT balance FROM Balance " +
                                                "Where user_id = @user_id AND game_id = @game_id;", balanceConn);
                        reader.Parameters.AddWithValue("@user_id", sellAStockDao.User_Id);
                        reader.Parameters.AddWithValue("@game_id", sellAStockDao.Game_Id);

                        SqlDataReader sqlBalance = reader.ExecuteReader();

                        while (sqlBalance.Read())
                        {
                            SellAStock see = new SellAStock();
                            see = CreateSellAStockFromReader(sqlBalance);
                            balance = see.Balance;
                        }

                        balance += sellAStockDao.Sale_Price * sellAStockDao.Quantity;

                        using (SqlConnection newBalance = new SqlConnection(tradeDao))
                        {
                            newBalance.Open();
                            SqlCommand update = new SqlCommand("Update balance Set balance = @balance " +
                                                               "Where user_id = @user_id AND game_id = @game_id", newBalance);
                            update.Parameters.AddWithValue("@balance", balance);
                            update.Parameters.AddWithValue("@user_id", sellAStockDao.User_Id);
                            update.Parameters.AddWithValue("@game_id", sellAStockDao.Game_Id);

                            update.ExecuteNonQuery();
                        }

                        result = balance;

                        using (SqlConnection conns = new SqlConnection(tradeDao))
                        {
                            conns.Open();

                            SqlCommand cmds = new SqlCommand("Update holdings set quantity = @sqlQuantity - @quantity " +
                                                            "WHERE stock = @stock AND user_id = @user_id AND game_id = @game_id AND purchase_price = @purchase_price", conns);
                            cmds.Parameters.AddWithValue("@stock", sellAStockDao.Stock);
                            cmds.Parameters.AddWithValue("@user_id", sellAStockDao.User_Id);
                            cmds.Parameters.AddWithValue("@game_id", sellAStockDao.Game_Id);
                            cmds.Parameters.AddWithValue("@purchase_price", sellAStockDao.Purchase_Price);
                            cmds.Parameters.AddWithValue("@quantity", sellAStockDao.Quantity);
                            cmds.Parameters.AddWithValue("@sqlQuantity", sqlQuantity);

                            cmds.ExecuteNonQuery();
                        }
                    }

                }
            }
           
            return result;
        }
        public SellAStock CreateSellAStockFromReader(SqlDataReader reader)
        {
            SellAStock list = new SellAStock();

            list.Balance = Convert.ToDecimal(reader["balance"]);

            return list; 
        }
        public SellAStock CreateQuantityFromReader(SqlDataReader reader)
        {
            SellAStock list = new SellAStock();

            list.Quantity = Convert.ToInt32(reader["quantity"]);

            return list;
        }
    }
 }

//using (SqlConnection insertSales = new SqlConnection(tradeDao))
//{
//    insertSales.Open();

//    SqlCommand insert = new SqlCommand("insert holdings (stock, user_id, game_id, quantity, purchase_price, sale_price) " +
//                                       "values (@stock, @user_id, @game_id, @quantity, @purchase_price, @sale_price)", insertSales);
//    insert.Parameters.AddWithValue("@sale_price", sellAStockDao.Sale_Price);
//    insert.Parameters.AddWithValue("@stock", sellAStockDao.Stock);
//    insert.Parameters.AddWithValue("@user_id", sellAStockDao.User_Id);
//    insert.Parameters.AddWithValue("@game_id", sellAStockDao.Game_Id);
//    insert.Parameters.AddWithValue("@purchase_price", sellAStockDao.Purchase_Price);
//    insert.Parameters.AddWithValue("@quantity", sellAStockDao.Quantity);
//    //insert.Parameters.AddWithValue("@sqlQuantity", sqlQuantity);

//    insert.ExecuteNonQuery();
//    //int insertedHoldings = Convert.ToInt16(insert.ExecuteScalar());
//}