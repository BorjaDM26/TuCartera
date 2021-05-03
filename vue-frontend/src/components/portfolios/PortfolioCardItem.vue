<template>
  <div
    class="portfolio-card"
    v-if="isPortfolioLoaded"
    @click="portfolioClicked"
  >
    <span class="portfolio-card_title">{{ portfolio.name }}</span>
    <span class="portfolio-card_value">
      <span class="portfolio-card_value-label">Valor:</span>
      <span class="portfolio-card_value-value"
        >{{ portfolio.value.toFixed(2) }}USD</span
      >
    </span>
    <span class="portfolio-card_benefit">
      <span class="portfolio-card_benefit-label">Beneficio:</span>
      <span
        class="portfolio-card_benefit-value"
        :class="[
          portfolio.benefit > 0 && 'green-color',
          portfolio.benefit < 0 && 'red-color',
        ]"
      >
        {{ portfolio.benefit.toFixed(2) }}USD
      </span>
    </span>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

import { PortfolioRow } from '@/models/portfolios/portfolioRow';

@Component({
  name: 'PortfolioCardItem',
})
export default class PortfolioCardItem extends Vue {
  @Prop({ default: null }) portfolio!: PortfolioRow;

  private get isPortfolioLoaded(): boolean {
    return !!this.portfolio;
  }

  private portfolioClicked(): void {
    this.$emit('portfolio-clicked', this.portfolio.id);
  }
}
</script>

<style scoped lang="scss">
.portfolio-card {
  background-color: $light-grey;
  border: 1px solid $light-black;
  border-radius: 4px;
  padding: 0.75rem 1.5rem;
  min-width: 250px;
  cursor: pointer;

  &_title {
    display: block;
    text-align: center;
    padding-bottom: 0.5rem;
    font-weight: bold;
  }

  &_value {
    display: flex;
    justify-content: space-between;
    padding: 0.5rem 0;

    &-label {
      display: inline-block;
      font-weight: bold;
    }
  }

  &_benefit {
    display: flex;
    justify-content: space-between;
    padding: 0.5rem 0;

    &-label {
      display: inline-block;
      font-weight: bold;
    }
  }
}

.green-color {
  color: $green;
}

.red-color {
  color: $red;
}
</style>
