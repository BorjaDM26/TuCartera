import { Actions } from 'vuex-smart-module';
import { PortfolioGetters } from './portfolioGetters';
import { PortfolioMutations } from './portfolioMutations';
import { PortfolioState } from './portfolioState';

import {
  portfolioList,
  portfolioAdd,
  portfolioEdit,
  portfolioDelete,
  portfolioItem,
} from '@/webservices/portfolios/portfolioWebservices';
import {
  PortfolioAddRequest,
  PortfolioEditRequest,
  PortfolioDeleteRequest,
  PortfolioItemRequest,
} from '@/webservices/portfolios/models';
import { FetchStatus } from '@/models/enum';
import { Portfolio } from '@/models/api';
import {
  tickersState,
  tickersValue,
} from '@/webservices/tickers/tickerWebservices';

export class PortfolioActions extends Actions<
  PortfolioState,
  PortfolioGetters,
  PortfolioMutations,
  PortfolioActions
> {
  public async initialise(): Promise<void> {
    if (this.state.fetchPortfoliosStatus === FetchStatus.PENDING) {
      await this.dispatch('fetchPortfolios');
      await this.dispatch('fetchTickersState');
      await this.dispatch('fetchTickersValue');
    }
  }

  public async fetchPortfolios(): Promise<void> {
    try {
      this.commit('setFetchPortfoliosStatus', FetchStatus.LOADING);
      const portfolios = await portfolioList();
      this.commit('setPortfolios', portfolios);
      this.commit('setFetchPortfoliosStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchPortfoliosStatus', FetchStatus.FAILURE);
    }
  }

  public async fetchPortfolioItem(params: PortfolioItemRequest): Promise<void> {
    try {
      this.commit('setFetchPortfolioItemStatus', FetchStatus.LOADING);
      const portfolio = await portfolioItem(params);
      this.dispatch('setSelectedPortfolio', portfolio);
      this.commit('setFetchPortfolioItemStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchPortfolioItemStatus', FetchStatus.FAILURE);
    }
  }

  public async setSelectedPortfolio(
    portfolio: Portfolio | null
  ): Promise<void> {
    this.commit('setSelectedPortfolio', portfolio);
  }

  public async addPortfolio(params: PortfolioAddRequest): Promise<void> {
    try {
      this.commit('setFetchPortfolioAddStatus', FetchStatus.LOADING);
      await portfolioAdd(params);
      await this.dispatch('fetchPortfolios');
      this.commit('setFetchPortfolioAddStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchPortfolioAddStatus', FetchStatus.FAILURE);
    }
  }

  public async editPortfolio(params: PortfolioEditRequest): Promise<void> {
    try {
      this.commit('setFetchPortfolioEditStatus', FetchStatus.LOADING);
      await portfolioEdit(params);
      await this.dispatch('fetchPortfolios');
      this.commit('setFetchPortfolioEditStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchPortfolioEditStatus', FetchStatus.FAILURE);
    }
  }

  public async deletePortfolio(params: PortfolioDeleteRequest): Promise<void> {
    try {
      this.commit('setFetchPortfolioDeleteStatus', FetchStatus.LOADING);
      await portfolioDelete(params);
      await this.dispatch('fetchPortfolios');
      this.commit('deletePortfolio', params.id);
      this.commit('setFetchPortfolioDeleteStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchPortfolioDeleteStatus', FetchStatus.FAILURE);
    }
  }

  public async fetchTickersState(): Promise<void> {
    try {
      this.commit('setFetchTickersStateStatus', FetchStatus.LOADING);
      const states = await tickersState();
      this.commit('setTickersState', states);
      this.commit('setFetchTickersStateStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchTickersStateStatus', FetchStatus.FAILURE);
    }
  }

  public async fetchTickersValue(): Promise<void> {
    try {
      this.commit('setFetchTickersValueStatus', FetchStatus.LOADING);
      const values = await tickersValue();
      this.commit('setTickersValue', values);
      this.commit('setFetchTickersValueStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchTickersValueStatus', FetchStatus.FAILURE);
    }
  }
}
