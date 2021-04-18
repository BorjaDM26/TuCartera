import Vue from 'vue';
import VueRouter from 'vue-router';

import { userStore } from '@/store/user/userStore';
import { FetchStatus } from '@/models/enum';

export const authGuard = (
  router: VueRouter,
  loginRouteName: string,
  homeRouteName: string
): void => {
  router.beforeEach(async (to, from, next) => {
    // Wait until user store is initialised
    if (!router.app.$store) {
      await Vue.nextTick();
    }
    const userCtx = userStore.context(router.app.$store);
    if (userCtx.state.fetchGetLoginStatus === FetchStatus.PENDING) {
      await userCtx.actions.initialise();
    }

    // Apply redirection if necessary
    const isLoggedIn = userCtx.getters.isLoggedIn;
    const requiresAuth = to.matched.some(record => record.meta.requiresAuth);

    if (!isLoggedIn && requiresAuth) {
      next({
        name: loginRouteName,
        query: { redirect: to.fullPath },
      });
    } else if (isLoggedIn && !requiresAuth) {
      next({ name: homeRouteName });
    } else {
      next();
    }
  });
};
