import { Module } from 'vuex-smart-module';
import { SelectorsActions } from './selectorsActions';
import { SelectorsGetters } from './selectorsGetters';
import { SelectorsMutations } from './selectorsMutations';
import { SelectorsState } from './selectorsState';

export const selectorsStore = new Module({
  state: SelectorsState,
  getters: SelectorsGetters,
  mutations: SelectorsMutations,
  actions: SelectorsActions,
});
