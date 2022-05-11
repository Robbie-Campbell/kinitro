import { createRouter, createWebHistory } from 'vue-router'
import Main from '../views/Main.vue'
import Link from '../views/Link.vue'
import PathNotFound from '../views/PathNotFound.vue'

const routes = [
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: '/data/:slug',
    component: Main,
    name: 'Display'
  },
  {
    path: '/',
    component: Link,
    name: 'Link'
  },
  { path: '/:pathMatch(.*)*', component: PathNotFound },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
