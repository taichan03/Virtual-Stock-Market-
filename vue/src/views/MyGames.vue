<template>
  <div class="content">

<div class="card-background text-center" id="game-actions">
       <h1>Hi {{profile.username}}!</h1>
        <h2>TE Office Stock Challenge</h2>

    <div class="buy-sell-container"> 
      
        <div class= "buy-stock-form">
          <h4> Get a stock quote here:</h4>
                <input type="text" id="search" v-model="searchStock" placeholder="Search for your stock" @keydown.enter="retrieveStock(searchStock)" />
               <h4>The {{searchStock}} price is ${{stock.close}}.</h4>


              <div class="buy-inputs">
              <input type="text" id="buy-stock-ticker" placeholder="Stock Ticker" v-model="buyStockObject.stockTicker" autofocus /> <br>
              <label for="quantity-buy">Number of Stocks:</label>   <br>
              <input type="number" id="quantity-buy" v-model="buyStockObject.quantity" autofocus />
              </div>
              <div class="button-group">
              <button type="button" 
              class="btn1 btn-primary btn-rounded btn-block buysell-button"
              v-on:click="clearForms"
              >Buy Stocks</button>
              </div>
        </div>
          
        <div class="sell-stock-form">
              <div class="sell-inputs">
              <input type="text" id="sell-stock-ticker" placeholder="Stock Ticker" v-model="sellStockObject.stockTicker" autofocus /> <br>
              <label for="quantity-buy">Number of Stocks:</label>   <br>
              <input type="number" id="quantity-sell" v-model="sellStockObject.quantity" autofocus />
              </div>
              <div class="button-group">
                <button type="button" 
                class="btn2 btn-danger btn-rounded btn-block buysell-button"
                v-on:click="clearForms"
                >Sell Stocks</button>
            </div>
        </div>  
    </div>
 </div>
     
  <div class="leaderboard card-background text-center" id="leaderboard">
      <div class="table-responsive">
        <h1>Leaderboard</h1>
          <table class="table table-hover table-dark">
            <thead class="thead-dark">
              <tr>
                <th scope="col">Name</th>
                <th scope="col">Portfolio Total</th>
              </tr>
            </thead>
            <tbody>
             <tr>
               <td>Myron.arcadeGuru88</td>
               <td>$120,128.63</td>
             </tr>
             <tr>
               <td>Yoav.83002</td>
               <td>$113,002.83</td>
             </tr>
             <tr>
               <td>Caitie.26.2</td>
               <td>$107,782.83</td>
             </tr>
             <tr>
               <td>Rich.motorcycleMan</td>
               <td>$100,784.50</td>
             </tr>
             <tr>
               <td>andrew</td>
               <td>$100,000.00</td>
             </tr>
            </tbody>
          </table>
      </div>
  </div>

      <div class="form-group" >
            <label for="name" class="sr-only">Player Name</label>
            <input
              type="text"
              id="name"
              class="form-control"
              placeholder="Player to Invite"
              v-model="invitePlayer"
              autofocus
            />
            <button
              type="button"
              class="btn btn-secondary btn-rounded"
              v-on:click="clearForms"
            >Invite Player</button>
       </div>
   

  </div>
</template>
<script>
import ApiService from '../services/ApiService.js';

export default {

  name: "my-games",
   data() {
    return {
      isOpen: false,
      invitePlayer: '', 
      searchStock: '',
      buyStockObject: {
        stockTicker: '',
        quantity: '', 
        price: '', 
      },
      sellStockObject: {
        stockTicker: '',
        quantity: '', 
        price: '', 
      },
    }
  },
  computed: {
    profile(){
      return this.$store.state.user;
    },
     game(){
      return this.$store.state.game;
    },
     stock(){
      return this.$store.state.stockPrice;
    },
  },

  methods:{
  clearForms(){
    this.searchStock= '',
    this.$store.state.stockPrice= '',
    this.buyStockObject.stockTicker= '',
    this.buyStockObject.quantity= '', 
    this.sellStockObject.stockTicker= '',
    this.sellStockObject.quantity= '', 
    this.invitePlayer= ''
  },


  addPlayerToGame(addNewPlayer){
    ApiService
    .addPlayer(addNewPlayer)
    .then(response => {
      if (response.status === 200){
        confirm(`You have added a player to the game: ${this.addNewPlayer.gameName}`)  //popup 
      }
      })
      .catch(error => {
        this.handleErrorResponse(error, "adding");
    })
    .catch(error => {
          if (error.response && error.response.status === 404) {
            alert(
              "Sorry, something went wrong. This game was not created. Please try again."
            );
          }
        });
    },

  getGameDetail(gameId) {
      ApiService
      .getGame(gameId)
      .then(response => {
        this.$store.commit("SET_CURRENT_GAME", response.data);
      });
    },

 buyStock(stockTransaction){
      ApiService
      .buyStock(stockTransaction)
      .then(response => {
      if (response.status === 200){
        this.$store.commit("SET_CURRENT_TRANSACTION", response.data);
        alert(`You have bought ${this.newBuyTransaction.Quantity} shares of ${this.newBuyTransaction.Stock} at $${this.newBuyTransaction.Purchase_Price}`)  
        //popup to inform the gameId
      }
      })
     .catch(error => {
        this.handleErrorResponse(error, "adding");
      })

      .catch(error => {
          if (error.response && error.response.status === 404) {
            alert(
              "Sorry, something went wrong. This transaction did not occur. Please try again."
            );
          }
        });
    },

  sellStock(stockTransaction){
      ApiService
      .sellStock(stockTransaction)
      .then(response => {
      if (response.status === 200){
        this.$store.commit("SET_CURRENT_TRANSACTION", response.data);
        alert(`You have sold ${this.newSellTransaction.Quantity} shares of ${this.newSellTransaction.Stock} at $${this.newSellTransaction.Purchase_Price}`)  
        //popup to inform the gameId
      }
      })
     .catch(error => {
        this.handleErrorResponse(error, "adding");
      })

      .catch(error => {
          if (error.response && error.response.status === 404) {
            alert(
              "Sorry, something went wrong. This transaction did not occur. Please try again."
            );
          }
        });
    },
//need to write the get leaderboard methods
  getGameLeaderboard(gameId) {
      ApiService
      .getLeaderboard(gameId)
      .then(response => {
        this.$store.commit("SET_CURRENT_LEADERBOARD", response.data);
      });
      console.log(this.$store.state.leaderboard)
     

    },
  retrieveStock(searchStock){
    ApiService
    .getStock(searchStock)
    .then(response => {
      this.$store.commit("SET_CURRENT_STOCK", response.data);
    })
    .catch(error => {
          if (error.response && error.response.status === 404) {
            alert(
              "Stock not available. This stock may not exist."
            );
            this.$router.push("/");
          }
        });
    },

  },

  created() {
    this.getGameLeaderboard(this.$route.params.id);  
    this.getGameDetail(this.$route.params.id);
    
  }, 
  
};

</script>




<style scoped>
.content{
  display: flex;
}
#leaderboard{
  color: white;
  }
#game-actions{
    align-self: flex-start;
  }
#form-group{
    min-width: 100px;
  }
.btn{
    color:white;
    background-color: blue;
  }
.btn1{
    color:white;
    background-color: green;
  }
.btn2{
    color:white;
    background-color: red;
  }



</style>
  