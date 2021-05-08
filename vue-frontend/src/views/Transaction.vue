<template>
  <div class="transaction">
    <router-view></router-view>
    <custom-loading :isLoading="isLoading" />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import { transactionStore } from '@/store/transaction/transactionStore';

import CustomLoading from '@/components/common/CustomLoading.vue';

@Component({
  name: 'Transaction',
  components: {
    CustomLoading,
  },
})
export default class Transaction extends Vue {
  private transactionCtx = transactionStore.context(this.$store);

  created(): void {
    this.transactionCtx.actions.initialise();
  }

  private get isLoading(): boolean {
    return this.transactionCtx.getters.isLoading;
  }
}
</script>
