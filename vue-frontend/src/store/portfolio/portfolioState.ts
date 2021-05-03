import { Portfolio, TickerState, TickerValue } from '@/models/api';
import { FetchStatus } from '@/models/enum';

export class PortfolioState {
  portfolios: Portfolio[] = [];
  fetchPortfoliosStatus: FetchStatus = FetchStatus.PENDING;
  selectedPortfolio: Portfolio | null = null;
  fetchPortfolioItemStatus: FetchStatus = FetchStatus.PENDING;
  fetchPortfolioAddStatus: FetchStatus = FetchStatus.PENDING;
  fetchPortfolioEditStatus: FetchStatus = FetchStatus.PENDING;
  fetchPortfolioDeleteStatus: FetchStatus = FetchStatus.PENDING;

  tickersState: TickerState[] = [];
  fetchTickersStateStatus: FetchStatus = FetchStatus.PENDING;
  tickersValue: TickerValue[] = [];
  fetchTickersValueStatus: FetchStatus = FetchStatus.PENDING;
}
