<template>
  <b-modal :active="isActive" @close="close" has-modal-card trap-focus>
    <div class="modal-card">
      <header class="modal-card-head">
        <p class="modal-card-title">{{ title }}</p>
        <button type="button" class="delete" @click="close()" />
      </header>
      <section class="modal-card-body">
        <slot />
      </section>
      <footer class="modal-card-foot">
        <slot name="footer">
          <button @click="confirm()" class="button green-button">
            Confirmar
          </button>
          <button @click="close()" class="button">Cerrar</button>
        </slot>
      </footer>
    </div>
  </b-modal>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component({
  name: 'ModalCard',
})
export default class ModalCard extends Vue {
  @Prop({ default: false }) readonly isActive: boolean | undefined;
  @Prop({ default: 'Confirmar' }) readonly title!: string;

  private confirm(): void {
    this.$emit('confirm');
  }

  private close(): void {
    this.$emit('close');
  }
}
</script>

<style scoped lang="scss">
.modal-card-foot {
  justify-content: end;
}
</style>
