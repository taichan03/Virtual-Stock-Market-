<template>

  <div class="body">
        <h1>WELCOME BACK {{profile.username}}!</h1>
          <h2> Below are details for your profile</h2>
            <ul>
              <li>username: {{profile.username}}</li>
              <li>Your access level is: {{profile.role}}</li>
              <li>Your userId is: {{profile.userId}}</li>
             
            </ul>


  </div>
</template>

<script>



export default {
  name: "user-detail",
  
  data(){
    return {
      isLoading: true
    };
  },
  computed: {
    profile(){
      return this.$store.state.user;
    }
  }
  
  
};
</script>

<style scoped>
/* .body{
    margin: auto;
  width: 60%;
  border: 3px solid ;
  padding-right: 400px;
  text-align: center;
  /* padding: 40px; */
  /* margin-top: 50px; */
  /* margin-right: 500px; */
 
ul{
  display: block;;
}
.content{
     margin: 0px 20px 0px 0px;
     padding: 0px 20px 0px 0px;
  width: 60%;
  border: 3px solid ;
  display: flex;
  text-align: center;
  display: block;
}
</style>