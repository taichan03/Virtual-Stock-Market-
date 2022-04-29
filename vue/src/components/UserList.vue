<template>
  <div>
 
        <h1>Placeholder component for list of users</h1>
      


  </div>
</template>

<script>



export default {
  name: "user-list",
  
  data(){
    return {
      profile: {
        userId: '',
        firstName: '',
        lastName: '',
        username:'',
      },
      isLoading: true
    };
  },
  
  
};
</script>
