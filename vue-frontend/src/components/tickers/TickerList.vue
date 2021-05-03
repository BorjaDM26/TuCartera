<template>
  <div class="ticker-list">
    <span class="c-subtitle">Tickers</span>
    <b-table
      class="ticker-list_table c-table"
      :data="tickers"
      :bordered="true"
      :hoverable="true"
      :mobile-cards="false"
    >
      <b-table-column
        field="code"
        label="Ticker"
        header-class="table-header table-header_code"
        v-slot="props"
      >
        {{ props.row.code }}
      </b-table-column>

      <b-table-column
        field="name"
        label="Nombre"
        header-class="table-header table-header_name"
        v-slot="props"
      >
        {{ props.row.name }}
      </b-table-column>

      <b-table-column
        field="bep"
        label="BEP"
        header-class="table-header table-header_bep"
        cell-class="table-cell_bep"
        v-slot="props"
      >
        {{ props.row.bep.toFixed(4) }}USD
      </b-table-column>

      <b-table-column
        field="currentValue"
        label="Valor Actual"
        header-class="table-header table-header_current-value"
        cell-class="table-cell_current-value"
        v-slot="props"
      >
        {{ props.row.currentValue }}USD
      </b-table-column>

      <b-table-column
        field="benefit"
        label="Beneficio (%)"
        header-class="table-header table-header_benefit"
        cell-class="table-cell_benefit"
        v-slot="props"
      >
        <span
          :class="[
            props.row.benefit > 0 && 'green-color',
            props.row.benefit < 0 && 'red-color',
          ]"
        >
          {{ props.row.benefit.toFixed(2) }}USD ({{
            getBenefitPercentage(props.row)
          }}%)
        </span>
      </b-table-column>

      <template #empty>
        <empty-message message="No hay tickers disponibles" />
      </template>
    </b-table>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

import EmptyMessage from '@/components/common/EmptyMessage.vue';
import { TickerRow } from '@/models/tickers/tickerRow';

@Component({
  name: 'TickerList',
  components: {
    EmptyMessage,
  },
})
export default class TickerList extends Vue {
  @Prop(Array) readonly tickers!: TickerRow[];

  private getBenefitPercentage(ticker: TickerRow): string {
    const percentage = (ticker.currentValue / ticker.bep - 1) * 100;
    return percentage.toFixed(2);
  }
}
</script>

<style scoped lang="scss">
.ticker-list {
  width: 100%;

  &_table /deep/ {
    & .table-header {
      &_code {
        width: 10%;
        min-width: 120px;
      }

      &_name {
        width: 45%;
        min-width: 250px;
      }

      &_bep {
        width: 15%;
        min-width: 120px;
      }

      &_current-value {
        width: 15%;
        min-width: 120px;
      }

      &_benefit {
        width: 15%;
        min-width: 120px;
      }
    }

    & .table-cell {
      &_bep,
      &_current-value,
      &_benefit {
        text-align: right;
      }
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
