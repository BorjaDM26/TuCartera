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

      <validation-provider
        rules="required"
        name="Tickers"
        class="column is-one-third"
        v-slot="{ errors }"
      >
        <b-field
          label="Ticker"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-select
            placeholder="Seleccione un ticker"
            required
            multiple
            v-model="tickerIds"
            expanded
            @keyup.native.enter="save(invalid)"
          >
            <option
              v-for="ticker in tickers"
              :key="ticker.id"
              :value="ticker.id"
            >
              {{ `${ticker.code} - ${ticker.name}` }}
            </option>
          </b-select>
        </b-field>
      </validation-provider>

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
          :disabled="invalid"
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

  created(): void {
    this.loadFormData(this.portfolioInput);
  }

  @Watch('portfolioInput')
  private updatedPortfolioInput(portfolio: Portfolio | null): void {
    this.loadFormData(portfolio);
  }

  @Ref()
  readonly portfolioEditObserver!: InstanceType<typeof ValidationObserver>;

  private loadFormData(portfolio: Portfolio | null): void {
    if (portfolio) {
      this.id = portfolio.id;
      this.name = portfolio.name;
      this.description = portfolio.description ?? '';
      this.tickerIds = portfolio.tickerIds;
    }
  }

  private save(isInvalid: boolean): void {
    if (isInvalid) {
      this.portfolioEditObserver.validate();
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

<style scoped lang="scss"></style>
