<template>

 
  <div class="content">
  <game-list>  

  </game-list>
<p></p>
 <router-link v-bind:to="{name: 'newgame'}"> <button> 
   Create a New Game</button></router-link>
  </div>



</template>
<script>
import GameList from '../components/GameList.vue';
export default {
  components: { GameList },
  name: "games",
  data() {
    return {
      isOpen: false,
    }
  },
  computed: {
    profile(){
      return this.$store.state.user;
    },
     game(){
      return this.$store.state.game;
    },
  }
};
</script>




<style scoped>
.content{
  display: grid;
  row-gap: 10px;
  column-gap: 20px;
  grid-template-columns: repeat(3, 1fr);
  gap: 10px;
  grid-auto-rows: minmax(100px, auto);
}
#leaderboard{
  display: flex;
    margin: 0;
    margin-left: 80px;
    min-width: 400px;
    align-items: stretch;
    box-sizing: border-box;
    font-size: 1rem;
    font-weight: 400;
    line-height: 1.5;
    background-color: indigo;
  }
  #game-actions{
    margin: 0;
    margin-left: 80px;
    min-width: 400px;
    flex-grow: 1;
    grid-column: 1;
    grid-row: 2 / 5;
  }
  .sr-only{
    text-align: center;
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


#leaderboard{
    margin: 0;
    margin-left: 80px;
    min-width: 400px;
    flex-grow: 1;
  }
  #game-actions{
     margin: 0;
    margin-left: 80px;
    min-width: 400px;
    flex-grow: 1;
  }
  #form-group{
        margin: 0;
    margin-left: 80px;
    min-width: 400px;
    flex-grow: 1;
    background-color: indianred;
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
  