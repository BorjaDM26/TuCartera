<template>
  <div class="portfolio-card-new" @click="goToAddition">
    <span class="portfolio-card-new_icon">
      <font-awesome-icon icon="plus" />
    </span>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component({
  name: 'PortfolioCardItemNew',
})
export default class PortfolioCardItemNew extends Vue {
  private goToAddition(): void {
    this.$emit('portfolio-add');
  }
}
</script>

<style scoped lang="scss">
.portfolio-card-new {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
  background-color: $light-grey;
  border: 1px solid $light-black;
  border-radius: 4px;
  cursor: pointer;

  &_icon {
    background-color: white;
    border: 1px solid $light-black;
    border-radius: 100%;
    padding: 0.75rem 1rem;
  }
}
</style>
