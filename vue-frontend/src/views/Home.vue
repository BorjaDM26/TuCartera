<template>
  <div class="home">
    <portfolio-card-list />
    <transaction-list :is-limited="true" />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import TransactionList from '@/components/transactions/TransactionList.vue';
import PortfolioCardList from '@/components/portfolios/PortfolioCardList.vue';

import { transactionStore } from '@/store/transaction/transactionStore';
import { portfolioStore } from '@/store/portfolio/portfolioStore';

@Component({
  name: 'Home',
  components: {
    PortfolioCardList,
    TransactionList,
  },
})
export default class Home extends Vue {
  private transactionCtx = transactionStore.context(this.$store);
  private portfolioCtx = portfolioStore.context(this.$store);

  created(): void {
    this.transactionCtx.actions.initialise();
    this.portfolioCtx.actions.initialise();
  }
}
</script>

<style scoped lang="scss">
.home {
  display: flex;
  flex-direction: column;
}
</style>
