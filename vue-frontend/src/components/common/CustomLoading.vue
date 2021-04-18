<template>
  <b-loading :is-full-page="true" v-model="isLoading" :can-cancel="true" />
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component({
  name: 'CustomLoading',
})
export default class CustomLoading extends Vue {
  @Prop({ default: false }) readonly isLoading: boolean | undefined;
}
</script>

<style scoped lang="scss"></style>
