<template>
  <div>
 
      <div id="joingame-container">
    <div id="joingame" class="text-center">
      <div class="table-responsive">
        <h1>See your games below:</h1>
        <table class="table table-hover table-dark leaderboard" v-if="game">
          <thead class="thead-purple">
            <tr>
              <th scope="col">Game Name</th>
              <th scope="col">Date Created</th>
              <th scope="col">Game Ends</th>
              <th scope="col"></th>
            </tr>
           <!-- "SELECT U.username, G.game_name, G.startdate, G.enddate, B.balance" -->
          </thead>
          <tbody class="thead-purple">
            <tr v-bind:key="game.gameId" v-for="game in this.$store.state.games">
              <td>{{ game.username }}</td>
              <td>{{ game.gameName }}</td>
              <td>{{ game.startDate }}</td>
              <td>{{ game.endDate }}</td>
              <td>
                  <button 
                  type="button" 
                  class="btn btn-primary btn-rounded btn-sm m-0"
                  @click="$router.push({name: 'mygames', params: {id: game.gameId},})"
                  >Game Details</button>
              </td>
            </tr>
            <tr></tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>

  </div>
</template>

<script>
import ApiService from '../services/ApiService.js';


export default {
  name: "game-list",
  
  data(){
    return {
      game: {
        startDate: '',
        endDate: '',
        gameId: '', 
        gameName: '', 
      },
    };
  },

  computed: {
    profile(){
      return this.$store.state.user;
    },
     gameData(){
      return this.$store.state.game;
    }
  },

  methods:{
    retrieveGames(userId) {
      ApiService
      .getUserGames(userId)
      .then(response => {
        this.$store.commit("SET_GAMES_LIST", response.data);
      });
    },
    setGameStore(game){
      this.$store.commit("SET_CURRENT_GAME", game)
    }
  },
 
   created(){
    this.retrieveGames(this.profile.userId);
  },
  
};
</script>

<style scoped>
#thead-purple {
 background: orange;
  background-size: cover;
  padding-top: 5%;
  position: fixed;
  overflow: auto;
  width: 100%;
  height: 100%;
  padding-top: 60px;
  padding-bottom: 220px;
}
#thead-purple{
  width: 75%;
  padding: 25px;
  margin: auto;
  border-radius: 25px;
  border: 2px solid rgba(0, 0, 0, 0.05);
  background-color: purple;
  color: black;
}
#leaderboard{
    margin: 0;
    margin-left: 80px;
    min-width: 1rem;
    flex-grow: 1;
  }
</style>