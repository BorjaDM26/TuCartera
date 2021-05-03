<template>
  <div class="portfolio-edit">
    <h2 class="c-title">{{ title() }}</h2>
    <portfolio-edit-form
      v-if="isFormInfoLoaded"
      :portfolio-input="portfolio"
      :tickers="tickers"
      :message="message"
      @save="save"
    />
  </div>
</template>

<script lang="ts">
import { Component, Watch, Vue } from 'vue-property-decorator';

import { portfolioStore } from '@/store/portfolio/portfolioStore';
import { selectorsStore } from '@/store/selectors/selectorsStore';

import PortfolioEditForm from '@/components/portfolios/PortfolioEditForm.vue';
import { HomeRouteName, PortfolioEditRouteName } from '@/router/routeNames';
import { Portfolio, Ticker } from '@/models/api';
import { FetchStatus } from '@/models/enum';

import {
  PortfolioAddRequest,
  PortfolioEditRequest,
} from '@/webservices/portfolios/models';

@Component({
  name: 'PortfolioEdit',
  components: {
    PortfolioEditForm,
  },
})
export default class PortfolioEdit extends Vue {
  private portfolioCtx = portfolioStore.context(this.$store);
  private selectorsCtx = selectorsStore.context(this.$store);

  private message = '';

  private portfolio: Portfolio = this.selectedPortfolio ?? {
    id: -1,
    name: '',
    isGlobal: false,
    tickerIds: [],
  };

  public created(): void {
    this.selectorsCtx.actions.initialisePortfolio();
    const fetchPortfolioToEdit =
      this.isEdition() && this.selectedPortfolio === null;
    if (fetchPortfolioToEdit) {
      const portfolioId = this.$route.params.id;
      this.portfolioCtx.actions.fetchPortfolioItem({
        id: Number(portfolioId),
      });
    }
  }

  @Watch('selectedPortfolio')
  private updatePortfolio(portfolio: Portfolio | null): void {
    if (portfolio && portfolio.isGlobal) {
      this.$buefy.toast.open({
        message: `No se puede editar la cartera global`,
        position: 'is-bottom-right',
        type: 'is-danger',
      });
      this.$router.push({ name: HomeRouteName });
    } else if (portfolio) {
      this.portfolio = {
        id: portfolio.id,
        name: portfolio.name,
        isGlobal: false,
        description: portfolio.description,
        tickerIds: portfolio.tickerIds,
      };
    }
  }

  private get selectedPortfolio(): Portfolio | null {
    return this.portfolioCtx.state.selectedPortfolio;
  }

  private get tickers(): Ticker[] {
    return this.selectorsCtx.state.tickers;
  }

  private title(): string {
    return this.isEdition() ? 'Editar cartera' : 'Añadir cartera';
  }

  private isEdition(): boolean {
    return this.$route.name === PortfolioEditRouteName;
  }

  private get isFormInfoLoaded(): boolean {
    const { areTickersFetched } = this.selectorsCtx.getters;
    return (
      (!this.isEdition() || this.selectedPortfolio !== null) &&
      areTickersFetched
    );
  }

  private async save(portfolio: Portfolio): Promise<void> {
    if (portfolio.id === -1) {
      const req: PortfolioAddRequest = {
        name: portfolio.name,
        description: portfolio.description,
        tickerIds: portfolio.tickerIds,
      };

      await this.portfolioCtx.actions.addPortfolio(req);
      if (
        this.portfolioCtx.state.fetchPortfolioAddStatus === FetchStatus.SUCCESS
      ) {
        this.$router.go(-1);
      } else {
        this.message =
          'Ha habido un problema al crear la cartera. Por favor, vuelva a intentarlo más tarde.';
      }
    } else {
      const req: PortfolioEditRequest = {
        id: portfolio.id,
        name: portfolio.name,
        description: portfolio.description,
        tickerIds: portfolio.tickerIds,
      };

      await this.portfolioCtx.actions.editPortfolio(req);
      if (
        this.portfolioCtx.state.fetchPortfolioEditStatus === FetchStatus.SUCCESS
      ) {
        this.$router.go(-1);
      } else {
        this.message =
          'Ha habido un problema al editar la cartera. Por favor, vuelva a intentarlo más tarde.';
      }
    }
  }
}
</script>

<style scoped lang="scss">
.portfolio-edit {
  width: 100%;
}
</style>
