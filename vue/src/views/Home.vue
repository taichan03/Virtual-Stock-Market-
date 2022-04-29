<template>
  <div class="content">
    

  <user-detail></user-detail>

  </div>


</template>
<script>
import UserDetail from '../components/UserDetail.vue';
export default {
  components: { UserDetail },
  name: "home"
};
</script>




<style>

.content{
  align-content: center;
}



</style>
  