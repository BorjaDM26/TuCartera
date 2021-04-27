import Vue from 'vue';
import * as Vuex from 'vuex';
import { createStore, Module } from 'vuex-smart-module';

import { userStore as user } from './user/userStore';
import { transactionStore as transaction } from './transaction/transactionStore';

Vue.use(Vuex);

const root = new Module({
  modules: {
    user,
    transaction,
  },
});

export const store = createStore(root, {
  strict: process.env.NODE_ENV !== 'production',
});
