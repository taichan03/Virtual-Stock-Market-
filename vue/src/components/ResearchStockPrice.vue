<template>
  <div>
   <div class="experiment">
     <h1> Here we are going to build buying and selling stocks</h1>
    <button type="button" class="btn btn-primary btn-rounded btn-block buysell-button">Buy Stocks</button>
            <button
              type="button"
              class="btn btn-danger btn-rounded btn-block buysell-button"
            >Sell Stocks</button>
  </div>
  </div>
</template>

<script>

export default {
  name: "research-stock",
  
  data(){
    return {
    };
  },
  computed: {
    
  }
  
  
};
</script>


<style scoped>
.experiment{
  background-color: papayawhip;
  color: black;
}


</style>