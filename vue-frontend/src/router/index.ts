import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';

import Home from '@/views/Home.vue';
import Login from '@/views/Login.vue';
import Register from '@/views/Register.vue';
import Transaction from '@/views/Transaction.vue';
import TransactionList from '@/components/transactions/TransactionList.vue';
import TransactionEdit from '@/views/TransactionEdit.vue';
import Portfolio from '@/views/Portfolio.vue';
import PortfolioList from '@/components/portfolios/PortfolioList.vue';
import PortfolioEdit from '@/views/PortfolioEdit.vue';
import PortfolioRead from '@/views/PortfolioRead.vue';

import {
  HomeRouteName,
  LoginRouteName,
  RegisterRouteName,
  TransactionListRouteName,
  TransactionAddRouteName,
  TransactionEditRouteName,
  PortfolioListRouteName,
  PortfolioReadRouteName,
  PortfolioAddRouteName,
  PortfolioEditRouteName,
} from './routeNames';
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
    path: '/transaction',
    component: Transaction,
    meta: { requiresAuth: true },
    children: [
      {
        path: '',
        name: TransactionListRouteName,
        component: TransactionList,
      },
      {
        path: 'add',
        name: TransactionAddRouteName,
        component: TransactionEdit,
      },
      {
        path: 'edit/:id',
        name: TransactionEditRouteName,
        component: TransactionEdit,
      },
    ],
  },
  {
    path: '/portfolio',
    component: Portfolio,
    meta: { requiresAuth: true },
    children: [
      {
        path: '',
        name: PortfolioListRouteName,
        component: PortfolioList,
      },
      {
        path: 'add',
        name: PortfolioAddRouteName,
        component: PortfolioEdit,
      },
      {
        path: 'edit/:id',
        name: PortfolioEditRouteName,
        component: PortfolioEdit,
      },
      {
        path: 'read/:id',
        name: PortfolioReadRouteName,
        component: PortfolioRead,
      },
    ],
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

authGuard(router, LoginRouteName, HomeRouteName);

export default router;
