<template>
  <b-navbar class="app-header" type="is-black">
    <template #brand>
      <b-navbar-item
        tag="router-link"
        :to="{ path: '/' }"
        class="app-header_logo-container"
      >
        <img
          class="app-header_logo"
          alt="Tu Cartera logo"
          src="../../assets/logo.svg"
        />
      </b-navbar-item>
    </template>
    <template #start v-if="isUserLogged">
      <b-navbar-item tag="router-link" :to="portfolioListRoute">
        Carteras
      </b-navbar-item>
      <b-navbar-item tag="router-link" :to="transactionListRoute">
        Transacciones
      </b-navbar-item>
    </template>

    <template #end v-if="isUserLogged">
      <b-navbar-item tag="a" @click="logout">Cerrar sesión</b-navbar-item>
    </template>

    <template #end v-else>
      <b-navbar-item tag="router-link" :to="registerRoute"
        >Registrarse</b-navbar-item
      >
      <b-navbar-item tag="router-link" :to="loginRoute"
        >Iniciar sesión</b-navbar-item
      >
    </template>
  </b-navbar>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import { userStore } from '@/store/user/userStore';
import {
  LoginRouteName,
  PortfolioListRouteName,
  RegisterRouteName,
  TransactionListRouteName,
} from '@/router/routeNames';

@Component({
  name: 'AppHeader',
})
export default class AppHeader extends Vue {
  private userCtx = userStore.context(this.$store);

  private loginRoute = { name: LoginRouteName };
  private registerRoute = { name: RegisterRouteName };
  private portfolioListRoute = { name: PortfolioListRouteName };
  private transactionListRoute = { name: TransactionListRouteName };

  private get isUserLogged(): boolean {
    return this.userCtx.getters.isLoggedIn;
  }

  private async logout() {
    await this.userCtx.actions.logout();
    this.$router.push(this.loginRoute);
  }
}
</script>

<style scoped lang="scss">
.app-header {
  padding: 0 $navbar-h-pad;
  height: $header-height;

  &_logo {
    width: auto;
    min-height: $header-height;
    max-height: $header-height;
  }

  &_logo-container {
    padding: 0 $logo-h-pad;
  }
}
</style>
