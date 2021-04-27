import { Transaction } from '@/models/api';
import { FetchStatus } from '@/models/enum';

export class TransactionState {
  transactions: Transaction[] = [];
  fetchTransactionsStatus: FetchStatus = FetchStatus.PENDING;
  selectedTransaction: Transaction | null = null;
  fetchTransactionItemStatus: FetchStatus = FetchStatus.PENDING;
  fetchTransactionAddStatus: FetchStatus = FetchStatus.PENDING;
  fetchTransactionEditStatus: FetchStatus = FetchStatus.PENDING;
  fetchTransactionDeleteStatus: FetchStatus = FetchStatus.PENDING;
}
