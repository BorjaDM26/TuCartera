import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';

import Home from '../views/Home.vue';
import Login from '@/views/Login.vue';
import Register from '@/views/Register.vue';

import { HomeRouteName, LoginRouteName, RegisterRouteName } from './routeNames';
import { authGuard } from './guards';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: HomeRouteName,
    component: Home,
    meta: { requiresAuth: true },
  },
  {
    path: '/login',
    name: LoginRouteName,
    component: Login,
  },
  {
    path: '/register',
    name: RegisterRouteName,
    component: Register,
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ '../views/About.vue'),
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

authGuard(router, LoginRouteName, HomeRouteName);

export default router;
