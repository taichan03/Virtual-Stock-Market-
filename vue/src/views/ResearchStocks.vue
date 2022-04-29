<template>
  <div class="content">

    <div>
     <h4>Get a stock quote here:</h4>
      <input type="text" id="search" v-model="searchStock" placeholder="Search for your stock" @keydown.enter="retrieveStock(searchStock)" />
        <div class="form-group">          
           <button class="btn btn-lg btn-primary btn-block" type="submit">Find Stock Price</button>        
       </div>  
      <h4>The {{searchStock}} price is ${{stock.close}}.</h4>
    </div>


</div>
</template>
<script>

import ApiService from '../services/ApiService.js'

export default {
  name: "research-stock-view",
  data(){
    return{
    searchStock: '',
    stockQuotes: [],
    newBuyTransaction: {
      Stock: '',
      User_Id: '',
      Game_Id: '',
      Quantity: '', 
      Purchase_Price: '', 
    },
    newSellTransaction: {
      Stock: '',
      User_Id: '',
      Game_Id: '',
      Quantity: '', 
      Purchase_Price: '', 
      Sale_Price: '',
    },
    Quote: {
      stock: '',
      price: '',
    },
    };
  },
  computed: {
    stock(){
      return this.$store.state.stockPrice;
    },
    portfolio(){
      return this.$store.state.holdings;
    }
    
  },
  methods: {
  getUserPortfolio(userId) {
      ApiService
      .getPortfolio(userId)
      .then(response => {
        this.$store.commit("SET_CURRENT_PORTFOLIO", response.data);
      });
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
  created(){
    this.getUserPortfolio(this.profile.userId);
  }  
};


</script>




<style>

/* .table{
  background-color: white;
}
.table-dark{
  color: rgb(223, 31, 63);
}
tr {
  display: table-row;
  vertical-align: middle;
} */
.research-background {
  background-color: blue;
  padding-top: 3%;
  position: fixed;
  overflow: auto;
  height: 100%;
  width: 100%;
  padding-top: 60px;
  padding-bottom: 220px;
}
#research-container {
  border: 2px solid black;
  border-radius: 25px;
  
  background-color: yellow;
  color: green;
  margin: auto;
  padding: 25px;
  width: 75%;
}
#search {
  margin: 20px;
  border-radius: 10px;
  padding: 2.5px;
  width: 40%;
  font-size: 55%;
}
#stocks-portfolio-container {
  margin-top: 5%;
  background-color: rgb(177, 176, 222);
}

</style>
  