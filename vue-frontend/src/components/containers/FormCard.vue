<template>
  <div class="form-card">
    <slot />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component({
  name: 'FormCard',
})
export default class FormCard extends Vue {}
</script>

<style scoped lang="scss">
.form-card {
  background-color: $light-grey;
  padding: 0.75rem 2.75rem 1.5rem;
  border: 1px solid $black;
  border-radius: 1.5rem;
}
</style>
