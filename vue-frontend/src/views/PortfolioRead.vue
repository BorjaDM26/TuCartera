<template>
  <div class="portfolio-read" v-if="isPortfolioLoaded">
    <h2 class="c-title">Cartera - {{ portfolio.name }}</h2>

    <b-field class="portfolio-read_description" label="Descripción">
      <b-input
        type="textarea"
        v-model="portfolio.description"
        disabled
      ></b-input>
    </b-field>

    <ticker-list :tickers="portfolioTickers" />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import { portfolioStore } from '@/store/portfolio/portfolioStore';
import { selectorsStore } from '@/store/selectors/selectorsStore';

import TickerList from '@/components/tickers/TickerList.vue';

import { Portfolio } from '@/models/api';
import { TickerRow } from '@/models/tickers/tickerRow';

@Component({
  name: 'PortfolioRead',
  components: {
    TickerList,
  },
})
export default class PortfolioRead extends Vue {
  private portfolioCtx = portfolioStore.context(this.$store);
  private selectorsCtx = selectorsStore.context(this.$store);

  public created(): void {
    const portfolioId = this.$route.params.id;
    this.portfolioCtx.actions.fetchPortfolioItem({
      id: Number(portfolioId),
    });
  }

  private get portfolio(): Portfolio | null {
    return this.portfolioCtx.state.selectedPortfolio;
  }

  private get isPortfolioLoaded(): boolean {
    return !!this.portfolio;
  }

  private get portfolioTickers(): TickerRow[] {
    const { tickersInfo } = this.portfolioCtx.getters;
    const { portfolio } = this;

    if (portfolio) {
      return tickersInfo.filter(
        ticker => portfolio.isGlobal || portfolio.tickerIds.includes(ticker.id)
      );
    } else {
      return [];
    }
  }
}
</script>

<style scoped lang="scss">
.portfolio-read {
  width: 100%;

  &_description:not(:last-child) {
    margin-bottom: 2rem;
  }
}
</style>
