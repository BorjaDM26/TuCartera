<template>
  <validation-observer
    ref="loginObserver"
    class="login-form"
    v-slot="{ invalid }"
  >
    <section>
      <div class="error-message" v-if="message">
        <span>{{ message }}</span>
      </div>

      <validation-provider
        rules="required|email"
        name="Email"
        v-slot="{ errors }"
      >
        <b-field
          label="Email"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input
            type="email"
            v-model="email"
            @keyup.native.enter="login(invalid)"
          ></b-input>
        </b-field>
      </validation-provider>

      <validation-provider
        rules="required"
        name="Contraseña"
        v-slot="{ errors }"
      >
        <b-field
          label="Contraseña"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input
            type="password"
            v-model="pass"
            @keyup.native.enter="login(invalid)"
          ></b-input>
        </b-field>
      </validation-provider>

      <div class="buttons">
        <button
          class="button green-button login-form_submit-button"
          @click="login(invalid)"
          :disabled="invalid"
        >
          Iniciar sesión
        </button>
      </div>
    </section>
  </validation-observer>
</template>

<script lang="ts">
import { Component, Ref, Vue } from 'vue-property-decorator';

import { ValidationObserver, ValidationProvider } from 'vee-validate';

import { userStore } from '@/store/user/userStore';
import { HomeRouteName } from '@/router/routeNames';

@Component({
  name: 'LoginForm',
  components: {
    ValidationObserver,
    ValidationProvider,
  },
})
export default class LoginForm extends Vue {
  private userCtx = userStore.context(this.$store);

  @Ref()
  readonly loginObserver!: InstanceType<typeof ValidationObserver>;

  private email = '';
  private pass = '';
  private message = '';

  private async login(isInvalid: boolean) {
    if (isInvalid) {
      this.loginObserver.validate();
      return;
    }

    const loginRequest = { email: this.email, password: this.pass };
    await this.userCtx.actions.doLogin(loginRequest);
    if (this.userCtx.getters.isLoggedIn) {
      const homeRoute = { name: HomeRouteName };
      this.$router.push(homeRoute);
    } else {
      this.message = 'El email o la contraseña son incorrectos';
    }
  }
}
</script>

<style scoped lang="scss">
.login-form {
  text-align: left;

  & .error-message {
    color: $red;
    padding-bottom: 1rem;
  }

  & /deep/ .field {
    margin-bottom: 1rem;
  }

  &_submit-button {
    width: 100%;
    margin-top: 0.75rem;
  }
}
</style>
