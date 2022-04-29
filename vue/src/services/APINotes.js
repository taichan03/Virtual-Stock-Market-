/*
(1) <invite player to game> (POST method)
path = https://localhost:44315/game/create
JSON BODY = {gameId}, {userId}, {startDate}, {endDate}
      { "gameName": "tyler1",
        "userId": 2,
        "startDate": "2012-06-18",
        "endDate": "2012-12-18" }
-----------------------------------------
(2) <get list of all games> (GET method)
query string = path = https://localhost:44315/game/{userId}
path = https://localhost:44315/game/1
-----------------------------------------
(3) <create game> (POST method)
query string = https://localhost:44315/game/invte/{userId} of user being invited
path = https://localhost:44315/game/invte/1
JSON BODY = {gameName}
      { "gameName": "testingGame5"}
-----------------------------------------
(4) <3rd party API to receive stock price> (GET method)
query string = https://localhost:44315/getprice/{stock ticker}
path https://localhost:44315/getprice/AAPL
-----------------------------------------
(5) <view games> (GET method)
query string = path = https://localhost:44315/trade/
JSON BODY = {UserName}
      { "UserName": "user"}

      (6) <buy a stock> (POST method)
path = https://localhost:44315/trade/buyastock
JSON BODY = {Stock, User_Id, Game_Id, Quantity, Purchase_Price}
 {
        "Stock": "AMD",
        "User_Id": 3,
        "Game_Id": 115,
        "Quantity": 100,
        "Purchase_Price": 95.53
 }
(7) <sell a stock> (POST method)
path = https://localhost:44315/Trade/sellastock
JSON BODY = {Stock, User_Id, Game_Id, Quantity, Purchase_Price, Sale_Price}
{
        "Stock": "CCC",
        "User_Id": 4,
        "Game_Id": 105,
        "Quantity": 200,
        "Purchase_Price": 500,
        "Sale_Price": 10
    }
*/