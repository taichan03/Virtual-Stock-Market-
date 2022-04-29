<template>
  <div>
    <div id="creategame-container">    
      <div id="creategame" class="text-center">
      <form class="form-group" @submit.prevent="createNewGame(newGame)">        
      <h1 class="h3 mb-3 font-weight-normal">Create Game</h1>


  <div class="form-group">          
  <label for="startdate">Start Date</label>          
  <input type="date" 
  id="startdate" 
  class="form-control" 
  v-model="newGame.startDate" 
  required />        
  </div>

<div class="form-group">          
  <label for="enddate">End Date</label>          
  <input type="date" 
  id="enddate" 
  class="form-control" 
  placeholder="DateTime.Now" 
  v-model="newGame.endDate" 
  required />
</div>

<div class="form-group">          
  <label for="enddate">End Time</label>          
  <input type="time" 
  id="endtime" 
  class="form-control" 
  placeholder= "10:00:00"
  
   /> 
</div>

<div class="form-group"> 
  <label for="name" class="sr-only">Name</label>         
  <input type="text" 
  id="name" 
  class="form-control" 
  placeholder="Game Name" 
  v-model="newGame.gameName" 
  required />
</div>

<div class="form-group">
  <label for="description" class="sr-only">Description</label>
  <textarea id="description" 
  class="form-control" 
  placeholder="Description"            
  v-model="newGame.description" 
  rows="4" 
  />        
</div>

<div class="form-group">          
  <button class="btn btn-lg btn-primary btn-block" 
  type="submit">Create Game</button>        
</div>      

      </form>
      </div>
      </div>

  </div> 
</template>    
         
        
<script>
import ApiService from '../services/ApiService.js';


export default {

  name: "new-game-form",
  
  data(){
    return {
      newGame: {
        gameName: '',
        userId: '', 
        startDate: '',
        endDate: '',
      },
    };
  },
   
   
  computed: {
    profile(){
      return this.$store.state.user;
    },
    createdGame(){
      return this.$store.state.game;
    }
  },
   
   methods: {
   createNewGame(newGame){
    newGame.userId= this.profile.userId
    ApiService
    .createGame(newGame)
    .then(response => {
      if (response.status === 200){
        this.$store.commit("SET_CURRENT_GAME", response.data);
        confirm(`You have created a new game!`)  //popup to inform the gameId
        this.createdGame()
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
    } 
  }
}
</script>


<style scoped>
label {
  float: left;
}
#creategame-container {
  background: linear-gradient(to right, #59c3c3, #6969b3);
        background-size: cover;
  padding-top: 5%;
  padding-bottom: 10%;
  position: relative;
  overflow: auto;
  width: 100%;
  height: 100%;
  padding-top: 60px;
  padding-bottom: 220px;
}
#creategame {
  width: 25%;
  padding: 25px;
  margin: auto;
  border-radius: 25px;
  border: 2px solid rgba(0, 0, 0, 0.05);
  background-color: purple;
  color: white;
}
</style>
