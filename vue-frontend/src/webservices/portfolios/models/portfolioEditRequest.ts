import { PortfolioAddRequest } from './portfolioAddRequest';

export interface PortfolioEditRequest extends PortfolioAddRequest {
  id: number;
}
