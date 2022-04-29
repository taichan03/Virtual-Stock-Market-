<template>
  <div id="login" class="text-center">
    
    <form class="form-signin" @submit.prevent="login">
      <h3  class="title h3 mb-3 font-weight-normal">Login</h3>
      <div
        class="alert alert-danger"
        role="alert"
        v-if="invalidCredentials"
      >Invalid username and password!</div>
      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >Thank you for registering, please sign in.</div>
      <!-- <label for="username" class="sr-only">Username</label> -->
      <input
        type="text"
        id="username"
        class="form-control"
        placeholder="Username"
        v-model="user.username"
        required
        autofocus
      />
      <!-- <label for="password" class="sr-only">Password</label> -->
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"
        required
      />
      <button type="submit" class="submit">Log in</button>
      <router-link :to="{ name: 'register' }">
        <p></p>
      <button type="submit" class="submit">Register</button>

        </router-link>
      
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  }
};
</script>

<style scoped >
h3{
  color: white;
}
body{
  background-image: url("../images/pexels-en.jpg");
  background-position: center;
  background-size: cover;
  position: fill;
  height: 100%;
  width: 100%;
  background-repeat: no-repeat ;
  
  }

.text-center{
  text-align: center;
  width: 380px;
  height: 480px;
  position: relative;
  margin: 6% auto;
  background: #fff;
  padding: 5px;

 }  

.form-signin{
  text-align: center;
  border: 0px;
  color: white;
  padding-top: 40px;
}
.sr-only{
  text-align: center;
  display: block;
}
button{
display: block;
margin: auto;
padding: 5px;
margin-bottom: 30px;
}

.form-control{

  margin: 20px;
  width: 55%;
  text-align: center;
  border-left: 0px;
  border-right: 0px;
  border-top: 0px;
  border-bottom: 1px solid #999;
  outline: none ;
  background: transparent;
}

.submit{
width: 50%;
padding: 10px 30px;
cursor: pointer;
display: block;
margin: auto;
background:linear-gradient(to right, #59c3c3, #6969b3 );
border: 0px;
outline: 0;
border-radius: 30px;
align-content: center;
color: white;

}
.submit1{
width: 85%;
padding: 10px 30px;
cursor: pointer;
display: block;
margin: 30px;
background:linear-gradient(to right, #59c3c3, #6969b3 );
border: 0px;
outline: 0;
border-radius: 30px;
align-content: center;
 

}
.title{
width: 53%;
  padding: 10px 30px;
  display: block;
  margin: auto;
  background:linear-gradient(to right, #59c3c3, #6969b3 );
  border: 0px;
outline: 0px;
border-radius: 30px;
}
.form-register{
  color: white;
 padding-top: 40px;
}
#body {
  align-content: center;
  
}
</style>

