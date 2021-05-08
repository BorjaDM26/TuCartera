<template>
  <validation-observer
    ref="transactionEditObserver"
    class="transaction-edit-form"
    v-slot="{ invalid }"
  >
    <div class="error-message" v-if="message">
      <span>{{ message }}</span>
    </div>
    <div class="columns is-multiline">
      <b-field label="Tickers" class="column is-one-third ticker-selector">
        <b-dropdown
          v-model="tickerId"
          multiple
          @keyup.native.enter="save(invalid)"
        >
          <template #trigger>
            <b-button :class="{ 'dropdown-error': showTickerError }">
              {{ tickerPlaceholder }}
            </b-button>
          </template>

          <b-field class="ticker-dropdown_filter">
            <b-input
              v-model="tickerFilter"
              type="text"
              placeholder="Filtro"
            ></b-input>
          </b-field>

          <recycle-scroller
            class="scroller"
            :items="tickersFiltered"
            :item-size="32"
            key-field="id"
            v-slot="{ item }"
          >
            <span
              class="ticker-dropdown_item"
              :class="{
                'ticker-dropdown_item--active': tickerId === item.id,
              }"
              @click="tickerItemClicked(item)"
            >
              {{ `${item.code} - ${item.name}` }}
            </span>
          </recycle-scroller>
        </b-dropdown>
        <p class="help is-danger" v-if="showTickerError">Campo obligatorio</p>
      </b-field>

      <validation-provider
        rules="required|min_value:1"
        name="N de acciones"
        class="column is-one-third"
        v-slot="{ errors }"
      >
        <b-field
          label="Nº de acciones"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input
            type="number"
            v-model.number="shares"
            @keyup.native.enter="save(invalid)"
          ></b-input>
        </b-field>
      </validation-provider>

      <validation-provider
        rules="required|min_value:0"
        name="Precio unitario"
        class="column is-one-third"
        v-slot="{ errors }"
      >
        <b-field
          label="Precio unitario"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input
            type="number"
            v-model.number="unitPrice"
            @keyup.native.enter="save(invalid)"
          ></b-input>
        </b-field>
      </validation-provider>

      <validation-provider
        rules="required"
        name="Divisa"
        class="column is-one-third"
        v-slot="{ errors }"
      >
        <b-field
          label="Divisa"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-select
            placeholder="Seleccione una divisa"
            required
            v-model="currencyId"
            expanded
            @keyup.native.enter="save(invalid)"
          >
            <option
              v-for="currency in currencies"
              :key="currency.id"
              :value="currency.id"
            >
              {{ currency.code }}
            </option>
          </b-select>
        </b-field>
      </validation-provider>

      <validation-provider
        rules="required|min_value:0.00000001"
        name="Cambio a USD"
        class="column is-one-third"
        v-slot="{ errors }"
      >
        <b-field
          label="Cambio a USD"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input
            type="number"
            v-model.number="exchangeToUSD"
            @keyup.native.enter="save(invalid)"
          ></b-input>
        </b-field>
      </validation-provider>

      <validation-provider
        rules="required"
        name="Tipo"
        class="column is-one-third"
        v-slot="{ errors }"
      >
        <b-field
          label="Tipo"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-select
            placeholder="Seleccione un tipo"
            required
            v-model="transactionTypeId"
            expanded
            @keyup.native.enter="save(invalid)"
          >
            <option
              v-for="type in transactionTypes"
              :key="type.id"
              :value="type.id"
            >
              {{ type.type }}
            </option>
          </b-select>
        </b-field>
      </validation-provider>

      <validation-provider
        rules="required"
        name="Fecha"
        class="column is-one-third"
        v-slot="{ errors }"
      >
        <b-field
          label="Fecha"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-datepicker
            v-model="date"
            placeholder="Seleccione una fecha"
            trap-focus
          >
          </b-datepicker>
        </b-field>
      </validation-provider>

      <validation-provider
        name="Comentario"
        class="column is-full"
        v-slot="{ errors }"
      >
        <b-field
          label="Comentario"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input type="textarea" v-model="comment"></b-input>
        </b-field>
      </validation-provider>

      <div class="buttons">
        <button
          class="button green-button register-form_submit-button"
          @click="save(invalid)"
          :disabled="invalid"
        >
          Guardar
        </button>
        <button
          class="button red-button register-form_submit-button"
          @click="goBack()"
        >
          Cancelar
        </button>
      </div>
    </div>
  </validation-observer>
