import { Module } from 'vuex-smart-module';
import { UserActions } from './userActions';
import { UserGetters } from './userGetters';
import { UserMutations } from './userMutations';
import { UserState } from './userState';

export const userStore = new Module({
  state: UserState,
  getters: UserGetters,
  mutations: UserMutations,
  actions: UserActions,
});
