import { Module } from 'vuex-smart-module';
import { PortfolioActions } from './portfolioActions';
import { PortfolioGetters } from './portfolioGetters';
import { PortfolioMutations } from './portfolioMutations';
import { PortfolioState } from './portfolioState';

export const portfolioStore = new Module({
  state: PortfolioState,
  getters: PortfolioGetters,
  mutations: PortfolioMutations,
  actions: PortfolioActions,
});
