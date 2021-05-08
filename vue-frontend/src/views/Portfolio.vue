<template>
  <div class="portfolio">
    <router-view></router-view>
    <custom-loading :isLoading="isLoading" />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import { portfolioStore } from '@/store/portfolio/portfolioStore';

import CustomLoading from '@/components/common/CustomLoading.vue';

@Component({
  name: 'Portfolio',
  components: {
    CustomLoading,
  },
})
export default class Portfolio extends Vue {
  private portfolioCtx = portfolioStore.context(this.$store);

  created(): void {
    this.portfolioCtx.actions.initialise();
  }

  private get isLoading(): boolean {
    return this.portfolioCtx.getters.isLoading;
  }
}
</script>
