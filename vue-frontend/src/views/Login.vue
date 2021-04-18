<template>
  <div class="login">
    <form-card>
      <img
        class="app-header_logo"
        alt="Tu Cartera logo"
        src="@/assets/logo.svg"
      />
      <p class="has-text-weight-bold is-size-4">Iniciar Sesión</p>
      <login-form />
    </form-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import FormCard from '@/components/containers/FormCard.vue';
import LoginForm from '@/components/login/LoginForm.vue';

@Component({
  name: 'Login',
  components: {
    FormCard,
    LoginForm,
  },
})
export default class Login extends Vue {}
</script>

<style scoped lang="scss">
.login {
  display: flex;
  justify-content: center;

  & /deep/ .form-card {
    width: 400px;
    max-width: 95%;
  }
}
</style>
