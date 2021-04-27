<template>
  <div class="transaction-list">
    <h2 class="c-title">Transacciones</h2>
    <b-table
      class="transaction-list_table c-table"
      :data="transactions"
      :bordered="true"
      :hoverable="true"
      :mobile-cards="false"
      :loading="isLoading"
    >
      <b-table-column
        field="date"
        label="Fecha"
        header-class="table-header table-header_date"
        :sortable="!isLimited"
        :searchable="!isLimited"
        v-slot="props"
      >
        {{ new Date(props.row.date).toLocaleDateString() }}
      </b-table-column>

      <b-table-column
        field="tickerCode"
        label="Ticker"
        header-class="table-header table-header_ticker-code"
        :sortable="!isLimited"
        :searchable="!isLimited"
        v-slot="props"
      >
        {{ props.row.tickerCode }}
      </b-table-column>

      <b-table-column
        field="tickerName"
        label="Nombre"
        header-class="table-header table-header_ticker-name"
        :sortable="!isLimited"
        :searchable="!isLimited"
        v-slot="props"
      >
        {{ props.row.tickerName }}
      </b-table-column>

      <b-table-column
        field="unitPrice"
        label="Precio"
        header-class="table-header table-header_unit-price"
        cell-class="table-cell_unit-price"
        :sortable="!isLimited"
        :searchable="!isLimited"
        v-slot="props"
      >
        {{ props.row.unitPrice }}{{ props.row.currencyCode }}
      </b-table-column>

      <b-table-column
        field="shares"
        label="N. Acciones"
        header-class="table-header table-header_shares"
        cell-class="table-cell_shares"
        :sortable="!isLimited"
        :searchable="!isLimited"
        v-slot="props"
      >
        {{ props.row.shares }}
      </b-table-column>

      <b-table-column
        field="transactionTypeType"
        label="Tipo"
        header-class="table-header table-header_transaction-type"
        :sortable="!isLimited"
        :searchable="!isLimited"
        v-slot="props"
      >
        {{ props.row.transactionTypeType }}
      </b-table-column>

      <b-table-column
        header-class="table-header table-header_actions"
        cell-class="table-cell_actions"
        v-slot="props"
      >
        <a @click="goToEdition(props.row)" class="edit-button">
          <font-awesome-icon icon="pen" />
        </a>
        <a @click="showDeleteModal(props.row.id)" class="delete-button">
          <font-awesome-icon icon="trash-alt" />
        </a>
      </b-table-column>

      <template #empty>
        <empty-message message="No hay transacciones disponibles" />
      </template>
    </b-table>
    <div class="transaction-list_add-button">
      <a @click="goToEdition()">
        <font-awesome-icon icon="plus" />
        Añadir
      </a>
    </div>

    <modal-card
      :is-active="isDeleteModalActive"
      @confirm="confirmDelete()"
      @close="closeDeleteModal()"
    >
      <p>¿Seguro que quieres eliminar la transacción?</p>
    </modal-card>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

import { Transaction } from '@/models/api';

import { transactionStore } from '@/store/transaction/transactionStore';
import {
  TransactionAddRouteName,
  TransactionEditRouteName,
} from '@/router/routeNames';

import EmptyMessage from '@/components/common/EmptyMessage.vue';
import ModalCard from '@/components/containers/ModalCard.vue';

@Component({
  name: 'TransactionList',
  components: {
    EmptyMessage,
    ModalCard,
  },
})
export default class TransactionList extends Vue {
  private transactionCtx = transactionStore.context(this.$store);

  @Prop({ default: false }) readonly isLimited: boolean | undefined;
  private limitedResults = 5;
  private isDeleteModalActive = false;
  private transactionToDeleteId: number | null = null;

  private get transactions(): Transaction[] {
    const { transactions } = this.transactionCtx.state;
    if (this.isLimited) {
      return transactions.slice(0, this.limitedResults);
    } else {
      return transactions;
    }
  }

  private goToEdition(transaction: Transaction): void {
    if (transaction) {
      this.transactionCtx.actions.setSelectedTransaction(transaction);
      this.$router.push({
        name: TransactionEditRouteName,
        params: { id: transaction.id.toString() },
      });
    } else {
      this.transactionCtx.actions.setSelectedTransaction(null);
      this.$router.push({ name: TransactionAddRouteName });
    }
  }

  private showDeleteModal(transactionId: number): void {
    this.transactionToDeleteId = transactionId;
    this.isDeleteModalActive = true;
  }

  private closeDeleteModal(): void {
    this.transactionToDeleteId = null;
    this.isDeleteModalActive = false;
  }

  private confirmDelete(): void {
    if (this.transactionToDeleteId) {
      this.transactionCtx.actions.deleteTransaction({
        id: this.transactionToDeleteId,
      });
      this.closeDeleteModal();
    }
  }

  private get isLoading(): boolean {
    return this.transactionCtx.getters.isLoading;
  }
}
</script>

<style scoped lang="scss">
.transaction-list {
  width: 100%;

  &_table /deep/ {
    & .table-header {
      &_date {
        width: 10%;
        min-width: 50px;
      }

      &_ticker-code {
        width: 10%;
        min-width: 50px;
      }

      &_ticker-name {
        width: 40%;
        min-width: 200px;
      }

      &_unit-price {
        width: 10%;
        min-width: 65px;
      }

      &_shares {
        width: 10%;
        min-width: 135px;
      }

      &_transaction-type {
        width: 10%;
        min-width: 60px;
      }

      &_actions {
        width: 10%;
        min-width: 50px;
      }
    }

    & .table-cell {
      &_unit-price {
        text-align: right;
      }

      &_shares {
        text-align: center;
      }

      &_actions {
        display: flex;
        justify-content: space-evenly;
      }
    }
  }
  &_add-button {
    text-align: right;
    padding: 0.5rem;
    font-weight: bold;

    & a {
      color: $green;
    }
  }
}

.edit-button {
  color: $blue;
  margin: 0 0.5rem;
}

.delete-button {
  color: $red;
  margin: 0 0.5rem;
}
</style>
