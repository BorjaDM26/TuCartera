<template>
  <div class="transaction-edit">
    <h2 class="c-title">{{ title() }}</h2>
    <transaction-edit-form
      v-if="isFormInfoLoaded"
      :transaction-input="transaction"
      :currencies="currencies"
      :tickers="tickers"
      :transaction-types="transactionTypes"
      :message="message"
      @save="save"
    />
  </div>
</template>

<script lang="ts">
import { Component, Watch, Vue } from 'vue-property-decorator';

import { transactionStore } from '@/store/transaction/transactionStore';
import { selectorsStore } from '@/store/selectors/selectorsStore';

import TransactionEditForm from '@/components/transactions/TransactionEditForm.vue';
import { TransactionEditRouteName } from '@/router/routeNames';
import { Currency, Ticker, Transaction, TransactionType } from '@/models/api';
import { FetchStatus } from '@/models/enum';
import {
  TransactionAddRequest,
  TransactionEditRequest,
} from '@/webservices/transactions/models';

import { formatDateToAPI } from '@/utils/dates';

@Component({
  name: 'TransactionEdit',
  components: {
    TransactionEditForm,
  },
})
export default class TransactionEdit extends Vue {
  private transactionCtx = transactionStore.context(this.$store);
  private selectorsCtx = selectorsStore.context(this.$store);

  private message = '';

  private transaction: Transaction = this.selectedTransaction ?? {
    id: -1,
    shares: 0,
    unitPrice: 0,
    exchangeToUSD: 1,
    date: new Date(),
    tickerId: 0,
    currencyId: 0,
    transactionTypeId: 0,
  };

  public created(): void {
    this.selectorsCtx.actions.initialiseTransaction();
    const fetchTransactionToEdit =
      this.isEdition() && this.selectedTransaction === null;
    if (fetchTransactionToEdit) {
      const transactionId = this.$route.params.id;
      this.transactionCtx.actions.fetchTransactionItem({
        id: Number(transactionId),
      });
    }
  }

  @Watch('selectedTransaction')
  private updateTransaction(transaction: Transaction | null): void {
    if (transaction) {
      this.transaction = {
        id: transaction.id,
        shares: transaction.shares,
        unitPrice: transaction.unitPrice,
        exchangeToUSD: transaction.exchangeToUSD,
        date: transaction.date,
        tickerId: transaction.tickerId,
        currencyId: transaction.currencyId,
        transactionTypeId: transaction.transactionTypeId,
      };
    }
  }

  private get selectedTransaction(): Transaction | null {
    return this.transactionCtx.state.selectedTransaction;
  }

  private get currencies(): Currency[] {
    return this.selectorsCtx.state.currencies;
  }

  private get tickers(): Ticker[] {
    return this.selectorsCtx.state.tickers;
  }

  private get transactionTypes(): TransactionType[] {
    return this.selectorsCtx.state.transactionTypes;
  }

  private title(): string {
    return this.isEdition() ? 'Editar transacción' : 'Añadir transacción';
  }

  private isEdition(): boolean {
    return this.$route.name === TransactionEditRouteName;
  }

  private get isFormInfoLoaded(): boolean {
    const {
      areCurrenciesFetched,
      areTickersFetched,
      areTransactionTypesFetched,
    } = this.selectorsCtx.getters;
    return (
      (!this.isEdition() || this.selectedTransaction !== null) &&
      areCurrenciesFetched &&
      areTickersFetched &&
      areTransactionTypesFetched
    );
  }

  private async save(transaction: Transaction): Promise<void> {
    if (transaction.id === -1) {
      const req: TransactionAddRequest = {
        shares: transaction.shares,
        unitPrice: transaction.unitPrice,
        exchangeToUSD: transaction.exchangeToUSD,
        date: formatDateToAPI(transaction.date),
        comment: transaction.comment,
        tickerId: transaction.tickerId,
        currencyId: transaction.currencyId,
        transactionTypeId: transaction.transactionTypeId,
      };

      await this.transactionCtx.actions.addTransaction(req);
      if (
        this.transactionCtx.state.fetchTransactionAddStatus ===
        FetchStatus.SUCCESS
      ) {
        this.$router.go(-1);
      } else {
        this.message =
          'Ha habido un problema al crear la transacción. Por favor, vuelva a intentarlo más tarde.';
      }
    } else {
      const req: TransactionEditRequest = {
        id: transaction.id,
        shares: transaction.shares,
        unitPrice: transaction.unitPrice,
        exchangeToUSD: transaction.exchangeToUSD,
        date: formatDateToAPI(transaction.date),
        comment: transaction.comment,
        tickerId: transaction.tickerId,
        currencyId: transaction.currencyId,
        transactionTypeId: transaction.transactionTypeId,
      };

      await this.transactionCtx.actions.editTransaction(req);
      if (
        this.transactionCtx.state.fetchTransactionEditStatus ===
        FetchStatus.SUCCESS
      ) {
        this.$router.go(-1);
      } else {
        this.message =
          'Ha habido un problema al editar la transacción. Por favor, vuelva a intentarlo más tarde.';
      }
    }
  }
}
</script>

<style scoped lang="scss">
.transaction-edit {
  width: 100%;
}
</style>
