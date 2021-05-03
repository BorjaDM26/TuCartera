<template>
  <div class="portfolio">
    <router-view></router-view>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import { portfolioStore } from '@/store/portfolio/portfolioStore';

@Component({
  name: 'Portfolio',
})
export default class Portfolio extends Vue {
  private portfolioCtx = portfolioStore.context(this.$store);

  created(): void {
    this.portfolioCtx.actions.initialise();
  }
}
</script>
