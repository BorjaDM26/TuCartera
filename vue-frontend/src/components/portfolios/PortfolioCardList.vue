<template>
  <div class="portfolio-card-list">
    <h2 class="c-title">Carteras</h2>
    <b-carousel-list
      v-model="currentIndex"
      :data="portfolios"
      :items-to-show="$mq | mq({ sm: 1, md: 2, lg: 3, xl: 4 })"
      :arrow="false"
    >
      <template #item="portfolio">
        <template v-if="portfolio.id !== -1">
          <portfolio-card-item
            :portfolio="portfolio"
            @portfolio-clicked="goToPortfolioRead"
          />
        </template>
        <template v-else>
          <portfolio-card-item-new @portfolio-add="goToPortfolioAdd" />
        </template>
      </template>
    </b-carousel-list>
    <a class="carousel-arrow-left" v-if="showPrevArrow" @click="goPrev">
      <font-awesome-icon icon="chevron-left" />
    </a>
    <a class="carousel-arrow-right" v-if="showNextArrow" @click="goNext">
      <font-awesome-icon icon="chevron-right" />
    </a>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import { portfolioStore } from '@/store/portfolio/portfolioStore';
import {
  PortfolioAddRouteName,
  PortfolioReadRouteName,
} from '@/router/routeNames';

import PortfolioCardItem from '@/components/portfolios/PortfolioCardItem.vue';
import PortfolioCardItemNew from '@/components/portfolios/PortfolioCardItemNew.vue';
import { PortfolioRow } from '@/models/portfolios/portfolioRow';

@Component({
  name: 'PortfolioCardList',
  components: {
    PortfolioCardItem,
    PortfolioCardItemNew,
  },
})
export default class PortfolioCardList extends Vue {
  private portfolioCtx = portfolioStore.context(this.$store);

  private currentIndex = 0;

  private get portfolios(): (PortfolioRow | null)[] {
    const { portfoliosInfo } = this.portfolioCtx.getters;
    const addPortfolio: PortfolioRow = {
      id: -1,
      name: '',
      isGlobal: false,
      value: 0,
      benefit: 0,
    };
    return [...portfoliosInfo, addPortfolio];
  }

  private get showPrevArrow(): boolean {
    return this.currentIndex > 0;
  }

  private get showNextArrow(): boolean {
    const currentItems = this.portfolios.length;
    let shownItems = 0;

    switch (this.$mq) {
      case 'sm':
        shownItems = 1;
        break;
      case 'md':
        shownItems = 2;
        break;
      case 'lg':
        shownItems = 3;
        break;
      default:
        shownItems = 4;
        break;
    }
    return this.currentIndex < currentItems - shownItems;
  }

  private goPrev(): void {
    this.currentIndex--;
  }

  private goNext(): void {
    this.currentIndex++;
  }

  private goToPortfolioRead(portfolioId: number): void {
    const portfolio = this.portfolioCtx.state.portfolios.find(
      portf => portf.id === portfolioId
    );

    if (!portfolio) {
      this.$buefy.toast.open({
        message: 'No se pudo redireccionar a la información de la cartera',
        position: 'is-bottom-right',
        type: 'is-danger',
      });
    } else {
      this.portfolioCtx.actions.setSelectedPortfolio(portfolio);
      this.$router.push({
        name: PortfolioReadRouteName,
        params: { id: portfolioId.toString() },
      });
    }
  }

  private goToPortfolioAdd(): void {
    this.portfolioCtx.actions.setSelectedPortfolio(null);
    this.$router.push({ name: PortfolioAddRouteName });
  }
}
</script>

<style scoped lang="scss">
.portfolio-card-list {
  margin-bottom: 2rem;
  width: 100%;
  position: relative;

  & .carousel-arrow {
    &-right,
    &-left {
      position: absolute;
      background: white;
      border: 1px solid black;
      padding: 0.25rem 0.5rem;
      border-radius: 20px;
      top: 60%;
    }

    &-right {
      right: 0;
    }

    &-left {
      left: 0;
    }
  }

  & /deep/ {
    & .carousel-list.has-shadow {
      box-shadow: none;
    }

    & .carousel-slide {
      padding: 0 0.5rem;
    }

    & .carousel-arrow .icon.has-icons {
      &-left {
        left: 0;
      }
      &-right {
        right: 0;
      }
    }
  }
}
</style>
