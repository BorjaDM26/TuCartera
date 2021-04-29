import { Currency, Ticker, TransactionType } from '@/models/api';
import { FetchStatus } from '@/models/enum';

export class SelectorsState {
  currencies: Currency[] = [];
  fetchCurrenciesStatus: FetchStatus = FetchStatus.PENDING;
  tickers: Ticker[] = [];
  fetchTickersStatus: FetchStatus = FetchStatus.PENDING;
  transactionTypes: TransactionType[] = [];
  fetchTransactionTypesStatus: FetchStatus = FetchStatus.PENDING;
}
