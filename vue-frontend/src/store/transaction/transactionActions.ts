import { Actions } from 'vuex-smart-module';
import { TransactionGetters } from './transactionGetters';
import { TransactionMutations } from './transactionMutations';
import { TransactionState } from './transactionState';

import {
  transactionList,
  transactionAdd,
  transactionEdit,
  transactionDelete,
  transactionItem,
} from '@/webservices/transactions/transactionWebservices';
import {
  TransactionAddRequest,
  TransactionEditRequest,
  TransactionDeleteRequest,
  TransactionItemRequest,
} from '@/webservices/transactions/models';
import { FetchStatus } from '@/models/enum';
import { Transaction } from '@/models/api';

export class TransactionActions extends Actions<
  TransactionState,
  TransactionGetters,
  TransactionMutations,
  TransactionActions
> {
  public async initialise(): Promise<void> {
    if (this.state.fetchTransactionsStatus === FetchStatus.PENDING) {
      await this.dispatch('fetchTransactions');
    }
  }

  public async fetchTransactions(): Promise<void> {
    try {
      this.commit('setFetchTransactionsStatus', FetchStatus.LOADING);
      let transactions = await transactionList();
      transactions = transactions.map(trans => ({
        ...trans,
        date: new Date(trans.date),
      }));
      this.commit('setTransactions', transactions);
      this.commit('setFetchTransactionsStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchTransactionsStatus', FetchStatus.FAILURE);
    }
  }

  public async fetchTransactionItem(
    params: TransactionItemRequest
  ): Promise<void> {
    try {
      this.commit('setFetchTransactionItemStatus', FetchStatus.LOADING);
      const transaction = await transactionItem(params);
      transaction.date = new Date(transaction.date);
      this.dispatch('setSelectedTransaction', transaction);
      this.commit('setFetchTransactionItemStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchTransactionItemStatus', FetchStatus.FAILURE);
    }
  }

  public async setSelectedTransaction(
    transaction: Transaction | null
  ): Promise<void> {
    this.commit('setSelectedTransaction', transaction);
  }

  public async addTransaction(params: TransactionAddRequest): Promise<void> {
    try {
      this.commit('setFetchTransactionAddStatus', FetchStatus.LOADING);
      await transactionAdd(params);
      await this.dispatch('fetchTransactions');
      this.commit('setFetchTransactionAddStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchTransactionAddStatus', FetchStatus.FAILURE);
    }
  }

  public async editTransaction(params: TransactionEditRequest): Promise<void> {
    try {
      this.commit('setFetchTransactionEditStatus', FetchStatus.LOADING);
      await transactionEdit(params);
      await this.dispatch('fetchTransactions');
      this.commit('setFetchTransactionEditStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchTransactionEditStatus', FetchStatus.FAILURE);
    }
  }

  public async deleteTransaction(
    params: TransactionDeleteRequest
  ): Promise<void> {
    try {
      this.commit('setFetchTransactionDeleteStatus', FetchStatus.LOADING);
      await transactionDelete(params);
      await this.dispatch('fetchTransactions');
      this.commit('deleteTransaction', params.id);
      this.commit('setFetchTransactionDeleteStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchTransactionDeleteStatus', FetchStatus.FAILURE);
    }
  }
}
