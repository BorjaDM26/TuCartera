<template>
  <div class="home">
    <transaction-list :is-limited="true" />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import TransactionList from '@/components/transactions/TransactionList.vue';

import { transactionStore } from '@/store/transaction/transactionStore';

@Component({
  name: 'Home',
  components: {
    TransactionList,
  },
})
export default class Home extends Vue {
  private transactionCtx = transactionStore.context(this.$store);

  created(): void {
    this.transactionCtx.actions.initialise();
  }
}
</script>
