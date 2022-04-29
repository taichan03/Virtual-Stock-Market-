<template>
  <div class="text-center description-background">
    <div id="text-container">
      <h1 id="h1">Game Description</h1>
      <p class="spacer"></p>
      <p id="description">
        &nbsp;&nbsp;&nbsp;&nbsp;Welcome to Amber Trade! Learning how to trade in the stock market with real money can be quite intimidating,
especially when it's your money. It would be great if there were a way to learn how to trade in the stock market without the risk of losing your shirt.
AT - The Virtual Stock Market is the solution!
Learn how to trade on a totally virtual platform.
A gamified environment where you can hone your stock market skills and invite/challenge others to show off your trading prowess.
      </p>
    </div>
  </div>
</template>


<script>

export default {
    name: "aboutAmberTrade",
    
}
</script>


<style scoped>
.description-background {
  background-color: transparent;
  padding-top: 5%;
  position: fixed;
  overflow: auto;
  height: 100%;
  width: 100%;
  padding-top: 60px;
  padding-bottom: 220px;
}
#text-container {
  border: 2px solid black;
  border-radius: 25px;
  background-image: none;
  background: linear-gradient(to right, #59c3c3, #6969b3 );
  color: white;
  margin: auto;
  padding: 25px;
  width: 60%;
}
</style>