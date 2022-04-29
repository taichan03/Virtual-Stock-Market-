<template>
  <div class="content">
    <new-game-form></new-game-form>


  </div>


</template>
<script>
import NewGameForm from '../components/NewGameForm.vue';


export default {
  components: { NewGameForm },
  
  name: "new-game",

 
};


</script>




<style>
.content{
  background:linear-gradient(to right, #96d9d9, #6969b3 );
  margin-left: 10px;
  padding-left: 20px;

}
* {
  box-sizing: border-box;
}

body {
  margin: 0;
  font-family: Arial, Helvetica, sans-serif;
}

</style>
  