</template>

<script lang="ts">
import { Component, Prop, Watch, Ref, Vue } from 'vue-property-decorator';

import { ValidationObserver, ValidationProvider } from 'vee-validate';

import { Currency, Ticker, Transaction, TransactionType } from '@/models/api';

@Component({
  name: 'TransactionEditForm',
  components: {
    ValidationObserver,
    ValidationProvider,
  },
})
export default class TransactionEditForm extends Vue {
  @Prop({ default: '' }) message!: string;
  @Prop(Object) transactionInput!: Transaction;
  @Prop(Array) currencies!: Currency[];
  @Prop(Array) tickers!: Ticker[];
  @Prop(Array) transactionTypes!: TransactionType[];

  // Form fields
  private id = -1;
  private tickerId = 0;
  private unitPrice = 0;
  private exchangeToUSD = 0;
  private currencyId = 0;
  private shares = 0;
  private transactionTypeId = 0;
  private date: Date = new Date();
  private comment = '';

  private tickerCode = '';
  private tickerFilter = '';
  private tickerModified = false;

  created(): void {
    this.loadFormData(this.transactionInput);
  }

  @Watch('transactionInput')
  private updatedTransactionInput(transaction: Transaction | null): void {
    this.loadFormData(transaction);
  }

  @Ref()
  readonly transactionEditObserver!: InstanceType<typeof ValidationObserver>;

  private loadFormData(transaction: Transaction | null): void {
    const { tickers } = this;
    if (transaction) {
      this.id = transaction.id;
      this.tickerId = transaction.tickerId;
      this.unitPrice = transaction.unitPrice;
      this.exchangeToUSD = transaction.exchangeToUSD;
      this.currencyId = transaction.currencyId;
      this.shares = transaction.shares;
      this.transactionTypeId = transaction.transactionTypeId;
      this.date = transaction.date;
      this.comment = transaction.comment ?? '';

      if (transaction.tickerId) {
        this.tickerCode =
          tickers.find(ticker => transaction.tickerId === ticker.id)?.code ??
          '';
      }
    }
  }

  private get tickersFiltered(): Ticker[] {
    return this.tickers.filter(ticker =>
      `${ticker.code} - ${ticker.name}`
        .toUpperCase()
        .includes(this.tickerFilter.toUpperCase())
    );
  }

  private get tickerPlaceholder(): string {
    const { tickerCode } = this;
    return !tickerCode ? 'Seleccione un ticker' : tickerCode;
  }

  private tickerItemClicked(ticker: Ticker): void {
    this.tickerId = ticker.id;
    this.tickerCode = ticker.code;
    this.tickerModified = true;
  }

  private get showTickerError(): boolean {
    return this.tickerModified && !this.tickerId;
  }

  private save(isInvalid: boolean): void {
    if (isInvalid) {
      this.transactionEditObserver.validate();
      this.tickerModified = true;
      return;
    }

    if (!isInvalid) {
      const transaction = {
        id: this.id,
        tickerId: this.tickerId,
        unitPrice: this.unitPrice,
        exchangeToUSD: this.exchangeToUSD,
        currencyId: this.currencyId,
        shares: this.shares,
        transactionTypeId: this.transactionTypeId,
        date: this.date,
        comment: this.comment,
      };
      this.$emit('save', transaction);
    }
  }

  private goBack(): void {
    this.$router.go(-1);
  }
}
</script>

<style scoped lang="scss">
.ticker-selector {
  & /deep/ .dropdown {
    &,
    &-trigger,
    &-trigger button,
    &-menu {
      width: 100%;
    }

    &-trigger button {
      justify-content: flex-start;
    }

    &-content {
      overflow-y: auto;
    }

    &-item {
      padding-right: 1rem;

      span {
        display: block;
        overflow: hidden;
        white-space: nowrap;
        text-overflow: ellipsis;
      }
    }
  }

  .ticker-dropdown {
    &_item {
      display: block;
      overflow: hidden;
      white-space: nowrap;
      text-overflow: ellipsis;
      padding: 0 1rem;

      &:hover,
      &--active {
        color: $white;
        background-color: $blue-active;
      }
    }

    &_filter {
      padding: 0 0.75rem;
    }
  }
}

.dropdown-error {
  color: $red;
  border-color: $red;
}

.scroller {
  max-height: 175px;
}
</style>
