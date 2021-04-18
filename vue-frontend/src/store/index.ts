import Vue from 'vue';
import * as Vuex from 'vuex';
import { createStore, Module } from 'vuex-smart-module';

import { userStore as user } from './user/userStore';

Vue.use(Vuex);

export const store = createStore(user, {
  strict: process.env.NODE_ENV !== 'production',
});
