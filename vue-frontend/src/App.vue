<template>
  <div id="app">
    <app-header />
    <div class="app-content">
      <router-view class="router-view" />
      <custom-loading :isLoading="isLoading" />
    </div>
    <app-footer />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import AppHeader from '@/components/layout/AppHeader.vue';
import AppFooter from '@/components/layout/AppFooter.vue';
import CustomLoading from '@/components/common/CustomLoading.vue';

import { userStore } from './store/user/userStore';

@Component({
  name: 'App',
  components: {
    AppHeader,
    AppFooter,
    CustomLoading,
  },
})
export default class App extends Vue {
  private userCtx = userStore.context(this.$store);

  private get isLoading(): boolean {
    return this.userCtx.getters.isLoading;
  }
}
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.app-content {
  display: flex;
  justify-content: center;
  flex-grow: 1;
  padding: 1.5rem 3.5rem;

  & .router-view {
    display: flex;
    align-items: flex-start;
    max-width: 1300px;
    width: 100%;
  }
}
</style>
