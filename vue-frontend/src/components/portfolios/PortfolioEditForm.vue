<template>
  <validation-observer
    ref="portfolioEditObserver"
    class="portfolio-edit-form"
    v-slot="{ invalid }"
  >
    <div class="error-message" v-if="message">
      <span>{{ message }}</span>
    </div>
    <div class="columns is-multiline">
      <validation-provider
        rules="required|min:3"
        name="Nombre"
        class="column is-one-third"
        v-slot="{ errors }"
      >
        <b-field
          label="Nombre"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input
            type="text"
            v-model.number="name"
            @keyup.native.enter="save(invalid)"
          ></b-input>
        </b-field>
      </validation-provider>

      <b-field label="Tickers" class="column is-one-third ticker-selector">
        <b-dropdown
          v-model="tickerIds"
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
                'ticker-dropdown_item--active': tickerIds.includes(item.id),
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
        name="Descripcion"
        class="column is-full"
        v-slot="{ errors }"
      >
        <b-field
          label="Descripción"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input type="textarea" v-model="description"></b-input>
        </b-field>
      </validation-provider>

      <div class="buttons">
        <button
          class="button green-button portfolio-form_submit-button"
          @click="save(invalid)"
          :disabled="invalid || tickerIds.length === 0"
        >
          Guardar
        </button>
        <button
          class="button red-button portfolio-form_submit-button"
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

import { Portfolio, Ticker } from '@/models/api';

const maxTickersShown = 4;

@Component({
  name: 'PortfolioEditForm',
  components: {
    ValidationObserver,
    ValidationProvider,
  },
})
export default class PortfolioEditForm extends Vue {
  @Prop({ default: '' }) message!: string;
  @Prop(Object) portfolioInput!: Portfolio;
  @Prop(Array) tickers!: Ticker[];

  // Form fields
  private id = -1;
  private name = '';
  private description = '';
  private tickerIds: number[] = [];
  private tickerCodes: string[] = [];
  private tickerFilter = '';
  private tickersModified = false;

  created(): void {
    this.loadFormData(this.portfolioInput);
  }

  @Watch('portfolioInput')
  private updatedPortfolioInput(portfolio: Portfolio | null): void {
    this.loadFormData(portfolio);
  }

  @Ref()
  readonly portfolioEditObserver!: InstanceType<typeof ValidationObserver>;

  private get tickersFiltered(): Ticker[] {
    return this.tickers.filter(ticker =>
      `${ticker.code} - ${ticker.name}`
        .toUpperCase()
        .includes(this.tickerFilter.toUpperCase())
    );
  }

  private loadFormData(portfolio: Portfolio | null): void {
    const { tickers } = this;
    if (portfolio) {
      this.id = portfolio.id;
      this.name = portfolio.name;
      this.description = portfolio.description ?? '';
      this.tickerIds = portfolio.tickerIds;

      if (portfolio.tickerIds.length) {
        this.tickerCodes = tickers
          .filter(ticker => portfolio.tickerIds.includes(ticker.id))
          .map(ticker => ticker.code);
      }
    }
  }

  private get tickerPlaceholder(): string {
    const { tickerCodes } = this;
    if (!tickerCodes.length) {
      return 'Seleccione al menos un ticker';
    } else {
      if (tickerCodes.length <= maxTickersShown) {
        return tickerCodes.join(', ');
      } else {
        return tickerCodes.slice(0, maxTickersShown).join(', ') + ', ...';
      }
    }
  }

  private get showTickerError(): boolean {
    return this.tickersModified && this.tickerIds.length === 0;
  }

  private tickerItemClicked(ticker: Ticker): void {
    if (this.tickerIds.includes(ticker.id)) {
      this.tickerIds = this.tickerIds.filter(
        tickerId => ticker.id !== tickerId
      );
      this.tickerCodes = this.tickerCodes.filter(
        tickerCode => ticker.code !== tickerCode
      );
    } else {
      this.tickerIds.push(ticker.id);
      this.tickerCodes.push(ticker.code);
    }
    this.tickersModified = true;
  }

  private save(isInvalid: boolean): void {
    if (isInvalid || this.tickerIds.length === 0) {
      this.portfolioEditObserver.validate();
      this.tickersModified = true;
      return;
    }

    if (!isInvalid) {
      const portfolio = {
        id: this.id,
        name: this.name,
        description: this.description,
        tickerIds: this.tickerIds,
      };
      this.$emit('save', portfolio);
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
