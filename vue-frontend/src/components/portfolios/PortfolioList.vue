<template>
  <div class="portfolio-list">
    <h2 class="c-title">Carteras</h2>
    <b-table
      class="portfolio-list_table c-table"
      :data="portfolios"
      :bordered="true"
      :hoverable="true"
      :mobile-cards="false"
      :loading="isLoading"
      @dblclick="goToRead"
    >
      <b-table-column
        field="name"
        label="Nombre"
        header-class="table-header table-header_name"
        :sortable="true"
        :searchable="true"
        v-slot="props"
      >
        {{ props.row.name }}
      </b-table-column>

      <b-table-column
        field="description"
        label="Descripción"
        header-class="table-header table-header_description"
        :sortable="true"
        :searchable="true"
        v-slot="props"
      >
        {{ props.row.description }}
      </b-table-column>

      <b-table-column
        field="value"
        label="Valor"
        header-class="table-header table-header_value"
        cell-class="table-cell_value"
        v-slot="props"
      >
        {{ props.row.value.toFixed(2) }}USD
      </b-table-column>

      <b-table-column
        field="benefit"
        label="Beneficio"
        header-class="table-header table-header_benefit"
        cell-class="table-cell_benefit"
        v-slot="props"
      >
        <span
          :class="[
            props.row.benefit > 0 && 'green-color',
            props.row.benefit < 0 && 'red-color',
          ]"
          >{{ props.row.benefit.toFixed(2) }}USD</span
        >
      </b-table-column>

      <b-table-column
        header-class="table-header table-header_actions"
        cell-class="table-cell_actions"
        v-slot="props"
      >
        <template v-if="!props.row.isGlobal">
          <a @click="goToEdition(props.row.id)" class="edit-button">
            <font-awesome-icon icon="pen" />
          </a>
          <a @click="showDeleteModal(props.row.id)" class="delete-button">
            <font-awesome-icon icon="trash-alt" />
          </a>
        </template>
        <span v-else>&nbsp;</span>
      </b-table-column>

      <template #empty>
        <empty-message message="No hay carteras disponibles" />
      </template>
    </b-table>
    <div class="portfolio-list_add-button">
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
      <p>¿Seguro que quieres eliminar la cartera?</p>
    </modal-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import { portfolioStore } from '@/store/portfolio/portfolioStore';
import {
  PortfolioAddRouteName,
  PortfolioEditRouteName,
  PortfolioReadRouteName,
} from '@/router/routeNames';

import EmptyMessage from '@/components/common/EmptyMessage.vue';
import ModalCard from '@/components/containers/ModalCard.vue';
import { PortfolioRow } from '@/models/portfolios/portfolioRow';

@Component({
  name: 'PortfolioList',
  components: {
    EmptyMessage,
    ModalCard,
  },
})
export default class PortfolioList extends Vue {
  private portfolioCtx = portfolioStore.context(this.$store);

  private isDeleteModalActive = false;
  private portfolioToDeleteId: number | null = null;

  private get portfolios(): PortfolioRow[] {
    return this.portfolioCtx.getters.portfoliosInfo;
  }

  private goToEdition(portfolioId?: number): void {
    if (portfolioId) {
      const portfolio =
        this.portfolioCtx.state.portfolios.find(
          portf => portf.id === portfolioId
        ) ?? null;
      this.portfolioCtx.actions.setSelectedPortfolio(portfolio);
      this.$router.push({
        name: PortfolioEditRouteName,
        params: { id: portfolioId.toString() },
      });
    } else {
      this.portfolioCtx.actions.setSelectedPortfolio(null);
      this.$router.push({ name: PortfolioAddRouteName });
    }
  }

  private goToRead(row: PortfolioRow) {
    const portfolio =
      this.portfolioCtx.state.portfolios.find(portf => portf.id === row.id) ??
      null;
    this.portfolioCtx.actions.setSelectedPortfolio(portfolio);
    this.$router.push({
      name: PortfolioReadRouteName,
      params: { id: row.id.toString() },
    });
  }

  private showDeleteModal(portfolioId: number): void {
    this.portfolioToDeleteId = portfolioId;
    this.isDeleteModalActive = true;
  }

  private closeDeleteModal(): void {
    this.portfolioToDeleteId = null;
    this.isDeleteModalActive = false;
  }

  private confirmDelete(): void {
    if (this.portfolioToDeleteId) {
      this.portfolioCtx.actions.deletePortfolio({
        id: this.portfolioToDeleteId,
      });
      this.closeDeleteModal();
    }
  }

  private get isLoading(): boolean {
    return this.portfolioCtx.getters.isLoading;
  }
}
</script>

<style scoped lang="scss">
.portfolio-list {
  width: 100%;

  &_table /deep/ {
    & .table-header {
      &_name {
        width: 20%;
        min-width: 160px;
      }

      &_description {
        width: 40%;
        min-width: 250px;
      }

      &_value {
        width: 10%;
        min-width: 150px;
      }

      &_benefit {
        width: 10%;
        min-width: 150px;
      }

      &_actions {
        width: 10%;
        min-width: 90px;
      }
    }

    & .table-cell {
      &_value,
      &_benefit {
        text-align: right;
      }

      &_actions {
        text-align: center;
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

.green-color {
  color: $green;
}

.red-color {
  color: $red;
}
</style>
