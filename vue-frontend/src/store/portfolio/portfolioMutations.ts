import { Portfolio, TickerState, TickerValue } from '@/models/api';
import { FetchStatus } from '@/models/enum';
import { Mutations } from 'vuex-smart-module';
import { PortfolioState } from './portfolioState';

export class PortfolioMutations extends Mutations<PortfolioState> {
  setPortfolios(portfolios: Portfolio[]): void {
    this.state.portfolios = portfolios;
  }

  setFetchPortfoliosStatus(status: FetchStatus): void {
    this.state.fetchPortfoliosStatus = status;
  }

  setSelectedPortfolio(portfolio: Portfolio | null): void {
    this.state.selectedPortfolio = portfolio;
  }

  setFetchPortfolioItemStatus(status: FetchStatus): void {
    this.state.fetchPortfolioItemStatus = status;
  }

  setFetchPortfolioAddStatus(status: FetchStatus): void {
    this.state.fetchPortfolioAddStatus = status;
  }

  setFetchPortfolioEditStatus(status: FetchStatus): void {
    this.state.fetchPortfolioEditStatus = status;
  }

  deletePortfolio(portfolioId: number): void {
    this.state.portfolios = this.state.portfolios.filter(
      portfolio => portfolio.id !== portfolioId
    );
  }

  setFetchPortfolioDeleteStatus(status: FetchStatus): void {
    this.state.fetchPortfolioDeleteStatus = status;
  }

  setTickersState(tickersState: TickerState[]): void {
    this.state.tickersState = tickersState;
  }

  setFetchTickersStateStatus(status: FetchStatus): void {
    this.state.fetchTickersStateStatus = status;
  }

  setTickersValue(tickersValue: TickerValue[]): void {
    this.state.tickersValue = tickersValue;
  }

  setFetchTickersValueStatus(status: FetchStatus): void {
    this.state.fetchTickersValueStatus = status;
  }
}
