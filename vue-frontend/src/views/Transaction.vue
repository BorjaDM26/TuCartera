<template>
  <div class="transaction">
    <router-view></router-view>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import { transactionStore } from '@/store/transaction/transactionStore';

@Component({
  name: 'Transaction',
})
export default class Transaction extends Vue {
  private transactionCtx = transactionStore.context(this.$store);

  created(): void {
    this.transactionCtx.actions.initialise();
  }
}
</script>
