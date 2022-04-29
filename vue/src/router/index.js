import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import Register from '../views/Register.vue'
import store from '../store/index'
import Games from '../views/Games.vue'
import NewGame from '../views/NewGame.vue'
import JoinGame from '../views/JoinGame.vue'
import ResearchStock from '../views/ResearchStocks.vue'
import AboutAmberTrade from '../views/AboutAmberTrade.vue'
import MyGames from '../views/MyGames.vue'
import ResearchStockPrice from '../components/ResearchStockPrice'

Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: true
      }
    },
   
    {
      path: '/stocks/:id',
      name: 'researchstockview',
      component: ResearchStock,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/researchstock',
      name: 'researchstock',
      component: ResearchStockPrice,
      meta: {
        requiresAuth: true
      }
    },
    {//parameterized : userId
      path: '/games/:id',
      name: 'games',
      component: Games,
      meta: {
        requiresAuth: true
      }
    },
    {//parameterized: gameId
      path: '/mygames/:id',
      name: 'mygames',
      component: MyGames,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/newgame',
      name: 'newgame',
      component: NewGame,
      meta: {
        requiresAuth: true
      }
    },
    { 
    path: '/joingame',
    name: 'joingame',
    component: JoinGame,
    meta: {
      requiresAuth: true
    }
    },  {
      path: "/aboutambertrade",
      name: "AboutAmberTrade",
      component: AboutAmberTrade,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/register",
      name: "register",
      component: Register,
      meta: {
        requiresAuth: false
      }
    },
  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;
