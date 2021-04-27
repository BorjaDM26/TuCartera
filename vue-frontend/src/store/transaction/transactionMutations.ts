import { Transaction } from '@/models/api';
import { FetchStatus } from '@/models/enum';
import { Mutations } from 'vuex-smart-module';
import { TransactionState } from './transactionState';

export class TransactionMutations extends Mutations<TransactionState> {
  setTransactions(transactions: Transaction[]): void {
    this.state.transactions = transactions;
  }

  setFetchTransactionsStatus(status: FetchStatus): void {
    this.state.fetchTransactionsStatus = status;
  }

  setSelectedTransaction(transaction: Transaction | null): void {
    this.state.selectedTransaction = transaction;
  }

  setFetchTransactionItemStatus(status: FetchStatus): void {
    this.state.fetchTransactionItemStatus = status;
  }

  setFetchTransactionAddStatus(status: FetchStatus): void {
    this.state.fetchTransactionAddStatus = status;
  }

  setFetchTransactionEditStatus(status: FetchStatus): void {
    this.state.fetchTransactionEditStatus = status;
  }

  deleteTransaction(transactionId: number): void {
    this.state.transactions = this.state.transactions.filter(
      transaction => transaction.id !== transactionId
    );
  }

  setFetchTransactionDeleteStatus(status: FetchStatus): void {
    this.state.fetchTransactionDeleteStatus = status;
  }
}
