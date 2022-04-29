import axios from 'axios';


export default {

  login(user) {
    return axios.post('/login', user)
  },

  register(user) {
    return axios.post('/register', user)
  },


//GET the c# endpoints:

  //will this be getting all users according to a gameId:  MUST CONFIRM THIS EXISTS
  getGame(gameId) {
    return axios.get(`/game/${gameId}`)
  },

  //#5:the complete portfolio:  all their stocks in all their games 
  getPortfolio(userName) {
    return axios.get(`/trade/seestocks/${userName}`)
  },

  // #8. Getting the leaderboard.  Send gameId.  returns list of everyone's username and total balance.  
  getLeaderboard(gameId){
    return axios.get(`/game/leaderboards/${gameId}`)
  },

  //#2 may not need userId in the URL
  getUserGames(userId) {
    return axios.get(`/game/viewgame/${userId}`)
  },

  //#3
  getStock(stockTicker) {
    return axios.get(`/getprice/${stockTicker}`)
  },

//POST the API endpoints

//#1
addPlayer(userId) {
  return axios.post(`/game/invite/${userId}`, userId)
},

//3
createGame(newGame) {
  return axios.post(`/game/create`, newGame)
},

buyStock(buyTransaction){
  return axios.post(`/trade/buyastock`, buyTransaction)
},

//#7
sellStock(sellTransaction){
  return axios.post(`/trade/sellastock`, sellTransaction)
}


}
