import { Currency, Ticker, TransactionType } from '@/models/api';
import { FetchStatus } from '@/models/enum';
import { Mutations } from 'vuex-smart-module';
import { SelectorsState } from './selectorsState';

export class SelectorsMutations extends Mutations<SelectorsState> {
  setCurrencies(currencies: Currency[]): void {
    this.state.currencies = currencies;
  }

  setFetchCurrenciesStatus(status: FetchStatus): void {
    this.state.fetchCurrenciesStatus = status;
  }

  setTickers(tickers: Ticker[]): void {
    this.state.tickers = tickers;
  }

  setFetchTickersStatus(status: FetchStatus): void {
    this.state.fetchTickersStatus = status;
  }

  setTransactionTypes(transactionTypes: TransactionType[]): void {
    this.state.transactionTypes = transactionTypes;
  }

  setFetchTransactionTypesStatus(status: FetchStatus): void {
    this.state.fetchTransactionTypesStatus = status;
  }
}
