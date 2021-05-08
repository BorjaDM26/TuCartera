<template>
  <div class="empty-message">
    {{ message }}
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component({
  name: 'EmptyMessage',
})
export default class EmptyMessage extends Vue {
  @Prop({ default: 'No hay datos disponibles' }) readonly message!: string;
}
</script>

<style scoped lang="scss">
.empty-message {
  padding: 2.5rem;
  text-align: center;
  font-weight: bold;
}
</style>
