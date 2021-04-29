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
      <validation-provider
        rules="required"
        name="Ticker"
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
            v-model="tickerId"
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
        rules="required|min_value:0.00000001"
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
  private currencyId = 0;
  private shares = 0;
  private transactionTypeId = 0;
  private date: Date = new Date();
  private comment = '';

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
    if (transaction) {
      this.id = transaction.id;
      this.tickerId = transaction.tickerId;
      this.unitPrice = transaction.unitPrice;
      this.currencyId = transaction.currencyId;
      this.shares = transaction.shares;
      this.transactionTypeId = transaction.transactionTypeId;
      this.date = transaction.date;
      this.comment = transaction.comment ?? '';
    }
  }

  private save(isInvalid: boolean): void {
    console.log('Save', !isInvalid);
    if (isInvalid) {
      this.transactionEditObserver.validate();
      return;
    }

    if (!isInvalid) {
      const transaction = {
        id: this.id,
        tickerId: this.tickerId,
        unitPrice: this.unitPrice,
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

<style scoped lang="scss"></style>
