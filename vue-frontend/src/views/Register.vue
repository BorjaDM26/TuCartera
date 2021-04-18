<template>
  <div class="register">
    <form-card>
      <img
        class="app-header_logo"
        alt="Tu Cartera logo"
        src="@/assets/logo.svg"
      />
      <p class="has-text-weight-bold is-size-4">Registrarse</p>
      <register-form />
    </form-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

import FormCard from '@/components/containers/FormCard.vue';
import RegisterForm from '@/components/register/RegisterForm.vue';

@Component({
  name: 'Register',
  components: {
    FormCard,
    RegisterForm,
  },
})
export default class Register extends Vue {}
</script>

<style scoped lang="scss">
.register {
  display: flex;
  justify-content: center;

  & /deep/ .form-card {
    width: 400px;
    max-width: 95%;
  }
}
</style>
