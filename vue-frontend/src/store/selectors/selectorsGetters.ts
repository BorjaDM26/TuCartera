import { FetchStatus } from '@/models/enum';
import { Getters } from 'vuex-smart-module';
import { SelectorsState } from './selectorsState';

export class SelectorsGetters extends Getters<SelectorsState> {
  get isLoading(): boolean {
    const {
      fetchCurrenciesStatus,
      fetchTickersStatus,
      fetchTransactionTypesStatus,
    } = this.state;
    return [
      fetchCurrenciesStatus,
      fetchTickersStatus,
      fetchTransactionTypesStatus,
    ].includes(FetchStatus.LOADING);
  }

  get areCurrenciesFetched(): boolean {
    const { fetchCurrenciesStatus } = this.state;
    return (
      fetchCurrenciesStatus === FetchStatus.SUCCESS ||
      fetchCurrenciesStatus === FetchStatus.FAILURE
    );
  }

  get areTickersFetched(): boolean {
    const { fetchTickersStatus } = this.state;
    return (
      fetchTickersStatus === FetchStatus.SUCCESS ||
      fetchTickersStatus === FetchStatus.FAILURE
    );
  }

  get areTransactionTypesFetched(): boolean {
    const { fetchTransactionTypesStatus } = this.state;
    return (
      fetchTransactionTypesStatus === FetchStatus.SUCCESS ||
      fetchTransactionTypesStatus === FetchStatus.FAILURE
    );
  }
}
