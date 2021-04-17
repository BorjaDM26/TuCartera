<template>
  <div id="app">
    <app-header />
    <div class="app-content">
      <router-view />
    </div>
    <app-footer />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import AppHeader from '@/components/Layout/AppHeader.vue';
import AppFooter from '@/components/Layout/AppFooter.vue';

@Component({
  name: 'App',
  components: {
    AppHeader,
    AppFooter,
  },
})
export default class App extends Vue {}
</script>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;

  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.app-content {
  flex-grow: 1;
  padding: 1.5rem 3.5rem;
}
</style>
