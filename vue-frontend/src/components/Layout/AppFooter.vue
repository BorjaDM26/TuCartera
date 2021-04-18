<template>
  <b-navbar class="app-footer" type="is-black" :mobile-burger="false">
    <template #brand>
      <b-navbar-item tag="router-link" :to="{ path: '/' }">
        <img
          class="app-footer_logo"
          alt="Tu Cartera logo"
          src="../../assets/logo.svg"
        />
      </b-navbar-item>
    </template>
    <template #end>
      <b-navbar-item tag="div">&copy; 2020 Borja Delgado Martín</b-navbar-item>
    </template>
  </b-navbar>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component({
  name: 'AppFooter',
})
export default class AppFooter extends Vue {}
</script>

<style scoped lang="scss">
.app-footer {
  padding: 0 $navbar-h-pad;
  height: $header-height;
  font-size: $footer-font-size;

  &_logo {
    width: auto;
    min-height: $header-height;
    max-height: $header-height;
  }
}
</style>
