import Vue from 'vue'
import Router from 'vue-router'
import Hello from 'components/Hello'
import Login from 'components/Login'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'Hello',
      component: Hello
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    }
  ]
})
