import { FetchStatus } from '@/models/enum';
import { PortfolioRow } from '@/models/portfolios/portfolioRow';
import { TickerRow } from '@/models/tickers/tickerRow';
import { Getters } from 'vuex-smart-module';
import { PortfolioState } from './portfolioState';

export class PortfolioGetters extends Getters<PortfolioState> {
  get isLoading(): boolean {
    const {
      fetchPortfoliosStatus,
      fetchPortfolioAddStatus,
      fetchPortfolioEditStatus,
      fetchPortfolioDeleteStatus,
      fetchTickersStateStatus,
      fetchTickersValueStatus,
    } = this.state;
    return [
      fetchPortfoliosStatus,
      fetchPortfolioAddStatus,
      fetchPortfolioEditStatus,
      fetchPortfolioDeleteStatus,
      fetchTickersStateStatus,
      fetchTickersValueStatus,
    ].includes(FetchStatus.LOADING);
  }

  get tickersInfo(): TickerRow[] {
    const { tickersState, tickersValue } = this.state;
    const tickersInfo: TickerRow[] = [];

    tickersState.forEach(state => {
      const tickerValue = tickersValue.find(value => value.id === state.id);
      const currentValue = tickerValue?.currentValue ?? -1;
      const row: TickerRow = {
        id: state.id,
        code: state.code,
        name: state.name,
        bep:
          state.currentShares !== 0
            ? state.totalInvested / state.currentShares
            : -1,
        currentValue,
        benefit: currentValue * state.currentShares - state.totalInvested,
        totatValue: state.currentShares * currentValue,
      };
      tickersInfo.push(row);
    });

    return tickersInfo;
  }

  get portfoliosInfo(): PortfolioRow[] {
    const { portfolios } = this.state;
    const { tickersInfo } = this.getters;
    const portfoliosInfo: PortfolioRow[] = [];

    portfolios.forEach(portfolio => {
      const tickersInPortfolio = tickersInfo.filter(
        ticker => portfolio.isGlobal || portfolio.tickerIds.includes(ticker.id)
      );
      const portfolioValues = tickersInPortfolio.reduce(
        (acc, val) => {
          return {
            totalValue: acc.totalValue + val.totatValue,
            benefit: acc.benefit + val.benefit,
          };
        },
        { totalValue: 0, benefit: 0 }
      );

      const row: PortfolioRow = {
        id: portfolio.id,
        name: portfolio.name,
        description: portfolio.description,
        isGlobal: portfolio.isGlobal,
        value: portfolioValues.totalValue,
        benefit: portfolioValues.benefit,
      };
      portfoliosInfo.push(row);
    });

    return portfoliosInfo;
  }
}
