import { Actions } from 'vuex-smart-module';
import { SelectorsGetters } from './selectorsGetters';
import { SelectorsMutations } from './selectorsMutations';
import { SelectorsState } from './selectorsState';

import { FetchStatus } from '@/models/enum';
import {
  currencyList,
  tickerList,
  transactionTypeList,
} from '@/webservices/selectors/selectorsWebservices';

export class SelectorsActions extends Actions<
  SelectorsState,
  SelectorsGetters,
  SelectorsMutations,
  SelectorsActions
> {
  public async initialiseTransaction(): Promise<void> {
    if (this.state.fetchCurrenciesStatus === FetchStatus.PENDING) {
      await this.dispatch('fetchCurrencies');
    }
    if (this.state.fetchTickersStatus === FetchStatus.PENDING) {
      await this.dispatch('fetchTickers');
    }
    if (this.state.fetchTransactionTypesStatus === FetchStatus.PENDING) {
      await this.dispatch('fetchTransactionTypes');
    }
  }

  public async initialisePortfolio(): Promise<void> {
    if (this.state.fetchTickersStatus === FetchStatus.PENDING) {
      await this.dispatch('fetchTickers');
    }
  }

  public async fetchCurrencies(): Promise<void> {
    try {
      this.commit('setFetchCurrenciesStatus', FetchStatus.LOADING);
      const currencies = await currencyList();
      this.commit('setCurrencies', currencies);
      this.commit('setFetchCurrenciesStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchCurrenciesStatus', FetchStatus.FAILURE);
    }
  }

  public async fetchTickers(): Promise<void> {
    try {
      this.commit('setFetchTickersStatus', FetchStatus.LOADING);
      const tickers = await tickerList();
      this.commit('setTickers', tickers);
      this.commit('setFetchTickersStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchTickersStatus', FetchStatus.FAILURE);
    }
  }

  public async fetchTransactionTypes(): Promise<void> {
    try {
      this.commit('setFetchTransactionTypesStatus', FetchStatus.LOADING);
      const transactionTypes = await transactionTypeList();
      this.commit('setTransactionTypes', transactionTypes);
      this.commit('setFetchTransactionTypesStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchTransactionTypesStatus', FetchStatus.FAILURE);
    }
  }
}
