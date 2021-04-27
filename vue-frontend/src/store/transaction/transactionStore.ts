import { Module } from 'vuex-smart-module';
import { TransactionActions } from './transactionActions';
import { TransactionGetters } from './transactionGetters';
import { TransactionMutations } from './transactionMutations';
import { TransactionState } from './transactionState';

export const transactionStore = new Module({
  state: TransactionState,
  getters: TransactionGetters,
  mutations: TransactionMutations,
  actions: TransactionActions,
});
