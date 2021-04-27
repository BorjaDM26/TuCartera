import { FetchStatus } from '@/models/enum';
import { Getters } from 'vuex-smart-module';
import { TransactionState } from './transactionState';

export class TransactionGetters extends Getters<TransactionState> {
  get isLoading(): boolean {
    const {
      fetchTransactionsStatus,
      fetchTransactionAddStatus,
      fetchTransactionEditStatus,
      fetchTransactionDeleteStatus,
    } = this.state;
    return [
      fetchTransactionsStatus,
      fetchTransactionAddStatus,
      fetchTransactionEditStatus,
      fetchTransactionDeleteStatus,
    ].includes(FetchStatus.LOADING);
  }
}
