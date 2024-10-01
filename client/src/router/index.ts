import { createRouter, createWebHashHistory, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import store from '../store/index'

let authNotRequiredRoutes =[
  'login',
  'statuscode'
]


const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/LoginView.vue')
    },
    {
      path: '/statuscode',
      name: 'statuscode',
      component: () => import('../views/StatusCodeView.vue')
    },
  ]
})

router.beforeEach((to, from, next) => {
  if(!router.getRoutes().find(r => r.name == to.name))
    return

  if (store.getters._isAuthorized) {
    // if(authNotRequiredRoutes.indexOf(to.name) != -1)
      next();
  }
  else {
    if(authNotRequiredRoutes.indexOf(to.name) > -1)
      next();
    else if(from.name == 'statuscode' && authNotRequiredRoutes.indexOf(to.name) == -1)
      next({name : 'login'});
    else
      next({ name: 'statuscode'})
  }
});

export default router
