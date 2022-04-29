<template>
  <div>
 
        <h1>Placeholder for details for single game. </h1>
        <ul>
          <li>{{game.startDate}}</li>
          <li>{{game.endDate}}</li>
          <li>{{game.gameId}}</li>
          <li>{{game.gameName}}</li>
          <li> the leaderboard.  Sort by name or by balance</li>
          
        </ul>
       


  </div>
</template>

<script>
import ApiService from '../services/ApiService.js';

export default {
  name: "game-detail",
  
  data(){
    return {
      game: {
        startDate: '4/1/2022',
        endDate: '1/1/2023',
        gameId: '2345', 
        gameName: 'game Not Connected to API services', 

      },
      isLoading: true
    };
  },

  methods:{
    getGameDetail(gameId) {
      ApiService
      .getGame(gameId)
      .then(response => {
        this.$store.commit("SET_CURRENT_GAME", response.data);

      });
    },
  }
  // for dynamic routing to the newly created game
        // if (this.$route.name == "Home" && response.status === 200 && response.data.length > 0) {
        //   this.$router.push(`/board/${response.data[0].id}`);
        // } 
  
};
</script>
